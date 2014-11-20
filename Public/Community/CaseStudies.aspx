<%@ Page Title="EosHealth - Case Studies" Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="CaseStudies.aspx.cs" Inherits="Public_WhoIsItFor_MedicalExpiriences" ValidateRequest="true" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommunityBoard" Src="~/Controls/Public/CommunityBoard.ascx" %>
<%@ Register TagPrefix="uc" TagName="MemberStories" Src="~/Controls/Public/MemberStories.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityMember" Src="~/Controls/Common/JoinAsACommunityMember.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

		
        
    <!-- CMS Content Block -->
	<div class="members_experience">
        <h4>Case Studies <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span></h4>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	<!-- CMS Content Block -->
    <asp:PlaceHolder runat="server" ID="phMain">
    <div class="nav_top">	    
	    <ZFort:XNavBarXSLT ID="nbMedicalExperienceTop" runat="server" PageSize="10" ControlToNavigate="gvMedicalExperience" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
	</div>    
    <ZFort:XGridView ID="gvMedicalExperience" runat="server" ShowHeader="false" AutoGenerateColumns="false" DataKeyNames="ID" AllowSorting="true" GridLines="None" Width="100%">
       <Columns>
         <ZFort:TemplateField>
            <ItemTemplate>	
            	<ul class="list resources">
                	<li class="no_bg">
                    	<div class="clearfix">
                            <div class="image image_box<%# System.IO.File.Exists(Server.MapPath(BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID))))) ? "" : " hidden" %>">
                                <img src="<%# BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID))) %>" border="0" alt="<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Title) %>" />
                            </div>	
                            <div class="text">
                                <h6><%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Title) %></h6>		
                                <p>
                                    <%# AppLayer.Functions.ToString(AppLayer.Functions.RemoveTags(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.Description).ToString())) %>                            
                                </p> 
				
				<!--//                               
                                <p class="bottom">
                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID) %>' CommandName="view">Read More > </asp:LinkButton>                                                
                                </p>
				//-->

                             </div>
						</div>   
                           
			    <!--//				                       
                            <asp:LinkButton ID="LinkButton1" CssClass="pdf_downl_lnk margin_top_10" runat="server" Visible="<%# System.IO.File.Exists(BSLayer.ContentItem.GetItemDocumentFullPath(BSLayer.ContentItem.ItemType.MedicalExperience, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID)))) %>" CommandArgument="<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID) %>" CommandName="download"><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.WhoIsItForMembershipProgram.ColumnName.DocName)).Trim().Length > 0 ? DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.DocName).ToString() : Resources.Form_PageContent.DownloadArticle %></asp:LinkButton>
			    //-->

			<div class="pdf_down_box">

			    <asp:Label ID="LabelPDFLink1"><a href="../../Upload/CMS/Doc/MedicalExperience/<%# DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.ID) %>.pdf" Class="pdf_downl_lnk margin_top_10" Target=_new><%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.WhoIsItForMembershipProgram.ColumnName.DocName)).Trim().Length > 0 ? DataBinder.Eval(Container.DataItem, DBLayer.MedicalExperience.ColumnName.DocName).ToString() : Resources.Form_PageContent.DownloadArticle %></a></asp:Label>                                                

			</div>                           
                        <asp:PlaceHolder runat="server" ID="phlDevider" Visible="false">
                        <div class="splitter_green"></div>
                        </asp:PlaceHolder>                        
                    </li>
				</ul>           
            </ItemTemplate>
        </ZFort:TemplateField>
        </Columns>
        <HeaderStyle Wrap="False" CssClass="tdHeader" />
        <RowStyle CssClass="tdRow" />
        <AlternatingRowStyle CssClass="tdAltRow" />
    </ZFort:XGridView>    
	<div class="nav_bot">
	    <ZFort:XNavBarXSLT ID="nbMedicalExperience" runat="server" PageSize="10" ControlToNavigate="gvMedicalExperience" TemplateUrl="../../aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
	</div>        
    </asp:PlaceHolder>                
    
    <asp:PlaceHolder runat="server" ID="phlDetailedView">
        <h3 class="subtitle"><asp:Literal runat="server" ID="ltMETitle"></asp:Literal></h3>
        <div class="font_type_1 article_1 clearfix">
            <ul class="list resources">
                <li class="no_bg">        
                <asp:PlaceHolder runat="server" ID="phlMEImage">
                    <div class="image image_box"><asp:Image runat="server" ID="imgMEImage" /></div>                        
                </asp:PlaceHolder>
                    <asp:Literal runat="server" ID="ltMEDescription"></asp:Literal>                
                </li>
            </ul>                
        </div>
        <div class="bot_splitter">
			<asp:LinkButton ID="lbtnMEDownloadArticle" runat="server" CssClass="pdf_downl_lnk"><asp:Literal runat="server" ID="ltMEDocName"></asp:Literal></asp:LinkButton>    	
		</div>                                                  
        <asp:LinkButton CssClass="btn_back right_back" ID="lbtnMEBack" runat="server" ><b>Back</b></asp:LinkButton>
    </asp:PlaceHolder>
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MemberStories ID="ucMemberStories" runat="server" />
    <div class="splitter_green splitter_m_10 no_bg"></div>
    <uc:CommunityBoard ID="ucCommunityBoard" runat="server" visible="false" />
</asp:Content>