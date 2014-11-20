<%@ Page Title="EosHealth - Community Register" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityRegister.aspx.cs" Inherits="Public_Register_CommunityRegister"  %>
<%@ Register TagPrefix="uc" TagName="CommunityRegistration" Src="~/Controls/Public/Registration/CommunityRegistration.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <!-- CMS Content Block -->
	<div class="box hidden">
        <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->
	<uc:CommunityRegistration runat="server" ID="ucCommunityRegistration"/>
</asp:Content>