<%@ Page Title="EosHealth - Meet The Team" Language="C#" ValidateRequest="false" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="LeaderShipTeam.aspx.cs" Inherits="Public_LeaderShipTeam" %>
<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

    <!-- CMS Content Block -->
	<div class="about_meet_the_team">
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
		<h4>Leadership <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>

        <div id="divGCNT" class="CmsContent left_content_fix about_c_content" style="font-size: <%=(Request.Cookies["CMSFontSize"] == null ? "12" : Request.Cookies["CMSFontSize"].Value) %>px;"><asp:Literal runat="server" ID="ltContent" /></div>	
	<!-- CMS Content Block -->
	
	<asp:Repeater runat="server" ID="rptLeaderShipTeam">
	<HeaderTemplate>
    <h3 class="category">Our Team Members</h2>
    <ul class="list resources">
	</HeaderTemplate>
	<ItemTemplate>
    	<li>
            <div class="f_left">
            	<div class="thumb_90x100"><span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2"><asp:Literal runat="server" ID="ltImage"></asp:Literal></span><span class="swc3">&nbsp;</span></span></span></div> 
			</div>                
            <div class="text">                   
                <h6><%# DataBinder.Eval(Container.DataItem, DBLayer.LeadershipTeam.ColumnName.FirstName) %>&nbsp;<%# DataBinder.Eval(Container.DataItem, DBLayer.LeadershipTeam.ColumnName.LastName) %></h6>
                
                <%# DataBinder.Eval(Container.DataItem, DBLayer.LeadershipTeam.ColumnName.Description) %>
                <asp:Literal runat="server" ID="ltlnkreadMore" />
			</div>		      
		</li>              
	</ItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>	    
	</asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false"/>
</asp:Content>
