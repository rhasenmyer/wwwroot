<%@ Page Title="EosHealth - Medical Experiences" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="MedicalExperiences.aspx.cs" Inherits="Public_WhoIsItFor_MedicalExpiriences" ValidateRequest="true" %>
<%@ Register TagPrefix="uc" TagName="CommunityBoard" Src="~/Controls/Public/CommunityBoard.ascx" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityMember" Src="~/Controls/Common/JoinAsACommunityMember.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

    <!-- CMS Content Block -->
	<div class="box">
    
        <div class="subp_box_c_title_line">
        	<div class="subp_box_c_title">
		        <span class="title_left">Who Is It For</span> <span><asp:Literal runat="server" ID="ltTitle" /></span>
            </div>
		</div>    
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	</div>
	<!-- CMS Content Block -->
    <asp:PlaceHolder runat="server" ID="phMain">	    
    <ZFort:XNavBarXSLT ID="nbMedicalExperienceTop" runat="server" PageSize="10" ControlToNavigate="gvMedicalExperience" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    <div class="members_stories margin_top_10 margin_bottom_10">
    <ZFort:XGridView ID="gvMedicalExperience" runat="server" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="ID" AllowSorting="true" GridLines="None" Width="100%">
       <Columns>
         <ZFort:TemplateField>
            <ItemTemplate>				
                    <h3 class="subtitle"><%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Title) %></h3>
                    <div class="text font_type_1 clearfix">
                        
                        <div class="text font_type_1">
                            <div class="img_cont_def member_stories_image float_l margin_top_0<%# System.IO.File.Exists(Server.MapPath(BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID))))) ? "" : " hidden" %>">
                                <div class="def_size">
                                    <table><tr><td><img src="<%# BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID))) %>" border="0" alt="<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Title) %>" /></td></tr></table>
                                </div>                            
                            </div>                        
                            <%# AppLayer.Functions.ToString(AppLayer.Functions.RemoveTags(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Description).ToString()), 200) %>                            
                        </div>
                        <div class="f_right margin_top_10">
	                        <asp:LinkButton CssClass="btn btn_grey" ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID) %>' CommandName="view"><b>Read More</b></asp:LinkButton>                                                
                        </div>
                        
                        <div class="clearer"></div>
                        <asp:LinkButton ID="LinkButton1" CssClass="pdf_downl_lnk margin_top_10" runat="server" Visible="<%# System.IO.File.Exists(BSLayer.ContentItem.GetItemDocumentFullPath(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID)))) %>" CommandArgument="<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID) %>" CommandName="download"><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.WhoIsItForMembershipProgram.ColumnName.DocName)).Trim().Length > 0 ? DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.DocName).ToString() : Resources.Form_PageContent.DownloadArticle %></asp:LinkButton>
                     </div>   
                     <asp:PlaceHolder runat="server" ID="phlDevider" Visible="false">
                        <div class="line_featured"></div>
                     </asp:PlaceHolder>
            </ItemTemplate>
        </ZFort:TemplateField>
        </Columns>
        <HeaderStyle Wrap="False" CssClass="tdHeader" />
        <RowStyle CssClass="tdRow" />
        <AlternatingRowStyle CssClass="tdAltRow" />
    </ZFort:XGridView>    
    </div>       
    <ZFort:XNavBarXSLT ID="nbMedicalExperience" runat="server" PageSize="10" ControlToNavigate="gvMedicalExperience" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </asp:PlaceHolder>                
    
    <asp:PlaceHolder runat="server" ID="phlDetailedView">
        <h3 class="subtitle"><asp:Literal runat="server" ID="ltMETitle"></asp:Literal></h3>
        <div class="text font_type_1">
            
            <div class="text font_type_1">
                
                <asp:PlaceHolder runat="server" ID="phlMEImage">
                    <div class="img_cont_def member_stories_image float_l margin_top_0">
                        <div class="def_size">
                            <table><tr><td><asp:Image runat="server" ID="imgMEImage" /></td></tr></table>
                        </div>
                    </div>                        
                </asp:PlaceHolder>
                
                <asp:Literal runat="server" ID="ltMEDescription"></asp:Literal>                
                <asp:LinkButton ID="lbtnMEDownloadArticle" runat="server" CssClass="pdf_downl_lnk"><asp:Literal runat="server" ID="ltMEDocName"></asp:Literal></asp:LinkButton>
            </div>                                                
            <div class="clearer"></div>                        
            <asp:LinkButton CssClass="btn btn_grey margin_top_10" ID="lbtnMEBack" runat="server" ><b>Back</b></asp:LinkButton>
         </div>                            
    </asp:PlaceHolder>
    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinAsACommunityMember ID="ucJoinAsACommunityMember" runat="server" />
    <uc:MemberStories ID="ucMemberStories" runat="server" />
    <uc:CommunityBoard ID="ucCommunityBoard" runat="server" />
</asp:Content>