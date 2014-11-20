<%@ Page Title="EosHealth - Community Postings" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityBoard.aspx.cs" Inherits="Public_Community_CommunityBoard" %>
<%@ Register Src="~/Controls/Common/ConfirmView.ascx" TagPrefix="uc" TagName="ConfirmView" %>
<%@ Register Src="~/Controls/Public/Community/CommunityThreadList.ascx" TagPrefix="uc" TagName="CommunityThreadList" %>
<%@ Register Src="~/Controls/Public/Community/CommunityThreadEdit.ascx" TagPrefix="uc" TagName="CommunityThreadEdit" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityPanel" Src="~/Controls/Public/Community/JoinAsACommunityPanel.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

<div class="community_board">
    <div class="title">
        <h4><strong>Community</strong> - <asp:Literal runat="server" ID="ltTitle" /></h4>
    </div>
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>
    <uc:ConfirmView ID="ucConfirmView" runat="server" />
    <uc:CommunityThreadList ID="ucCommunityThreadList" runat="server" />
    <uc:CommunityThreadEdit ID="ucCommunityThreadEdit" runat="server" />
</div>
            
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MemberStories runat="server" ID="ucMemberStories"></uc:MemberStories>
    <uc:MembersExperiencesVideo runat="server" ID="ucMembersExperiencesVideo"></uc:MembersExperiencesVideo>
    <uc:JoinAsACommunityPanel ID="ucJoinAsACommunityPanel" runat="server" visible="false"/>
</asp:Content>