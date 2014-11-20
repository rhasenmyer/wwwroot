<%@ Page Title="EosHealth - Overview" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Overview.aspx.cs" Inherits="Public_Overview" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
   
    <!-- CMS Content Block -->
	<div class="about_overview">
	    
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
		<h4>
        	Overview <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
		</h4>            

        <div id="divGCNT" class="about_c_content left_content_fix CmsContent" style="font-size: <%=(Request.Cookies["CMSFontSize"] == null ? "12" : Request.Cookies["CMSFontSize"].Value) %>px;">
        <asp:Literal runat="server" ID="ltContent" />
        </div>
	</div>
	<!-- CMS Content Block -->   
    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MemberStories ID="ucMemberStories" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false" />
</asp:Content>
