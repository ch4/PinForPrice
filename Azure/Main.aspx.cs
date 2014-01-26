using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Main : Page {
    //public class PinterestItem {
    //    public string ImageURI { get; set; }
    //    //public string Name { get; set; }
    //    public string Description { get; set; }
    //    public string Id { get; set; }
    //    //PinterestItem(string itemId = "", string itemname = "Unnamed Item", string itemdescription = "No description", string itemURL = "http://placehold.it/700x300") {
    //    public PinterestItem(string itemId = "", string itemdescription = "No description", string itemURL = "http://placehold.it/700x300") {
    //        this.ImageURI = itemURL;
    //        //this.Name = itemname;
    //        this.Description = itemdescription;
    //        this.Id = itemId;
    //    }
    //}
    //public List<PinterestItem> itemList = new List<PinterestItem>(4);
    //public Literal[] literalPicList = {item0pic,item1pic,item2pic,item3pic};

    public static string ByteArrayToString(byte[] ba) {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    protected void Page_Init(object sender, EventArgs e) {
        //        var hash;
        //        var response;
        //        function testOauth(){
        //            var date = (new Date()).getTime().toString()
        //            var params = "client_id=1435816&timestamp=" + date;
        var date = DateTime.Now.Ticks;
        var queryParams = "client_id=1435816&timestamp=" + date.ToString();
        //            var username = $('#username').val() == "" ? "nsputnik" : $('#username').val();
        //            var boardname = $('#boardname').val() == "" ? "synths-and-studios" : $('#boardname').val();
        //
        //            var hmacString = "GET&https%3A%2F%2Fapi.pinterest.com%2Fv3";
        //            hmacString += "%2Fboards";
        //            hmacString += "%2F" + username;
        //            hmacString += "%2F" + boardname;
        //            hmacString += "%2Fpins%2F";
        //            hmacString += "&";
        //            hmacString += params;
        var hmacString = "GET&https%3A%2F%2Fapi.pinterest.com%2Fv3";
        hmacString += "%2Fboards";
        hmacString += "%2F" + "nsputnik";//username;
        hmacString += "%2F" + "synths-and-studios";//boardname;
        hmacString += "%2Fpins%2F";
        hmacString += "&";
        hmacString += queryParams;
        //            
        //            var clientSecret = "eedca713dc5b13ef3572c55c50c135b159384894";
        //            hash = CryptoJS.HmacSHA256(hmacString, clientSecret);
        HMACSHA256 hmac = new HMACSHA256(new ASCIIEncoding().GetBytes("eedca713dc5b13ef3572c55c50c135b159384894"));
        hmac.ComputeHash(new ASCIIEncoding().GetBytes(hmacString));

        var hashbytes = hmac.Hash;
        var hash = ByteArrayToString(hashbytes);

        //
        //            $('#hashOutput').text(hash);
        //            $('#hashString').text(hmacString);
        //
        //            //https://api.pinterest.com/v3/boards/nsputnik/synths-and-studios/
        //            // ?client_id=1435816&timestamp=1390694928470&oauth_signature=cccf9d745d9379021a9604c2208f3d5784bc3b44ea662c1dfbf7f110e5512352
        //
        //            var apiLink = "https://api.pinterest.com/v3/boards/" + username + "/" + boardname + "/pins/";
        //            apiLink += "?" + params + "&oauth_signature=" + hash;
        var apiLink = "https://api.pinterest.com/v3/boards/" + "nsputnik" + "/" + "synths-and-studios" + "/pins/";
        apiLink += "?" + queryParams + "&oauth_signature=" + hash;
        //
        //            $('#apiLink').attr('href',apiLink);
        //            $('#apiLink').show();
        //
        //            $.ajax({
        //                //dataType: 'jsonp',
        //                type: "GET",
        //                url: apiLink
        //            }).done(function( jsonResponse ) {
        //                response = jsonResponse;
        //            });
        //
        //        }
        WebClient webClient = new WebClient();
        Stream stream = webClient.OpenRead(apiLink);
        StreamReader reader = new StreamReader(stream);
        string response = reader.ReadToEnd();

        JObject responseObject = JObject.Parse(response);
        // get JSON result objects into a list
        IList<JToken> allResults = responseObject["data"].Children().ToList();

        List<JToken> results = new List<JToken>();
        Random r = new Random();
        for (int i = 0; i < 4; i++) {
            var randomNum = r.Next(0, allResults.Count - 1);
            results.Add(allResults[randomNum]);
            allResults.RemoveAt(randomNum);
        }

        int count = 0;
        pinforpricedbEntities dbentities = new pinforpricedbEntities();

        // serialize JSON results into .NET objects
        foreach (JToken result in results) {
            string itemId = (string)result["id"];
            var item = dbentities.pinforpricedbs.FirstOrDefault(pinforpricedb => pinforpricedb.Pinterest_ID == itemId);
            //string itemName = (string)responseObject["data"][0]["id"];
            string itemDesc = (string)result["description"];
            string itemPic = (string)result["image_large_url"];

            //if (itemList.Count > 4) {
            //    itemList[count] = new PinterestItem(itemId, itemDesc, itemPic);
            //} else {
            //    itemList.Add(new PinterestItem(itemId, itemDesc, itemPic));
            //}

            //if(count ==0){
            //item0pic,item1pic,item2pic,item3pic
            //}
            switch (count) {
                case 0:
                    item0pic.Text = itemPic;
                    item0desc.Text = itemDesc;
                    item0id1.Text = itemId;
                    item0id2.Text = itemId;
                    item0id3.Text = itemId;
                    //item0price.Text = item == null ? "5" : item.Price.ToString();
                    break;
                case 1:
                    item1pic.Text = itemPic;
                    item1desc.Text = itemDesc;
                    item1id1.Text = itemId;
                    item1id2.Text = itemId;
                    item1price.Text = item == null ? "5" : item.Price.ToString();
                    break;
                case 2:
                    item2pic.Text = itemPic;
                    item2desc.Text = itemDesc;
                    item2id1.Text = itemId;
                    item2id2.Text = itemId;
                    item2price.Text = item == null ? "5" : item.Price.ToString();
                    break;
                case 3:
                    item3pic.Text = itemPic;
                    item3desc.Text = itemDesc;
                    item3id1.Text = itemId;
                    item3id2.Text = itemId;
                    item3price.Text = item == null ? "5" : item.Price.ToString();
                    break;
                default:
                    break;
            }

            ++count;
            if (count >= 4) break;
        }
    }

    //public String GetImage(int number){
    //    return itemList[number].ImageURI;
    //}
}