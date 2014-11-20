<%@ Page Title="EosHealth - FAQ" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQ" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>
<%@ Register TagPrefix="uc" TagName="JoinBanner3" Src="~/Controls/Public/JoinBanners/JoinBanner3.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">   
<script type="text/javascript">
<!-- 

function ShowAnswer(ansBlock)
{
    document.getElementById(ansBlock).className = document.getElementById(ansBlock).className == "visible" ? "hidden" : "visible";
}

-->
</script>
       
    <h4><strong><asp:Literal runat="server" ID="ltTitle" /></strong> - <asp:Literal runat="server" ID="ltCatName"></asp:Literal></h4>

    <div class="CmsContent">
        <asp:Literal runat="server" ID="ltContent" />
    </div>   
    <div class="faq">
        <div class="form">
            <form action="#">
                <fieldset>
                    <label for="tbFilter"><%=Resources.Form_FAQ.QSearch %>:</label>
                    <span class="bg"><asp:TextBox runat="server" ID="tbFilter" onmousedown="if(this.value == 'Enter question') {this.value='';} this.focus();" onblur="if(this.value == '') {this.value='Enter question';}" value="Enter question"  /></span>
                    
                    <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass="submit" Text="<%=Resources.Strings.StartSearch %>">submit</asp:LinkButton>
                </fieldset>
            </form>
        </div>        
       
<asp:Repeater runat="server" ID="rptFAQ">
    <ItemTemplate>
        <asp:PlaceHolder runat="server" ID="phlCatName">
            <h3 class="category"><%# DataBinder.Eval(Container.DataItem, DBLayer.FAQCategory.ColumnName.Name) %></h3>
        </asp:PlaceHolder>                    
            <div class="question">
                <a href="javascript:void(0);" onclick="ShowAnswer('trAnswer<%# DataBinder.Eval(Container.DataItem, DBLayer.FAQ.ColumnName.ID) %>'); if(this.style.textDecoration!='none'){this.style.textDecoration='none'} else {this.style.textDecoration=''}">
                    <%# DataBinder.Eval(Container.DataItem, DBLayer.FAQ.ColumnName.Question) %>
                </a>
            </div>
            <div id="trAnswer<%# DataBinder.Eval(Container.DataItem, DBLayer.FAQ.ColumnName.ID) %>" class="hidden">
                <div class="answer">
                    <div class="faq-answer"><%# DataBinder.Eval(Container.DataItem, DBLayer.FAQ.ColumnName.Answer) %></div>
                </div>
           </div>
    </ItemTemplate>	
</asp:Repeater>
        
<asp:PlaceHolder runat="server" ID="phlNoRecords">
        <h1 class="warning">
            <%=Resources.ErrorMessages.NoRecords %>
        </h1>            
</asp:PlaceHolder>		
	</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:JoinBanner3 ID="ucJoinBanner3" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" />
</asp:Content>