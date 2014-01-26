using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DBProxy : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        string vote = Request.QueryString["vote"];
        pinforpricedbEntities dbentities = new pinforpricedbEntities();
        
        var item = dbentities.pinforpricedbs.FirstOrDefault(pinforpricedb => pinforpricedb.Pinterest_ID == id);

        if (Request.QueryString["username"] != null && Request.QueryString["boardname"] != null) {
            pinboard newPinboard = new pinboard();
            newPinboard.username = Request.QueryString["username"].Trim();
            newPinboard.board = Request.QueryString["boardname"].Trim();
            dbentities.pinboards.Add(newPinboard);
            dbentities.SaveChanges();
            return;
        }

        if (vote != "high" && vote != "low") {
            //return the price
            jsonResponse.Text = item == null ? "5" : item.Price.ToString();
            return;
        }

        if (item == null) {
            pinforpricedb newEntry = new pinforpricedb();
            newEntry.Pinterest_ID = id;
            newEntry.Price = 5; //start off at 5 bucks
            newEntry.Votes_TooHigh = 0;
            newEntry.Votes_TooLow = 0;
            if (vote == "high") {
                newEntry.Votes_TooHigh += 1;
            } else if (vote == "low") {
                newEntry.Votes_TooLow += 1;
            }
            newEntry.Price = CalcPrice(newEntry.Price, newEntry.Votes_TooHigh, newEntry.Votes_TooLow);
            dbentities.pinforpricedbs.Add(newEntry);
            dbentities.SaveChanges();
        } else {
            if (vote == "high") {
                item.Votes_TooHigh += 1;
            } else if (vote == "low") {
                item.Votes_TooLow += 1;
            }
            item.Price = CalcPrice(item.Price, item.Votes_TooHigh, item.Votes_TooLow);
            dbentities.Entry(item).State = System.Data.EntityState.Modified;
            dbentities.SaveChanges();
        }
    }

    public double CalcPrice(double currentPrice, int highVotes, int lowVotes) {
        if (highVotes == 0) {
            //this means the the votes are all too low
            return currentPrice * 2;
        } else if (lowVotes == 0) {
            //this means the votes are all too high
            return currentPrice / 2;
        } else {
            return currentPrice * (((double)lowVotes) / ((double)highVotes));
        }

    }
}