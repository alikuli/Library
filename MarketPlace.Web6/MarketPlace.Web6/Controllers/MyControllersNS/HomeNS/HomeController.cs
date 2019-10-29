using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.CostNS;
using ModelsClassLibrary.ModelsNS.CostNS.CostStateNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.SuperLayerNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    public class HomeController : AbstractController
    {
        SuperBiz _superBiz;
        public HomeController(AbstractControllerParameters param, SuperBiz superBiz)
            : base(param)
        {
            _superBiz = superBiz;
        }


        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;

                return _superBiz;
            }
        }
        UserBiz UserBiz
        {
            get
            {
                return SuperBiz.UserBiz;
            }
        }



        public ActionResult Cost()
        {
            ICostClass costclass = new PaymentForContact();

            costclass.Heading = "If you choose to continue, you will get information to help you make a decision.";
            costclass.DetailOfInfo = "You will receive the deliveryman's phone, email, webpage. Also, you will see all the number of completed tasks and any comments by users about him.";
            //costclass.Amount = 10m;
            return View(costclass);
        }

        [HttpPost]
        public ActionResult Cost(string button, CostClass_Abstract costclass)
        {
            switch (button.ToLower())
            {
                case "continuebtn":
                    costclass.Result = "In Continue";
                    break;
                case "stopbtn":
                    costclass.Result = "In stop";
                    break;

                default:
                    costclass.Result = "In Default";
                    break;
            }
            return View(costclass);

        }
        public PartialViewResult AddUserPartialView()
        {
            return PartialView("AddUserPartialView", new AddUserViewModel());
        }

        [HttpPost]
        public JsonResult AddUserInfo(AddUserViewModel model)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                //isSuccess = Save data here return boolean
            }
            return Json(new { result = isSuccess, responseText = "Something wrong!" });
        }
        public ActionResult Index()
        {
            try
            {
                ConfigManagerHelper ConfigManagerHelper = new AliKuli.UtilitiesNS.ConfigManagerHelper();

                ApplicationUser user = UserBiz.InitializeSystem();

                //UserBiz.SaveChanges();

                //add person
                Person person = addPerson(ConfigManagerHelper, user);

                addCustomer(ConfigManagerHelper, person);
                addOwner(ConfigManagerHelper, person);
                addMailer(ConfigManagerHelper, person);
                addSalesman(ConfigManagerHelper, person);


                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject;
                globalObject.IsNullThrowException("Global Object is null");
                ControllerCreateEditParameter param = new ControllerCreateEditParameter();
                param.Entity = user as ICommonWithId;
                param.GlobalObject = globalObject;

                UserBiz.UpdateAndSave(param);


                //UserBiz.UpdateAndSave(user);
                ErrorsGlobal.ClearAllErrors();
                ErrorsGlobal.Messages.Clear();
                return View();
            }
            catch (Exception e)
            {


                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong while creating Index in Home.", "Home"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                string responseCode = Response.StatusCode.ToString();
                string message = string.Format("Response Code: {0}. {1}", responseCode, ErrorsGlobal.ToString());
                return RedirectToAction("Index", "Errors", new { message = message });
            }
        }

        private Person addPerson(ConfigManagerHelper ConfigManagerHelper, ApplicationUser user)
        {
            Person person = SuperBiz.PersonBiz.FindByName(ConfigManagerHelper.AdminName);
            if (person.IsNull())
            {
                person = SuperBiz.PersonBiz.Factory() as Person;
                person.IsNullThrowException();
                person.Name = ConfigManagerHelper.AdminName;
                SuperBiz.PersonBiz.Create(person);
            }
            else
            {
                user.PersonId = person.Id;
            }
            return person;
        }

        private void addCustomer(ConfigManagerHelper ConfigManagerHelper, Person person)
        {
            Customer customer = SuperBiz.CustomerBiz.FindByName(ConfigManagerHelper.AdminName);

            //add customer
            if (customer.IsNull())
            {
                customer = SuperBiz.CustomerBiz.Factory() as Customer;
                customer.Name = ConfigManagerHelper.AdminName;
                customer.PersonId = person.Id;
                SuperBiz.CustomerBiz.Create(customer);

            }
            else
            {
                customer.PersonId = person.Id;
                SuperBiz.CustomerBiz.Update(customer);


            }
        }

        private void addOwner(ConfigManagerHelper ConfigManagerHelper, Person person)
        {
            Owner owner = SuperBiz.OwnerBiz.FindByName(ConfigManagerHelper.AdminName);

            //add owner
            if (owner.IsNull())
            {
                owner = SuperBiz.OwnerBiz.Factory() as Owner;
                owner.Name = ConfigManagerHelper.AdminName;
                owner.PersonId = person.Id;
                SuperBiz.OwnerBiz.Create(owner);

            }
            else
            {
                owner.PersonId = person.Id;
                SuperBiz.OwnerBiz.Update(owner);


            }
        }

        private void addSalesman(ConfigManagerHelper ConfigManagerHelper, Person person)
        {
            Salesman salesman = SuperBiz.SalesmanBiz.FindByName(ConfigManagerHelper.AdminName);

            //add salesman
            if (salesman.IsNull())
            {
                salesman = SuperBiz.SalesmanBiz.Factory() as Salesman;
                salesman.Name = ConfigManagerHelper.AdminName;
                salesman.PersonId = person.Id;
                
                SuperBiz.SalesmanBiz.Create(salesman);

            }
            else
            {
                salesman.PersonId = person.Id;
                SuperBiz.SalesmanBiz.Update(salesman);


            }
        }


        private void addMailer(ConfigManagerHelper ConfigManagerHelper, Person person)
        {
            Mailer mailer = SuperBiz.MailerBiz.FindByName(ConfigManagerHelper.AdminName);

            //add mailer
            if (mailer.IsNull())
            {
                mailer = SuperBiz.MailerBiz.Factory() as Mailer;
                mailer.Name = ConfigManagerHelper.AdminName;
                mailer.PersonId = person.Id;
                mailer.TrustLevelEnum = EnumLibrary.EnumNS.VerificationNS.TrustLevelENUM.Level5;
                SuperBiz.MailerBiz.Create(mailer);

            }
            else
            {
                mailer.PersonId = person.Id;
                SuperBiz.MailerBiz.Update(mailer);


            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {

            try
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            catch (Exception e)
            {


                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong in About.", "Home"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Print()
        {
            return View();
        }

        public ActionResult InitializeSystem()
        {
            try
            {
                SuperBiz.InitializeDb();
                return View("InitializeSystem");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }



        public ActionResult ProductMenu()
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");
            GlobalObject globalObject = getGlobalObject();
            if (!globalObject.IsOwner)
                throw new Exception("You are not authourized to view this report.");

            return View(globalObject);
        }


        public ActionResult CashMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");

        }
        public ActionResult JobRequestMenu()
        {
            try
            {

                GlobalObject globalObject = getGlobalObject();
                if (globalObject.IsSuperSalesman || globalObject.IsAdmin)
                {
                }
                else
                {
                    throw new Exception("You are not authourized to view this report.");
                }

                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");

        }


        public ActionResult MenusMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsAdmin)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }

        public ActionResult MiscMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }
        public ActionResult PlayersMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsAdmin)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }
        public ActionResult AdminMenu()
        {

            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsAdmin)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }

        public ActionResult DeliverymanMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsDeliveryman)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");

        }

        public ActionResult SellerMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsOwner)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }

        public ActionResult CustomerMenu()
        {

            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsCustomer)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }

        public ActionResult SalesmanMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsSalesman)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }

        public ActionResult MailerMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsMailer)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");
        }


        public ActionResult BankMenu()
        {
            try
            {
                GlobalObject globalObject = getGlobalObject();
                if (!globalObject.IsBank)
                    throw new Exception("You are not authourized to view this report.");
                return View(globalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");

        }

        private GlobalObject getGlobalObject()
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");

            GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject;
            if (globalObject.IsNull())
            {
                globalObject = new ModelsClassLibrary.ModelsNS.GlobalObjectNS.GlobalObject();
            }


            return globalObject;
        }

        public ActionResult RunDailyTasks()
        {
            try
            {
                SuperBiz.Run_Daily_Task();
                return View();
            }
            catch (Exception e)
            {
                
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            return View("Error");

        }
    }
}