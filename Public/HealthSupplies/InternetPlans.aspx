<%@ Page Title="EosHealth - Internet Plans" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="InternetPlans.aspx.cs" Inherits="Public_InternetPlans" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

    <!-- CMS Content Block -->
	<div class="box">
	    
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
		<div class="subp_box_c_title_line">
        	<div class="subp_box_c_title">        	    
        		<span class="title_left">Products</span> <span><asp:Literal runat="server" ID="ltTitle" /></span>
            </div>
		</div>                 
        <div id="divGCNT" class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	
	<!-- CMS Content Block -->		
    </div>
    
    <!-- Core information -->
    <div class="register_fifth_box_1">
	<div class="main_box">
<asp:Repeater ID="rptCompanys" runat="server" OnItemDataBound="rptCompanys_ItemDataBound"
    OnItemCommand="rptCompanys_ItemCommand">
    <ItemTemplate>
    	
	<div class="f_left margin_right_10">        
        <div class="border_1">  
        	<div class="search_title"><asp:Literal ID="litName" runat="server"></asp:Literal></div>
            <div class="a_center">
                <img src="/images/MobilePlans/<%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompany.ColumnName.ID) %>.gif" alt="Complany Logo" border="0" />
			</div>                
        </div>
		<table align="center"><tr><td><asp:LinkButton CssClass="btn" ID="lbtnSelect" runat="server" CommandName="select" ToolTip="Select"><b>Select</b></asp:LinkButton></td></tr></table>
        
	</div>        
    </ItemTemplate>
</asp:Repeater>
	</div>
</div>
<asp:PlaceHolder ID="phlPlans1" runat="server" Visible="false">
<div class="clearer margin_top_10"></div>
<div class="margin_top_10">&nbsp;</div>
<h3 class="type_1 a_left margin_top_10"><asp:Literal runat="server" ID="ltMobileGoupTitle1"></asp:Literal></h3>
<div class="clearer"></div>
    <asp:Repeater ID="rptPlans1" runat="server">
        <HeaderTemplate>
            <table class="tbl_register_fifth">
                <tr class="head">                    
                    <td>
                        Plan Name
                    </td>
                    <td>
                        Whenever Minutes
                    </td>                    
                    <td>
                        Weekend/<br />Night minutes
                    </td>
                    <td>
                        Extra minutes cost
                    </td>
                    <td>
                        Data Plan
                    </td>                    
                    <td>
                        Price per month
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="body tr_0">                
                <td class="td_2 bold color_blue_1">
                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.Name) %>
                </td>
                <td class="td_3">
                	<span>
	                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.Minutes) %>
                    </span>
                </td>                
                <td class="td_5">
                	<span>
	                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.WNMinutes) %>
					</span>                        
                </td>
                <td class="td_6">
                	<span>
	                    <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.ExtraMinCost)) == "-" ? "-" : "$" + DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.ExtraMinCost).ToString() %>
					</span>                        
                </td>
                <td class="td_6">
                	<span>
	                    <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.DataPlan)) %>
					</span>                        
                </td>                
                <td class="td_7 bold color_grey_2">
                	<span>
	                    $<%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.PricePerPeriod) %>
					</span>                        
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:PlaceHolder>
    
<asp:PlaceHolder ID="phlPlans2" runat="server" Visible="false">
<div class="clearer margin_top_10"></div>
<div class="margin_top_10">&nbsp;</div>
<h3 class="type_1 a_left margin_top_10"><asp:Literal runat="server" ID="ltMobileGoupTitle2"></asp:Literal></h3>
<div class="clearer"></div>    
    
    <asp:Repeater ID="rptPlans2" runat="server">
        <HeaderTemplate>
            <table class="tbl_register_fifth">
                <tr class="head">                    
                    <td>
                        Plan Name
                    </td>
                    <td>
                        Whenever Minutes
                    </td>                    
                    <td>
                        Weekend/<br />Night minutes
                    </td>
                    <td>
                        Extra minutes cost
                    </td>
                    <td>
                        Data Plan
                    </td>                    
                    <td>
                        Price per month
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="body tr_0">                
                <td class="td_2 bold color_blue_1">
                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.Name) %>
                </td>
                <td class="td_3">
                	<span>
	                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.Minutes) %>
                    </span>
                </td>                
                <td class="td_5">
                	<span>
	                    <%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.WNMinutes) %>
					</span>                        
                </td>
                <td class="td_6">
                	<span>
	                    <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.ExtraMinCost)) == "-" ? "-" : "$" + DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.ExtraMinCost).ToString() %>
					</span>                        
                </td>
                <td class="td_6">
                	<span>
	                    <%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.DataPlan)) %>
					</span>                        
                </td>                
                <td class="td_7 bold color_grey_2">
                	<span>
	                    $<%# DataBinder.Eval(Container.DataItem, DBLayer.MobileCompanyPlan.ColumnName.PricePerPeriod) %>
					</span>                        
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>    
    </asp:PlaceHolder>
    <!-- Core information -->
</asp:Content>