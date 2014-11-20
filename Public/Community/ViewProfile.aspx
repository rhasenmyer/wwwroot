<%@ Page Title="EosHealth - View Profile" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true"
    CodeFile="ViewProfile.aspx.cs" Inherits="Public_Community_ViewProfile" %>

<%@ Register TagPrefix="uc" TagName="UserInfo" Src="~/Controls/User/UserInfo_ViewProfile.ascx" %>
<%@ Register TagPrefix="uc" TagName="Questions" Src="~/Controls/Public/AskAnExpert/UsersOwnData/MyLastQuestions.ascx" %>
<%@ Register TagPrefix="uc" TagName="Boards" Src="~/Controls/Public/Community/UsersOwnThreads.ascx" %>
<%@ Register TagPrefix="uc" TagName="Postings" Src="~/Controls/Public/Community/UsersOwnPosts.ascx" %>
<%@ Register TagPrefix="uc" TagName="Videos" Src="~/Controls/User/UserVideo/UserOwnVideos.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserFriendsOverView" Src="~/Controls/User/UserFriendsOverView.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityMember" Src="~/Controls/Common/JoinAsACommunityMember.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserFriends" Src="~/Controls/User/UserFriends.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
    <!-- CMS Content Block -->
    <div class="user_profile">
        <div class="title"><h4><strong>Community</strong> - <asp:Literal runat="server" ID="ltTitle" /></h4></div>

        <div class="CmsContent">
            <asp:Literal runat="server" ID="ltContent" />
        </div>
    <!-- CMS Content Block -->
        <div class="clearfix">
            <h3 class="category"><asp:Label ID="lblProfileFirstName" runat="server" ></asp:Label> <asp:Label ID="lblProfileLastName" runat="server" ></asp:Label></h3>
            <asp:HyperLink ID="hlnkBackToSearch" CssClass="btn_back f_right" runat="server" ToolTip="Back To Search Results" Visible="false"><b>Back To Search Results</b></asp:HyperLink>
        </div>        
    <uc:UserInfo runat="server" ID="ucUserInfo" />
 
    
    <asp:PlaceHolder ID="phlMainTab" runat="server">
        <table class="tbl_tabs_1">
            <tr>
                <td class="<%=this.IsQuestions ? "state_" : "" %>tab">
                    <asp:LinkButton runat="server" ID="lbtnQuestions" CssClass="block"><span class="l_bg block"><span class="r_bg block"><span class="c_bg block">Questions</span></span></span></asp:LinkButton></td>
                <td>
                    <div class="splitter" />
                </td>
                <td class="<%=this.IsPostings ? "state_" : "" %>tab">
                    <asp:LinkButton runat="server" ID="lbtnPostings" CssClass="block"><span class="l_bg block"><span class="r_bg block"><span class="c_bg block">Postings</span></span></span></asp:LinkButton></td>
                <td>
                    <div class="splitter" />
                </td>
                <td class="<%=this.IsBoards ? "state_" : "" %>tab">
                    <asp:LinkButton runat="server" ID="lbtnBoards" CssClass="block"><span class="l_bg block"><span class="r_bg block"><span class="c_bg block">Boards</span></span></span></asp:LinkButton></td>
                <td>
                    <div class="splitter" />
                </td>
                <td class="<%=this.IsPhoto ? "state_" : "" %>tab">
                    <asp:LinkButton runat="server" ID="lbtnPhotos" CssClass="block"><span class="l_bg block"><span class="r_bg block"><span class="c_bg block">Photos</span></span></span></asp:LinkButton></td>
                <td>
                    <div class="splitter" />
                </td>
                <td class="<%=this.IsVideo ? "state_" : "" %>tab">
                    <asp:LinkButton runat="server" ID="lbtnVideos" CssClass="block"><span class="l_bg block"><span class="r_bg block"><span class="c_bg block">Videos</span></span></span></asp:LinkButton></td>
                <td class="last" />
            </tr>
        </table>
        <div class="mem_account">
        <asp:PlaceHolder runat="server" ID="phlQuestions">
            <div class="tab_container_2 user_questions">
                <uc:Questions runat="server" ID="ucQuestions" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phlPostings">
            <div class="tab_container_2 user_questions">
                <uc:Postings runat="server" ID="ucPostings" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phlBoards">
            <div class="tab_container_2 user_questions">
                <uc:Boards runat="server" ID="ucBoards" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phlPhoto">
            <div class="tab_container_2 user_photos_tab">
            
                <asp:PlaceHolder runat="server" ID="phlPhotoNoRecords">
                    <h1 class="warning">
                         There are no Photos
                    </h1>
                    <asp:PlaceHolder ID="phlRegisterFromPhotos" runat="server" Visible="false">
                    <div class="signupmess">
                        Please
                        <a href="/Public/Register/Registration.aspx?register_type=community">sign up</a>
                        for free to post your photos!
                    </div>
                    </asp:PlaceHolder>
                </asp:PlaceHolder>        
                
                <asp:PlaceHolder runat="server" ID="phlPhotoList">
                    <div id="divPhotoGallery"></div>
                        <script type="text/javascript">
                        // <![CDATA[		    	    	
                        var vi = new SWFObject("/flash/photo_gallery.swf", "vi", "520", "420", "8", "#fff", false);
                        vi.addParam("wmode", "opaque");
                        vi.addParam("FlashVars", "xml=/WS/WS.aspx?action=getuserphotos%26user_id=<%=this.UserID %>%26rand=<%=DateTime.Now.Ticks %>");
                        vi.addParam("allowScriptAccess", "always");
                        vi.write("divPhotoGallery");
                        // ]]>                                                    
                    
                        </script>
                </asp:PlaceHolder>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phlVideo">
            <div class="tab_container_2 user_video_list_tab">
                <uc:Videos runat="server" ID="ucVideos" />
            </div>
        </asp:PlaceHolder>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phlFriendTab" runat="server" Visible="false">
	    <div class="clearfix">
            <h3 class="subtitle">Members friends</h3>
            <div class="f_right"><asp:LinkButton CssClass="btn_back" ID="lbtnBack" runat="server" Text="<b>Back</b>" OnClick="lbtnBack_Click"><b>Back</b></asp:LinkButton></div>
		</div>		
        <uc:UserFriendsOverView ID="ucUserFriendsOverView" runat="server" />        
    </asp:PlaceHolder>
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinAsACommunityMember ID="ucJoinAsACommunityMember" runat="server" />
    <uc:UserFriends runat="server" ID="ucUserFriends" />
</asp:Content>
