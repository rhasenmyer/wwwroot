<%@ Page Title="EosHealth - Login" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Login2.aspx.cs" Inherits="Public_Login" ValidateRequest="true" %>
<%@ Register Src="~/Controls/Common/Login2.ascx" TagName="Login" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Common/ContactUsRightSidePanel.ascx" TagName="ContactUsRightSidePanel" TagPrefix="uc" %>
<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="login_box">
    <h4>Login <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
    <div class="signup_box">
        <asp:Literal runat="server" ID="ltContent" />
        <a href="/Public/Register/Registration.aspx" class="btn_arrow">Sign Up for Free</a>
    </div>            
    <uc:Login runat="server" ID="ucLogin" />
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:ContactUsRightSidePanel ID="ucContactUsRightSidePanel" runat="server" />
</asp:Content>