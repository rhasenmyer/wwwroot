<%@ Page Language="C#" MasterPageFile="~/MasterPages/Simple.master" AutoEventWireup="true" CodeFile="RegistrationDisclaimer.aspx.cs" Inherits="Public_RegistrationDisclaimer" ValidateRequest="true" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    <div class="box">
        <h1><h4>Disclaimer <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
</asp:Content>