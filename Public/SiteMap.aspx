<%@ Page Title="EosHealth - Site Map" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="Public_SiteMap"  %>
<%@ Register Src="~/Controls/Public/SiteMap.ascx" TagName="SiteMap" TagPrefix="uc" %>
<%@ Register TagPrefix="uc" TagName="ContactUsRightSidePanel" Src="~/Controls/Common/ContactUsRightSidePanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <uc:SiteMap ID="ucSiteMap" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphlRightPanel" Runat="Server">
<uc:ContactUsRightSidePanel ID="ucContactUsRightSidePanel" runat="server" />
</asp:Content>

