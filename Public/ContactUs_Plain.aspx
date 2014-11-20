<%@ Page Title="EosHealth - Contact Us" Language="C#" MasterPageFile="~/MasterPages/Plain.master" AutoEventWireup="true" CodeFile="ContactUs_Plain.aspx.cs" Inherits="ContactUs" ValidateRequest="true" %>
<%@ Register TagPrefix="uc" TagName="ContactUs" Src="~/Controls/Common/ContactUs.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
   
<asp:PlaceHolder runat="server" ID="phlForm">
	<span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
	<div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	<uc:ContactUs runat="server" ID="ucContactUs" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="phlThankTou">
    <h1 class="warning"><%= Resources.Form_ContactUs.ThankYou %></h1>
</asp:PlaceHolder>

</asp:Content>
