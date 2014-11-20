<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="TeamMemberContent.aspx.cs" Inherits="TeamMemberContent" %>

<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    <script type="text/javascript">
        $(function () {
            $('div.menu ul').html('<li class="first">' +
                '<a href="/Public/AboutUs/Overview.aspx">Overview</a><li>' +
                '<a href="/Public/AboutUs/OurMission.aspx">Vision</a>' +
            '</li>' +
            '<li>' +
                '<a href="/Public/AboutUs/HistoryAndPhilosophy.aspx">History</a> ' +
            '</li>' +
            '<li class="active">' +
                '<a href="/Public/AboutUs/LeaderShipTeam.aspx">Leadership</a>' +
            '</li>' +
            '<li>' +
                '<a href="/Public/AboutUs/ProgramComponents.aspx">Health Program</a>' +
            '</li>'
                );
        })
    </script>
    <br />
    <a href="LeaderShipTeam.aspx">back</a>
    <asp:Literal ID="ltTitle" runat="server" />
    <br />
    <div class="f_left">
        <div class="thumb_90x100">
            <span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2">
                <asp:Literal ID="ltImage" runat="server" />
            </span></span>
        </div>
        </div>
     <div class="text">         
        <asp:Literal ID="ltDesc" runat="server" />
         </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphlRightPanel" runat="server">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" />
</asp:Content>
