<%@ Page Title="EosHealth - Member Experiences" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="MemberExperiences.aspx.cs" Inherits="Public_WhoIsItFor_MemberStories" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiencesVideo" Src="~/Controls/Public/MembersExperiencesVideo.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommunityBoard" Src="~/Controls/Public/CommunityBoard.ascx" %>
<%@ Register TagPrefix="uc" TagName="MembersExperiences" Src="~/Controls/Public/MembersExperiences.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

    <asp:PlaceHolder ID="phlVideos" runat="server" visible="false">

	<h4>Member Videos</h4>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="Literal2" />
	</div>

	<!-- CMS Content Block -->
        <asp:PlaceHolder runat="server" ID="phFeatured">
        	<div class="featured">
                <h3 class="category">FEATURED VIDEO</h3>
                <div id="flashContainer" class="video"></div>
                <script type="text/javascript">        
                    // <![CDATA[		
                    var fo = new FlashObject("/flash/video_home.swf", "swf", 274, 224, "9,0,0,0", "#000000");
                        fo.addParam("wmode", "opaque");
                        fo.addParam("FlashVars", "video=<%=FeaturedVideoUrl%>&title=<%=FeaturedVideoTitle%>&description=<%=FeaturedVideoDescription%>&image=<%=FeaturedVideoImage%> ");	                            
                        fo.addParam("allowScriptAccess", "always");
                        fo.write("flashContainer");                            
                   // ]]>          
                </script>      
			</div>                
        </asp:PlaceHolder>                  
        <div id="flashContainer2" class="featured_video"></div>
        <script type="text/javascript">
            // <![CDATA[

            var vi = new SWFObject("/flash/video_component.swf", "vi", "450", "310", "8", "#fff", false);
            vi.addParam("wmode", "opaque");
            vi.addParam("FlashVars", "xml=/WS/WS.aspx?action=get_wisf_member_videos");
            vi.addParam("allowScriptAccess", "always");
            vi.write("flashContainer2");
            //getlsvxml
            /*var vi = new SWFObject("/flash/video_component.swf", "vi", "430", "500", "8", "#fff", false);
                vi.addParam("wmode", "opaque");
                vi.addParam("FlashVars", "path=/WS/WS.aspx?action=get_wisf_member_videos");
                vi.addParam("allowScriptAccess", "always");
                vi.write("flashContainer2"); */                           
           // ]]>
        </script>
 
    </asp:PlaceHolder>


    <!-- CMS Content Block -->
	<div class="member_stories">    
        <h4>Member's Experiences</h4> <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
        <div class="CmsContent">
        	<asp:Literal runat="server" ID="ltContent" />
		</div>
	
	<!-- CMS Content Block -->
		<div class="clearfix">
     <asp:PlaceHolder runat="server" ID="phMain">	
            <ul class="list resources">    
        <asp:Repeater runat="server" ID="rptMemberStories">
            <HeaderTemplate>
            </HeaderTemplate>            
            <ItemTemplate>
            	<li class="with_splitter">
                	<div class="image image_box thumb_90x100"><span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2"><img src="<%# BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MemberStory, AppLayer.Functions.ToInt(DataBinder.Eval(Container.DataItem, DBLayer.MemberStory.ColumnName.ID))) %>" border="0" alt="Members Experience" /></span><span class="swc3">&nbsp;</span></span></span></div>
                    <div class="text">
	                    <h6 id='<%# DataBinder.Eval(Container.DataItem, DBLayer.MemberStory.ColumnName.ID) %>'><%# DataBinder.Eval(Container.DataItem, DBLayer.MemberStory.ColumnName.Title) %></h6>
                        <%# DataBinder.Eval(Container.DataItem, DBLayer.MemberStory.ColumnName.Description) %>									
                    </div>                                                                        

                	<asp:PlaceHolder ID="phSplitter" runat="server" Visible='<%#!IsLastItem(Convert.ToInt32(DataBinder.Eval(Container.DataItem, DBLayer.MemberStory.ColumnName.ID)))%>'>
                 </asp:PlaceHolder>
            </ItemTemplate>  
            <SeparatorTemplate>
                <li class="splitter_green splitter_m_10"></li>
            </SeparatorTemplate>
            <FooterTemplate>
             </FooterTemplate>
            </asp:Repeater>                    
            </ul>            
    </asp:PlaceHolder>  
		</div>
			<asp:LinkButton runat="server" ID="lbtnDownloadPDF" Visible="false" CssClass="pdf_downl_lnk"><%=Resources.Form_PageContent.DownloadArticle%></asp:LinkButton>  			
			<div class="splitter_green splitter_m_10"></div>
    	<div class="f_right">
            <asp:HyperLink ID="hlnkShowAll" runat="server" CssClass="btn_arrow" NavigateUrl="/Public/Community/MemberExperiences.aspx?showAll=true">Show All</asp:HyperLink>
		</div>            
	</div>    
</asp:Content>

<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:MembersExperiences ID="ucMembersExperiences" runat="server"/>
    <uc:CommunityBoard ID="ucCommunityBoard" runat="server"/>
</asp:Content>