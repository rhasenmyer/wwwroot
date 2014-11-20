<%@ Page Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Public_Register" %>
<%@ Register TagPrefix="uc" TagName="Register" Src="~/Controls/Public/Register.ascx" %>
<%@ Register TagPrefix="uc" TagName="Program" Src="~/Controls/Public/Program.ascx" %>
<%@ Register TagPrefix="uc" TagName="FAQShortTable" Src="~/Controls/Public/FAQShortTable.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    

	
	<uc:Register runat="server" ID="ucRegister"  />
	<uc:Program runat="server" ID="ucProgram" />
    
    <!-- CMS Content Block -->
	<div class="box">
        <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->    
	
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:FAQShortTable ID="ucFAQShortTable" runat="server" />
</asp:Content>
