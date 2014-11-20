<%@ Page Title="EosHealth - Ask An Expert" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="OverView.aspx.cs" Inherits="Public_AskAnExpert_OverView"  %>
<%@ Register Src="~/Controls/Public/AskAnExpert/AskQuestion.ascx" TagName="AskQuestion" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/SearchQuestion.ascx" TagName="SearchQuestion" TagPrefix="uc" %>
<%@ Register Src="~/Controls/Public/AskAnExpert/SmallQuestionTable/SmallQuestionTable.ascx" TagName="SmallQuestionTable" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="FAQShortTable" Src="~/Controls/Public/FAQShortTable.ascx" %>
<%@ Register TagPrefix="uc" TagName="TipOfTheDay" Src="~/Controls/Public/TipOfTheDay.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpertShortPost" Src="~/Controls/Public/AskAnExpert/AskAnExpertShortPost.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinBanner1" Src="~/Controls/Public/JoinBanners/JoinBanner1.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <!-- CMS Content Block -->
	<div class="ask_an_expert_overview">
        <h4><asp:Literal runat="server" ID="ltTitle" /></h4>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	<!-- CMS Content Block -->  
		<div class="clearfix">    	
	    	<uc:SearchQuestion ID="ucSearchQuestion" runat="server"/>
    	</div>
    <asp:PlaceHolder runat="server" ID="phlQSent" EnableViewState="false" Visible="false"> 
        <h1 class="message no_upper">Your question has been successfully submitted. <br />It will be answered by our expert as soon as possible.</h1>
    </asp:PlaceHolder>
        
    <uc:AskQuestion ID="ucAskQuestion" runat="server" OnPost="ucAskQuestion_Post"/>
    <uc:SmallQuestionTable ID="ucSmallQuestionTable" runat="server"/>
    
	</div>
    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinBanner1 ID="ucJoinBanner1" runat="server" />
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
    <uc:AskAnExpertShortPost ID="ucAskAnExpertShortPost" runat="server" />
    <uc:TipOfTheDay ID="ucTipOfTheDay" runat="server" />
</asp:Content>
