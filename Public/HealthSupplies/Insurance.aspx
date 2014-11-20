<%@ Page Title="EosHealth - Pricing & Insurance" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Insurance.aspx.cs" Inherits="Public_Insurance" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommunicatorsSmall" Src="~/Controls/Public/WellnessProducts/FeaturedPhone.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityTopPanel" Src="~/Controls/Public/Community/JoinAsACommunityTopPanel.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinBanner4" Src="~/Controls/Public/JoinBanners/JoinBanner4.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="health_supplies_pricing">
    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
    <h4>Pricing &amp; Return Policy <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
    <div id="divGCNT" class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
<asp:Repeater runat="server" ID="rptInsurance">
<HeaderTemplate>
</HeaderTemplate>
<ItemTemplate>
    <div class="clearfix">
        <h3 class="category"><%# DataBinder.Eval(Container.DataItem, DBLayer.Insurance.ColumnName.Title) %></h3>
        <ul class="list resources">
            <li class="no_bg">
                <div class="image img_left_box">
                    <asp:Literal runat="server" ID="ltImage"></asp:Literal>
                </div>
                <div class="text">
                    <%# DataBinder.Eval(Container.DataItem, DBLayer.Insurance.ColumnName.Description) %>
                </div>                
            </li>
        </ul>
    </div>    
</ItemTemplate>
<FooterTemplate>
</FooterTemplate>	    
</asp:Repeater>
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinBanner4 ID="ucJoinBanner4" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" />
    <uc:CommunicatorsSmall runat="server" ID="ucCommunicatorsSmall" />
</asp:Content>
