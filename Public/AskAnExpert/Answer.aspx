<%@ Page Title="EosHealth - Ask An Expert" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Answer.aspx.cs" Inherits="Public_AskAnExpert_Answer" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/SearchCategoryQuestion.ascx" TagName="SearchCategoryQuestion" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/Answer.ascx" TagName="Answer" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="FAQShortTable" Src="~/Controls/Public/FAQShortTable.ascx" %>
<%@ Register TagPrefix="uc" TagName="TipOfTheDay" Src="~/Controls/Public/TipOfTheDay.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <!-- CMS Content Block -->
	<div class="ask_an_expert_overview">
    	<h4>Search results <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	<!-- CMS Content Block -->  		
        <uc:SearchCategoryQuestion ID="ucSearchCategoryQuestion" runat="server"/>
        <uc:Answer ID="ucAnswer" runat="server"/>
	</div>    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
	<div class="clearfix">
        <div class="title"><h3>Categories</h3></div>
        <asp:Repeater runat="server" ID="rptAskAnExpertCategory">
            <HeaderTemplate>
                <ul class="blog_articles_list">    
            </HeaderTemplate>
            <ItemTemplate>
                <li <%# (AppLayer.Functions.ToInt(Request["CategoryID"]) == (int)DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.ID) ? "class=\"active\"" : "") %>>
                <a class="link_type_1" href="/Public/AskAnExpert/Search.aspx?CategoryID=<%# DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.ID) %>"
                    ><%# DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.Title)%></a>
                </li>                        
            </ItemTemplate>
            <FooterTemplate>
                </ul>   
            </FooterTemplate>
        </asp:Repeater>	       
	</div>        
    <uc:FAQShortTable ID="ucFAQShortTable" runat="server" />
    <uc:TipOfTheDay ID="ucTipOfTheDay" runat="server" />
</asp:Content>
