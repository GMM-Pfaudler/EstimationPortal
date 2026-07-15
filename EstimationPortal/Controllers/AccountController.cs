using CaptchaMvc.HtmlHelpers;
using Castle.Core.Logging;
using SUP_BAL.IRepository;
using SUP_CORE.EFModel;
using SUP_DAL.EFContextProvider;
using EstimationPortal.CustomeAttribute;
using EstimationPortal.Models;
using EstimationPortal.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;
using OfficeOpenXml.Drawing.Slicer.Style;
using System.Web.Security;
using EstimationPortal.App_Start;
using EstimationPortal.CustomeAttribute;

namespace EstimationPortal.Controllers
{
    public class AccountController : Controller
    {
        private SupplierDbContext _db;
        private readonly ILogger _logger;
        private readonly IGenericRepository<Tbl_UserMasters> _genericUserMasterRepository;
        private readonly IGenericRepository<Tbl_LoginLogs> _genericLoginLogsRepository;
        string password;
        string _domain = ConfigurationManager.ConnectionStrings["do_main"].ConnectionString;
        public AccountController()
        {
            SupplierDbContext db= new SupplierDbContext();
            _db = db;
        }
        public AccountController(ILogger logger, IGenericRepository<Tbl_LoginLogs> genericLoginLogsRepository, SupplierDbContext db, IGenericRepository<Tbl_UserMasters> genericUserMasterRepository)
        {
            _logger = logger;
            _genericUserMasterRepository = genericUserMasterRepository;
            _genericLoginLogsRepository=genericLoginLogsRepository;
            _db = db;
        }
        // GET: Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                bool UserExist = _db.Tbl_UserMasters
                                    .Any(s => s.UserID == model.UserID.Trim() && s.IsDelete == false);

                if (UserExist)
                {
                    var GetData = _genericUserMasterRepository
                                  .Find(s => s.UserID.Trim() == model.UserID.Trim()
                                          && s.Password == model.Password.Trim())
                                  .FirstOrDefault();

                    if (GetData != null)
                    {
                        Session["RoleID"] = GetData.RoleId;
                        Session["UserID"] = GetData.UserID;
                        Session["ID"] = GetData.Id;
                        Session["UserName"] = GetData.UserName;
                        Session["FinancialYear"] = model.FinancialYear;
                        string Fyear = Session["FinancialYear"].ToString();
                        Tbl_LoginLogs mm = new Tbl_LoginLogs();
                        mm.UserId = GetData.Id;
                        mm.LoginDate = DateTime.Now;
                        mm.LoginToken = "";
                        _genericLoginLogsRepository.Insert(mm);
                        _genericLoginLogsRepository.Save();

                        this.AddNotification("Login Successfully!!", "Success");

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "UserName or Password Wrong!!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found!!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View(model);
            }

        }
        


        public string CheckUser(string UserN, string Passwrd)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter("SP_UserLogin", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@UserName", UserN);
                adp.SelectCommand.Parameters.AddWithValue("@Password", Passwrd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string login = dt.Rows[0]["Users"].ToString();
                return login;
            }
            catch(Exception ex)
            {
                return "False";
            }
        }
        [HttpGet]
        [NoDirectAccess]
        public ActionResult ResetPassword()
        {
            string SUserId = Session["UserID"].ToString();
            if (SUserId != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string userName = Session["UserID"].ToString();
                //var dataUpdate = _genericUserMasterRepository.Find(t => t.UserID == userName && t.Active == true).FirstOrDefault();
                //dataUpdate.Password = model.ConfirmPassword;
                //dataUpdate.ModifiedBy = userName;
                //dataUpdate.ModifiedDate = DateTime.Now;
                ////dataUpdate.LoginFirstDT = DateTime.Now;
                //_genericUserMasterRepository.Update(dataUpdate);
                //_genericUserMasterRepository.Save();
                this.AddNotification("Password Changed Successfully!!!", "Success");
            }
            else
            {
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        [NoDirectAccess]
        public ActionResult LoginWithOTP()
        {
            string hostName = Dns.GetHostName(); 
            IPAddress[] localIPs1 = Dns.GetHostAddresses(hostName);
            IPAddress ipv4Address1 = Array.Find(localIPs1, ip => ip.AddressFamily == AddressFamily.InterNetwork); 
            ViewBag.userIpAddress2 = Request.UserHostAddress;
            string clientIpAddress = NetworkHelper.GetClientIpAddress(Request);
            ViewBag.userIpAddress3 = clientIpAddress;
            return View();
        }
        [HttpPost]
        public ActionResult LoginWithOTP(LoginViewModelWithOTP model)
        {
            //if (ModelState.IsValid)
            //{
            //    var CheckRAt = CheckOTPAttmptUser(model.UserEmailId);
            //    if (CheckRAt == "Success")
            //    {
            //        bool checkUserEmail = _db.UserMasters.Any(s => s.EmailAddress == model.UserEmailId);
            //        if (checkUserEmail == true)
            //        {
            //            bool checkUserLockOrNot = _db.UserMasters.Any(s => s.EmailAddress == model.UserEmailId && s.IsActive == true);
            //            if (checkUserLockOrNot == true)
            //            {
            //                //check here role of user
            //                var GetUData = _db.UserMasters.Where(s => s.EmailAddress == model.UserEmailId && s.IsActive == true).FirstOrDefault();
            //                if ((GetUData.RoleId == 2 || GetUData.RoleId == 4 || GetUData.RoleId == 34) )
            //                {//restrict gmm user otp (Admin,Buyer,planning)
            //                    //OTP Generate
            //                    Random generator = new Random();
            //                    int newotp = generator.Next(100000, 999990);

            //                    var dataUpdate = _genericUserMasterRepository.Find(t => t.EmailAddress == model.UserEmailId && t.IsActive == true).FirstOrDefault();
            //                    //dataUpdate.OTP = newotp;
            //                    //dataUpdate.ModifiedDate = DateTime.Now;
            //                    _genericUserMasterRepository.Update(dataUpdate);
            //                    _genericUserMasterRepository.Save();

            //                    Session["UserEmailId"] = model.UserEmailId;
            //                    try
            //                    {
            //                        EmailService.EmailSentWithOTP(newotp, model.UserEmailId);
            //                        return RedirectToAction("VerifyOTP", "Account");
            //                    }
            //                    catch (SmtpException ex)
            //                    {
            //                        return RedirectToAction("Login", "Account");
            //                    }
            //                }
            //                else if (GetUData.RoleId == 1 ||GetUData.RoleId == 3 || GetUData.RoleId == 6 || GetUData.RoleId == 7)
            //                { // SAdmin,Supplier,QltMgr,QltEng
            //                    //OTP Generate
            //                    Random generator = new Random();
            //                    int newotp = generator.Next(100000, 999990);

            //                    var dataUpdate = _genericUserMasterRepository.Find(t => t.EmailID == model.UserEmailId && t.Active == true && t.Active == true).FirstOrDefault();
            //                    //dataUpdate.OTP = newotp;
            //                    dataUpdate.ModifiedDate = DateTime.Now;
            //                    _genericUserMasterRepository.Update(dataUpdate);
            //                    _genericUserMasterRepository.Save();

            //                    Session["UserEmailId"] = model.UserEmailId;
            //                    try
            //                    {
            //                        EmailService.EmailSentWithOTP(newotp, model.UserEmailId);
            //                        return RedirectToAction("VerifyOTP", "Account");
            //                    }
            //                    catch (SmtpException ex)
            //                    {
            //                        return RedirectToAction("Login", "Account");
            //                    }
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError(string.Empty, " You are not authorised to access from this device");
            //                    return View(model);
            //                }

            //            }
            //            else
            //            {
            //                ModelState.AddModelError(string.Empty, "Your account has locked,contact your IT Administrator.");
            //                return View(model);
            //            }
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, "Invalid EmailId!");
            //            return View(model);
            //        }
            //    }
            //    else if (CheckRAt == "Not Exist")
            //    {
            //        ModelState.AddModelError(string.Empty, "Your EmailId does not Exits!!");
            //        return View(model);
            //    }
            //    else if (CheckRAt == "Locked")
            //    {
            //        ModelState.AddModelError(string.Empty, "You are Locked for today. Please come tomorrow & login");
            //        return View(model);
            //    }
            //    else if(CheckRAt == "FAIL")
            //    {
            //        ModelState.AddModelError(string.Empty, "Multiple Account Is Exits!!");
            //        return View(model);
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid EmailId!");
            //        return View(model);
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Something Went Wrong!!");
            //    return View(model);
            //}
            return View();
        }
        public string CheckOTPAttmptUser(string EmailId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter("SP_OTPUserAttmptLogin", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@EmailId", EmailId);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string login = dt.Rows[0]["Users"].ToString();
                return login;
            }
            catch(Exception ex)
            {
                return "FAIL";
            }
        }
        [HttpGet]
        [NoDirectAccess]
        public ActionResult VerifyOTP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerifyOTP(VerifyOTP model, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{
            //    if (this.IsCaptchaValid("Validate your captcha"))
            //    {
            //        string UserElId = Session["UserEmailId"].ToString();
            //        string vrfyOTPNumbr = model.first.ToString() + model.second.ToString() + model.third.ToString() + model.fourth.ToString() + model.fifth.ToString() + model.sixth.ToString();

            //        bool? checkUserWithOTP = _db.UserMasters.Any(s => s.EmailID == UserElId && s.Active == true);
            //        if (checkUserWithOTP == true)
            //        {
            //            var GetData = _db.UserMasters.Where(s => s.EmailID == UserElId && s.Active == true).FirstOrDefault();
            //            //Login History Save Details
            //            LoginHistory LH = new LoginHistory();
            //            LH.UserId = GetData.UserID;
            //            LH.UserName = GetData.UserName;
            //            LH.LoginStartTime = DateTime.Now;
            //            _db.LoginHistories.Add(LH);
            //            _db.SaveChanges();

            //            bool? CheckData = _genericUAMRepository.Find(s => s.UserMaster_Id == GetData.Id).Any();
            //            if (CheckData == true)
            //            {
            //                Session["RoleID"] = GetData.RoleId;
            //                Session["UserID"] = GetData.UserID;
            //                Session["ID"] = GetData.Id;
            //                Session["UserName"] = GetData.UserName;
            //                Session["EmailID"] = GetData.EmailID;
            //                //Session["PO"] = GetData.PO;
            //                this.AddNotification("Login Successfully!!", "Success");
            //                return RedirectToLocal(returnUrl);
            //            }
            //            else
            //            {
            //                Session["RoleID"] = GetData.RoleId;
            //                Session["UserID"] = GetData.UserID;
            //                Session["ID"] = GetData.Id;
            //                Session["UserName"] = GetData.UserName;
            //                Session["EmailID"] = GetData.EmailID;
            //                //Session["PO"] = GetData.PO;
            //                var GetFirst = _genericUAMRepository.Find(s => s.RoleId == GetData.RoleId).FirstOrDefault();
            //                var GetList = _genericUAMRepository.Find(s => s.UserMaster_Id == GetFirst.UserMaster_Id).ToList();
            //                var userAccesses = new List<UserAccessMaster>();
            //                foreach (var item in GetList.ToList())
            //                {
            //                    userAccesses.Add(new UserAccessMaster
            //                    {
            //                        RoleId = item.RoleId,
            //                        UserMaster_Id = GetData.Id,
            //                        MainMenuMaster_Id = item.MainMenuMaster_Id,
            //                        SubMenuMaster_Id = item.SubMenuMaster_Id,
            //                        IsDelete = false,
            //                        CreatedBy = "Admin",
            //                        CreatedDate = DateTime.Now 

            //                });
            //                }
            //                _genericUAMRepository.InsertRange(userAccesses);
            //                _genericUAMRepository.Save();
            //                this.AddNotification("Login Successfully!!", "Success");
            //                return RedirectToLocal(returnUrl);
            //            }
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, "Invalid OTP!!Your Account has been locked due to 3 failed attempts!");
            //            return View(model);
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid Captcha!!");
            //        return View(model);
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Something Is Wrong!!Try Agian!!");
            //    return View(model);
            //}
            return View();
        }
        [HttpGet]
        [NoDirectAccess]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ResetEmailPasswordViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var CheckRAt = CheckResetUser(model.UserEmailId);
            //        if (CheckRAt == "Success")
            //        {
            //            bool? GetCheck = _genericUserMasterRepository.Find(t => t.EmailID == model.UserEmailId && t.Active == true && t.Active == true).Any();
            //            if (GetCheck == true)
            //            {
            //                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            //                byte[] key = Guid.NewGuid().ToByteArray();
            //                string token = Convert.ToBase64String(time.Concat(key).ToArray());
            //                string userEIDCypert = Encrypt(model.UserEmailId);
            //                var callbackUrl = Url.Action("ChangePassword", "Account", new { userEID = userEIDCypert, Code = token }, protocol: Request.Url.Scheme);
            //                var dataUpdate = _genericUserMasterRepository.Find(t => t.EmailID == model.UserEmailId && t.Active == true).FirstOrDefault();
            //                //dataUpdate.PRestTimePunch = DateTime.Now;
            //                //dataUpdate.TokenId = token;
            //                _genericUserMasterRepository.Update(dataUpdate);
            //                _genericUserMasterRepository.Save();
            //                EmailService.EmailWithForgtPasswrd(model.UserEmailId, callbackUrl);
            //                ModelState.AddModelError(string.Empty, "Reset Password Link Sent your Registerd Email ID!! Your reset link expired after 15 mint or has already been used.");
            //            }
            //            else
            //            {
            //                return RedirectToAction("Login", "Account");
            //            }
            //        }
            //        else if (CheckRAt == "Not Exist")
            //        {
            //            ModelState.AddModelError(string.Empty, "Your EmailId does not Exits!!");
            //        }
            //        else if (CheckRAt == "Hang")
            //        {
            //            ModelState.AddModelError(string.Empty, "Something went Wrong!!");
            //        }
            //        else if (CheckRAt == "Locked")
            //        {
            //            ModelState.AddModelError(string.Empty, "You are Locked for today. Please come tomorrow & login");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            return View();
        }

        //for vsignup start
        [HttpGet]
        [NoDirectAccess]
        public ActionResult VSignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VSignup(VSignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CheckRAt = CheckVSignup(model.EmailId1);
                    //if (CheckRAt != "Success")
                    if (CheckRAt != "Success")
                    {
                        password = GetRandomPassword();
                        byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                        byte[] key = Guid.NewGuid().ToByteArray();
                        string token = Convert.ToBase64String(time.Concat(key).ToArray());
                        string fname = model.Name;
                        string fnameCon = model.NameContact;
                        string mobile = model.MobileNo;
                        string userEIDCypert = Encrypt(model.EmailId1);
                        var callbackUrl = Url.Action("Login", "Account", new { userEID = userEIDCypert, Code = token }, protocol: Request.Url.Scheme);
                        //var dataUpdate = _genericVendorMasterRepository.Find(t => t.EmailId1 == model.EmailId1 && t.IsDelete == false).FirstOrDefault();
                        //dataUpdate.PRestTimePunch = DateTime.Now;
                        //dataUpdate.TokenId = token;
                        //_genericUserMasterRepository.Update(dataUpdate);
                        //_genericUserMasterRepository.Save();

                        try
                        {
                            string Status = Signup(model.EmailId1, model.Name, model.NameContact, model.MobileNo, password).ToString();
                            //var t = _VRFTrBAL.SaveData(model, "Self Signup"); 
                            if (Status == "Success")
                            {
                                EmailService.EmailWithVSignup(model.EmailId1, model.Name, model.NameContact, model.MobileNo, password, callbackUrl);
                                ModelState.AddModelError(string.Empty, "Login Credential Sent to your Registerd Email ID!!");
                                //  this.AddNotification("Login Credential Sent to your Registerd Email ID!!", "Success");
                                // return RedirectToAction("Login");
                            }
                            else if (Status == "hang")
                            {
                                this.AddNotification("Something went wrong", NotificationType.ERROR);
                                return RedirectToAction("Index");
                            }
                            else if (Status == "Error")
                            {
                                ModelState.AddModelError(string.Empty, "You are already registerd!!");
                                 
                            }

                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message);
                            this.AddNotification("Something went wrong", NotificationType.ERROR);
                            return RedirectToAction("Login");
                        }


                    }
                    else if (CheckRAt == "Success")
                    {
                        ModelState.AddModelError(string.Empty, "Your EmailId already Exits!!");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        private object Signup(string emailId1, string name, string nameContact, string mobileNo, string pwd)
        {
            try
            {

                string CreatedDate = DateTime.Now.ToString();
                string Status = "Active";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter("SP_VSignup", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@EmailId1", emailId1);
                adp.SelectCommand.Parameters.AddWithValue("@Name", name);
                adp.SelectCommand.Parameters.AddWithValue("@NameContact", nameContact);
                adp.SelectCommand.Parameters.AddWithValue("@MobileNo", mobileNo);
                adp.SelectCommand.Parameters.AddWithValue("@Active", 1);
                adp.SelectCommand.Parameters.AddWithValue("@CreatedBy", "Self");
                adp.SelectCommand.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                adp.SelectCommand.Parameters.AddWithValue("@IsDelete", 0);
                adp.SelectCommand.Parameters.AddWithValue("@IsLocked", 0);
                adp.SelectCommand.Parameters.AddWithValue("@Password", pwd);
                adp.SelectCommand.Parameters.AddWithValue("@Status", Status);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string login = dt.Rows[0]["Users"].ToString();
                return login;
            }
            catch (Exception ex)
            {
                return "Hang";
            }
        }
        public string CheckVSignup(string EmailId1)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter("SP_VSignupCheck", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@EmailId1", EmailId1);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string login = dt.Rows[0]["Users"].ToString();
                return login;
            }
            catch (Exception ex)
            {
                return "Hang";
            }
        }

        public static string GetRandomPassword()
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
        //for vsignup end
        public string CheckResetUser(string EmailId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter("SP_ResetUserAttmptLogin", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@EmailId", EmailId);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string login = dt.Rows[0]["Users"].ToString();
                return login;
            }
            catch(Exception ex)
            {
                return "Hang";
            }
        }
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        [HttpGet]
        [NoDirectAccess]
        public ActionResult FirstTimeChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FirstTimeChangePassword(ChangePasswordViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    if (this.IsCaptchaValid("Validate your captcha"))
            //    {
            //        bool checkUser = _db.UserMasters.Any(s => s.EmailID == model.UserEID && s.Active == true);
            //        if (checkUser == true)
            //        {
            //            var dataUpdateF = _genericUserMasterRepository.Find(t => t.EmailID == model.UserEID && t.Active == true && t.Active == true).FirstOrDefault();
            //            dataUpdateF.Password = model.ConfirmPassword;
            //            //dataUpdateF.LoginFirstDT = DateTime.Now;
            //            dataUpdateF.ModifiedDate = DateTime.Now;
            //            _genericUserMasterRepository.Update(dataUpdateF);
            //            _genericUserMasterRepository.Save();
            //            ModelState.AddModelError(string.Empty, "Password Changed Successfully!!");
            //            return RedirectToAction("Login", "Account");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, "Your EmailId does not Exits!!");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid Captcha!!");
            //    }
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword(string userEID, string Code)
        {
            //if (userEID != null && Code != null)
            //{
            //    string OrglEmaiLID = Decrypt(userEID);
            //    bool? GetCheck = _genericUserMasterRepository.Find(t => t.EmailID == OrglEmaiLID  && t.Active == true && t.Active == true).Any();
            //    if (GetCheck == true)
            //    {
            //        var dataGet = _genericUserMasterRepository.Find(t => t.EmailID == OrglEmaiLID  && t.Active == true && t.Active == true).FirstOrDefault();
            //        TimeSpan? diff = DateTime.Now - dataGet.CreatedDate;
            //        if (diff.Value.Minutes > 15)
            //        {
            //            return RedirectToAction("ForgotPassword", "Account");
            //            //ModelState.AddModelError(string.Empty, "Your reset link expired,Please ReEnter EmailID!!");
            //        }
            //        else
            //        {
            //            return View();
            //        }
            //    }
            //    else
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    if (this.IsCaptchaValid("Validate your captcha"))
            //    {
            //        bool checkUser = _db.UserMasters.Any(s => s.EmailID == model.UserEID && s.Active == true);
            //        if (checkUser == true)
            //        {
            //            var dataUpdateF = _genericUserMasterRepository.Find(t => t.EmailID == model.UserEID && t.Active == true).FirstOrDefault();
            //            dataUpdateF.Password = model.ConfirmPassword;
            //            //dataUpdateF.LoginFirstDT = DateTime.Now;
            //            dataUpdateF.ModifiedDate = DateTime.Now;
            //            _genericUserMasterRepository.Update(dataUpdateF);
            //            _genericUserMasterRepository.Save();
            //            ModelState.AddModelError(string.Empty, "Password Changed Successfully!!");
            //            return RedirectToAction("Login", "Account");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, "Your EmailId does not Exits!!");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid Captcha!!");
            //    }
            //}
            //else
            //{

            //}
            return View();
        }

        [SessionExpire]
        [NoDirectAccess]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            // update logs for logout history
            var UserId = Session["UserID"];
            var mm = _db.Tbl_LoginLogs.Where(s => s.UserId.ToString() == UserId.ToString()).OrderByDescending(s => s.id).FirstOrDefault();
            if (mm != null)
            {
                mm.LogoutDate = DateTime.Now;
                mm.LoginToken = "";
                _genericLoginLogsRepository.Update(mm);
                _genericLoginLogsRepository.Save();
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}