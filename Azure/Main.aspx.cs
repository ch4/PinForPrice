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

public partial class Main : Page {
    public List<PinterestItem> itemList = new List<PinterestItem>(5);

    public class PinterestItem {
        public string ImageURI { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        PinterestItem(string itemId = "", string itemname = "Unnamed Item", string itemdescription = "No description", string itemURL = "http://placehold.it/700x300") {
            this.ImageURI = itemURL;
            this.Name = itemname;
            this.Description = itemdescription;
            this.Id = itemId;
        }
    }

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
        String response = reader.ReadToEnd();


    }

    public string GetImage(int number){
        return "#";
    }
}