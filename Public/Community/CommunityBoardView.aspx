<%@ Page Title="EosHealth - Community Board" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityBoardView.aspx.cs" Inherits="Public_Community_CommunityBoardView" %>
<%@ Register Src="~/Controls/Common/ConfirmView.ascx" TagPrefix="uc" TagName="ConfirmView" %>
<%@ Register Src="~/Controls/Public/Community/CommunityPostList.ascx" TagPrefix="uc" TagName="CommunityPostList" %>
<%@ Register Src="~/Controls/Public/Community/CommunityPostEdit.ascx" TagPrefix="uc" TagName="CommunityPostEdit" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
	<div class="community_board">
        <div class="title">
        	<h4><strong>Community</strong> - <asp:Literal runat="server" ID="ltTitle" /></h4>
		</div>    
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
        <uc:ConfirmView ID="ucConfirmView" runat="server" />
        <uc:CommunityPostList ID="ucCommunityPostList" runat="server" />
        <uc:CommunityPostEdit ID="ucCommunityPostEdit" runat="server" />
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MembersExperiencesVideo runat="server" ID="ucMembersExperiencesVideo"></uc:MembersExperiencesVideo>
    <uc:MemberStories runat="server" ID="ucMemberStories"></uc:MemberStories>
</asp:Content>