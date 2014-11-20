<%@ Page Title="EosHealth - News" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight_NoDropDown.master" AutoEventWireup="true" CodeFile="PressRelease.aspx.cs" Inherits="Public_PressRelease" %>
<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<%@ Register TagPrefix="uc" TagName="Twitter" Src="~/Controls/Public/Twitter.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinAsACommunityPanel" Src="~/Controls/Public/Community/JoinAsACommunityPanel.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="press_release">
    <!-- CMS Content Block -->
	    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
        <h4><asp:Literal runat="server" ID="ltTitle" visible="false" /></h4>
        <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	    
	
	<!-- CMS Content Block -->
	
	<asp:PlaceHolder runat="server" ID="phlList">
    <ZFort:XGridView ID="gvPress" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" AllowSorting="false" GridLines="None" Width="100%" RowStyle-CssClass="gridcontent" EmptyDataRowStyle-CssClass="norecords_mess" CssClass="press-table" HeaderStyle-CssClass="hidden">
    <Columns>
    <ZFort:TemplateField HeaderStyle-CssClass="press_td_head_1" ItemStyle-CssClass="press_td_1">	    
        <ItemTemplate>
            
            <asp:PlaceHolder runat="server" ID="phlFeaturedTitle">
                <h2 class="subtitle bot_marg_10">Featured Article</h2>
            </asp:PlaceHolder>
            <div class="title">
                <div class="ico_pdf"><asp:Literal runat="server" ID="ltPDFDoc"></asp:Literal></div>
                <h3 class="subtitle"><asp:LinkButton runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.ID) %>' CommandName="view" ID="LinkButton1" CssClass="link_type_1"><%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.Title) %></asp:LinkButton></h3>
			</div>                
            <div class="font_type_1 article_1 clearfix"><%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.BriefDescription) %><asp:LinkButton runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.ID) %>' CommandName="view" ID="lbtnReadMore" CssClass="read_more_1">Read More ></asp:LinkButton>
            </div>
			<div class="authors clearfix">
            	<div class="posted_label b">Posted:</div>
				<div class="posted"><%# AppLayer.Functions.ToDateTime(DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.DateCreated)).ToShortDateString() %></div>
                <div class="by_label b">By:</div>
				<div class="by"><%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.UserProduced) %></div>
				<div class="publications_label b">Publications:</div>
				<div class="publications"><%# DataBinder.Eval(Container.DataItem, DBLayer.PressRelease.ColumnName.Publications) %></div>	                    
               
            </div>
            <asp:PlaceHolder runat="server" ID="phlFeatured">
                <div class="line_featured"></div>
            </asp:PlaceHolder>   
            <asp:PlaceHolder runat="server" ID="phlRegular">
                <div class="line_regular"></div>
            </asp:PlaceHolder>                                   
            
        </ItemTemplate>
    </ZFort:TemplateField>
    </Columns>
    </ZFort:XGridView>        
    
    <div class="navbar">   
        <ZFort:XNavBarXSLT ID="nbPress" runat="server" PageSize="12" ControlToNavigate="gvPress" TemplateUrl="/aspnet_client/ZFort/ZFortWebUIWebControls/XNavBar/XNavBarTemplate.xml" Method="Post" />
    </div>
    
    </asp:PlaceHolder>        

    <asp:PlaceHolder runat="server" ID="phlPressView">
        <h3 class="subtitle"><asp:Literal runat="server" ID="ltPressTitle"></asp:Literal></h3>
        
		<div class="font_type_1 article_1">
	        <asp:Literal runat="server" ID="ltPressDescription"></asp:Literal>
		</div>     
        <asp:LinkButton runat="server" ID="lbtnDownloadArticle" CssClass="pdf_downl_lnk">Click here to download the article</asp:LinkButton>
        <div class="authors">
            <div class="posted_label b">Posted:</div>
            <div class="posted"><asp:Literal runat="server" ID="ltPressDate"></asp:Literal></div>
            <div class="by_label b">By:</div>
            <div class="by"><asp:Literal runat="server" ID="ltUserName"></asp:Literal></div>
            <div class="publications_label b">Publications:</div>
            <div class="publications"><asp:Literal runat="server" ID="ltPublications"></asp:Literal></div> 	                    
        </div>

        <asp:LinkButton runat="server" CssClass="btn_back" ID="lbtnPressBack" ToolTip="Back"><b><%=Resources.Strings.Back %></b></asp:LinkButton>
    </asp:PlaceHolder> 
        

    
    <div class="f_right">
        <asp:LinkButton CssClass="btn_read_more" runat="server" ID="lbtnMore"><b>More Articles</b></asp:LinkButton>      
    </div>
</div>    
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:Twitter ID="ucTwitter" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false"/>
    <uc:JoinAsACommunityPanel ID="ucJoinAsACommunityPanel" runat="server" Visible="false" />    
</asp:Content>
