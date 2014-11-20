<%@ Page Title="EosHealth - On Call Vivid Upload Software" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="AconUpload.aspx.cs" Inherits="Public_HistoryAndPhilosophy" %>
<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
	<div class="about_history_and_philosophy">
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
		<h4>Using the Acon On Call Vivid Upload Software</h4>
		<span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>

	<br/><br/>
	<div id="Acon_Send_cphlRightPanel_howToVideo_objFlash" class="video" style="text-align: center;"></div> 
        
    	
	<script type="text/javascript"> 
        	var fo = new FlashObject("/flash/video_home.swf", "swf", 550, 450, "9,0,0,0", "#000000");
        	fo.addParam("FlashVars","video=/Upload/UserGuideVideos/OnCallVivid_SendResults.flv&image=/Upload/UserGuideVideos/OnCallVivid_SendResults.jpg");
        	fo.addParam("wmode","opaque");
        	fo.write("Acon_Send_cphlRightPanel_howToVideo_objFlash");
	</script>
	

        <div id="divGCNT" class="about_c_content left_content_fix CmsContent" style="font-size: <%=(Request.Cookies["CMSFontSize"] == null ? "12" : Request.Cookies["CMSFontSize"].Value) %>px;">
		<asp:Literal runat="server" ID="ltContent" visible="False" />
	</div>
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false"/>
</asp:Content>
