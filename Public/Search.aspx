<%@ Page Title="EosHealth - Search" Language="C#" MasterPageFile="~/MasterPages/Simple.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Public_Search"  %>
<%@ Register Src="~/Controls/Public/Search/SearchResult.ascx" TagName="SearchResult" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
<div class="cntr_col search_results">
    <div class="title">
        <h3><asp:Literal runat="server" ID="ltTitle" /></h3>
    </div>        
    <div class="content">
	    <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
        <uc:SearchResult ID="ucSearchResult" runat="server" />    
	</div>
</div>    
</asp:Content>

