using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class PersonsController : EntityAbstractController<Person>
    {

        PersonBiz _personBiz;
        AddressBiz _addressBiz;
        //UserBiz _userBiz;
        public PersonsController(AbstractControllerParameters param, AddressBiz addressBiz)
            : base(addressBiz.PersonBiz, param)
        {
            _personBiz = addressBiz.PersonBiz;
            _addressBiz = addressBiz;
        }


        public AddressBiz AddressBiz
        {
            get
            {
                _addressBiz.IsNull();
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }

        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException();
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Person person = parm.Entity as Person;
            person.IsNullThrowException("Unable to unbox Person");

            person.SelectListPersonCategory = PersonBiz.PersonCategoryBiz.SelectList();
            person.PersonComplex.SelectListSonOfOrWifeOf = PersonBiz.SelectListSonOfWifeOf();
            person.PersonComplex.SelectListSex = PersonBiz.SelectListSex();

            person.SelectListBillAddress = AddressBiz.SelectListBillAddressCurrentUser();
            person.SelectListUsers = AddressBiz.UserBiz.SelectList();
            person.SelectListCountries = AddressBiz.CountryBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }


        public ActionResult IWannaTrade()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IWannaTrade(FormCollection fc)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in!");
            //create person record for user
            string returnUrl = Url.Action("Index", "Menus");
            try
            {
                var person = PersonBiz.Factory();
                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    person,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    MenuENUM.CreateDefault,
                    UserName,
                    UserId,
                    returnUrl);

                PersonBiz.CreateAndSave(parm);


            }
            catch (System.Exception e)
            {

                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

            }
            return Redirect(returnUrl);
        }

        //[HttpGet]
        //public ActionResult UpdateAndSaveDefaultAddress(
        //    string addressId,
        //    string searchFor,
        //    string isandForSearch,
        //    string selectedId,
        //    string returnUrl,
        //    MenuENUM menuEnum = MenuENUM.IndexDefault,
        //    SortOrderENUM sortBy = SortOrderENUM.Item1_Asc,
        //    bool print = false,
        //    bool isMenu = false,
        //    string menuPathMainId = "")
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
        //    addressId.IsNullOrWhiteSpaceThrowArgumentException();
        //    try
        //    {
        //        PersonBiz.UpdateAndSaveDefaultAddress(UserId, addressId);
        //        return RedirectToAction("Index", "Addresses", new
        //        {
        //            id = addressId,
        //            searchFor = searchFor,
        //            isandForSearch = isandForSearch,
        //            selectedId = selectedId,
        //            returnUrl = returnUrl,
        //            menuEnum = menuEnum,
        //            sortBy = sortBy
        //        });

        //    }
        //    catch (System.Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}