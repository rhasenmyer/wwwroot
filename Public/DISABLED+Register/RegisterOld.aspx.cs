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

public partial class Public_RegisterOld : AppLayer.BasePage
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
        ((PublicRight)Master).AllProgramsSelectProgram(new Controls_Public_AllPrograms.SelectProgramEventHandler(ucAllPrograms_SelectProgram));
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
		}
	}
	#endregion

    #region GetControlFromType
    private UserControl GetControlFromType(Type controlType)
    {
        if (typeof(Controls_Public_Registration_Billing).Name == controlType.Name)
        {
            return ucBilling;
        }
        if (typeof(Controls_Public_Registration_EighthStep).Name == controlType.Name)
        {
            return ucEighthStep;
        }
        if (typeof(Controls_Public_Registration_FifthStep).Name == controlType.Name)
        {
            return ucFifthStep;
        }
        if (typeof(Controls_Public_Registration_FourthStep).Name == controlType.Name)
        {
            return ucFourthStep;
        }
        if (typeof(Controls_Public_Registration_Review).Name == controlType.Name)
        {
            return ucReview;
        }
        if (typeof(Controls_Public_Registration_SecondStep).Name == controlType.Name)
        {
            return ucSecondStep;
        }
        if (typeof(Controls_Public_Registration_SeventhStep).Name == controlType.Name)
        {
            return ucSeventhStep;
        }
        if (typeof(Controls_Public_Registration_Shipping).Name == controlType.Name)
        {
            return ucShipping;
        }
        if (typeof(Controls_Public_Registration_SixthStep).Name == controlType.Name)
        {
            return ucSixthStep;
        }
        if (typeof(Controls_Public_Registration_ThirdStep).Name == controlType.Name)
        {
            return ucThirdStep;
        }
        return new UserControl();
    }
    #endregion

    #region ucAllPrograms events
    protected void ucAllPrograms_SelectProgram(object sender,Controls_Public_AllPrograms.SelectProgramEventArgs e)
    {
        List<int> tempList =new List<int>();
        DataTable selectedServices = DBLayer.Service.GetListByPackage(e.ProgramID);
        foreach (DataRow service in selectedServices.Rows)
        {
            tempList.Add(Convert.ToInt32(service[DBLayer.Service.ColumnName.ID]));
        }
        mSelectedServices = tempList;

        ucFourthStep.EmitPackage(e.ProgramID);
        ucFourthStep.BackControl = null;
        ShowControl(ucFourthStep);
    }
    #endregion

    #region ucFirstStep events
    protected void ucFirstStep_Continue(object sender, Controls_Public_Registration_FirstStep.ContinueEventArgs e)
    {
        mFuturedUserDetails = e.FuturedUser;
        ShowControl(ucSecondStep);
    }
    #endregion

    #region ucSecondStep events
    protected void ucSecondStep_Continue(object sender,Controls_Public_Registration_SecondStep.ContinueEventArgs e)
    {
        mSelectedServices = e.SelectedServices;
        if (e.AccessPlanType == BSLayer.AccessPlan.Type.Online)
        {
            double priceSum = 0;
            foreach (int service in e.SelectedServices)
            {
                DBLayer.Service.Details serviceDetails = DBLayer.Service.GetDetails(new ZFort.DB.DataType.DBInt(service));
                priceSum += serviceDetails.Price.Value;
            }
            if (priceSum == 0)
            {
                RegisterUser();
                Response.Redirect("~/Public/Register/ThankYou.aspx");
            }
            else
            {
                if (e.NextControl == null)
                {
                    ucShipping.BackControl = typeof(Controls_Public_Registration_SecondStep);
                    ShowControl(ucShipping);
                }
                else
                {
                    ShowControl(GetControlFromType(e.NextControl));
                    ucReview.Refresh(
                    mOrderDetails,
                    mSelectedServices,
                    mSelectedAdditionalServices,
                    mMobileCompanyPlanID,
                    mMobileCompanyID,
                    mCommunicatorID,
                    mHealthSupplies,
                    mAccessories,
                    mFuturedUserDetails,
                    mPackageID,
                    mShippingSpeed);
                }
            }
        }
        else
        {
            if (e.NextControl == null)
            {
                ucFifthStep.BackControl = typeof(Controls_Public_Registration_SecondStep);
                ShowControl(ucFifthStep);
            }
            else
            {
                ShowControl(GetControlFromType(e.NextControl));
                ucReview.Refresh(
                mOrderDetails,
                mSelectedServices,
                mSelectedAdditionalServices,
                mMobileCompanyPlanID,
                mMobileCompanyID,
                mCommunicatorID,
                mHealthSupplies,
                mAccessories,
                mFuturedUserDetails,
                mPackageID,
                mShippingSpeed);
            }
        }
    }
    protected void ucSecondStep_Back(object sender, EventArgs e)
    {
        ShowControl(ucFirstStep);
    }
    protected void ucSecondStep_ChooseAssistant(object sender, Controls_Public_Registration_SecondStep.ChooseAssistantEventArgs e)
    {
        ucThirdStep.Refresh(e.AccesPlanType);
        ucThirdStep.NextControl = ucSecondStep.NextControl;
        ShowControl(ucThirdStep);
    }
    protected void ucSecondStep_ChooseProgram(object sender, Controls_Public_Registration_SecondStep.ChooseProgramEventArgs e)
    {
        mPackageID = e.PackageID;
        ucFourthStep.EmitPackage(e.PackageID);
        ShowControl(ucFourthStep);
    }
    protected void ucSecondStep_ShowPackages(object sender, Controls_Public_Registration_SecondStep.ShowPackagesEventArgs e)
    {
        ucAllPrograms.Refresh(e.AccessPlanType);
        ucAllPrograms.Visible = true;
    }
    #endregion

    #region ucThirdStep events
    protected void ucThirdStep_Back(object sender, EventArgs e)
    {
        ucSecondStep.NextControl = null;
        ShowControl(ucSecondStep);
    }
    protected void ucThirdStep_SelectAnswer(object sender, Controls_Public_Registration_ThirdStep.SelectAnswerEventArgs e)
    {
        List<int> tempList = new List<int>();
        DataTable selectedServices = DBLayer.Service.GetListByPackage(e.PackageID);
        foreach (DataRow service in selectedServices.Rows)
        {
            tempList.Add(Convert.ToInt32(service[DBLayer.Service.ColumnName.ID]));
        }
        mSelectedServices = tempList;

        ucFourthStep.NextControl = e.NextControl;
        ucFourthStep.BackControl = typeof(Controls_Public_Registration_ThirdStep);
        ucFourthStep.EmitPackage(e.PackageID);
        
        ShowControl(ucFourthStep);
    }
    #endregion

    #region ucFourthStep events
    protected void ucFourthStep_Back(object sender, Controls_Public_Registration_FourthStep.BackEventArgs e)
    {
        if (e.BackControl == null)
        {
            ShowControl(ucSecondStep);
        }
        else
        {
            ShowControl(GetControlFromType(e.BackControl));
            if (e.BackControl == typeof(Controls_Public_Registration_SecondStep))
                ucSecondStep.NextControl = null;
        }
    }
    protected void ucFourthStep_Continue(object sender, Controls_Public_Registration_FourthStep.ContinueEventArgs e)
    {
        mSelectedAdditionalServices = e.SelectedAdditionalServices;

        if (e.NextControl == null)
        {
            ucFifthStep.NextControl = null;
            ucFifthStep.BackControl = null;
            ShowControl(ucFifthStep);
            ucFifthStep.Refresh();
        }
        else
        {
            ShowControl(GetControlFromType(e.NextControl));
            ucReview.Refresh(
            mOrderDetails,
            mSelectedServices,
            mSelectedAdditionalServices,
            mMobileCompanyPlanID,
            mMobileCompanyID,
            mCommunicatorID,
            mHealthSupplies,
            mAccessories,
            mFuturedUserDetails,
            mPackageID,
            mShippingSpeed);
        }
    }
    protected void ucFourthStep_ContinueOnlineOnly(object sender, Controls_Public_Registration_FourthStep.ContinueEventArgs e)
    {
        /*mSelectedServices = e.SelectedAdditionalServices;
        int UserAccountId = RegisterUser();
        
        foreach (int service in mSelectedServices)
        {
            DBLayer.UserAccountToServices.Details USDetails = new DBLayer.UserAccountToServices.Details();
            USDetails.ServiceID = service;
            USDetails.UserAccountID = UserAccountId;
            DBLayer.UserAccountToServices.Add(USDetails);
        }

        Response.Redirect("~/Public/Register/ThankYou.aspx");*/
    }
    #endregion

    #region ucFifthStep events
    protected void ucFifthStep_Back(object sender, Controls_Public_Registration_FifthStep.BackEventArgs e)
    {
        if (e.BackControl == null)
        {
            ShowControl(ucFourthStep);
        }
        else
        {
            ShowControl(GetControlFromType(e.BackControl));
        }
    }
    protected void ucFifthStep_Continue(object sender, Controls_Public_Registration_FifthStep.ContinueEventArgs e)
    {
        mMobileCompanyPlanID = e.MobileCompanyPlanID;
        mMobileCompanyID = e.MobileCompanyID;

        if (e.NextControl == null)
        {
            ucSixthStep.Refresh();
            ShowControl(ucSixthStep);
        }
        else
        {
            ShowControl(GetControlFromType(e.NextControl));
                ucReview.Refresh(
                mOrderDetails,
                mSelectedServices,
                mSelectedAdditionalServices,
                mMobileCompanyPlanID,
                mMobileCompanyID,
                mCommunicatorID,
                mHealthSupplies,
                mAccessories,
                mFuturedUserDetails,
                mPackageID,
                mShippingSpeed);
        }
    }
    #endregion

    #region ucSixthStep events
    protected void ucSixthStep_Back(object sender, EventArgs e)
    {
        ucFifthStep.NextControl = null;
        ShowControl(ucFifthStep);
    }
    protected void ucSixthStep_Continue(object sender, Controls_Public_Registration_SixthStep.ContinueEventArgs e)
    {
        mCommunicatorID = e.CommunicatorID;
        ucSeventhStep.NextControl = e.NextControl;

        ShowControl(ucSeventhStep);
        ucSeventhStep.Refresh(e.CommunicatorID);
    }
    #endregion

    #region ucSeventhStep events
    protected void ucSeventhStep_Back(object sender, EventArgs e)
    {
        ucSixthStep.NextControl = ucSeventhStep.NextControl;
        ShowControl(ucSixthStep);
    }
    protected void ucSeventhStep_Continue(object sender, Controls_Public_Registration_SeventhStep.ContinueEventArgs e)
    {
        mAccessories = e.Accessories;
        if (e.NextControl == null)
        {
            ucEighthStep.NextControl = null;
            ShowControl(ucEighthStep);
            ucEighthStep.Refresh();
        }
        else
        {
            ShowControl(GetControlFromType(e.NextControl));
            ucReview.Refresh(
            mOrderDetails,
            mSelectedServices,
            mSelectedAdditionalServices,
            mMobileCompanyPlanID,
            mMobileCompanyID,
            mCommunicatorID,
            mHealthSupplies,
            mAccessories,
            mFuturedUserDetails,
            mPackageID,
            mShippingSpeed);
        }
    }
    #endregion

    #region ucEighthStep events
    protected void ucEighthStep_Back(object sender, EventArgs e)
    {
        ucSeventhStep.NextControl = null;
        ShowControl(ucSeventhStep);
    }
    protected void ucEighthStep_Continue(object sender, Controls_Public_Registration_EighthStep.ContinueEventArgs e)
    {
        mHealthSupplies = e.HealthSupply;
        if (e.NextControl == null)
        {
            ucShipping.BackControl = typeof(Controls_Public_Registration_EighthStep);
            ucShipping.NextControl = null;
            ShowControl(ucShipping);
            ucShipping.ControlInit(mFuturedUserDetails);
        }
        else
        {
            ShowControl(GetControlFromType(e.NextControl));
            ucReview.Refresh(
            mOrderDetails,
            mSelectedServices,
            mSelectedAdditionalServices,
            mMobileCompanyPlanID,
            mMobileCompanyID,
            mCommunicatorID,
            mHealthSupplies,
            mAccessories,
            mFuturedUserDetails,
            mPackageID,
            mShippingSpeed);
        }
    }
    #endregion

    #region ucShipping events
    protected void ucShipping_Back(object sender, Controls_Public_Registration_Shipping.BackEventArgs e)
    {
        if (e.BackControl != null)
        {
            ShowControl(GetControlFromType(e.BackControl));
        }
        else
        {
            ShowControl(ucEighthStep);
        }
    }
    protected void ucShipping_Continue(object sender, Controls_Public_Registration_Shipping.ContinueEventArgs e)
    {
        DBLayer.Order.Details tempDetails = mOrderDetails;
        tempDetails.ShippingAdressLine1 = e.ShippingDetails.ShippingAdressLine1;
        tempDetails.ShippingCity = e.ShippingDetails.ShippingCity;
        tempDetails.ShippingCountryID = e.ShippingDetails.ShippingCountryID;
        tempDetails.ShippingFirstName = e.ShippingDetails.ShippingFirstName;
        tempDetails.ShippingLastName = e.ShippingDetails.ShippingLastName;
        tempDetails.ShippingPhone = e.ShippingDetails.ShippingPhone;
        tempDetails.ShippingStateID = e.ShippingDetails.ShippingStateID;
        mOrderDetails=tempDetails;

        if (e.NextControl == null)
        {
            ShowControl(ucBilling);
            ucBilling.Refresh(
                mOrderDetails,
                mSelectedServices,
                mMobileCompanyPlanID,
                mCommunicatorID,
                mHealthSupplies,
                mAccessories,
                mFuturedUserDetails,
                mPackageID);
        }
        else
        {
            ShowControl(GetControlFromType(e.NextControl));
            ucReview.Refresh(
            mOrderDetails,
            mSelectedServices,
            mSelectedAdditionalServices,
            mMobileCompanyPlanID,
            mMobileCompanyID,
            mCommunicatorID,
            mHealthSupplies,
            mAccessories,
            mFuturedUserDetails,
            mPackageID,
            mShippingSpeed);
        }
    }
    #endregion

    #region ucBilling events
    protected void ucBilling_Back(object sender, EventArgs e)
    {
        ucShipping.NextControl = null;
        ShowControl(ucShipping);
    }
    protected void ucBilling_Continue(object sender, Controls_Public_Registration_Billing.ContinueEventArgs e)
    {
        mShippingSpeed = e.ShippingSpeed;
        mOrderDetails = e.ShippingDetails;
        ShowControl(ucReview);
        ucReview.Refresh(
            mOrderDetails,
            mSelectedServices,
            mSelectedAdditionalServices,
            mMobileCompanyPlanID,
            mMobileCompanyID,
            mCommunicatorID,
            mHealthSupplies,
            mAccessories,
            mFuturedUserDetails,
            mPackageID,
            mShippingSpeed);
    }
    #endregion

    #region ucReview events
    protected void ucReview_Back(object sender, EventArgs e)
    {
        ShowControl(ucBilling);
    }
    protected void ucReview_CompletePurchase(object sender, EventArgs e)
    {
        int UserAccountId = RegisterUser();
        RegisterOrders(UserAccountId);
        Response.Redirect("~/Public/Register/ThankYou.aspx");
    }
    protected void ucReview_ChangeAdress(object sender, EventArgs e)
    {
        ucShipping.NextControl = typeof(Controls_Public_Registration_Review);
        ShowControl(ucShipping);
    }
    protected void ucReview_ChangeHealthSupply(object sender, EventArgs e)
    {
        ucEighthStep.NextControl = typeof(Controls_Public_Registration_Review);
        ShowControl(ucEighthStep);
    }
    protected void ucReview_ChangeInformation(object sender, EventArgs e)
    {
        ShowControl(ucBilling);
    }
    protected void ucReview_ChangeOrder(object sender, EventArgs e)
    {
        ucSixthStep.NextControl = typeof(Controls_Public_Registration_Review);
        ShowControl(ucSixthStep);
    }
    protected void ucReview_ChangePlan(object sender, EventArgs e)
    {
        ucFifthStep.NextControl = typeof(Controls_Public_Registration_Review);
        ShowControl(ucFifthStep);
    }
    protected void ucReview_ChangeProgram(object sender, EventArgs e)
    {
        ucSecondStep.NextControl = typeof(Controls_Public_Registration_Review);
        ShowControl(ucSecondStep);
    }
    protected void ucReview_ChangeShippingSpeed(object sender, EventArgs e)
    {
        ShowControl(ucBilling);
    }
    #endregion

    #region Save data to DB
    private int RegisterUser()
    {
        int userAccountID=DBLayer.UserAccount.Add(mFuturedUserDetails);

        DBLayer.UserGroup.Details UGDetails = new DBLayer.UserGroup.Details();
        UGDetails.UserAccountID = userAccountID;
        UGDetails.GroupID = DBLayer.Group.GetBaseUserId();
        DBLayer.UserGroup.Add(UGDetails);

        foreach(int selectedService in mSelectedServices)
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
            ucAllPrograms.Refresh();
            ucAllPrograms.Visible = true;
        }
        else
        {
            ucAllPrograms.Visible = false;
        }

        if (target is Controls_Public_Registration_FirstStep)
        {
            ((PublicRight)Master).CurrentStep = 1;
        }
        else if ((target is Controls_Public_Registration_SecondStep) || (target is Controls_Public_Registration_ThirdStep) || (target is Controls_Public_Registration_FourthStep))
        {
            ((PublicRight)Master).CurrentStep = 2;
        }
        else if (target is Controls_Public_Registration_FifthStep)
        {
            ((PublicRight)Master).CurrentStep = 3;
        }
        else if ((target is Controls_Public_Registration_SixthStep) || (target is Controls_Public_Registration_SeventhStep))
        {
            ((PublicRight)Master).CurrentStep = 4;
        }
        else if (target is Controls_Public_Registration_EighthStep)
        {
            ((PublicRight)Master).CurrentStep = 5;
        }
        else if (target is Controls_Public_Registration_Shipping)
        {
            ((PublicRight)Master).CurrentStep = 6;
        }
        else if (target is Controls_Public_Registration_Billing)
        {
            ((PublicRight)Master).CurrentStep = 7;
        }
        else if (target is Controls_Public_Registration_Review)
        {
            ((PublicRight)Master).CurrentStep = 8;
        }

        this.ucFirstStep.Visible = false;
        this.ucSecondStep.Visible = false;
        this.ucThirdStep.Visible = false;
        this.ucFourthStep.Visible = false;
        this.ucFifthStep.Visible = false;
        this.ucSixthStep.Visible = false;
        this.ucSeventhStep.Visible = false;
        this.ucEighthStep.Visible = false;
        this.ucShipping.Visible = false;
        this.ucBilling.Visible = false;
        this.ucReview.Visible = false;
        target.Visible = true;
	}
	#endregion
}
