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

using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

using System.Xml;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mail;
using System.Text;
using System.IO;

public partial class User_Orders_Sterling : AppLayer.BasePage
{
	static string strMemberWelcomeKitOrder;
	static string strMemberRefillOrder;
	static string strRefillOrderCompare;

	
	protected void WizardGroupOrders_Init(object sender, EventArgs e)
	{

		//=========================================================================================================================
        	// BEGIN if a promo code has been sent to the URL, get the promo code and prepopulate the field during INIT event of wizard
        	if (WizardGroupOrders.ActiveStepIndex == 0)
        	{
			if (!AppLayer.USession.IsMemberLogged)
        		{
            			Session["LastRequest"] = HttpContext.Current.Request.Url.ToString();
                		Response.Redirect("/Public/Login.aspx", true);
        		}

			Populate_DropDownState();

            		txtFirstName.Text = Convert.ToString(AppLayer.USession.MemberDetails.FirstName.Value);
			
			txtLastName.Text = Convert.ToString(AppLayer.USession.MemberDetails.LastName.Value);
			
			txtCellPhone.Text = Convert.ToString(AppLayer.USession.MemberDetails.Phone.Value);
			
			txtEmail.Text = Convert.ToString(AppLayer.USession.MemberDetails.Email.Value);

			fillOrderForm();
			
			LabelStepNumber.Text = "<img src='" + AppConfig.Site.DomainUrl + "/images/sterling_order_steps_bar_001.jpg' border='0' width='700' height='60' />";
        	}
        	// END
        	//=========================================================================================================================

		//hide side steps bar -- comment out the line below if needed for testing
		WizardGroupOrders.SideBarStyle.CssClass = "hidden";

	}

    
	protected void Page_Load(object sender, EventArgs e)
	{
        	Response.Redirect("https://www.eoshealth.com/intouchliv/Orders_Refills_Frame.aspx");

        	if (!AppLayer.USession.IsMemberLogged)
        	{
            		Session["LastRequest"] = HttpContext.Current.Request.Url.ToString();
                	Response.Redirect("/Public/Login.aspx", true);
        	}

		base.MetaDataDetails = new BSLayer.MetaData();

		strMemberWelcomeKitOrder = Convert.ToString(getDBValue("SELECT OrderOnlineSupplies.OrderType FROM EOS..OrderOnlineSupplies WHERE OrderOnlineSupplies.PID = '"  + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value)  + "'"));

		strRefillOrderCompare = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "RF" + DateTime.Now.ToString("ddMMyyyy");
	
		strMemberRefillOrder =  Convert.ToString(getDBValue("SELECT TOP 1 OrderOnlineSupplies.PONumber FROM EOS..OrderOnlineSupplies WHERE OrderOnlineSupplies.PONumber = '"  + strRefillOrderCompare  + "' ORDER BY DateSubmitted DESC"));

		String strMemberPCCode = Convert.ToString(getDBValue("SELECT UserAccount.PCCOde FROM EOS..UserAccount WHERE UserAccount.Email = '"  + Convert.ToString(AppLayer.USession.MemberDetails.Email.Value)  + "'"));

		if ((strMemberPCCode != null) && (strMemberPCCode != ""))
        	{

			if ((strMemberPCCode == "KLEINISD") || (strMemberPCCode == "HARRISCOUNTY"))
			{
				LabelStepNumber.Text = "<div style='text-align: left;'>Currently, the " + strMemberPCCode + " group is not eligible for free supplies or rewards through the EosHealth program.  Please contact your HR department to find out your options.  If you wish to purchase supplies through EosHealth out-of-pocket or through your health fund...or if you have any other questions, email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a>, include your name and phone number.</div>";

				WizardGroupOrders.Visible = false;
			}
			else
			{
				this.MetaDataDetails.Title = "Supply Orders for Group: " + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value);

				String strUserPCCode = "As part of the " + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + " group, eligible members...with ACTIVE* EosHealth accounts...can receive free 3-month refills of supplies every 90 days (member is responsible for replacing meter batteries).<br/><br/>To receive the order, you MUST accurately fill out and submit this form.  Incorrect information may delay shipment of your supplies.<br/><br/><br/>*Make sure to submit your readings regularly so that your account remains active!";
				LabelTestValue.Text = strUserPCCode;
			}
		}
        
	}

	protected void WizardGroupOrders_ActiveStepChanged(object sender, EventArgs e)
	{
		if (WizardGroupOrders.ActiveStep.Title == "Submitted")
		{
			string strCountEosOnlineOrderDate = getDBValue("SELECT COUNT(DateSubmitted) AS CountDateSubmitted FROM OrderOnlineSupplies WHERE PID = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");

			if (strCountEosOnlineOrderDate != "0")
			{
				string strLastOrderDate = getDBValue("SELECT TOP 1 CONVERT(VARCHAR(12),DateSubmitted, 107) AS FormattedDateSubmitted FROM OrderOnlineSupplies WHERE PID = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "' ORDER BY DateSubmitted DESC");
			
				DateTime dtLastOrderDate = Convert.ToDateTime(strLastOrderDate);

				DateTime dtNextOrderDate = dtLastOrderDate.AddDays(70);				

				System.TimeSpan diffResult = (DateTime.Now).Subtract(dtLastOrderDate);

				int intDaysSinceLastOrder = Convert.ToInt32(diffResult.Days);

				if (intDaysSinceLastOrder < 70)
				{
					LabelStepNumber.Text = "<b>You have already submitted a supply refill order on " + strLastOrderDate + ", you are allowed to submit a refill order once every 90 days (which you can order 20 days before that time, on " + dtNextOrderDate.ToString("MMMM dd yyyy") + ").  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you did not receive your order or if you have questions.</b>";
					LabelStepNumber.ForeColor = System.Drawing.Color.Red;

					return;
				}
				else
				{
		

					if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
					{

						if (strMemberRefillOrder == strRefillOrderCompare)
						{
							LabelStepNumber.Text = "<b>You have already submitted a supply refill order on " + strLastOrderDate + ", you are allowed a refill order once every 90 days (which you can order 20 days before that time, on " + dtNextOrderDate.ToString("MMMM dd yyyy") + ").  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you did not receive your order or if you have questions.</b>";
							LabelStepNumber.ForeColor = System.Drawing.Color.Red;

							return;
						}
						else
						{
							processOrder();
						}
					}
					else
					{
			
						if (strMemberWelcomeKitOrder == "WELCOME KIT")
						{
							LabelStepNumber.Text = "<b>You have already ordered a Welcome Kit.  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you have questions.</b>";
							LabelStepNumber.ForeColor = System.Drawing.Color.Red;

							return;
						}
						else 
						{
							processOrder();
						}
					}
				}
			}
			else
			{
				processOrder();
			}
		}


	}

	
	public void processOrder()
	{
		//get the supplier ID from the promo code table
		String strSupplierID = Convert.ToString(getDBValue("SELECT PromoCode.SupplierID FROM EOS..PromoCode WHERE PromoCode.Code = '"  + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value)  + "'"));

		//based on the supplier ID, send or do not send email to supplier
		if ((strSupplierID == "1") || (strSupplierID == "2"))
		{
				String strSupplierEmail = Convert.ToString(getDBValue("SELECT PromoCode.SupplierEmail FROM EOS..PromoCode WHERE PromoCode.Code = '"  + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value)  + "'"));
			
				string strPONumber = "";

				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					strPONumber = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "RF" + DateTime.Now.ToString("ddMMyyyy");
				}
				else
				{
					strPONumber = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "WK" + DateTime.Now.ToString("ddMMyyyy");
					//strPONumber = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "WK";
				}


				//pre-fill insurance group number field if data is available
				if ((strSupplierEmail != null) && (strSupplierEmail != "0"))
				{
					strSupplierEmail = strSupplierEmail;
				}
				else
				{
					strSupplierEmail = "Admin@EosHealth.com";
				}

				// send an email to the supplier
				// Sterling: sms-services@sterlingmedical.com
				// Global: steveh@gmphs.com, davidh@gmphs.com
				String strEmailTo = strSupplierEmail;

				//send email
				createAndSendSterlingEmail(strEmailTo, strPONumber);
				createSterlingOutboundMessage(strEmailTo, strPONumber);

				//update User Account Record
				updateUserAccountWithOrderInfo();

				//update activation data
				updateUserActivationRecord(Convert.ToString(AppLayer.USession.MemberDetails.Email.Value));

				//save record of the order
				insertOrderRecord(strEmailTo, strPONumber);

				//send order to supply fulfillment queue
				insertOrderQueueRecord(strPONumber);

                        	if (Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) != "USF")
				{
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<h4 style='margin:0 0 0 0; text-align: left;'>Thank You, Your Order Has Been Submitted to the Supplier.</h4><br/>";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If your information is confirmed, your supplies will arrive shortly. Depending on your group, supplies can come from either Sterling McKesson OR from EosHealth internal supplies.  ";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If you do not receive your supplies, you may have submitted your order prematurely (before your current 3-month supply period has expired), or you may have submitted an incorrect insurance group Member ID or other incorrect information, in these cases, please check your information and try to submit again.<br/><br/>Please contact your HR department if you need assistance with your health insurance information.<br/><br/>You can also go to your group's landing page (<a href='https://www.eoshealth.com/partners/" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "' target=_top>www.eoshealth.com/partners/" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "</a>) to access any documents related to your enrollment.<br /><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<b><a href='https://www.eoshealth.com/User/SubmitByText.aspx' style='text-decoration: none;'>Remember to log and submit your readings regularly so that your account remains active (only active accounts can participate in the EosHealth program)!</a></b><br/><br/>";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<br/><h4 style='margin:0 0 0 0; text-align: left;'>Keep Your Health Profile Updated</h4><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If you have not done so, take a moment to set up you personal Health Profile.  If you have set up your Health Profile, keep it up-to-date...by doing so, your personal health analysis will be more accurate so that you can better chart your progress and track your health trends.<br /><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<table align='center'><tr><td><a href='../User/Orders_PersonalHealthAccount.aspx' class='btn'><b>GO TO MY HEALTH PROFILE</b></a></td></tr></table>";
				}
				
				if (Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) == "USF")
				{
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<h4 style='margin:0 0 0 0; text-align: left;'>Thank You, Your Order Has Been Submitted to the Supplier.</h4><br/>";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If your information is confirmed, your supplies will arrive shortly. Depending on your group, your supplies can come from either Sterling McKesson OR from EosHealth internal supplies.  ";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If you do not receive your supplies, you may have submitted your order prematurely (before your current 3-month supply period has expired), or you may have submitted an incorrect insurance group Member ID or other incorrect information, in these cases, please check your information and try to submit again.<br/><br/>Please contact your HR department if you need assistance with your health insurance information.<br/><br/>You can also go to your group's landing page (<a href='https://www.eoshealth.com/USFHEALTH/' target=_top>www.eoshealth.com/USFHEALTH/</a>) to access any documents related to your enrollment.<br /><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<b><a href='https://www.eoshealth.com/User/SubmitByText.aspx' style='text-decoration: none;'>Remember to log and submit your readings regularly so that your account remains active (only active accounts can participate in the EosHealth program)!</a></b><br/><br/>";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<br/><h4 style='margin:0 0 0 0; text-align: left;'>Keep Your Health Profile Updated</h4><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "If you have not done so, take a moment to set up you personal Health Profile.  If you have set up your Health Profile, keep it up-to-date...by doing so, your personal health analysis will be more accurate so that you can better chart your progress and track your health trends.<br /><br />";
					LabelConfirmationMessage.Text = LabelConfirmationMessage.Text + "<table align='center'><tr><td><a href='../User/Orders_PersonalHealthAccount.aspx' class='btn'><b>GO TO MY HEALTH PROFILE</b></a></td></tr></table>";
				
				}
				
			}

			LabelStepNumber.Text = "<img src='" + AppConfig.Site.DomainUrl + "/images/sterling_order_steps_bar_002.jpg' border='0' width='700' height='60' />";

			WizardGroupOrders.FinishCompleteButtonStyle.CssClass = "hidden";

			WizardGroupOrders.FinishPreviousButtonStyle.CssClass = "hidden";
	}

	public void createAndSendSterlingEmail(string strEmailTo, string strPONumber)
	{
				//use the email address or the userid to retrieve the supply type for the PromoCode (group)
				string strSupplyType = getDBValue("SELECT SupplyType FROM EOS..PromoCode WHERE Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'");
		

				MailMessage msg = new MailMessage();
  
    				msg.To = strEmailTo;
			
				//msg.Cc = "GroupOrders@EosHealth.com";

				msg.Bcc = "rhasenmyer@eoshealth.com, CustomerCare@Livongo.com";

    				msg.From = "CustomerCare@Livongo.com"; 


				//Begin Message Body
    				msg.Body += Environment.NewLine;
				

				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					msg.Subject = "Refill Order from EosHealth: " + txtFirstName.Text + " " + txtLastName.Text;
					msg.Body += "Order Type: Reorder/Refill";
					msg.Body += Environment.NewLine;
					msg.Body += Environment.NewLine;
					msg.Body += "--member should NEVER be billed or invoiced, please email snightingale@eoshealth.com with invoice/billing issues--";
					msg.Body += Environment.NewLine;
					msg.Body += "--member should ONLY receive EOSHEALTH program approved supplies--";
					msg.Body += Environment.NewLine;
					msg.Body += Environment.NewLine;
				}
				else
				{
					if (strMemberWelcomeKitOrder == "WELCOME KIT")
					{
						LabelStepNumber.Text = "<b>You have already ordered a Welcome Kit.  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you have questions.</b>";
						LabelStepNumber.ForeColor = System.Drawing.Color.Red;

						return;
 
					}
					else
					{
						msg.Subject = "Welcome Kit Order from EosHealth: " + txtFirstName.Text + " " + txtLastName.Text;
						msg.Body += "Order Type: Welcome Kit Order";
						msg.Body += Environment.NewLine;
						msg.Body += "--ENTER THIS PERSON INTO AUTO REFILL PROGRAM--";
						msg.Body += Environment.NewLine;
						msg.Body += "--member should NEVER be billed or invoiced, please email snightingale@eoshealth.com with invoice/billing issues--";
						msg.Body += Environment.NewLine;
						msg.Body += "--member should ONLY receive EOSHEALTH program approved supplies--";
						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						
					}
				}

				msg.Body += "P.O. No.: " + strPONumber;
				msg.Body += Environment.NewLine;
				msg.Body += "Insurer's Name: " + DropDownInsurer.Text.Trim();
				msg.Body += Environment.NewLine;
				

				//update the Member ID in the database
				if ((txtMemberID.Text.Trim() == "") || (txtMemberID.Text == null) || (txtMemberID.Text == "") || (txtMemberID.Text == null) || (txtMemberID.Text == "") || (txtMemberID.Text == null))
				{
					msg.Body += "Member ID.: I do not know my Member ID, please call me to help.";
				}
				else
				{

					msg.Body += "Member ID.: " + txtMemberID.Text;

		
				}

				string strStateName = Convert.ToString(getDBValue("SELECT State.Name FROM EOS..State WHERE State.ID = " + Convert.ToInt16(DropDownState.Text)));

			
				//calculate strip and lancet supply quantities
					
				string strStripQty = "";
				string strLancetQty = "";

				if (DropDownBloodSugarTest.Text == "1 time a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "2 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "3 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "4 times a day")
				{
					strStripQty = "8";
					strLancetQty = "4";
				}
				else if (DropDownBloodSugarTest.Text == "5 times a day")
				{
					strStripQty = "10";
					strLancetQty = "5";
				}
				else if (DropDownBloodSugarTest.Text == "6 times a day")
				{
					strStripQty = "12";
					strLancetQty = "6";
				}
				else if (DropDownBloodSugarTest.Text == "7 times a day")
				{
					strStripQty = "14";
					strLancetQty = "7";
				}
				else if (DropDownBloodSugarTest.Text == "8 times a day")
				{
					strStripQty = "16";
					strLancetQty = "8";
				}
				else if (DropDownBloodSugarTest.Text == "More than 8 times a day")
				{
					strStripQty = "18";
					strLancetQty = "9";
				}
				else
				{
					strStripQty = "6";
					strLancetQty = "3";
				}

				
				msg.Body += Environment.NewLine;
				msg.Body += "Group No.: " + txtGroupNo.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Group Name: " + txtGroupName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Insurer's Phone No.: " + txtInsurerPhone.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Policy Holder's Name: " + txtPolicyHolderName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;

   				msg.Body += "First Name: " + txtFirstName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Last Name: " + txtLastName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Cell Phone No.: " + txtCellPhone.Text;
                		msg.Body += Environment.NewLine;
                		msg.Body += "Email Address: " + txtEmail.Text;
                		msg.Body += Environment.NewLine;
				msg.Body += "Address (line 1): " + txtAddress1.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Address (line 2): " + txtAddress2.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "City: " + txtCity.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "State: " + strStateName;
				msg.Body += Environment.NewLine;
				msg.Body += "Zip Code: " + txtZIP.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Country: " + txtCountry.Text;
				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;

				msg.Body += "Phone No. Alt: " + txtHomePhone.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Best Time to Call: " + DropDownSupplyCallTime.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Preferred Language: " + DropDownSupplyLanguage.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "DOB: " + txtSupplyDOB.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Gender: " + DropDownSupplyGender.Text;
				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;

				msg.Body += "Diabetes Type: " + DropDownDiabetesType.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Injects Insulin: " + DropDownInsulinInjected.Text;

				msg.Body += Environment.NewLine;
				msg.Body += "Blood Sugar Tests per Day: " + strLancetQty;

				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;

				msg.Body += "Doctor's Information: " + txtSupplyDoctorInfo.Text;
				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;

				msg.Body += Environment.NewLine;
				msg.Body += "SPECIAL NOTES: " + txtSpecialNotes.Text;



				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					if (strSupplyType.Trim() == "NOVAMAX")
					{
						if (Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) == "SITEL")
						{	
							string strCountEosOnlineOrderDate = getDBValue("SELECT COUNT(DateSubmitted) AS CountDateSubmitted FROM OrderOnlineSupplies WHERE DateSubmitted > '11/01/2012' AND PID = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");
		
							if (strCountEosOnlineOrderDate == "0")
							{
								msg.Body += Environment.NewLine;
								msg.Body += Environment.NewLine;
								msg.Body += "REORDER:";
								msg.Body += Environment.NewLine;
								msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
								msg.Body += Environment.NewLine;
								msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
								msg.Body += Environment.NewLine;
								msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
								msg.Body += Environment.NewLine;
								msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
								msg.Body += Environment.NewLine;
							}
							else
							{

								msg.Body += Environment.NewLine;
								msg.Body += Environment.NewLine;
								msg.Body += "WELCOME KIT:";
								msg.Body += Environment.NewLine;
								msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
								msg.Body += Environment.NewLine;
								msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
								msg.Body += Environment.NewLine;
								msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
								msg.Body += Environment.NewLine;
								msg.Body += "43435  Nova Max Plus Full Meter Kit (Quant 1 Kit)";
								msg.Body += Environment.NewLine;
								msg.Body += "Cable  Cable for Nova Max Plus (Quant 1 cable)";
								msg.Body += Environment.NewLine;
								msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
								msg.Body += Environment.NewLine;
								msg.Body += "Welcome Letter";
							}
						}
						else
						{
					
							msg.Body += Environment.NewLine;
							msg.Body += Environment.NewLine;
							msg.Body += "REORDER:";
							msg.Body += Environment.NewLine;
							msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
							msg.Body += Environment.NewLine;
							msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
							msg.Body += Environment.NewLine;
							msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
							msg.Body += Environment.NewLine;
						}

					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{
						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "REORDER:";
						msg.Body += Environment.NewLine;
						msg.Body += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "Lancets  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
					
					}
					else
					{	
						//get the user's registration date
						string strDateUserAccountRegistered = getDBValue("SELECT CONVERT(VARCHAR(12), Registered, 107) AS Registered FROM UserAccount WHERE Login = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");

						DateTime dtDateUserAccountRegistered = Convert.ToDateTime(strDateUserAccountRegistered);

						//var result = DateTime.Compare(dtDateUserAccountRegistered, Convert.ToDateTime("01/16/2013"));

						if(dtDateUserAccountRegistered < Convert.ToDateTime("01/16/2013"))
						{

							msg.Body += Environment.NewLine;
							msg.Body += Environment.NewLine;
							msg.Body += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G115-132  On Call Vivid Meter and  EH G124-11A Lancing Device  (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH OCV Data Link Cable (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "Welcome Letter";
						}
						else
						{
							msg.Body += Environment.NewLine;
							msg.Body += Environment.NewLine;
							msg.Body += "REORDER: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
							msg.Body += Environment.NewLine;
						}
					}
				
					
				}
				else
				{

					if (strSupplyType.Trim() == "NOVAMAX")
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT:";
						msg.Body += Environment.NewLine;
						msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
						msg.Body += Environment.NewLine;
						msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
						msg.Body += Environment.NewLine;
						msg.Body += "43435  Nova Max Plus Full Meter Kit (Quant 1 Kit)";
						msg.Body += Environment.NewLine;
						msg.Body += "Cable  Cable for Nova Max Plus (Quant 1 cable)";
						msg.Body += Environment.NewLine;
						msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT:";
						msg.Body += Environment.NewLine;
						msg.Body += "GM550 METER, BLOOD GLUCOSE RIGHTEST  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "CAB559  CABLE, USB F/GM550 GLUCOMETER (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "Lancets  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Log Book, Warranty Card + Emergency Card, One CR2032 Battery and Carrying Case";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}
					else
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G115-132  On Call Vivid Meter and  EH G124-11A Lancing Device  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH OCV Data Link Cable (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}

				}


    				//send the email
				//SmtpMail.SmtpServer = "secure.emailsrvr.com";
               			SmtpMail.Send(msg);
	}

	public void createSterlingOutboundMessage(string strEmailTo, string strPONumber)
	{
		string strMessageSubject = "";
		string strMessageBody = "";

		//use the email address or the userid to retrieve the supply type for the PromoCode (group)
		string strSupplyType = getDBValue("SELECT SupplyType FROM EOS..PromoCode WHERE Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'");


		//Begin Message Body
    		strMessageBody += Environment.NewLine;
				

				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					strMessageSubject = "Refill Order from EosHealth/" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + ": " + txtFirstName.Text + " " + txtLastName.Text;
					strMessageBody += "Order Type: Reorder/Refill";
					strMessageBody += Environment.NewLine;
					strMessageBody += Environment.NewLine;
					strMessageBody += "--member should NEVER be billed or invoiced, please email snightingale@eoshealth.com with invoice/billing issues--";
					strMessageBody += Environment.NewLine;
					strMessageBody += "--member should ONLY receive EOSHEALTH program approved supplies--";
					strMessageBody += Environment.NewLine;
					strMessageBody += Environment.NewLine;
				}
				else
				{
					if (strMemberWelcomeKitOrder == "WELCOME KIT")
					{
						LabelStepNumber.Text = "<b>You have already ordered a Welcome Kit.  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you have questions.</b>";
						LabelStepNumber.ForeColor = System.Drawing.Color.Red;

						return;
 
					}
					else
					{
						strMessageSubject = "Welcome Kit Order from EosHealth:/" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + ": " + txtFirstName.Text + " " + txtLastName.Text;
						strMessageBody += "Order Type: Welcome Kit Order";
						strMessageBody += Environment.NewLine;
						strMessageBody += "--ENTER THIS PERSON INTO AUTO REFILL PROGRAM--";
						strMessageBody += Environment.NewLine;
						strMessageBody += "--member should NEVER be billed or invoiced, please email snightingale@eoshealth.com with invoice/billing issues--";
						strMessageBody += Environment.NewLine;
						strMessageBody += "--member should ONLY receive EOSHEALTH program approved supplies--";
						strMessageBody += Environment.NewLine;
						strMessageBody += Environment.NewLine;
						
					}
				}

				strMessageBody += "P.O. No.: " + strPONumber;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Insurer's Name: " + DropDownInsurer.Text.Trim();
				strMessageBody += Environment.NewLine;
				

				//update the Member ID in the database
				if ((txtMemberID.Text.Trim() == "") || (txtMemberID.Text == null) || (txtMemberID.Text == "") || (txtMemberID.Text == null) || (txtMemberID.Text == "") || (txtMemberID.Text == null))
				{
					strMessageBody += "Member ID.: I do not know my Member ID, please call me to help.";
				}
				else
				{

					strMessageBody += "Member ID.: " + txtMemberID.Text;

		
				}

				string strStateName = Convert.ToString(getDBValue("SELECT State.Name FROM EOS..State WHERE State.ID = " + Convert.ToInt16(DropDownState.Text)));

			
				//calculate strip and lancet supply quantities
					
				string strStripQty = "";
				string strLancetQty = "";

				if (DropDownBloodSugarTest.Text == "1 time a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "2 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "3 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "4 times a day")
				{
					strStripQty = "8";
					strLancetQty = "4";
				}
				else if (DropDownBloodSugarTest.Text == "5 times a day")
				{
					strStripQty = "10";
					strLancetQty = "5";
				}
				else if (DropDownBloodSugarTest.Text == "6 times a day")
				{
					strStripQty = "12";
					strLancetQty = "6";
				}
				else if (DropDownBloodSugarTest.Text == "7 times a day")
				{
					strStripQty = "14";
					strLancetQty = "7";
				}
				else if (DropDownBloodSugarTest.Text == "8 times a day")
				{
					strStripQty = "16";
					strLancetQty = "8";
				}
				else if (DropDownBloodSugarTest.Text == "More than 8 times a day")
				{
					strStripQty = "18";
					strLancetQty = "9";
				}
				else
				{
					strStripQty = "6";
					strLancetQty = "3";
				}

				
				strMessageBody += Environment.NewLine;
				strMessageBody += "Group No.: " + txtGroupNo.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Group Name: " + txtGroupName.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Insurer's Phone No.: " + txtInsurerPhone.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Policy Holder's Name: " + txtPolicyHolderName.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += Environment.NewLine;

   				strMessageBody += "First Name: " + txtFirstName.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Last Name: " + txtLastName.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Cell Phone No.: " + txtCellPhone.Text;
                		strMessageBody += Environment.NewLine;
                		strMessageBody += "Email Address: " + txtEmail.Text;
                		strMessageBody += Environment.NewLine;
				strMessageBody += "Address (line 1): " + txtAddress1.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Address (line 2): " + txtAddress2.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "City: " + txtCity.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "State: " + strStateName;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Zip Code: " + txtZIP.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Country: " + txtCountry.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += Environment.NewLine;

				strMessageBody += "Phone No. Alt: " + txtHomePhone.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Best Time to Call: " + DropDownSupplyCallTime.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Preferred Language: " + DropDownSupplyLanguage.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "DOB: " + txtSupplyDOB.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Gender: " + DropDownSupplyGender.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += Environment.NewLine;

				strMessageBody += "Diabetes Type: " + DropDownDiabetesType.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += "Injects Insulin: " + DropDownInsulinInjected.Text;

				strMessageBody += Environment.NewLine;
				strMessageBody += "Blood Sugar Tests per Day: " + strLancetQty;

				strMessageBody += Environment.NewLine;
				strMessageBody += Environment.NewLine;

				strMessageBody += "Doctor's Information: " + txtSupplyDoctorInfo.Text;
				strMessageBody += Environment.NewLine;
				strMessageBody += Environment.NewLine;

				strMessageBody += Environment.NewLine;
				strMessageBody += "SPECIAL NOTES: " + txtSpecialNotes.Text;



				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					if (strSupplyType.Trim() == "NOVAMAX")
					{
						if (Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) == "SITEL")
						{	
							string strCountEosOnlineOrderDate = getDBValue("SELECT COUNT(DateSubmitted) AS CountDateSubmitted FROM OrderOnlineSupplies WHERE DateSubmitted > '11/01/2012' AND PID = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");
		
							if (strCountEosOnlineOrderDate == "0")
							{
								strMessageBody += Environment.NewLine;
								strMessageBody += Environment.NewLine;
								strMessageBody += "REORDER:";
								strMessageBody += Environment.NewLine;
								strMessageBody += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
								strMessageBody += Environment.NewLine;
							}
							else
							{

								strMessageBody += Environment.NewLine;
								strMessageBody += Environment.NewLine;
								strMessageBody += "WELCOME KIT:";
								strMessageBody += Environment.NewLine;
								strMessageBody += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "43435  Nova Max Plus Full Meter Kit (Quant 1 Kit)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "Cable  Cable for Nova Max Plus (Quant 1 cable)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
								strMessageBody += Environment.NewLine;
								strMessageBody += "Welcome Letter";
							}
						}
						else
						{
					
							strMessageBody += Environment.NewLine;
							strMessageBody += Environment.NewLine;
							strMessageBody += "REORDER:";
							strMessageBody += Environment.NewLine;
							strMessageBody += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
							strMessageBody += Environment.NewLine;
						}

					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{
						strMessageBody += Environment.NewLine;
						strMessageBody += Environment.NewLine;
						strMessageBody += "REORDER:";
						strMessageBody += Environment.NewLine;
						strMessageBody += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Lancets  (Quant " + strLancetQty + " boxes)";
						strMessageBody += Environment.NewLine;
					
					}
					else
					{	
						//get the user's registration date
						string strDateUserAccountRegistered = getDBValue("SELECT CONVERT(VARCHAR(12), Registered, 107) AS Registered FROM UserAccount WHERE Login = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");

						DateTime dtDateUserAccountRegistered = Convert.ToDateTime(strDateUserAccountRegistered);

						//var result = DateTime.Compare(dtDateUserAccountRegistered, Convert.ToDateTime("01/16/2013"));

						if(dtDateUserAccountRegistered < Convert.ToDateTime("01/16/2013"))
						{

							strMessageBody += Environment.NewLine;
							strMessageBody += Environment.NewLine;
							strMessageBody += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G115-132  On Call Vivid Meter and  EH G124-11A Lancing Device  (Quant 1)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH OCV Data Link Cable (Quant 1)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "Welcome Letter";
						}
						else
						{
							strMessageBody += Environment.NewLine;
							strMessageBody += Environment.NewLine;
							strMessageBody += "REORDER: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							strMessageBody += Environment.NewLine;
							strMessageBody += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
							strMessageBody += Environment.NewLine;
						}
					}
				
					
				}
				else
				{

					if (strSupplyType.Trim() == "NOVAMAX")
					{

						strMessageBody += Environment.NewLine;
						strMessageBody += Environment.NewLine;
						strMessageBody += "WELCOME KIT:";
						strMessageBody += Environment.NewLine;
						strMessageBody += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "43435  Nova Max Plus Full Meter Kit (Quant 1 Kit)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Cable  Cable for Nova Max Plus (Quant 1 cable)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Welcome Letter";
					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{

						strMessageBody += Environment.NewLine;
						strMessageBody += Environment.NewLine;
						strMessageBody += "WELCOME KIT:";
						strMessageBody += Environment.NewLine;
						strMessageBody += "GM550 METER, BLOOD GLUCOSE RIGHTEST  (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "CAB559  CABLE, USB F/GM550 GLUCOMETER (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Lancets  (Quant " + strLancetQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Log Book, Warranty Card + Emergency Card, One CR2032 Battery and Carrying Case";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Welcome Letter";
					}
					else
					{

						strMessageBody += Environment.NewLine;
						strMessageBody += Environment.NewLine;
						strMessageBody += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "EH G115-132  On Call Vivid Meter and  EH G124-11A Lancing Device  (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "EH OCV Data Link Cable (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "EH G125-122 On Call Vivid Glucose Control Solution (Quant 1)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "EH G124-10A On Call Lancets 100 ct  (Quant " + strLancetQty + " boxes)";
						strMessageBody += Environment.NewLine;
						strMessageBody += "Welcome Letter";
					}

				}


    		string connString = ConfigurationManager.ConnectionStrings["WW2ReadWriteOnly"].ConnectionString;
       		SqlConnection conn = new SqlConnection(connString);
		SqlCommand sqlInsertCmd;

		// Create a new command object
		sqlInsertCmd = new SqlCommand();

		sqlInsertCmd.Connection = conn;

		string strSqlInsert = "INSERT INTO DBWIRELESS..OutBoundMessages (MessageTo,MessageFrom,MessageSubject,MessageBody) ";
		strSqlInsert = strSqlInsert + "Values (@ParamMessageTo, @ParamMessageFrom, @ParamMessageSubject, @ParamMessageBody)";
				
		// execute the dynamic sql
		sqlInsertCmd.CommandText = strSqlInsert;

		sqlInsertCmd.Parameters.Add("@ParamMessageTo", SqlDbType.NVarChar).Value = "rhasenmyer@gmail.com";//strEmailTo;

		sqlInsertCmd.Parameters.Add("@ParamMessageFrom", SqlDbType.NVarChar).Value = "CustomerCare@Livongo.com";

		sqlInsertCmd.Parameters.Add("@ParamMessageSubject", SqlDbType.NVarChar).Value = strMessageSubject;

		sqlInsertCmd.Parameters.Add("@ParamMessageBody", SqlDbType.NVarChar).Value = strMessageBody;
				

		try
		{
			conn.Open();
			sqlInsertCmd.ExecuteNonQuery();
		}
		catch (SqlException sqlEx)
		{ 
			//LabelConfirmationMessage.Text = sqlEx.ToString();
			//LabelConfirmationMessage.ForeColor = System.Drawing.Color.Red;
		} 
		finally 
		{
			conn.Close();
			conn.Dispose();
		}
	}


	public void createAndSendGlobalEmail(string strEmailTo, string strPONumber)
	{

				//use the email address or the userid to retrieve the supply type for the PromoCode (group)
				string strSupplyType = getDBValue("SELECT SupplyType FROM EOS..PromoCode WHERE Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'");
		
				MailMessage msg = new MailMessage();
  
    				msg.To = strEmailTo;
			
				//msg.Cc = "GroupOrders@EosHealth.com";

				msg.Bcc = "rhasenmyer@eoshealth.com, CustomerCare@Livongo.com";

    				msg.From = "CustomerCare@Livongo.com"; 


				//Begin Message Body
    				msg.Body += Environment.NewLine;
				

				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					msg.Subject = "Refill Order from EosHealth for: " + txtFirstName.Text + " " + txtLastName.Text;
					msg.Body += "Order Type: Reorder/Refill";
					msg.Body += Environment.NewLine;
					msg.Body += Environment.NewLine;
				}
				else
				{
					if (strMemberWelcomeKitOrder == "WELCOME KIT")
					{
						LabelStepNumber.Text = "<b>You have already ordered a Welcome Kit.  Please email <a href='mailto:Escalation@EosHealth.com'>Escalation@EosHealth.com</a> or call 1-800-945-4355 if you have questions.</b>";
						LabelStepNumber.ForeColor = System.Drawing.Color.Red;

						return;
 
					}
					else
					{
						msg.Subject = "Welcome Kit Order from EosHealth for: " + txtFirstName.Text + " " + txtLastName.Text;
						msg.Body += "Order Type: Welcome Kit Order";
						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						
					}
				}

				//msg.Body += "P.O. No.: " + strPONumber;
				msg.Body += Environment.NewLine;
				

				string strStateName = Convert.ToString(getDBValue("SELECT State.Name FROM EOS..State WHERE State.ID = " + Convert.ToInt16(DropDownState.Text)));

				
				msg.Body += "First Name: " + txtFirstName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Last Name: " + txtLastName.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Address (line 1): " + txtAddress1.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Address (line 2): " + txtAddress2.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "City: " + txtCity.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "State: " + strStateName;
				msg.Body += Environment.NewLine;
				msg.Body += "Zip Code: " + txtZIP.Text;
				msg.Body += Environment.NewLine;
				msg.Body += "Country: " + txtCountry.Text;
				msg.Body += Environment.NewLine;
				msg.Body += Environment.NewLine;


				//calculate strip and lancet supply quantities
					
				string strStripQty = "";
				string strLancetQty = "";

				if (DropDownBloodSugarTest.Text == "1 time a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "2 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "3 times a day")
				{
					strStripQty = "6";
					strLancetQty = "3";
				}
				else if (DropDownBloodSugarTest.Text == "4 times a day")
				{
					strStripQty = "8";
					strLancetQty = "4";
				}
				else if (DropDownBloodSugarTest.Text == "5 times a day")
				{
					strStripQty = "10";
					strLancetQty = "5";
				}
				else if (DropDownBloodSugarTest.Text == "6 times a day")
				{
					strStripQty = "12";
					strLancetQty = "6";
				}
				else if (DropDownBloodSugarTest.Text == "7 times a day")
				{
					strStripQty = "14";
					strLancetQty = "7";
				}
				else if (DropDownBloodSugarTest.Text == "8 times a day")
				{
					strStripQty = "16";
					strLancetQty = "8";
				}
				else if (DropDownBloodSugarTest.Text == "More than 8 times a day")
				{
					strStripQty = "18";
					strLancetQty = "9";
				}



				if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
				{
					if (strSupplyType.Trim() == "NOVAMAX")
					{
						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "REORDER:";
						msg.Body += Environment.NewLine;
						msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
						msg.Body += Environment.NewLine;
						msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
						msg.Body += Environment.NewLine;
						msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;

					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{
						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "REORDER:";
						msg.Body += Environment.NewLine;
						msg.Body += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "Lancets  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
					}
					else
					{	
						//get the user's registration date
						string strDateUserAccountRegistered = getDBValue("SELECT CONVERT(VARCHAR(12), Registered, 107) AS Registered FROM UserAccount WHERE Login = '" + Convert.ToString(AppLayer.USession.MemberDetails.Login.Value) + "'");

						DateTime dtDateUserAccountRegistered = Convert.ToDateTime(strDateUserAccountRegistered);

						//var result = DateTime.Compare(dtDateUserAccountRegistered, Convert.ToDateTime("01/16/2013"));

						if(dtDateUserAccountRegistered < Convert.ToDateTime("01/16/2013"))
						{

							msg.Body += Environment.NewLine;
							msg.Body += Environment.NewLine;
							msg.Body += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G115-102 On Call Vivid Blood Glucose Starter Kit  (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH OCV Data Link Cable (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EHNov48738 Lancets, Sureflex 100 Count 33G  (Quant " + strLancetQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "Welcome Letter";
						}
						else
						{
							msg.Body += Environment.NewLine;
							msg.Body += Environment.NewLine;
							msg.Body += "REORDER: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
							msg.Body += Environment.NewLine;
							msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
							msg.Body += Environment.NewLine;
							msg.Body += "125-122 On Call Vivid Glucose Control Solution (Quant 1)";
							msg.Body += Environment.NewLine;
							msg.Body += "EHNov48738 Lancets, Sureflex 100 Count 33G  (Quant " + strLancetQty + " boxes)";
							msg.Body += Environment.NewLine;
						}
					}
				
					
				}
				else
				{

					if (strSupplyType.Trim() == "NOVAMAX")
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT:";
						msg.Body += Environment.NewLine;
						msg.Body += "43523  Nova Max Test Strips 50/Bx  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "48607  Nova Max Plus Blood Ketone test strips 10ct box  (Quant 1 box)";
						msg.Body += Environment.NewLine;
						msg.Body += "43446  Nova Max Plus Control Solution  0.1oz  (Quant 1 bottle)";
						msg.Body += Environment.NewLine;
						msg.Body += "43435  Nova Max Plus Full Meter Kit (Quant 1 Kit)";
						msg.Body += Environment.NewLine;
						msg.Body += "Cable  Cable for Nova Max Plus (Quant 1 cable)";
						msg.Body += Environment.NewLine;
						msg.Body += "16-030-100   LANCETS 30G TWIST  (100/BX) (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}
					else if (strSupplyType.Trim() == "BIONIME GM550")
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT:";
						msg.Body += Environment.NewLine;
						msg.Body += "GM550 METER, BLOOD GLUCOSE RIGHTEST  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "GS550-50  GLUCOMETER RIGHTEST STRIPS  (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "GC-300  CONTROL SOLUTION, F/AUTOCODE  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "CAB559  CABLE, USB F/GM550 GLUCOMETER (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "Lancets  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Log Book, Warranty Card + Emergency Card, One CR2032 Battery and Carrying Case";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}
					else
					{

						msg.Body += Environment.NewLine;
						msg.Body += Environment.NewLine;
						msg.Body += "WELCOME KIT: EosHealth Supplies ONLY (Mfg EOS HEALTH)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G115-102 On Call Vivid Blood Glucose Starter Kit  (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH OCV Data Link Cable (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EH G135-102 On Call Vivid Test Strips 50 ct (Quant " + strStripQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "125-122 On Call Vivid Glucose Control Solution (Quant 1)";
						msg.Body += Environment.NewLine;
						msg.Body += "EHNov48738 Lancets, Sureflex 100 Count 33G  (Quant " + strLancetQty + " boxes)";
						msg.Body += Environment.NewLine;
						msg.Body += "Welcome Letter";
					}

				}


    				//send the email
				//SmtpMail.SmtpServer = "secure.emailsrvr.com";
				
               			SmtpMail.Send(msg);
	}

	public void updateUserAccountWithOrderInfo()
	{
			int intGender = 0;
	
			if (DropDownSupplyGender.Text == "Male")
			{
				intGender = 1;
			}
			else if (DropDownSupplyGender.Text == "Female")
			{
				intGender = 2;
			}

			int intStateID = 0;
			intStateID = Convert.ToInt16(DropDownState.Text);

			int intInsRelationship = 0;
			intInsRelationship = Convert.ToInt16(DropDownInsRelationship.Text);
					
			string connString = ConfigurationManager.ConnectionStrings["WWReadWriteOnly"].ConnectionString;
       			SqlConnection conn = new SqlConnection(connString);
			SqlCommand sqlUpdateCmd;

			// Create a new command object
			sqlUpdateCmd = new SqlCommand();

			sqlUpdateCmd.Connection = conn;

			string strSqlUpdate = "UPDATE UserAccount SET ";
			strSqlUpdate = strSqlUpdate + "InsMemberID = @ParamInsMemberID, ";
			strSqlUpdate = strSqlUpdate + "InsGroupID = @ParamInsGroupID, ";
			strSqlUpdate = strSqlUpdate + "InsDoctorInfo = @ParamInsDoctorInfo, ";
			strSqlUpdate = strSqlUpdate + "InsEmployerName = @ParamInsEmployerName, ";
			strSqlUpdate = strSqlUpdate + "InsInsurerName = @ParamInsInsurerName, ";
			strSqlUpdate = strSqlUpdate + "InsInsurerPhone = @ParamInsInsurerPhone, ";
			strSqlUpdate = strSqlUpdate + "InsPolicyHolderName = @ParamInsPolicyHolderName, ";
			strSqlUpdate = strSqlUpdate + "InsCurrentNotes = @ParamInsCurrentNotes, ";
			strSqlUpdate = strSqlUpdate + "InsRelationship = @ParamInsRelationship, ";

			strSqlUpdate = strSqlUpdate + "AddressLine1 = @ParamAddressLine1, ";
			strSqlUpdate = strSqlUpdate + "AddressLine2 = @ParamAddressLine2, ";
			strSqlUpdate = strSqlUpdate + "City = @ParamCity, ";
			strSqlUpdate = strSqlUpdate + "Zip = @ParamZip, ";

			strSqlUpdate = strSqlUpdate + "StateID = @ParamStateID, ";
			strSqlUpdate = strSqlUpdate + "HomePhone = @ParamHomePhone, ";
			strSqlUpdate = strSqlUpdate + "BestTimeToCall = @ParamBestTimeToCall, ";
			strSqlUpdate = strSqlUpdate + "PreferredLanguage = @ParamPreferredLanguage, ";
			strSqlUpdate = strSqlUpdate + "Gender = @ParamGender, ";
			strSqlUpdate = strSqlUpdate + "DiabetesType = @ParamDiabetesType, ";
			strSqlUpdate = strSqlUpdate + "InjectInsulin = @ParamInjectInsulin, ";
			strSqlUpdate = strSqlUpdate + "ContactMethod = @ParamContactMethod, ";
			strSqlUpdate = strSqlUpdate + "DailyBGTestNo = @ParamDailyBGTestNo, ";
			strSqlUpdate = strSqlUpdate + "BirthDate = @ParamBirthDate ";
			strSqlUpdate = strSqlUpdate + "WHERE UserAccount.Email = '" + Convert.ToString(AppLayer.USession.MemberDetails.Email.Value) + "'";

			// execute the dynamic sql
			sqlUpdateCmd.CommandText = strSqlUpdate;

			sqlUpdateCmd.Parameters.Add("@ParamInsMemberID", SqlDbType.VarChar).Value = txtMemberID.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsGroupID", SqlDbType.VarChar).Value = txtGroupNo.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsDoctorInfo", SqlDbType.VarChar).Value = txtSupplyDoctorInfo.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsEmployerName", SqlDbType.VarChar).Value = txtGroupName.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsInsurerName", SqlDbType.VarChar).Value = DropDownInsurer.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsInsurerPhone", SqlDbType.VarChar).Value = txtInsurerPhone.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsPolicyHolderName", SqlDbType.VarChar).Value = txtPolicyHolderName.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsCurrentNotes", SqlDbType.VarChar).Value = txtSpecialNotes.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInsRelationship", SqlDbType.Int).Value = intInsRelationship;

			sqlUpdateCmd.Parameters.Add("@ParamAddressLine1", SqlDbType.VarChar).Value = txtAddress1.Text;
			sqlUpdateCmd.Parameters.Add("@ParamAddressLine2", SqlDbType.VarChar).Value = txtAddress2.Text;
			sqlUpdateCmd.Parameters.Add("@ParamCity", SqlDbType.VarChar).Value = txtCity.Text;
			sqlUpdateCmd.Parameters.Add("@ParamZip", SqlDbType.VarChar).Value = txtZIP.Text;

			sqlUpdateCmd.Parameters.Add("@ParamStateID", SqlDbType.Int).Value = intStateID;
			sqlUpdateCmd.Parameters.Add("@ParamHomePhone", SqlDbType.VarChar).Value = txtHomePhone.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamBestTimeToCall", SqlDbType.VarChar).Value = DropDownSupplyCallTime.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamPreferredLanguage", SqlDbType.VarChar).Value = DropDownSupplyLanguage.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamGender", SqlDbType.Int).Value = intGender;
			sqlUpdateCmd.Parameters.Add("@ParamDiabetesType", SqlDbType.VarChar).Value = DropDownDiabetesType.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamInjectInsulin", SqlDbType.VarChar).Value = DropDownInsulinInjected.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamContactMethod", SqlDbType.VarChar).Value = DropDownContactMethod.Text;
			sqlUpdateCmd.Parameters.Add("@ParamDailyBGTestNo", SqlDbType.VarChar).Value = DropDownBloodSugarTest.Text.Trim();
			sqlUpdateCmd.Parameters.Add("@ParamBirthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtSupplyDOB.Text.Trim());
															
			try
			{
				conn.Open();
				sqlUpdateCmd.ExecuteNonQuery();

			}
			catch (SqlException sqlEx)
			{ 
				LabelUpdateErrorMessage.Text = sqlEx.ToString();
				LabelUpdateErrorMessage.ForeColor = System.Drawing.Color.Red;
			} 
			finally 
			{
				// close the connection
				conn.Close();
				conn.Dispose();
			}
	}


	public void updateUserActivationRecord(string strUserNameEmail)
    	{
	        //get the email address/userid entered
	        string strUserName = strUserNameEmail.Trim();

	        //use the email address or the userid to retrieve the Login/PID from the database for comparison
		string username = getDBValue("SELECT Login FROM EOS..UserAccount WHERE Email = '" + strUserName + "'");
       
	        //get the userid value and set to upper case (to ensure hashing on uppper case version, to match backend)
		string plainText = username.ToUpper();
					
		string connString = ConfigurationManager.ConnectionStrings["WW2ReadWriteOnly"].ConnectionString;
		SqlConnection conn = new SqlConnection(connString);
		SqlCommand sqlUpdateCmd;

		// Create a new command object
		sqlUpdateCmd = new SqlCommand();

		sqlUpdateCmd.Connection = conn;

		string strSqlUpdate = "UPDATE ActivationData SET ";
		strSqlUpdate = strSqlUpdate + "InsulinUser = @ParamInjectInsulin, ";
		strSqlUpdate = strSqlUpdate + "Gender = @ParamGender, ";
		strSqlUpdate = strSqlUpdate + "BirthDate = @ParamBirthDate ";
		strSqlUpdate = strSqlUpdate + "WHERE ActivationData.PID = '" + username + "'";

		// execute the dynamic sql
		sqlUpdateCmd.CommandText = strSqlUpdate;

		if (DropDownInsulinInjected.Text.Trim() == "Yes")
		{
			sqlUpdateCmd.Parameters.Add("@ParamInjectInsulin", SqlDbType.Int).Value = 1;
		}
		else if (DropDownInsulinInjected.Text.Trim() == "No")
		{
			sqlUpdateCmd.Parameters.Add("@ParamInjectInsulin", SqlDbType.Int).Value = 0;
		}
		
		sqlUpdateCmd.Parameters.Add("@ParamGender", SqlDbType.VarChar).Value = DropDownSupplyGender.Text.Trim();			
		sqlUpdateCmd.Parameters.Add("@ParamBirthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtSupplyDOB.Text.Trim());
					
															
		try
		{
			conn.Open();
			sqlUpdateCmd.ExecuteNonQuery();	
		}
			catch (SqlException sqlEx)
		{ 
			//LabelUpdateErrorMessage.Text = sqlEx.ToString();
			//LabelUpdateErrorMessage.ForeColor = System.Drawing.Color.Red;
		} 
		finally 
		{
			// close the connection
			conn.Close();
			conn.Dispose();
		}
		    

		
    	}


	//function to archive order record in Eos database
	private void insertOrderRecord(string strEmailTo, string strPONumber)
	{

		string connString = ConfigurationManager.ConnectionStrings["WWReadWriteOnly"].ConnectionString;
       		SqlConnection conn = new SqlConnection(connString);
		SqlCommand sqlInsertCmd;

		// Create a new command object
		sqlInsertCmd = new SqlCommand();
        sqlInsertCmd.CommandTimeout = 300;
		sqlInsertCmd.Connection = conn;

		string strSqlInsert = "INSERT INTO OrderOnlineSupplies (PONumber, PromoCode, PID, MemberFirstName, MemberLastName, MemberEmail, OrderType, SubmittedFrom, SentTo, DateSubmitted, DailyBGTestNo) ";
		strSqlInsert = strSqlInsert + "Values (@ParamPONumber, @ParamPromoCode, @ParamPID, @ParamMemberFirstName, @ParamMemberLastName, @ParamMemberEmail, @ParamOrderType, @ParamSubmittedFrom, @ParamSentToEmail, @ParamSubmitDate, @ParamDailyBGTestNo)";
				
		// execute the dynamic sql
		sqlInsertCmd.CommandText = strSqlInsert;

		sqlInsertCmd.Parameters.Add("@ParamPONumber", SqlDbType.NVarChar).Value = strPONumber;

		sqlInsertCmd.Parameters.Add("@ParamPromoCode", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value);

		sqlInsertCmd.Parameters.Add("@ParamPID", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value);

		sqlInsertCmd.Parameters.Add("@ParamMemberFirstName", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.FirstName.Value);

		sqlInsertCmd.Parameters.Add("@ParamMemberLastName", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.LastName.Value);

		sqlInsertCmd.Parameters.Add("@ParamMemberEmail", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();

		if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
		{
			sqlInsertCmd.Parameters.Add("@ParamOrderType", SqlDbType.NVarChar).Value = "REORDER";
		}
		else
		{
			sqlInsertCmd.Parameters.Add("@ParamOrderType", SqlDbType.NVarChar).Value = "WELCOME KIT";
		}

		sqlInsertCmd.Parameters.Add("@ParamSubmittedFrom", SqlDbType.NVarChar).Value = "NEW REFILL FORM";

		sqlInsertCmd.Parameters.Add("@ParamSentToEmail", SqlDbType.NVarChar).Value = strEmailTo;
		
		sqlInsertCmd.Parameters.Add("@ParamSubmitDate", SqlDbType.DateTime).Value = DateTime.Now;

		sqlInsertCmd.Parameters.Add("@ParamDailyBGTestNo", SqlDbType.VarChar).Value = DropDownBloodSugarTest.Text.Trim();
								

		try
		{
			conn.Open();
			sqlInsertCmd.ExecuteNonQuery();
	
		}
		catch (SqlException sqlEx)
		{ 
			LabelConfirmationMessage.Text = sqlEx.ToString();
			LabelConfirmationMessage.ForeColor = System.Drawing.Color.Red;
		} 
		finally 
		{
			conn.Close();
			conn.Dispose();

		}
	}

	//function to add order record to queue in DBWireless database
	private void insertOrderQueueRecord(string strPONumber)
	{
		//get the email address/userid entered
		//TextBox user = (TextBox)this.FindControl("txtEmail");
	        string strUserEmail = txtEmail.Text.Trim();

		//use the email address or the userid to retrieve the PID from the database for comparison
            	string strPID = Convert.ToString(AppLayer.USession.MemberDetails.Login.Value);

		//use the email address or the userid to retrieve the supply type for the PromoCode (group)
		string strSupplyType = getDBValue("SELECT SupplyType FROM EOS..PromoCode WHERE Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'");
		
		//get State name
		String strStateName = Convert.ToString(getDBValue("SELECT State.Name FROM EOS..State WHERE State.ID = " + Convert.ToInt16(DropDownState.Text)));

		//get the promocode entered
		string strPromoCode = Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value);

		int intDailyBGTestNo = 0;

		if (DropDownBloodSugarTest.Text == "1 time a day")
		{
			intDailyBGTestNo = 1;
		}
		else if (DropDownBloodSugarTest.Text == "2 times a day")
		{
			intDailyBGTestNo = 2;
		}
		else if (DropDownBloodSugarTest.Text == "3 times a day")
		{
			intDailyBGTestNo = 3;
		}
		else if (DropDownBloodSugarTest.Text == "4 times a day")
		{
			intDailyBGTestNo = 4;
		}
		else if (DropDownBloodSugarTest.Text == "5 times a day")
		{
			intDailyBGTestNo = 5;
		}
		else if (DropDownBloodSugarTest.Text == "6 times a day")
		{
			intDailyBGTestNo = 6;
		}
		else if (DropDownBloodSugarTest.Text == "7 times a day")
		{
			intDailyBGTestNo = 7;
		}
		else if (DropDownBloodSugarTest.Text == "8 times a day")
		{
			intDailyBGTestNo = 8;
		}
		else if (DropDownBloodSugarTest.Text == "More than 8 times a day")
		{
			intDailyBGTestNo = 9;
		}


		string connString = ConfigurationManager.ConnectionStrings["WW2ReadWriteOnly"].ConnectionString;
       		SqlConnection conn = new SqlConnection(connString);
		SqlCommand sqlInsertCmd;

		// Create a new command object
		sqlInsertCmd = new SqlCommand();

		sqlInsertCmd.Connection = conn;

		string strSqlInsert = "INSERT INTO tbl_Supply_Order_Queue (PONumber, PromoCode, PID, MemberFirstName, MemberLastName, MemberEmail, OrderType, DailyBGTestNo, OrderNotes, DateSubmitted, SentToADDR1, SentToADDR2, SentToCity, SentToState, SentToZip) ";
		strSqlInsert = strSqlInsert + "Values (@ParamPONumber, @ParamPromoCode, @ParamPID, @ParamMemberFirstName, @ParamMemberLastName, @ParamMemberEmail, @ParamOrderType, @ParamDailyBGTestNo, @ParamOrderNotes, @ParamSubmitDate, @ParamSentToADDR1, @ParamSentToADDR2, @ParamSentToCity, @ParamSentToState, @ParamSentToZip)";
				
		// execute the dynamic sql
		sqlInsertCmd.CommandText = strSqlInsert;

		sqlInsertCmd.Parameters.Add("@ParamPONumber", SqlDbType.NVarChar).Value = strPONumber;

		sqlInsertCmd.Parameters.Add("@ParamPromoCode", SqlDbType.NVarChar).Value = strPromoCode.Trim();

		sqlInsertCmd.Parameters.Add("@ParamPID", SqlDbType.NVarChar).Value = strPID;

		sqlInsertCmd.Parameters.Add("@ParamMemberFirstName", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.FirstName.Value);

		sqlInsertCmd.Parameters.Add("@ParamMemberLastName", SqlDbType.NVarChar).Value = Convert.ToString(AppLayer.USession.MemberDetails.LastName.Value);

		sqlInsertCmd.Parameters.Add("@ParamMemberEmail", SqlDbType.NVarChar).Value = strUserEmail.Trim();

		if (DropDownReorder.SelectedValue == "Yes, this is reorder.")
		{
			sqlInsertCmd.Parameters.Add("@ParamOrderType", SqlDbType.NVarChar).Value = "REORDER";
		}
		else
		{
			sqlInsertCmd.Parameters.Add("@ParamOrderType", SqlDbType.NVarChar).Value = "WELCOME KIT";
		}

		sqlInsertCmd.Parameters.Add("@ParamDailyBGTestNo", SqlDbType.Int).Value = intDailyBGTestNo;

		sqlInsertCmd.Parameters.Add("@ParamOrderNotes", SqlDbType.NVarChar).Value = strSupplyType + " Order for " + strPromoCode.Trim() + " member. " + txtSpecialNotes.Text.Trim();

		sqlInsertCmd.Parameters.Add("@ParamSubmitDate", SqlDbType.DateTime).Value = DateTime.Now;

		sqlInsertCmd.Parameters.Add("@ParamSentToADDR1", SqlDbType.NVarChar).Value = txtAddress1.Text.Trim();

		sqlInsertCmd.Parameters.Add("@ParamSentToADDR2", SqlDbType.NVarChar).Value = txtAddress2.Text.Trim();

		sqlInsertCmd.Parameters.Add("@ParamSentToCity", SqlDbType.NVarChar).Value = txtCity.Text.Trim();

		sqlInsertCmd.Parameters.Add("@ParamSentToState", SqlDbType.NVarChar).Value = strStateName.Trim();

		sqlInsertCmd.Parameters.Add("@ParamSentToZip", SqlDbType.NVarChar).Value = txtZIP.Text.Trim();
				

		try
		{
			conn.Open();
			sqlInsertCmd.ExecuteNonQuery();
		}
		catch (SqlException sqlEx)
		{ 
			//LabelConfirmationMessage.Text = sqlEx.ToString();
			//LabelConfirmationMessage.ForeColor = System.Drawing.Color.Red;
		} 
		finally 
		{
			conn.Close();
			conn.Dispose();
		}
	}

	public void Populate_DropDownState()
    	{ 
		string connString = ConfigurationManager.ConnectionStrings["WWReadOnly"].ConnectionString;

		SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM State ORDER BY Name ASC", new SqlConnection(connString));

		try
		{
			cmd.Connection.Open();

			SqlDataReader ddStateValues;
			ddStateValues = cmd.ExecuteReader();

			DropDownState.DataSource = ddStateValues;
			DropDownState.DataValueField = "ID";
			DropDownState.DataTextField = "Name";
			DropDownState.DataBind();
		}
		finally
		{
			cmd.Connection.Close();
			cmd.Connection.Dispose();
		}

		//add item to first postion
		DropDownState.Items.Insert(0, "Select A State");
    	}

	//function to execute a SQL statement and return a value
   	protected string getDBValue(string Sql)
    	{
        	string ret = "";
        
        	string connString = ConfigurationManager.ConnectionStrings["WWReadOnly"].ConnectionString;
        	SqlConnection conn = new SqlConnection(connString);
        	SqlCommand sqlCmd;

        	// Create a new command object
        	sqlCmd = new SqlCommand();
        	// Specify the command to be excecuted
        	sqlCmd.CommandType = CommandType.Text;

        	// execute the dynamic sql
        	sqlCmd.CommandText = Sql;

        	sqlCmd.Connection = conn;
        	
		try
      		{
        		conn.Open();
        		SqlDataReader myReader = sqlCmd.ExecuteReader();
        		while (myReader.Read())
        		{
            		ret = myReader[0].ToString();
        		}

			myReader.Close();
			return ret;
		}
		finally
		{
        		
        		// close the connection
        		conn.Close();
			conn.Dispose();
		}
    	}

	//function to fill order form with the data that has been stored
	private void fillOrderForm()
	{
		string connString = ConfigurationManager.ConnectionStrings["WWReadOnly"].ConnectionString;
        	SqlConnection conn = new SqlConnection(connString);
        	SqlCommand sqlCmd;

        	// Create a new command object
        	sqlCmd = new SqlCommand();
        	// Specify the command to be excecuted
        	sqlCmd.CommandType = CommandType.Text;

        	// execute the dynamic sql
        	string strSqlCommandText = "SELECT StateID, ";
		strSqlCommandText = strSqlCommandText + "Gender, ";
		strSqlCommandText = strSqlCommandText + "BestTimeToCall, ";
		strSqlCommandText = strSqlCommandText + "PreferredLanguage, ";
		strSqlCommandText = strSqlCommandText + "DiabetesType, ";
		strSqlCommandText = strSqlCommandText + "InjectInsulin, ";
		strSqlCommandText = strSqlCommandText + "ContactMethod, ";
		strSqlCommandText = strSqlCommandText + "DailyBGTestNo, ";
		strSqlCommandText = strSqlCommandText + "BirthDate, ";
		strSqlCommandText = strSqlCommandText + "AddressLine1, ";
		strSqlCommandText = strSqlCommandText + "AddressLine2, ";
		strSqlCommandText = strSqlCommandText + "City, ";
		strSqlCommandText = strSqlCommandText + "ZIP, ";
		strSqlCommandText = strSqlCommandText + "InsMemberID, ";
		strSqlCommandText = strSqlCommandText + "InsGroupID, ";
		strSqlCommandText = strSqlCommandText + "InsDoctorInfo, ";
		strSqlCommandText = strSqlCommandText + "InsCurrentNotes, ";
		strSqlCommandText = strSqlCommandText + "InsPolicyHolderName, ";
		strSqlCommandText = strSqlCommandText + "HomePhone, ";
		strSqlCommandText = strSqlCommandText + "PCCode, ";
		strSqlCommandText = strSqlCommandText + "InsEmployerName, ";
		strSqlCommandText = strSqlCommandText + "InsInsurerName, ";
		strSqlCommandText = strSqlCommandText + "InsInsurerPhone, ";
		strSqlCommandText = strSqlCommandText + "InsRelationship ";
		strSqlCommandText = strSqlCommandText + "FROM EOS..UserAccount WHERE UserAccount.Email = '" + Convert.ToString(AppLayer.USession.MemberDetails.Email.Value) + "'";

		sqlCmd.CommandText = strSqlCommandText;

        	sqlCmd.Connection = conn;

		try
		{
        		conn.Open();
        		SqlDataReader myReader = sqlCmd.ExecuteReader();


			txtFirstName.Text = Convert.ToString(AppLayer.USession.MemberDetails.FirstName.Value);
			
			txtLastName.Text = Convert.ToString(AppLayer.USession.MemberDetails.LastName.Value);
			
			txtCellPhone.Text = Convert.ToString(AppLayer.USession.MemberDetails.Phone.Value);
			
			txtEmail.Text = Convert.ToString(AppLayer.USession.MemberDetails.Email.Value);


        		while (myReader.Read())
        		{
				if (myReader["StateID"] != DBNull.Value)
				{
					ListItem item = DropDownState.Items.FindByValue(myReader["StateID"].ToString());

					if(item != null)
					{
						DropDownState.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

				if (myReader["InsRelationship"] != DBNull.Value)
				{
					ListItem item = DropDownInsRelationship.Items.FindByValue(myReader["InsRelationship"].ToString());

					if(item != null)
					{
						DropDownInsRelationship.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

				if (myReader["Gender"] != DBNull.Value)
				{
					if (myReader["Gender"].ToString() == "1")
					{
						ListItem item = DropDownSupplyGender.Items.FindByValue("Male");

						if(item != null)
						{
							DropDownSupplyGender.SelectedItem.Selected = false;
							item.Selected = true;
						}	
					}
					else if (myReader["Gender"].ToString() == "2")
        				{
            					ListItem item = DropDownSupplyGender.Items.FindByValue("Female");

						if(item != null)
						{
							DropDownSupplyGender.SelectedItem.Selected = false;
							item.Selected = true;
						}
        				}
				}

				if (myReader["BestTimeToCall"] != DBNull.Value)
				{
					ListItem item = DropDownSupplyCallTime.Items.FindByValue(myReader["BestTimeToCall"].ToString());

					if(item != null)
					{
						DropDownSupplyCallTime.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

			
				if (myReader["PreferredLanguage"] != DBNull.Value)
				{
					ListItem item = DropDownSupplyLanguage.Items.FindByValue(myReader["PreferredLanguage"].ToString());

					if(item != null)
					{
						DropDownSupplyLanguage.SelectedItem.Selected = false;
						item.Selected = true;
					}
				
				}

				if (myReader["DiabetesType"] != DBNull.Value)
				{
					ListItem item = DropDownDiabetesType.Items.FindByValue(myReader["DiabetesType"].ToString());

					if(item != null)
					{
						DropDownDiabetesType.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

				if (myReader["InjectInsulin"] != DBNull.Value)
				{
					ListItem item = DropDownInsulinInjected.Items.FindByValue(myReader["InjectInsulin"].ToString());

					if(item != null)
					{
						DropDownInsulinInjected.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

				if (myReader["ContactMethod"] != DBNull.Value)
				{
					ListItem item = DropDownContactMethod.Items.FindByValue(myReader["ContactMethod"].ToString());

					if(item != null)
					{
						DropDownContactMethod.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}

				if (myReader["DailyBGTestNo"] != DBNull.Value)
				{
					ListItem item = DropDownBloodSugarTest.Items.FindByValue(myReader["DailyBGTestNo"].ToString());

					if(item != null)
					{
						DropDownBloodSugarTest.SelectedItem.Selected = false;
						item.Selected = true;
					}
				}
			

				if (myReader["BirthDate"] != DBNull.Value)
				{
					txtSupplyDOB.Text = (Convert.ToDateTime(myReader["BirthDate"])).ToShortDateString();
				}

				if (myReader["AddressLine1"] != DBNull.Value)
				{
					txtAddress1.Text = myReader["AddressLine1"].ToString();
				}

			
				if (myReader["AddressLine2"] != DBNull.Value)
				{
					txtAddress2.Text = myReader["AddressLine2"].ToString();
				}

				if (myReader["City"] != DBNull.Value)
				{
					txtCity.Text = myReader["City"].ToString();
				}

				if (myReader["ZIP"] != DBNull.Value)
				{
					txtZIP.Text = myReader["ZIP"].ToString();
				}
			
				if (myReader["InsMemberID"] != DBNull.Value)
				{
					txtMemberID.Text = myReader["InsMemberID"].ToString();
				}

				if (myReader["InsGroupID"] != DBNull.Value)
				{
					txtGroupNo.Text = myReader["InsGroupID"].ToString();
				}

				if (myReader["InsDoctorInfo"] != DBNull.Value)
				{
					txtSupplyDoctorInfo.Text = myReader["InsDoctorInfo"].ToString();
				}

				if (myReader["InsCurrentNotes"] != DBNull.Value)
				{
					txtSpecialNotes.Text = myReader["InsCurrentNotes"].ToString();
				}

				if (myReader["InsPolicyHolderName"] != DBNull.Value)
				{
					txtPolicyHolderName.Text = myReader["InsPolicyHolderName"].ToString();
				}

				if (myReader["HomePhone"] != DBNull.Value)
				{
					txtHomePhone.Text = myReader["HomePhone"].ToString();
				}

				if (myReader["InsInsurerName"] != DBNull.Value)
				{
					ListItem item = DropDownInsurer.Items.FindByValue(myReader["InsInsurerName"].ToString());

					if(item != null)
					{
						DropDownInsurer.SelectedItem.Selected = false;
						item.Selected = true;
					}
				
				}
				else
				{
					if ((myReader["PCCode"] != DBNull.Value) && (myReader["PCCode"].ToString() != ""))
        				{
						String strGroupInsurer = Convert.ToString(getDBValue("SELECT PromoCode.GroupInsurer FROM EOS..PromoCode WHERE PromoCode.Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'"));
			

						ListItem item = DropDownInsurer.Items.FindByValue(strGroupInsurer);

						if(item != null)
						{
							DropDownInsurer.SelectedItem.Selected = false;
							item.Selected = true;
						}
					}
				}

				if (myReader["InsInsurerPhone"] != DBNull.Value)
				{
					txtInsurerPhone.Text = myReader["InsInsurerPhone"].ToString();
				}
				else
				{
					if ((myReader["PCCode"] != DBNull.Value) && (myReader["PCCode"].ToString() != ""))
        				{
						String strGroupInsurerPhone = Convert.ToString(getDBValue("SELECT PromoCode.GroupInsurerPhone FROM EOS..PromoCode WHERE PromoCode.Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'"));
			
						if (strGroupInsurerPhone != null)
						{
							txtInsurerPhone.Text = strGroupInsurerPhone;
						}
						else
						{
							txtInsurerPhone.Text = "";
						}
					}
				}

				if ((myReader["InsEmployerName"] != DBNull.Value) && (myReader["InsEmployerName"].ToString() != ""))
        			{
					txtGroupName.Text = myReader["InsEmployerName"].ToString();
				}
				else
				{
					if ((myReader["PCCode"] != DBNull.Value) && (myReader["PCCode"].ToString() != ""))
        				{
				
						String strGroupName = Convert.ToString(getDBValue("SELECT PromoCode.Name FROM EOS..PromoCode WHERE PromoCode.Code = '" + Convert.ToString(AppLayer.USession.MemberDetails.PCCode.Value) + "'"));
			
						if (strGroupName != null)
						{
							txtGroupName.Text = strGroupName;
						}
				
					}
				}
			            		
        		}
        		myReader.Close();
		}
		finally
		{        	
	        	// close the connection
        		conn.Close();
			conn.Dispose();
		}
	}


}

