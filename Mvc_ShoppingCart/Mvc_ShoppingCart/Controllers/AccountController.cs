using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mvc_ShoppingCart.Models;
using System.Net;
using Newtonsoft.Json;
using DataAccess;
using BusinessLogic;
using System.Web.UI;
using DotNetOpenAuth.AspNet.Clients;
using Facebook;
using System.Dynamic;


namespace Mvc_ShoppingCart.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn


        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Facebook()
        {
            var fb = new Facebook.FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1575063379408402",
                client_secret = "d25a4799942e8f6d7a0cc6af79dbe624",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new Facebook.FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "1575063379408402",
                client_secret = "d25a4799942e8f6d7a0cc6af79dbe624",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session for farther use
            Session["AccessToken"] = accessToken;

            // update the facebook client with the access token so 
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information, like email, first name, middle name etc
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;

            try
            {
                new UserBL().Register(email, me.id, firstname, lastname);//register a new user
            }
            catch(Exception e)
            {
            }

            // Set the auth cookie
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (new UserBL().AuthenticateUserByUsernameAndPassword(model.Email, model.Password) != null)
                    {

                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        string x = User.Identity.Name;
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }
                

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie("", true);

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var response = Request["g-recaptcha-response"];

            const string secret = "6LezkQQTAAAAAIrUxyd9TPDwaQSa1OYJseJB_tPv";//google re captch secret key

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);


            if (captchaResponse.Success)
            {
                try
                {

                    new UserBL().Register(model.Email, model.Password, model.Name, model.Surname);//register a new user

                    // If we got this far, something failed, redisplay form
                    return RedirectToAction("Index", "Home");//back to home page
                }


                catch (MissingFieldException mfe)
                {
                    foreach (var modelValue in ModelState.Values)
                    {
                        modelValue.Errors.Clear();
                    }

                    if (mfe.Message == "Email already Registered")
                    {
                        ModelState.AddModelError("Email", "Email address already exists. Please enter a different email address.");
                    }
                    if (mfe.Message == "Week Password")
                    {
                        ModelState.AddModelError("Password", "Your Password is Week. Please try a Different Password");
                    }
                }
            }
            else ModelState.AddModelError(string.Empty, "Please Tick the Verification box to Proceed");
                
            




            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
