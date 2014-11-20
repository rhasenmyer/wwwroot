<%@ Page Title="EosHealth - Featured Phones" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="FeaturedPhones.aspx.cs" Inherits="Public_WellnessProducts_FeaturedPhones" %>
<%@ Register TagPrefix="ZFort" Namespace="ZFort.Web.UI.WebControls" %>
<%@ Register TagPrefix="uc" TagName="HealthSuppliesSmall" Src="~/Controls/Public/WellnessProducts/HealthSupplies.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

<script type="text/javascript">
<!--

function ShowDivInfo(divId, e)
{        
    for(var z=0; z < 2; z++)
    {
        var divInfo = document.getElementById(divId);
        
        ScreenRes.getAll();		    	
        var x=e.pageX?e.pageX:e.clientX+ScreenRes.scrollX;
        var y=e.pageY?e.pageY:e.clientY+ScreenRes.scrollY;	       		        	
        if(x + divInfo.offsetWidth + 5 > ScreenRes.width+ScreenRes.scrollX)
        {			
	        x=x-divInfo.offsetWidth-5;if(x<0)x=0;
        }    	
        else x=x+20;            
        
        y=y-20-document.getElementById(divId).clientHeight;
        
        divInfo.style.left = x+"px";
        divInfo.style.top=y+"px";
    	
        var elements = document.getElementById('divFrame').getElementsByTagName("DIV");
        for(var i=0; i< elements.length; i++)
        {
            if(elements[i].id.indexOf('divP') != -1)
            {
                elements[i].style.display = 'none';
            }
        }
    		
        divInfo.style.display='block';	    
    }
}

function HideDivInfo(divId)
{
    document.getElementById(divId).style.display = 'none';    
}

//-->
</script>

    <!-- CMS Content Block -->
<div class="health_supplies_featured">
    <h4>Featured Phone</h4>
    <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span> 
    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>
    <asp:PlaceHolder runat="server" ID="phlPA" EnableViewState="false" Visible="false">    
    <div>        
        <br />
        <center>
        <h3>The product has been added to your shopping cart.</h3>
        </center>
    </div>
    </asp:PlaceHolder>
    
    <div id="FeaturedPhone_list">
        <div class="frame" id="divFrame">
            <ZFort:XRepeater ID="xrptFeaturedPhones" runat="server" OnItemCommand="xrptFeaturedPhones_ItemCommand" >
                <ItemTemplate>                        
                        <center>
	                        <asp:LinkButton runat="server" ID="lbtnImage" CommandName="select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>' CssClass="image_box"><%# BSLayer.FeaturedPhone.GetImage(AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID)), false) %></asp:LinkButton>
                            
	                        <asp:LinkButton runat="server" ID="lbtnTitle" CommandName="select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>'>
                            <h3 class="subtitle">
							    <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.Name)) %>
						    </h3>
						</asp:LinkButton>						
                        	<span class="price">
                        	Price: $<%# AppLayer.Functions.ToMoney(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.Price)).ToString("0.00") %>
						</span>
						</center>
                        <div class="splitter_green splitter_m_10"></div>
                        <div class="a_justify font_size_2"> 
							<%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.Description)) %> 
						</div>
                        <div class="splitter_green splitter_m_10"></div>
                        <div class="btns f_right">                            
                            <asp:LinkButton CssClass="btn f_btn" runat="server" ID="lbtnBuy" CommandName="select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>' ToolTip="Buy"><b>Buy</b></asp:LinkButton>                            
                            <a onclick="ShowDivInfo('divPF<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>', event)" class="btn" href="javascript:void(0);"><b>Phone Features</b></a>
                            <a onclick="ShowDivInfo('divPC<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>', event)" class="btn" href="javascript:void(0);"><b>Program Compatibility</b></a>                                
                        </div>                                                    
                        <div id="divPF<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>" class="popup_box hidden">
                            <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.TechnicalDescription)).Replace("\n", "<br />") %>
                            <br />
                            <a  class="btn" href="javascript:void(0);" onclick="HideDivInfo('divPF<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>');"><b>Hide</b></a>
                        </div>
                        <div id="divPC<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>" class="popup_box hidden">
                            <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.SupportedFeatures)).Replace("\n", "<br />") %>
                            <br />
                            <a  class="btn" href="javascript:void(0);" onclick="HideDivInfo('divPC<%# DataBinder.Eval(Container.DataItem, DBLayer.FeaturedPhone.ColumnName.ID) %>');"><b>Hide</b></a>
                        </div>                                            
                    <asp:PlaceHolder runat="server" ID="phlDevider" Visible="false">		
						<div class="splitter_green splitter_m_10"></div>
                    </asp:PlaceHolder>                                                             
                </ItemTemplate>
            </ZFort:XRepeater>
        </div>            
    </div>
</div>    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:HealthSuppliesSmall runat="server" ID="ucHealthSuppliesSmall" />
</asp:Content>
