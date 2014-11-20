<%@ Page Title="EosHealth - Thank You" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/User.master" AutoEventWireup="true" CodeFile="ThankYou.aspx.cs" Inherits="Public_ThankYou" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<!--
    <div class="subp_box_c_title_line">
        <div class="subp_box_c_title">
            <span class="border_0 padding_left_0 f_size_14">Plans > Phone > Shipping > Payment > Review > thank you</span>
        </div>
    </div>
-->    
    <div class="thanks">
    <!--
		<h3 class="type_1">Thank you for your purchase</h3>
        <p>
        	Some content will be here. It will be controll from CMS. Some content will be here. It will be controll from CMS.Some content will be here. It will be controll from CMS.Some content will be here. It will be controll from CMS.Some content will be here. It will be controll from CMS.
        </p>
        <h5 class="upper">Please contact us, if you have any questions 1-877-docinpocket</h5>
        <div class="bot_message a-center">
        	Would you like to show someone your profile and how you are going to start to monitor your health
        </div>
        <div align="center">
        	<a href="#null;" class="btn_share_profile" title="Share Profile"><b>Share Profile</b></a>
        </div>
        <div align="center">
            <br /><br />
            
	-->    
    <!-- CMS Content Block -->
	<div class="box">
        <h1 align="center"><asp:Literal runat="server" ID="ltTitle" /></h1>
        <br />
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->    
    
    <asp:PlaceHolder runat="server" ID="phlPayment" Visible="false">
    <br /><br />    
    <div align="center">
		<font color="red">The payment processor has not been integrated yet. The order is being created automatically without the actual transaction.</font>		
    </div>            
    </div>
    </asp:PlaceHolder>
</div>   
         

    
</asp:Content>