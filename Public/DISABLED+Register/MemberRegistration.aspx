<%@ Page Title="EosHealth - Member Registration" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="MemberRegistration.aspx.cs" Inherits="Public_MemberRegistration" %>
<%@ Register TagPrefix="uc" TagName="FirstStep" Src="~/Controls/Public/Registration/FirstStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="AllPrograms" Src="~/Controls/Public/Registration/AllPrograms.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">    
    <!-- CMS Content Block -->
	<div class="box">
        <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->
    
    <div class="reg_top_bg">
    <asp:PlaceHolder ID="phlFromProduct" runat="server" Visible="false">
        <h3 class="a_center">To purchase this product, you need to be registered as a Member.</h3>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder ID="phlUpgradeRegister" runat="server" Visible="false">
        <h3 class="a_center">You should register as member to use all features of Wellness4life.mobi.</h3>
    </asp:PlaceHolder>
    </div>
	<uc:FirstStep runat="server" ID="ucFirstStep" OnContinue="ucFirstStep_Continue"/>	   
	
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:AllPrograms ID="ucAllPrograms" runat="server" />
</asp:Content>
