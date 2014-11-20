<%@ Page Title="EosHealth - Health Supplies" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="HealthSupplies.aspx.cs" Inherits="Public_WellnessProducts_HealthSupplies" %>
<%@ Register TagPrefix="ZFort" Namespace="ZFort.Web.UI.WebControls" %>
<%@ Register TagName="GenericUC" TagPrefix="uc" Src="~/Controls/Public/GenericUC.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<script type="text/javascript">
<!--

    function HidePopups() {
        $(".popup_box").css("display", "none");
    }
    
    function ShowHealthSupply(divId, e) {
    HidePopups();
    var divHealthSupply = document.getElementById(divId);
    
    ScreenRes.getAll();		    	
    var x=e.pageX?e.pageX:e.clientX+ScreenRes.scrollX;
    var y=e.pageY?e.pageY:e.clientY+ScreenRes.scrollY;	       		        	
    if(x + divHealthSupply.offsetWidth + 5 > ScreenRes.width+ScreenRes.scrollX)
    {			
	    x=x-divHealthSupply.offsetWidth-5;if(x<0)x=0;
    }    	
    else x=x+20;	    
    y=y+20;
    divHealthSupply.style.left = x+"px";
    divHealthSupply.style.top=y+"px";

    divHealthSupply.style.display = 'block'; 
	    
}

function HideHealthSupply(divId)
{
    document.getElementById(divId).style.display = 'none';    
}

//-->
</script>
    <!-- CMS Content Block -->
<div class="health_supplies_supplies">
    <h4>Health Supplies: <asp:Label runat="server" ID="lblTest" Text="(Login to Shop)"/>
        <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span> 
	</h4>
	
	
    <asp:Repeater runat="server" ID="rptSpecialOffers">
	    <HeaderTemplate>
	        <div class="health_supplies">
	            <div class="health_supplies_content">
	                <h3>Special Offers</h3>
	                <div class="health_supplies_content_text clearfix">
	            </HeaderTemplate>
	            <ItemTemplate>
	                <div class="left_side">
	                    <%# BSLayer.HealthSupply.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.SpecialOffer.ColumnName.HealthSupplyID)), true) %>
	                    <span><%# DBLayer.HealthSupply.GetDetails(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.SpecialOffer.ColumnName.HealthSupplyID))).Name.Value %></span>
	                </div>
	                <div class="right_side">
	                    <p><%# DataBinder.Eval(Container.DataItem, DBLayer.SpecialOffer.ColumnName.HealthSuppliesCMS) %></p><br/><br/>
	                    <a class="order_now" href="/Public/Register/Registration.aspx?program_id=<%# DataBinder.Eval(Container.DataItem, DBLayer.SpecialOffer.ColumnName.ProgramID) %>"></a>
	                </div>
	                <div class="c_both"></div>    
	            </ItemTemplate>
	            <FooterTemplate>
	                    <div class="health_supplies_line"></div>
	                    </div>
	                </div>	    
	        </div>
	    </FooterTemplate>
	</asp:Repeater>
        
	
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>
    <div class="ask_an_expert_overview clearfix">
    	<div class="form">
        	<fieldset>
                <label>Products Search</label>
                <span class="bg"><asp:TextBox ID="tbKeyWord" runat="server" value="Keywords Search" onblur="if(this.value == '') {this.value='Keywords Search';}" onmousedown="if(this.value == 'Keywords Search') {this.value='';} this.focus();"></asp:TextBox></span>
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"></asp:DropDownList>
                <asp:LinkButton ID="lbtnSearch" runat="server" Text="<%$Resources:Strings,Search %>" CssClass="submit">Search</asp:LinkButton>
			</fieldset>
		</div>            
    </div>
        
   
    <asp:PlaceHolder runat="server" ID="phlPA" EnableViewState="false" Visible="false">    
    <h1 class="message no_upper">The product has been added to your shopping cart.</h1>
    </asp:PlaceHolder>
    
    <asp:PlaceHolder runat="server" ID="phlNoResult" Visible="false" EnableViewState="false">
     <h1 class="message no_upper">There are no results matching your search criteria, you can select from the health supplies listed below or try another search.</h1>
    </asp:PlaceHolder>
    
   <div class="nav_top">
        <ZFort:XNavBarXSLT ID="nbHealthSupplysTop" runat="server" PageSize="12" ControlToNavigate="xrptHealthSupplies" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </div>
    <div id="communicator_list">
        <div class="frame" id="divFrame">
        <ZFort:XRepeater ID="xrptHealthSupplies" runat="server" OnItemCommand="xrptHealthSupplies_ItemCommand" OnItemDataBound="xrptHealthSupplies_ItemDataBound" >
            <ItemTemplate>
                <asp:PlaceHolder ID="phlSeparator" runat="server" Visible="false"><div class="splitter_green clearfix"></div></asp:PlaceHolder>
                <div class="communicators_box_1"> 
                    <a class="thumb_90x100" href="javascript:void(0);" onclick="ShowHealthSupply('divHealthSupply<%# DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID) %>', event);"><span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2"><%# BSLayer.HealthSupply.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID)), true) %></span><span class="swc3">&nbsp;</span></span></span></a>
                    <div class="title a_center">                        				
                        <a href="javascript:void(0);" class="bold link_none_hover" onclick="ShowHealthSupply('divHealthSupply<%# DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID) %>', event);" title="<%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Name)) %>" ><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Name), 18) %></a> 
                    </div>
                    <div class="price a_center"> 
                        Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Price)).ToString("0.00") %>
                    </div>
                </div>
                <div id="divHealthSupply<%# DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID) %>" class="hidden popup_box">
                	<div class="clearfix">
                        <span class="popup_image"><%# BSLayer.HealthSupply.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID)), false) %></span>
                        <p class="title color_blue_1 upper">
                            <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Name)) %>
                        </p>
                        <asp:PlaceHolder ID="phlSpecialPrices" runat="server">
                            Special prices for members. You must be a member to buy. Membership is FREE. 
                        </asp:PlaceHolder>
                        <p class="price">   
                            Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Price)).ToString("0.00") %>              
                        </p>
                        <div class="a_justify">           
                            <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.Description)) %>                        
                        </div>
					</div>
                    <table align="center" class="margin_top_10"><tr><td>
                        <asp:LinkButton CssClass="btn" runat="server" ID="lbtnBuy" CommandName="select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID) %>' ToolTip="Buy"><b>Buy</b></asp:LinkButton>
                        </td><td width="10"></td><td>
                        <a href="javascript:void(0);" onclick="HideHealthSupply('divHealthSupply<%# DataBinder.Eval(Container.DataItem, DBLayer.HealthSupply.ColumnName.ID) %>');" class="btn" title="Hide"><b>Hide</b></a>
					</td></tr></table>
				</div> 
            </ItemTemplate>
        </ZFort:XRepeater>
		</div>        
    </div>        
    <div class="nav_bot">
        <ZFort:XNavBarXSLT ID="nbHealthSupplys" runat="server" PageSize="12" ControlToNavigate="xrptHealthSupplies" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </div>

    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>

      <h3 class="category">See also these products on Amazon.com:</h3>
	<br/><br/>
      <div align="center">
        <script type='text/javascript'>
        var amzn_wdgt={widget:'Carousel'};
         amzn_wdgt.tag='eohe-20';
         amzn_wdgt.widgetType='ASINList';
         amzn_wdgt.searchIndex='';
        amzn_wdgt.ASIN='B00067E9WM,B0018ZKBSM,B0002Q09D0,B000ROJTT2,B000I1GQ04,B000I1FB8M,B000I1BR7Q,B00170B77M,B001706L4Q';
        amzn_wdgt.browseNode='51550011';
         amzn_wdgt.title='EosHealth Products';
         amzn_wdgt.width='440';
         amzn_wdgt.height='400';
        </script>
        <script type='text/javascript' src='http://wms.assoc-amazon.com/20070822/US/js/swfobject_1_5.js'>
        </script>        
	</div>        
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:GenericUC ID="ucGeneric" runat="server" Config="GucTop1" />
</asp:Content>