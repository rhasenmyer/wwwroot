<%@ Page Title="EosHealth - Contact Us" Language="C#" MasterPageFile="~/MasterPages/PublicRight_NoDropDown.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" ValidateRequest="true" %>
<%@ Register TagPrefix="uc" TagName="ContactUs" Src="~/Controls/Common/ContactUs.ascx" %>
<%@ Register Src="~/Controls/Common/ContactUsRightSidePanel.ascx" TagName="ContactUsRightSidePanel" TagPrefix="uc" %>
<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    
<asp:PlaceHolder runat="server" ID="phlForm">
	<h4>Contact Us <br/> <span style="text-transform: capitalize; font-size: 10pt;">Please be patient after you submit the form, you only need to click "Send" once.</span></h4>
	<span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
    <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
    <uc:ContactUs runat="server" ID="ucContactUs" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phlThankTou">
    <h1 class="warning"><%= Resources.Form_ContactUs.ThankYou %></h1>
</asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:ContactUsRightSidePanel ID="ucContactUsRightSidePanel" runat="server" />
</asp:Content>