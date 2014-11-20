<%@ Page Title="EosHealth - Registration" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Public_Register_Registration"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    
   
<script type="text/javascript">
<!--

function ShowTip(divId, e)
{    
    var divTip = document.getElementById(divId);    
    ScreenRes.getAll();		    	
    var x=e.pageX?e.pageX:e.clientX+ScreenRes.scrollX;
    var y=e.pageY?e.pageY:e.clientY+ScreenRes.scrollY;
    x=x-190;
    divTip.style.display = 'block';
    y = y - divTip.offsetHeight - 5;
    divTip.style.display = 'none';        
    divTip.style.left = x+"px";
    divTip.style.top=y+"px";	
	divTip.style.display='block';     
}

function HideTip(divId)
{
    document.getElementById(divId).style.display = 'none';
}

//-->
</script>
	<div class="home_content_bg no-back reg_page clearfix">   
    <!-- CMS Content Block -->
        <div class="subp_box_c_title_line">
            <div class="subp_box_c_title">
                <span class="border_0 padding_left_0"><asp:Literal runat="server" ID="ltTitle" /></span>
            </div>
        </div>                          
        
        <asp:PlaceHolder runat="server" Visible="false">
            <div class="font_type_1 top_cont">
                <asp:Literal runat="server" ID="ltContent" />
            </div>
        </asp:PlaceHolder>
	<!-- CMS Content Block -->

		<h3 class="blue_title">Welcome to the Wellness4Life Community!</h3>
        <div class="f_right bold alredy_a_member">
            <a href="/Public/Login.aspx" title="Log In">Already a member? ></a>
        </div>
        
        <table class="tbl_register_benefits color_grey_5" id="tbl_1">
        	<tr>
            	<th class="th_1 a_left"><b>Features</b></th>
                <th class="th_2 a_left"><b>Member</b></th>
                <th class="th_3 a_left"><b>Subscriber</b></th>
            </tr>
            <tr>
            	<td class="td_1">Membership</td>
                <td class="td_2"><strong class="color_green_1">FREE</strong></td>
                <td class="td_3"><strong class="color_blue_1">Starting from  $12.50</strong></td>                
            </tr>            
            
            <asp:Repeater runat="server" ID="rptRegisterTips">
                <ItemTemplate>
                    <tr>
            	        <td class="td_1">
            	        <asp:PlaceHolder runat="server" ID="phlHaveDescription">
				            <div id="divDescription<%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.ID) %>" class="bubble_popup_1 abs" >
                	            <div class="bubble_top_1 png_bg"></div>
                                <div class="bubble_mid_1 png_bg color_blue_1 a_left">
                                    <%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.Description) %>
					            </div>
                                <div class="bubble_bot_1 png_bg"></div>                            
                            </div>
            	            <span onmouseover="ShowTip('divDescription<%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.ID) %>', event); this.style.cursor = 'hand';" onmouseout="HideTip('divDescription<%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.ID) %>');"><%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.Name) %></span>
                        </asp:PlaceHolder>            	            
                        <asp:PlaceHolder runat="server" ID="phlHaveNoDescription">
                            <%# DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.Name) %>                            
                        </asp:PlaceHolder>
            	        </td>
                        <td class="td_2"><%# AppLayer.Functions.ToBool(DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.IsMember)) ? "<span class=\"checked\"></span>" : "" %></td>
                        <td class="td_3"><%# AppLayer.Functions.ToBool(DataBinder.Eval(Container.DataItem, DBLayer.RegisterTip.ColumnName.IsSubscriber)) ? "<span class=\"checked\"></span>" : "" %></td>                
                    </tr>                    
                </ItemTemplate>
            </asp:Repeater>                               
            <tr>
            	<td class="td_1"></td>
                <td class="td_2">
					<div id="divCommunityRegister" class="bubble_popup_1 abs" >
                    	<div class="bubble_top_1 png_bg"></div>
                        <div class="bubble_mid_1 png_bg color_blue_1 a_left">
	                        Access to the community features listed above are free to you, giving you access to specific community engagements, challenges and support while engaging you into a healthier lifestyle.
						</div>
                        <div class="bubble_bot_1 png_bg"></div>                            
                    </div>
                	<a href="javascript:void(0);" onmouseover="ShowTip('divCommunityRegister', event);" onmouseout="HideTip('divCommunityRegister');" class="bold">What do i get?</a>
                    <div class="w_fix_1"></div>
                </td>
                <td class="td_3">
                    <div id="divRegister" class="bubble_popup_1 abs"  style="cursor:hand;">
                    	<div class="bubble_top_1 png_bg"></div>
                        <div class="bubble_mid_1 png_bg color_blue_1 a_left">
	                        Access to all Wellness4Life features listed above, including special mobile phone application and entire Wellness4Life online and mobile community network. 
						</div>
                        <div class="bubble_bot_1 png_bg"></div>                            
                    </div> 	                
                    <a href="javascript:void(0);" onmouseover="ShowTip('divRegister', event);" onmouseout="HideTip('divRegister');" class="bold">What do i get?</a>
                    <div class="w_fix_2"></div>
                </td>                
            </tr> 
             <tr>
            	<td class="td_1"></td>
                <td class="td_2">
                	<table align="center"><tr><td>
	                	<asp:LinkButton CssClass="btn btn_grey" runat="server" ID="lbtnCommunityRegister"><b>Member Registration</b></asp:LinkButton>
                    </td></tr></table>
				</td>
                <td class="td_3">
                	<table align="center"><tr><td>
	                    <asp:LinkButton CssClass="btn btn_grey" runat="server" ID="lbtnRegister"><b>Subscriber Registration</b></asp:LinkButton>	
					</td></tr></table>                        
                </td>                
            </tr>                                  
        </table>
        
<script type="text/javascript">
// <![CDATA[
var tables = document.getElementsByTagName("table");
for ( var t = 0; t < tables.length; t++ ) {
  var rows = tables[t].getElementsByTagName("tr");
  for ( var i = 2; i < rows.length; i += 2 )
  	
    if ( !/(^|s)odd(s|$)/.test( rows[i].className ) )
      rows[i].className += " tr_hilite";
	
}
//]]>
</script>        
        
</div>
			
</asp:Content>

