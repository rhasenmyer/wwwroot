<%@ Page Title="EosHealth - Forgot Password" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Public_Login" ValidateRequest="true" %>
<%@ Register TagPrefix="uc" TagName="ForgotPassword" Src="~/Controls/Common/ForgotPassword.ascx" %>
<%@ Register Src="~/Controls/Common/ContactUsRightSidePanel.ascx" TagName="ContactUsRightSidePanel" TagPrefix="uc" %>
<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="login_box">
    <h4>Forgot Password<span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
    <asp:Literal runat="server" ID="ltContent" />
    <uc:ForgotPassword runat="server" ID="ucForgotPassword" />
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:ContactUsRightSidePanel ID="ucContactUsRightSidePanel" runat="server" />
</asp:Content>