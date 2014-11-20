<%@ Page Title="EosHealth - Community Registration" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CommunityRegistration.aspx.cs" Inherits="Public_Register_CommunityRegistration"  %>
<%@ Register TagPrefix="uc" TagName="CommunityRegistration" Src="~/Controls/Public/Registration/CommunityRegistration.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlMain" Runat="Server">
    <!-- CMS Content Block -->	
	
    <div class="register_page">
            
        <div class="subp_box_c_title_line">
            <div class="subp_box_c_title">
                <span class="border_0 padding_left_0"><asp:Literal runat="server" ID="ltTitle" /></span>
            </div>
        </div>
            

        <fieldset class="data_container">            
    		<div class="bg_join_top">
	            <asp:Literal runat="server" ID="ltContent" />
			</div>                
	<!-- CMS Content Block -->
	
	        <uc:CommunityRegistration runat="server" ID="ucCommunityRegistration"/>
	
        </fieldset>        
    </div>	
</asp:Content>