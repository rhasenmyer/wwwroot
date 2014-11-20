<%@ Page Title="EosHealth - Community Register" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityRegisterConfirm.aspx.cs" Inherits="Public_Register_CommunityRegisterConfirm"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
<!-- CMS Content Block -->
	<div class="box">
        <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->
	<div class="subp_box_c thanks_box">
    	<div class="border_1 a_center">
            <div class="search_title">Thank you!</div>
            <div class="box clearfix">
                Your account has been succesfully created.
                <br />
                Your account information has been sent to the email address you specified.
                <asp:LinkButton CssClass="btn btn_ok" ID="lbtnOk" runat="server" OnClick="lbtnOk_Click"><b>OK</b></asp:LinkButton>
            </div>                
		</div>        
	</div>        
</asp:Content>

