<%@ Page Title="EosHealth - Overview" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Overview.aspx.cs" Inherits="Public_Community_Overview" %>
<%@ Register TagPrefix="uc" TagName="FeaturedThreads" Src="~/Controls/Public/Community/FeaturedThreads.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopularThreads" Src="~/Controls/Public/Community/PopularThreads.ascx" %>
<%@ Register TagPrefix="uc" TagName="FeaturedUsers" Src="~/Controls/Public/Community/FeaturedUsers.ascx" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityTopPanel" Src="~/Controls/Public/Community/JoinAsACommunityTopPanel.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="community_overview">	
    <h4><asp:Literal ID="litTitle" runat="server"></asp:Literal></h4>
    <div class="CmsContent">
	    <asp:Literal ID="litContent" runat="server"></asp:Literal>
    </div>
    <uc:FeaturedUsers ID="ucFeaturedUsers" runat="server" visible="false" />
    <uc:FeaturedThreads runat="server" ID="ucFeaturedThreads" />
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MemberStories runat="server" ID="ucMemberStories"></uc:MemberStories>
    <uc:JoinAsACommunityTopPanel ID="ucJoinAsACommunityTopPanel" runat="server" visible="false"/>
    <uc:MembersExperiencesVideo runat="server" ID="ucMembersExperiencesVideo"></uc:MembersExperiencesVideo>
    
</asp:Content>