<%@ Page Title="EosHealth - Members" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Public_Community_Members" %>
<%@ Register Src="~/Controls/Public/Community/Members.ascx" TagName="Members" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommunityBoard" Src="~/Controls/Public/CommunityBoard.ascx" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiences" Src="~/Controls/Public/MembersExperiences.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <div class="community_members">
        <h4><strong>Community</strong> - <asp:Literal runat="server" ID="ltTitle" /></h4>
        <div class="CmsContent">
            <asp:Literal runat="server" ID="ltContent" />
        </div>
        <uc:Members ID="ucMembers" runat="server"/>
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MembersExperiences ID="ucMembersExperiences" runat="server" visible="false" />
    <uc:CommunityBoard ID="ucCommunityBoard" runat="server" visible="false" />
</asp:Content>