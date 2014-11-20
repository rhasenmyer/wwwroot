<%@ Page Title="EosHealth - Search" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Public_AskAnExpert_Search" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/SearchCategoryQuestion.ascx" TagName="SearchCategoryQuestion" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/QuestionTable.ascx" TagName="QuestionTable" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpertShortPost" Src="~/Controls/Public/AskAnExpert/AskAnExpertShortPost.ascx" %>
<%@ Register TagPrefix="uc" TagName="FAQShortTable" Src="~/Controls/Public/FAQShortTable.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinBanner3" Src="~/Controls/Public/JoinBanners/JoinBanner3.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <!-- CMS Content Block -->
	<div class="ask_an_expert_overview">    
        <h4>Search results <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	<!-- CMS Content Block -->  		    	
        <uc:SearchCategoryQuestion ID="ucSearchCategoryQuestion" runat="server" />        
        <uc:QuestionTable ID="ucQuestionTable" runat="server" />
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinBanner3 ID="ucJoinBanner3" runat="server" />
	<div class="clearfix">
        <div class="title"><h3>Categories</h3></div>
        <asp:Repeater runat="server" ID="rptAskAnExpertCategory">
            <HeaderTemplate>
                <ul class="blog_articles_list">    
            </HeaderTemplate>
            <ItemTemplate>
                <li <%# (AppLayer.Functions.ToInt(Request["CategoryID"]) == (int)DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.ID) ? "class=\"active\"" : "") %>>
                <a href="/Public/AskAnExpert/Search.aspx?CategoryID=<%# DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.ID) %>"
                    ><%# DataBinder.Eval(Container.DataItem, DBLayer.AskAnExpertCategory.ColumnName.Title)%></a>
                </li>                        
            </ItemTemplate>
            <FooterTemplate>
                </ul>   
            </FooterTemplate>
        </asp:Repeater>	       
	</div>
    <uc:FAQShortTable ID="ucFAQShortTable" runat="server" />
    <uc:AskAnExpertShortPost ID="ucAskAnExpertShortPost" runat="server" />
</asp:Content>
