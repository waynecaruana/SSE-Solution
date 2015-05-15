using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_ShoppingCart.Models;
using PayPal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcApplication1.Controllers
{
    public class PaypalController : Controller
    {
        //
        // GET: /Paypal/

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult PaymentWithCreditCard()
        //{
        //    //create and item for which you are taking payment
        //    //if you need to add more items in the list
        //    //Then you will need to create multiple item objects or use some loop to instantiate object
        //    Item item = new Item();
        //    item.name = "Demo Item";
        //    item.currency = "USD";
        //    item.price = "5";
        //    item.quantity = "1";
        //    item.sku = "sku";

        //    //Now make a List of Item and add the above item to it
        //    //you can create as many items as you want and add to this list
        //    List<Item> itms = new List<Item>();
        //    itms.Add(item);
        //    ItemList itemList = new ItemList();
        //    itemList.items = itms;

        //    //Address for the payment
        //    Address billingAddress = new Address();
        //    billingAddress.city = "NewYork";
        //    billingAddress.country_code = "US";
        //    billingAddress.line1 = "23rd street kew gardens";
        //    billingAddress.postal_code = "43210";
        //    billingAddress.state = "NY";


        //    //Now Create an object of credit card and add above details to it
        //    CreditCard crdtCard = new CreditCard();
        //    crdtCard.billing_address = billingAddress;
        //    crdtCard.cvv2 = "874";
        //    crdtCard.expire_month = 1;
        //    crdtCard.expire_year = 2020;
        //    crdtCard.first_name = "Aman";
        //    crdtCard.last_name = "Thakur";
        //    crdtCard.number = "1234567890123456";
        //    crdtCard.type = "discover";

        //    // Specify details of your payment amount.
        //    Details details = new Details();
        //    details.shipping = "1";
        //    details.subtotal = "5";
        //    details.tax = "1";

        //    // Specify your total payment amount and assign the details object
        //    Amount amnt = new Amount();
        //    amnt.currency = "USD";
        //    // Total = shipping tax + subtotal.
        //    amnt.total = "7";
        //    amnt.details = details;

        //    // Now make a trasaction object and assign the Amount object
        //    Transaction tran = new Transaction();
        //    tran.amount = amnt;
        //    tran.description = "Description about the payment amount.";
        //    tran.item_list = itemList;
        //    tran.invoice_number = "your invoice number which you are generating";

        //    // Now, we have to make a list of trasaction and add the trasactions object
        //    // to this list. You can create one or more object as per your requirements

        //    List<Transaction> transactions = new List<Transaction>();
        //    transactions.Add(tran);

        //    // Now we need to specify the FundingInstrument of the Payer
        //    // for credit card payments, set the CreditCard which we made above

        //    FundingInstrument fundInstrument = new FundingInstrument();
        //    fundInstrument.credit_card = crdtCard;

        //    // The Payment creation API requires a list of FundingIntrument

        //    List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
        //    fundingInstrumentList.Add(fundInstrument);

        //    // Now create Payer object and assign the fundinginstrument list to the object
        //    Payer payr = new Payer();
        //    payr.funding_instruments = fundingInstrumentList;
        //    payr.payment_method = "credit_card";

        //    // finally create the payment object and assign the payer object & transaction list to it
        //    Payment pymnt = new Payment();
        //    pymnt.intent = "sale";
        //    pymnt.payer = payr;
        //    pymnt.transactions = transactions;

        //    try
        //    {
        //        //getting context from the paypal, basically we are sending the clientID and clientSecret key in this function 
        //        //to the get the context from the paypal API to make the payment for which we have created the object above.

        //        //Code for the configuration class is provided next

        //        // Basically, apiContext has a accesstoken which is sent by the paypal to authenticate the payment to facilitator account. An access token could be an alphanumeric string

        //        APIContext apiContext = Configuration.GetAPIContext();

        //        // Create is a Payment class function which actually sends the payment details to the paypal API for the payment. The function is passed with the ApiContext which we received above.

        //        Payment createdPayment = pymnt.Create(apiContext);

        //        //if the createdPayment.State is "approved" it means the payment was successfull else not

        //        if (createdPayment.state.ToLower() != "approved")
        //        {
        //            return View("FailureView");
        //        }
        //    }
        //    catch (PayPal.PayPalException ex)
        //    {
        //        Logger.Log("Error: " + ex.Message);
        //        return View("FailureView");
        //    }

        //    return View("SuccessView");
        //}

        //public ActionResult PaymentWithPaypal()
        //{
        //    //getting the apiContext as earlier
        //    APIContext apiContext = Configuration.GetAPIContext();

        //    try
        //    {
        //        string payerId = Request.Params["PayerID"];

        //        if (string.IsNullOrEmpty(payerId))
        //        {
        //            //this section will be executed first because PayerID doesn't exist

        //            //it is returned by the create function call of the payment class

        //            // Creating a payment

        //            // baseURL is the url on which paypal sendsback the data.

        //            // So we have provided URL of this controller only


        //            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

                    
        //            //guid we are generating for storing the paymentID received in session

        //            //after calling the create function and it is used in the payment execution

        //            var guid = Convert.ToString((new Random()).Next(100000));

        //            //CreatePayment function gives us the payment approval url

        //            //on which payer is redirected for paypal acccount payment

        //            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

        //            //get links returned from paypal in response to Create function call

        //            var links = createdPayment.links.GetEnumerator();

        //            string paypalRedirectUrl = null;

        //            while (links.MoveNext())
        //            {
        //                Links lnk = links.Current;

        //                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
        //                {
        //                    //saving the payapalredirect URL to which user will be redirected for payment
        //                    paypalRedirectUrl = lnk.href;
        //                }
        //            }

        //            // saving the paymentID in the key guid
        //            Session.Add(guid, createdPayment.id);

        //            return Redirect(paypalRedirectUrl);
        //        }
        //        else
        //        {
        //            // This section is executed when we have received all the payments parameters

        //            // from the previous call to the function Create

        //            // Executing a payment

        //            var guid = Request.Params["guid"];

        //            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

        //            if (executedPayment.state.ToLower() != "approved")
        //            {
        //                return View("FailureView");
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log("Error"+ ex.Message);
        //        return View("FailureView");
        //    }

        //    return View("SuccessView");
        //}

        //private PayPal.Api.Payment payment;

        //private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        //{
        //    var paymentExecution = new PaymentExecution() { payer_id = payerId };
        //    this.payment = new Payment() { id = paymentId };
        //    return this.payment.Execute(apiContext, paymentExecution);
        //}

        //private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        //{

        //    //similar to credit card create itemlist and add item objects to it
        //    var itemList = new ItemList() { items = new List<Item>() };

        //    itemList.items.Add(new Item()
        //    {
        //        name = "Item Name",
        //        currency = "USD",
        //        price = "5",
        //        quantity = "1",
        //        sku = "sku"
        //    });

        //    var payer = new Payer() { payment_method = "paypal" };

        //    // Configure Redirect Urls here with RedirectUrls object
        //    var redirUrls = new RedirectUrls()
        //    {
        //        cancel_url = redirectUrl,
        //        return_url = redirectUrl
        //    };

        //    // similar as we did for credit card, do here and create details object
        //    var details = new Details()
        //    {
        //        tax = "1",
        //        shipping = "1",
        //        subtotal = "5"
        //    };

        //    // similar as we did for credit card, do here and create amount object
        //    var amount = new Amount()
        //    {
        //        currency = "USD",
        //        total = "7", // Total must be equal to sum of shipping, tax and subtotal.
        //        details = details
        //    };

        //    var transactionList = new List<Transaction>();

        //    transactionList.Add(new Transaction()
        //    {
        //        description = "Transaction description.",
        //        invoice_number = "your invoice number",
        //        amount = amount,
        //        item_list = itemList
        //    });

        //    this.payment = new Payment()
        //    {
        //        intent = "sale",
        //        payer = payer,
        //        transactions = transactionList,
        //        redirect_urls = redirUrls
        //    };

        //    // Create a payment using a APIContext
        //    return this.payment.Create(apiContext);

        //}

        public ActionResult CreatePayment(string description, decimal price, decimal tax = 0, decimal shipping = 0)
        {
            var viewData = new PayPalViewData();
            var guid = Guid.NewGuid().ToString();

            var paymentInit = new Payment
            {
                intent = "authorize",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "USD",
                            total = (price + tax + shipping).ToString(),
                            details = new Details
                            {
                                subtotal = price.ToString(),
                                tax = tax.ToString(),
                                shipping = shipping.ToString()
                            }
                        },
                        description = description
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = Utilities.ToAbsoluteUrl(HttpContext, String.Format("~/paypal/confirmed?id={0}", guid)),
                    cancel_url = Utilities.ToAbsoluteUrl(HttpContext, String.Format("~/paypal/canceled?id={0}", guid)),
                },
            };

            viewData.JsonRequest = JObject.Parse(paymentInit.ConvertToJson()).ToString(Formatting.Indented);

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var createdPayment = paymentInit.Create(apiContext);

                var approvalUrl = createdPayment.links.ToArray().FirstOrDefault(f => f.rel.Contains("approval_url"));

                if (approvalUrl != null)
                {
                    Session.Add(guid, createdPayment.id);

                    return Redirect(approvalUrl.href);
                }

                viewData.JsonResponse = JObject.Parse(createdPayment.ConvertToJson()).ToString(Formatting.Indented);

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }

        public ActionResult Confirmed(Guid id, string token, string payerId)
        {
            var viewData = new ConfirmedViewData
            {
                Id = id,
                Token = token,
                PayerId = payerId
            };

            var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            var payment = new Payment()
            {
                id = (string)Session[id.ToString()],
            };

            var executedPayment = payment.Execute(apiContext, new PaymentExecution { payer_id = payerId });

            viewData.AuthorizationId = executedPayment.transactions[0].related_resources[0].authorization.id;
            viewData.JsonRequest = JObject.Parse(payment.ConvertToJson()).ToString(Formatting.Indented);
            viewData.JsonResponse = JObject.Parse(executedPayment.ConvertToJson()).ToString(Formatting.Indented);

            return View(viewData);
        }

        public ActionResult Canceled(Guid id, string token, string payerId)
        {
            return Content("Asshole.");
        }

        public ActionResult Capture(string authorizationId)
        {
            var viewData = new PayPalViewData();

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var authorization = Authorization.Get(apiContext, authorizationId);

                if (authorization != null)
                {
                    var total = Convert.ToDecimal(authorization.amount.total);

                    var capture = authorization.Capture(apiContext, new Capture
                       {
                           is_final_capture = true,
                           amount = new Amount
                           {
                               currency = "USD",
                               total = (total + (total * .05m)).ToString("f2")
                           },
                       });


                    viewData.JsonResponse = JObject.Parse(capture.ConvertToJson()).ToString(Formatting.Indented);

                    return View("Success", viewData);
                }

                viewData.ErrorMessage = "Could not find previous authorization.";

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }

        public ActionResult Void(string authorizationId)
        {
            var viewData = new PayPalViewData();

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var authorization = Authorization.Get(apiContext, authorizationId);

                if (authorization != null)
                {
                    var voidedAuthorization = authorization.Void(apiContext);

                    viewData.JsonResponse = JObject.Parse(voidedAuthorization.ConvertToJson()).ToString(Formatting.Indented);

                    return View(viewData);
                }

                viewData.ErrorMessage = "Could not find previous authorization.";

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }
    }
}

