<%@ Page Title="EosHealth - Books" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Books.aspx.cs" Inherits="Public_Books" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommunicatorsSmall" Src="~/Controls/Public/WellnessProducts/FeaturedPhone.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

    <!-- CMS Content Block -->
<div class="health_supplies_books">
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
		<h4>Books <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
        <div align="center">
	        <div id="divGCNT" class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
		</div>            
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" />
    <uc:CommunicatorsSmall runat="server" ID="ucCommunicatorsSmall" />
</asp:Content>