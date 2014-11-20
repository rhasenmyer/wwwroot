using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class Public_Register : AppLayer.BasePage
{

    #region Private members
    private int mPackageID
    {
        get
        {
            return (int?)ViewState["mPackageID"] ?? 0;
        }
        set
        {
            ViewState["mPackageID"] = value;
        }
    }
    private DBLayer.UserAccount.Details mFuturedUserDetails
    {
        get
        {
            return (DBLayer.UserAccount.Details)ViewState["mFuturedUserDetails"] ?? new DBLayer.UserAccount.Details();
        }
        set
        {
            ViewState["mFuturedUserDetails"] = value;
        }
    }
    private List<int> mSelectedServices
    {
        get
        {
            return (List<int>)ViewState["mSelectedServices"] ?? new List<int>();
        }
        set
        {
            ViewState["mSelectedServices"] = value;
        }
    }
    private List<int> mSelectedAdditionalServices
    {
        get
        {
            return (List<int>)ViewState["mSelectedAdditionalServices"] ?? new List<int>();
        }
        set
        {
            ViewState["mSelectedAdditionalServices"] = value;
        }
    }
    private int mMobileCompanyPlanID
    {
        get
        {
            return (int?)ViewState["mMobileCompanyPlanID"] ?? 0;
        }
        set
        {
            ViewState["mMobileCompanyPlanID"] = value;
        }
    }
    private int mCommunicatorID
    {
        get
        {
            return (int?)ViewState["mCommunicatorID"] ?? 0;
        }
        set
        {
            ViewState["mCommunicatorID"] = value;
        }
    }
    private List<int> mAccessories
    {
        get
        {
            return (List<int>)ViewState["mAccessories"] ?? new List<int>();
        }
        set
        {
            ViewState["mAccessories"] = value;
        }
    }
    private List<int> mHealthSupplies
    {
        get
        {
            return (List<int>)ViewState["mHealthSupplies"] ?? new List<int>();
        }
        set
        {
            ViewState["mHealthSupplies"] = value;
        }
    }
    private DBLayer.Order.Details mOrderDetails
    {
        get
        {
            return (DBLayer.Order.Details)ViewState["mOrderDetails"] ?? new DBLayer.Order.Details();
        }
        set
        {
            ViewState["mOrderDetails"] = value;
        }
    }
    private BSLayer.Order.ShippingSpeedList mShippingSpeed
    {
        get
        {
            return (BSLayer.Order.ShippingSpeedList?)ViewState["mShippingSpeed"] ?? BSLayer.Order.ShippingSpeedList.NoDefined;
        }
        set
        {
            ViewState["mShippingSpeed"] = value;
        }
    }
    private int mMobileCompanyID
    {
        get
        {
            return (int?)ViewState["mMobileCompanyID"] ?? 0;
        }
        set
        {
            ViewState["mMobileCompanyID"] = value;
        }
    }
    #endregion

    #region Initialize
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);        
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        DBLayer.PageContent.Details CMS = BSLayer.PageContent.GetPageContent(
            DBLayer.Page.GetPageIDByCode(Request.Url.AbsolutePath),
            BSLayer.Language.GetMembersLanguageId());

        base.MetaDataDetails = new BSLayer.MetaData();

        if (CMS != null)
        {
            this.ltTitle.Text = CMS.PageTitle.Value;
            this.ltContent.Text = CMS.Content.Value;

            this.MetaDataDetails = new BSLayer.MetaData();
            this.MetaDataDetails.Description = CMS.MetaDescription.Value;
            this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
            this.MetaDataDetails.Title = CMS.PageTitle.Value;
        }

        if (!Page.IsPostBack)
        {
            ShowControl(ucFirstStep);

            phlFromProduct.Visible = false;
            if (Request["from"] == "product")
            {
                phlFromProduct.Visible = true;
                phlUpgradeRegister.Visible = false;
                ((PublicRight)Master).AddTextToContentID();
            }
            else if (Request["register"] == "upgrade")
            {
                phlFromProduct.Visible = false;
                phlUpgradeRegister.Visible=true;
                ((PublicRight)Master).AddTextToContentID();
            }
        }
    }
    #endregion        

    #region ucFirstStep events
    protected void ucFirstStep_Continue(object sender, Controls_Public_Registration_FirstStep.ContinueEventArgs e)
    {
        mFuturedUserDetails = e.FuturedUser;
        if (true)
        {
            int UserAccountId = RegisterUser();
            Response.Redirect("~/Public/Register/ThankYou.aspx?thtype=online");
        } ////// Simple Registration
    }
    #endregion

    #region Save Data
    private int RegisterUser()
    {
        int userAccountID = DBLayer.UserAccount.Add(mFuturedUserDetails);

        DBLayer.UserGroup.Details UGDetails = new DBLayer.UserGroup.Details();
        UGDetails.UserAccountID = userAccountID;
        UGDetails.GroupID = DBLayer.Group.GetBaseUserId();
        DBLayer.UserGroup.Add(UGDetails);

        foreach (int selectedService in mSelectedServices)
        {
            DBLayer.UserAccountToServices.Details selectedServicesDetails = new DBLayer.UserAccountToServices.Details();
            selectedServicesDetails.UserAccountID = userAccountID;
            selectedServicesDetails.ServiceID = selectedService;
        }

        return userAccountID;
    }
    private void RegisterOrders(int UserAccountId)
    {
        mOrderDetails.ShippingSpeed = Convert.ToInt32(mShippingSpeed);
        mOrderDetails.UserAccountID = UserAccountId;
        mOrderDetails.OrderDate = DateTime.Now;
        int orderID = DBLayer.Order.Add(mOrderDetails);

        #region Add services

        foreach (int service in mSelectedServices)
        {
            DBLayer.UserAccountToServices.Details USDetails = new DBLayer.UserAccountToServices.Details();
            USDetails.ServiceID = service;
            USDetails.UserAccountID = UserAccountId;
            DBLayer.UserAccountToServices.Add(USDetails);
        }

        BSLayer.WWApp.RegisterUser(UserAccountId);
        BSLayer.WWApp.SetUserFeatures(UserAccountId);

        if ((mPackageID == 0))
        {
            foreach (int service in mSelectedServices)
            {
                DBLayer.Service.Details serviceDetails = DBLayer.Service.GetDetails(new ZFort.DB.DataType.DBInt(service));
                AddOrderItem(BSLayer.OrderItem.OrderTypeList.Service,
                    serviceDetails.Name.Value,
                    serviceDetails.Description.Value,
                    serviceDetails.Price.Value,
                    serviceDetails.ID.Value,
                    orderID);
            }
        }
        else
        {
            DBLayer.Package.Details packageDetails = DBLayer.Package.GetDetails(new ZFort.DB.DataType.DBInt(mPackageID));
            AddOrderItem(BSLayer.OrderItem.OrderTypeList.Package,
                packageDetails.Name.Value,
                packageDetails.DescriptionMP.Value,
                packageDetails.Price.Value,
                packageDetails.ID.Value,
                orderID);
        }
        #endregion

        #region Add mobile plan
        DBLayer.MobileCompanyPlan.Details mobileCompanyPlanDetails = DBLayer.MobileCompanyPlan.GetDetails(new ZFort.DB.DataType.DBInt(mMobileCompanyPlanID));
        AddOrderItem(BSLayer.OrderItem.OrderTypeList.CommunicatorPlan,
                mobileCompanyPlanDetails.Name.Value,
                string.Empty,
                mobileCompanyPlanDetails.PricePerPeriod.Value,
                mobileCompanyPlanDetails.ID.Value,
                orderID);
        #endregion

        #region Add communicator
        DBLayer.Communicator.Details communicatorDetails = DBLayer.Communicator.GetDetails(new ZFort.DB.DataType.DBInt(mCommunicatorID));
        AddOrderItem(BSLayer.OrderItem.OrderTypeList.Communicator,
                communicatorDetails.Name.Value,
                communicatorDetails.Description.Value,
                communicatorDetails.Price.Value,
                mCommunicatorID,
                orderID);
        #endregion

        #region Add communicator accesories
        foreach (int accesory in mAccessories)
        {
            DBLayer.Accessory.Details accesoryDetails = DBLayer.Accessory.GetDetails(new ZFort.DB.DataType.DBInt(accesory));
            AddOrderItem(BSLayer.OrderItem.OrderTypeList.Service,
                accesoryDetails.Name.Value,
                accesoryDetails.Description.Value,
                accesoryDetails.Price.Value,
                accesoryDetails.ID.Value,
                orderID);
        }
        #endregion

        #region Add Health Sypplies
        foreach (int healthSypply in mHealthSupplies)
        {
            DBLayer.HealthSupply.Details healthSuppliesDetails = DBLayer.HealthSupply.GetDetails(new ZFort.DB.DataType.DBInt(healthSypply));
            AddOrderItem(BSLayer.OrderItem.OrderTypeList.HealthSypply,
                healthSuppliesDetails.Name.Value,
                healthSuppliesDetails.Description.Value,
                healthSuppliesDetails.Price.Value,
                healthSuppliesDetails.ID.Value,
                orderID);
        }
        #endregion
    }
    private void AddOrderItem(
        BSLayer.OrderItem.OrderTypeList type,
        string name,
        string description,
        double price,
        int itemID,
        int orderID)
    {
        DBLayer.OrderItem.Details orderDetails = new DBLayer.OrderItem.Details();
        orderDetails.ItemType = Convert.ToInt32(type);
        orderDetails.Name = name;
        orderDetails.Description = description;
        orderDetails.OrderID = orderID;
        orderDetails.Price = price;
        orderDetails.Quantity = 1;
        orderDetails.ItemID = itemID;
        DBLayer.OrderItem.Add(orderDetails);
    }
    #endregion

    #region ShowControl
    private void ShowControl(UserControl target)
    {
        if (!(target is Controls_Public_Registration_FirstStep))
        {
            ucAllPrograms.Visible = true;
            ucAllPrograms.Refresh();
        }
        else
        {
            ucAllPrograms.Visible = false;
        }      
        
        this.ucFirstStep.Visible = false;
        target.Visible = true;
    }
    #endregion
}
