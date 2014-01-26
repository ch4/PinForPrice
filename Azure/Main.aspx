<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main"%>

<asp:Content runat="server" ID="HeaderContent" ContentPlaceHolderID="HeadContent">

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     

    <div class="container">

        <div class="row">

            <div class="col-lg-12">
                <h1 class="page-header">Pin For Price
                    <small>Do you think these items are over priced or under priced?</small>
                </h1>
            </div>

        </div>

        <div class="row">
            <div class="col-lg-7 col-md-7">
                <a href="#">
                    <img class="img-responsive" src="<asp:Literal runat="server" id="item0pic" />" alt="">
                </a>
            </div>

            <div class="col-lg-5 col-md-5">
                <h3 id="item-0-price">$0</h3>
                <h4><asp:Literal runat="server" id="item0desc" /></h4>
                <div id="1" class="alert alert-success">
              <strong>Thanks!</strong> Your opinion is greatly appreciated!
            </div>
                <a class="btn btn-primary" id = "11" onclick="OverPriced('<asp:Literal runat="server" id="item0id1" />')">Over Priced <span class="glyphicon glyphicon-chevron-up"></span></a>
                <a class="btn btn-primary" id = "12" onclick="UnderPriced('<asp:Literal runat="server" id="item0id2" />')">Under Priced <span class="glyphicon glyphicon-chevron-down"></span></a>
            </div>

        </div>

        <hr>

        <div class="row">

            <div class="col-lg-7 col-md-7">
                <a href="#">
                    <img class="img-responsive" src="<asp:Literal runat="server" id="item1pic" />" alt="">
                </a>
            </div>

            <div class="col-lg-5 col-md-5">
                <h3 id="item-1-price">$<asp:Literal runat="server" id="item1price" /></h3>
                <h4><asp:Literal runat="server" id="item1desc" /></h4>
                 <div id="2" class="alert alert-success">
              <strong>Thanks!</strong> Your opinion is greatly appreciated!
            </div>
                <a class="btn btn-primary" id = "21" onclick="OverPriced('<asp:Literal runat="server" id="item1id1" />')">Over Priced <span class="glyphicon glyphicon-chevron-up"></span></a>
                <a class="btn btn-primary" id = "22" onclick="UnderPriced('<asp:Literal runat="server" id="item1id2" />')">Under Priced<span class="glyphicon glyphicon-chevron-down"></span></a>
            </div>

        </div>

        <hr>

        <div class="row">

            <div class="col-lg-7 col-md-7">
                <a href="#">
                    <img class="img-responsive" src="<asp:Literal runat="server" id="item2pic" />" alt="">
                </a>
            </div>
            <div class="col-lg-5 col-md-5">
                <h3 id="item-2-price">$<asp:Literal runat="server" id="item2price" /></h3>
                <h4><asp:Literal runat="server" id="item2desc" /></h4>
                <div id="3" class="alert alert-success">
              <strong>Thanks!</strong> Your opinion is greatly appreciated!
            </div>
                <a class="btn btn-primary" id = "31" onclick="OverPriced('<asp:Literal runat="server" id="item2id1" />')">Over Priced <span class="glyphicon glyphicon-chevron-up"></span></a>
                <a class="btn btn-primary" id = "32" onclick="UnderPriced('<asp:Literal runat="server" id="item2id2" />')">Under Priced <span class="glyphicon glyphicon-chevron-down"></span></a>
            </div>

        </div>

        <hr>

        <div class="row">

            <div class="col-lg-7 col-md-7">
                <a href="#">
                    <img class="img-responsive" src="<asp:Literal runat="server" id="item3pic" />" alt="">
                </a>
            </div>

            <div class="col-lg-5 col-md-5">
                <h3 id="item-3-price">$<asp:Literal runat="server" id="item3price" /></h3>
                <h4><asp:Literal runat="server" id="item3desc" /></h4>
                 <div id="4" class="alert alert-success">
              <strong>Thanks!</strong> Your opinion is greatly appreciated!
            </div>
                <div class="alert alert-warning alert-dismissable">
              <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
              <strong>Warning!</strong> Best check yo self, you're not looking too good.
            </div>
                <a class="btn btn-primary" id = "41" onclick="OverPriced('<asp:Literal runat="server" id="item3id1" />')">Over Priced <span class="glyphicon glyphicon-chevron-up"></span></a>
                <a class="btn btn-primary" id = "42" onclick="UnderPriced('<asp:Literal runat="server" id="item3id2" />')">Under Priced <span class="glyphicon glyphicon-chevron-down"></span></a>
            </div>

        </div>

        <hr>

        <div class="row text-center">

            <div class="col-lg-12">
                <a class="load" href="#" onclick="location.reload()">Load More</a>
            </div>

        </div>

        <hr>

        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p>Copyright &copy; Company 2013</p>
                </div>
            </div>
        </footer>

    </div>
    <!-- /.container -->

    <!-- JavaScript -->    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js">
    <script src="js/bootstrap.js"></script>

</script>
   <script>
       function OverPriced(itemId) {
           $.ajax({
               type: "GET",
               url: "/DBProxy.aspx?id=" + itemId + "&vote=high"
           });
       }
       function UnderPriced(itemId) {
           $.ajax({
               type: "GET",
               url: "/DBProxy.aspx?id=" + itemId + "&vote=low"
           });
       }

       $(document).ready(function () {
           //get prices hack
           $.ajax({
               type: "GET",
               url: "/DBProxy.aspx?id=" + "<asp:Literal runat="server" id="item0id3" />"
           }).done(function (jsonResponse) {
               $('#item-0-price').text("$"+jsonResponse);
           });

           $("#11").click(function () {
               $("#11").fadeOut();
               $("#12").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#1").fadeIn();
               }, delay)

           });

           $("#21").click(function () {
               $("#21").fadeOut();
               $("#22").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#2").fadeIn();
               }, delay)
           });
           $("#31").click(function () {
               $("#31").fadeOut();
               $("#32").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#3").fadeIn();
               }, delay)
           });
           $("#41").click(function () {
               $("#41").fadeOut();
               $("#42").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#4").fadeIn();
               }, delay)
           });
           $("#51").click(function () {
               $("#51").fadeOut();
               $("#52").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#5").fadeIn();
               }, delay)

           });
           $("#12").click(function () {
               $("#11").fadeOut();
               $("#12").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#1").fadeIn();
               }, delay)
           });
           $("#22").click(function () {
               $("#21").fadeOut();
               $("#22").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#2").fadeIn();
               }, delay)
           });
           $("#32").click(function () {
               $("#31").fadeOut();
               $("#32").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#3").fadeIn();
               }, delay)
           });
           $("#42").click(function () {
               $("#41").fadeOut();
               $("#42").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#4").fadeIn();
               }, delay)
           });
           $("#52").click(function () {
               $("#51").fadeOut();
               $("#52").fadeOut();
               var delay = 500
               setTimeout(function () {
                   $("#5").fadeIn();
               }, delay)
           });

       });
</script>


</asp:Content>