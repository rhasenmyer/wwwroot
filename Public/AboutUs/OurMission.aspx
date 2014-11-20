<%@ Page Title="EosHealth - Our Mission" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="OurMission.aspx.cs" Inherits="Public_OurMission" %>
<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="Twitter" Src="~/Controls/Public/Twitter.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
	<div class="about_our_mission">
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
        <h4>
        	Vision <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
		</h4>
        <div id="divGCNT" class="about_c_content left_content_fix CmsContent" style="font-size: <%=(Request.Cookies["CMSFontSize"] == null ? "12" : Request.Cookies["CMSFontSize"].Value) %>px;"><asp:Literal runat="server" ID="ltContent" /></div>        
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:Twitter ID="ucTwitter" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false"/>
</asp:Content>
