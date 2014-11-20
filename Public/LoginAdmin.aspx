<%@ Page Title="EosHealth - Login" Language="C#" MasterPageFile="~/MasterPages/PublicRight_NoDropDown.master" AutoEventWireup="true" CodeFile="LoginAdmin.aspx.cs" Inherits="Public_Login" ValidateRequest="true" %>
<%@ Register Src="~/Controls/Common/Login.ascx" TagName="Login" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Common/ContactUsRightSidePanel.ascx" TagName="ContactUsRightSidePanel" TagPrefix="uc" %>
<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="login_box">
    <h4>Login <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
    <div class="signup_box">
        <asp:Literal runat="server" ID="ltContent" />
        <a href="/Public/Register/Registration.aspx" class="btn_arrow"style="width: 50px; height: 18px;">Sign Up</a>
    </div>

    <uc:Login runat="server" ID="ucLogin" />
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:ContactUsRightSidePanel ID="ucContactUsRightSidePanel" runat="server" />
</asp:Content>