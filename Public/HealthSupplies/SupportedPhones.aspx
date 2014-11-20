<%@ Page Title="EosHealth - Supported Phones" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="SupportedPhones.aspx.cs" Inherits="Public_WellnessProducts_SupportedPhones" %>
<%@ Register TagPrefix="ZFort" Namespace="ZFort.Web.UI.WebControls" %>
<%@ Register TagPrefix="uc" TagName="CommunicatorsSmall" Src="~/Controls/Public/WellnessProducts/FeaturedPhone.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    <script type="text/javascript">
<!--

function ShowCommunicator(divId, e)
{
    var divCommunicator = document.getElementById(divId);
    
    ScreenRes.getAll();		    	
    var x=e.pageX?e.pageX:e.clientX+ScreenRes.scrollX;
    var y=e.pageY?e.pageY:e.clientY+ScreenRes.scrollY;	       		        	
    if(x + divCommunicator.offsetWidth + 5 > ScreenRes.width+ScreenRes.scrollX)
    {			
	    x=x-divCommunicator.offsetWidth-5;if(x<0)x=0;
    }    	
    else x=x+20;	    
    y=y+20;
    divCommunicator.style.left = x+"px";
    divCommunicator.style.top=y+"px";	       
	
	var elements = document.getElementById('divFrame').getElementsByTagName("DIV");
    for(var i=0; i< elements.length; i++)
    {
        if(elements[i].id.indexOf('divC') != -1)
        {
            elements[i].style.display = 'none';
        }
    }
	
	divCommunicator.style.display='block';     
}

function HideCommunicator(divId)
{
    document.getElementById(divId).style.display = 'none';    
}

//-->
</script>
<div class="health_supplies_supported">
    <h4>Supported Phones
        <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span> 
    </h4>                
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>
        
    <div class="ask_an_expert_overview clearfix">
    	<div class="form">
        	<fieldset>
                <label>Phone Search</label>
                <span class="bg"><asp:TextBox ID="tbKeyWord" runat="server" value="Enter keywords" onblur="if(this.value == '') {this.value='Enter keywords';}" onmousedown="if(this.value == 'Enter keywords') {this.value='';} this.focus();"></asp:TextBox></span>
                <asp:DropDownList ID="ddlCarrier" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:LinkButton ID="lbtnSearch" runat="server" Text="<%$Resources:Strings,Search %>" CssClass="submit">Search</asp:LinkButton>
			</fieldset>                
		</div>            
    </div>
    <asp:PlaceHolder runat="server" ID="phlPA" EnableViewState="false" Visible="false">    
        <h1 class="message no_upper">The product has been added to your shopping cart.</h`>
    </asp:PlaceHolder>    
    <asp:PlaceHolder runat="server" ID="phlNoResult" Visible="false" EnableViewState="false">
        <h1 class="message no_upper">There are no results matching your search criteria, you can select from the phones listed below or try another search.</h1>
    </asp:PlaceHolder>
    <div class="nav_top">
        <ZFort:XNavBarXSLT ID="nbCommunicatorsTop" runat="server" PageSize="12" ControlToNavigate="xrptCommunicators" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </div>
    <div id="communicator_list" class="clearfix">
        <div class="frame" id="divFrame">
        <ZFort:XRepeater ID="xrptCommunicators" runat="server" OnItemDataBound="xrptCommunicators_ItemDataBound">
            <ItemTemplate>
                <asp:PlaceHolder ID="phlSeparator" runat="server" Visible="false"><div class="splitter_green clearfix"></div></asp:PlaceHolder>
            
                <div class="communicators_box_1">
					<a class="thumb_90x100" href="javascript:void(0);" onclick="ShowCommunicator('divCommunicator<%# DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID) %>', event);" ><span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2"><%# BSLayer.Communicator.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID)), true) %></span><span class="swc3">&nbsp;</span></span></span></a>
                    <div class="title a_center">                        				
                        <a href="javascript:void(0);" class="bold link_none_hover" onclick="ShowCommunicator('divCommunicator<%# DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID) %>', event);" ><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.Name)) %></a>
                    </div>  
                </div>
                <div id="divCommunicator<%# DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID) %>" class="hidden popup_box">
                	<div class="clearfix">
                        <span class="popup_image"><%# BSLayer.Communicator.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID)), false) %>
                        </span>                                                                                                                        
                        <p class="title color_blue_1 upper"><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.Name)) %>
                        </p>
                        <p class="price hidden">
                            Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.Price)).ToString("0.00") %>
                        </p>
                        <div class="a_justify">           
                            <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.Description)) %>
                        </div>
					</div>
                    <table align="center"><tr><td><a href="javascript:void(0);" onclick="HideCommunicator('divCommunicator<%# DataBinder.Eval(Container.DataItem, DBLayer.Communicator.ColumnName.ID) %>');" class="btn margin_top_10" title="Hide"><b>Hide</b></a></td></tr></table>
				</div> 
                
            </ItemTemplate>
        </ZFort:XRepeater>
        </div>
    </div>
    <div class="nav_bot">
        <%--<ZFort:XNavBar ID="nbCommunicators" VisibleAddButton="false" runat="server" ControlToNavigateID="xrptCommunicators" />--%>
        <ZFort:XNavBarXSLT ID="nbCommunicators" runat="server" PageSize="12" ControlToNavigate="xrptCommunicators" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
	</div>        
</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:CommunicatorsSmall runat="server" ID="ucCommunicatorsSmall" />
</asp:Content>
