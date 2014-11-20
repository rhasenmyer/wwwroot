<%@ Page Title="EosHealth - Register" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="RegisterOld.aspx.cs" Inherits="Public_RegisterOld" %>
<%@ Register TagPrefix="uc" TagName="FirstStep" Src="~/Controls/Public/Registration/FirstStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="SecondStep" Src="~/Controls/Public/Registration/SecondStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="ThirdStep" Src="~/Controls/Public/Registration/ThirdStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="FourthStep" Src="~/Controls/Public/Registration/FourthStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="FifthStep" Src="~/Controls/Public/Registration/FifthStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="SixthStep" Src="~/Controls/Public/Registration/SixthStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="SeventhStep" Src="~/Controls/Public/Registration/SeventhStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="EighthStep" Src="~/Controls/Public/Registration/EighthStep.ascx" %>
<%@ Register TagPrefix="uc" TagName="Shipping" Src="~/Controls/Public/Registration/Shipping.ascx" %>
<%@ Register TagPrefix="uc" TagName="Billing" Src="~/Controls/Public/Registration/Billing.ascx" %>
<%@ Register TagPrefix="uc" TagName="Review" Src="~/Controls/Public/Registration/Review.ascx" %>
<%@ Register TagPrefix="uc" TagName="AllPrograms" Src="~/Controls/Public/Registration/AllPrograms.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    

	
	<uc:FirstStep runat="server" ID="ucFirstStep" OnContinue="ucFirstStep_Continue"/>
	<uc:SecondStep runat="server" ID="ucSecondStep" OnBack="ucSecondStep_Back" OnChooseAssistant="ucSecondStep_ChooseAssistant" OnChooseProgram="ucSecondStep_ChooseProgram" OnContinue="ucSecondStep_Continue" OnShowPackages="ucSecondStep_ShowPackages" />
	<uc:ThirdStep runat="server" ID="ucThirdStep" OnBack="ucThirdStep_Back" OnSelectAnswer="ucThirdStep_SelectAnswer" />
	<uc:FourthStep runat="server" ID="ucFourthStep" OnBack="ucFourthStep_Back" OnContinue="ucFourthStep_Continue" OnContinueOnlineOnly="ucFourthStep_ContinueOnlineOnly" />
	<uc:FifthStep runat="server" ID="ucFifthStep" OnBack="ucFifthStep_Back" OnContinue="ucFifthStep_Continue" />
	<uc:SixthStep runat="server" ID="ucSixthStep" OnBack="ucSixthStep_Back" OnContinue="ucSixthStep_Continue"/>
	<uc:SeventhStep runat="server" ID="ucSeventhStep" OnBack="ucSeventhStep_Back" OnContinue="ucSeventhStep_Continue"/>
	<uc:EighthStep runat="server" ID="ucEighthStep" OnBack="ucEighthStep_Back" OnContinue="ucEighthStep_Continue"/>
	
	<uc:Shipping runat="server" ID="ucShipping" OnBack="ucShipping_Back" OnContinue="ucShipping_Continue"/>
	<uc:Billing runat="server" ID="ucBilling" OnBack="ucBilling_Back" OnContinue="ucBilling_Continue"/>
	<uc:Review runat="server" ID="ucReview" OnBack="ucReview_Back" OnCompletePurchase="ucReview_CompletePurchase" 
	    OnChangeAdress="ucReview_ChangeAdress" OnChangeHealthSupply="ucReview_ChangeHealthSupply" 
	    OnChangeInformation="ucReview_ChangeInformation" OnChangeOrder="ucReview_ChangeOrder" 
	    OnChangePlan="ucReview_ChangePlan" OnChangeProgram="ucReview_ChangeProgram" 
	    OnChangeShippingSpeed="ucReview_ChangeShippingSpeed"/>
    
    <!-- CMS Content Block -->
	<div class="box">
        <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	</div>
	<!-- CMS Content Block -->    
	
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:AllPrograms ID="ucAllPrograms" runat="server" />
</asp:Content>
