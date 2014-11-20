<%@ Page Title="EosHealth - Overview" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Overview.aspx.cs" Inherits="Public_WellnessProducts_Overview" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
   
    <!-- CMS Content Block -->
<div class="health_supplies_all">	    		    
    <h4>All Products <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>        
	<%--<div class="clearfix">
		<h3 class="category"><a href="/Public/HealthSupplies/FeaturedPhones.aspx">Featured Phones</a></h3>
        <div class="wellness_prod_flash_1">
		    <div id="divRotatorWPC" align="center"></div>		
		</div> 
		<a class="btn_arrow f_right" href="/Public/HealthSupplies/FeaturedPhones.aspx" title="View All"><b>View All</b></a>
	</div>        
    <script type="text/javascript">
	    // <![CDATA[		    	    	
	    var vi = new SWFObject("/flash/rotator3.swf", "vi", "435", "170", "8", "#fff", false);
	    vi.addParam("wmode", "opaque");
	    vi.addParam("FlashVars", "xml=/WS/WS.aspx?action=getwp%26type=featuredphones%26rand=<%=DateTime.Now.Ticks %>");
	    vi.addParam("wmode", "transparent");
	    vi.write("divRotatorWPC");	    	    
	    // ]]>
    </script>--%>
	<div class="clearfix">
		<h3 class="category"><a href="/Public/HealthSupplies/HealthSupplies.aspx">Health Supplies</a></h3>
        <div class="wellness_prod_flash_1">
            <div id="divRotatorWPHP" align="center"></div>		        
        </div>
		<a class="btn_arrow f_right"  href="/Public/HealthSupplies/HealthSupplies.aspx" title="View All"><b>View All</b></a>
	</div>                    
    <script type="text/javascript">
        // <![CDATA[
	    var vi = new SWFObject("/flash/rotatorwp.swf", "vi", "400", "135", "9", "#ffffff", false);
	    vi.addParam("wmode", "transparent");
	    vi.addParam("FlashVars", "xml=/WS/WS.aspx?action=getwp%26type=healthsupplies%26rand=<%=DateTime.Now.Ticks %>");
	    vi.addParam("allowScriptAccess", "always");
	    vi.write("divRotatorWPHP");
	    // ]]>
    </script>            
</div>
    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" />
</asp:Content>
