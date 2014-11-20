<%@ Page Title="EosHealth - Register" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="RegisterChoise.aspx.cs" Inherits="Public_Register_RegisterChoise"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    
<div class="register_page">
   
    <!-- CMS Content Block -->
        <div class="subp_box_c_title_line">
            <div class="subp_box_c_title">
                <span class="border_0 padding_left_0"><asp:Literal runat="server" ID="ltTitle" /></span>
            </div>
        </div>                          
            
        <div class="font_type_1 top_cont">
            <asp:Literal runat="server" ID="ltContent" />
        </div>
	<!-- CMS Content Block -->
	
	<br /><br />	
	<center>	
	
	<asp:LinkButton runat="server" ID="lbtnCommunityRegister">Community Member Registration ></asp:LinkButton>
	&nbsp;
	<asp:LinkButton runat="server" ID="lbtnRegister">Base Registration ></asp:LinkButton>	
	
	</center>

</div>
			
</asp:Content>

