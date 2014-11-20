<%@ Page Title="EosHealth - Blog" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true"
    CodeFile="Blog.aspx.cs" Inherits="Public_Blog" ValidateRequest="true" %>

<%@ Register Src="~/Controls/Public/Blog/BlogList.ascx" TagName="BlogList" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/Blog/BlogOverview.ascx" TagName="BlogOverview" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/Community/JoinAsACommunityPanel.ascx" TagName="JoinAsACommunityPanel" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" runat="Server">

    <!-- CMS Content Block -->
    <h4><asp:Literal runat="server" ID="ltTitle" /></h4>
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>

	<!-- CMS Content Block -->  	
	<div class="blog">	
        <uc:BlogList ID="ucBlogList" runat="server" OnSelectBlog="ucBlogList_SelectBlog" />
        <uc:BlogOverview ID="ucBlogOverview" runat="server" OnBack="ucBlogOverview_Back" />
	</div>        
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    
    <asp:Repeater runat="server" ID="rptBlogCategory">
        <HeaderTemplate>
        	<div class="title">
	            <h3>Categories</h3>
            </div>
            <ul class="blog_articles_list">
        </HeaderTemplate>
        <ItemTemplate>
        	    <li <%# (AppLayer.Functions.ToInt(Request["cat_id"]) == (int)DataBinder.Eval(Container.DataItem, DBLayer.BlogCategory.ColumnName.ID) ? "class=\"active first\"" : "class=\"first\"") %>><a href="/Public/Blog.aspx?cat_id=<%# DataBinder.Eval(Container.DataItem, DBLayer.BlogCategory.ColumnName.ID) %>" class="link_type_1"><%# DataBinder.Eval(Container.DataItem, DBLayer.BlogCategory.ColumnName.Name) %></a></li>                        
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>

    <!--//<uc:AskAnExpert ID="ucAskAnExpert" runat="server" />//-->
    
    
    
    
</asp:Content>