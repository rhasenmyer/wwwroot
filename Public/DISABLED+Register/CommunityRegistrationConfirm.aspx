<%@ Page Title="EosHealth - Community Registration" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityRegistrationConfirm.aspx.cs" Inherits="Public_Register_CommunityRegisterConfirm"  %>
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
            <asp:PlaceHolder runat="server" ID="phlCfmStart">
                <div class="box clearfix">
                    Your account has been succesfully created.
                    <br />
                    Account information and activation link have been sent to the email address you specified.
                </div>            
            </asp:PlaceHolder>
            
            <asp:PlaceHolder runat="server" ID="phlCfmEnd">
                <div class="box clearfix">
                    Your account has been succesfully verified.                    
                    <asp:LinkButton CssClass="btn btn_ok" ID="lbtnOk" runat="server"><b>OK</b></asp:LinkButton>
                </div>            
            </asp:PlaceHolder>            
		</div>
	</div>        
</asp:Content>

