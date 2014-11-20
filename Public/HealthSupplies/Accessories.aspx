<%@ Page Title="EosHealth - Accessories" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Accessories.aspx.cs" Inherits="Public_WellnessProducts_Accessories" %>
<%@ Register TagPrefix="ZFort" Namespace="ZFort.Web.UI.WebControls" %>
<%@ Register TagPrefix="uc" TagName="CommunicatorsSmall" Src="~/Controls/Public/WellnessProducts/FeaturedPhone.ascx" %>
<%@ Register TagPrefix="uc" TagName="HealthSuppliesSmall" Src="~/Controls/Public/WellnessProducts/HealthSupplies.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    <script type="text/javascript">
<!--

function ShowAccessory(divId, e)
{
    var divAccessory = document.getElementById(divId);
    
    ScreenRes.getAll();		    	
    var x=e.pageX?e.pageX:e.clientX+ScreenRes.scrollX;
    var y=e.pageY?e.pageY:e.clientY+ScreenRes.scrollY;	       		        	
    if(x + divAccessory.offsetWidth + 5 > ScreenRes.width+ScreenRes.scrollX)
    {			
	    x=x-divAccessory.offsetWidth-5;if(x<0)x=0;
    }    	
    else x=x+20;	    
    y=y+20;
    divAccessory.style.left = x+"px";
    divAccessory.style.top=y+"px";	       
	
	divAccessory.style.display='block';     
}

function HideAccessory(divId)
{
    document.getElementById(divId).style.display = 'none';    
}

//-->
</script>
    <!-- CMS Content Block -->
    <div class="box">
        <div class="subp_box_c_title_line">
            <div class="subp_box_c_title">
            	<span class="title_left">Products</span> 
                <span><asp:Literal runat="server" ID="ltTitle" /></span> 
			</div>
        </div>
        <div class="CmsContent">
            <asp:Literal runat="server" ID="ltContent" />
        </div>
    </div>
    <!-- CMS Content Block -->
    <div class="border_1 margin_bottom_0">
        <div class="search_title">Products Search</div>
        <div class="search_box">
            <asp:LinkButton ID="lbtnSearch" runat="server" Text="<span>&nbsp;</span><b><%$Resources:Strings,Search %></b>" CssClass="search_btn"><span>&nbsp;</span><b>Search</b></asp:LinkButton>
            <span class="search_input_txt"><asp:TextBox ID="tbKeyWord" runat="server" value="Enter keywords" onblur="if(this.value == '') {this.value='Enter keywords';}" onmousedown="if(this.value == 'Enter keywords') {this.value='';} this.focus();"></asp:TextBox></span>
        </div>
    </div>
    
    <asp:PlaceHolder runat="server" ID="phlPA" EnableViewState="false" Visible="false">    
    <div>        
        <br />
        <center>
        <h3>The product has been added to your shopping cart.</h3>
        </center>
    </div>
    </asp:PlaceHolder>    
    
    <div class="margin_top_10" id="communicator_list">
        <div class="frame">
        <ZFort:XRepeater ID="xrptAccessorys" runat="server" OnItemCommand="xrptAccessorys_ItemCommand" >
            <ItemTemplate>
                <div  class="communicators_box_1">
                    <div class="right_border">
                        <div class="img_cont_def recent_quest_img">
                            <div class="def_size">
                                <table>
                                    <tr>
                                        <td><a href="javascript:void(0);" onclick="ShowAccessory('divAccessory<%# DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID) %>', event);" ><%# BSLayer.Accessory.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID)), true) %></a></td>
									</tr>
								</table>
							</div>
						</div>
						<div class="title a_center">                        				
	                        <a href="javascript:void(0);" class="bold link_none_hover" onclick="ShowAccessory('divAccessory<%# DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID) %>', event);" ><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.Name)) %></a>
						</div>  
						<div class="price a_center"> 
                             Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.Price)).ToString("0.00") %>                     
                        </div>
                    </div>
                </div>
                <div id="divAccessory<%# DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID) %>" class="popup_communicator">
                
                   	<span class="popup_image"><%# BSLayer.Accessory.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID)), false) %>
					</span>                                                                                                                        
					<p class="title color_blue_1 upper"><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.Name)) %>
                    </p>
					<p class="price">
						Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.Price)).ToString("0.00") %>
                    </p>
                    <div>           
						<%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.Description)) %>
					</div>
                    <br clear="all" />
                    <div class="btns f_left">
                        <asp:LinkButton CssClass="btn" runat="server" ID="lbtnBuy" CommandName="select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID) %>' ToolTip="Buy"><b>Buy</b></asp:LinkButton>
                        <a href="javascript:void(0);" onclick="HideAccessory('divAccessory<%# DataBinder.Eval(Container.DataItem, DBLayer.Accessory.ColumnName.ID) %>');" class="btn" title="Hide"><b>Hide</b></a>
					</div>                        
				</div> 
            </ItemTemplate>
        </ZFort:XRepeater>
        </div>
    </div>
    <div class="clearer"></div>
    <div class="margin_top_10">
        <%--<ZFort:XNavBar ID="nbAccessorys" VisibleAddButton="false" runat="server" ControlToNavigateID="xrptAccessorys" />--%>
        <ZFort:XNavBarXSLT ID="nbAccessorys" runat="server" PageSize="10" ControlToNavigate="xrptAccessorys" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:CommunicatorsSmall runat="server" ID="ucCommunicatorsSmall" />
    <uc:HealthSuppliesSmall runat="server" ID="ucHealthSuppliesSmall" />
</asp:Content>