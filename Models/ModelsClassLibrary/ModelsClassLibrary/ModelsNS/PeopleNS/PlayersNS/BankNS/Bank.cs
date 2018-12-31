using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.Interfaces.PeopleNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Bank is owner who has privilages to:
    ///     Create money in the system.
    ///     The system does not check the account of the bank when it receives money.
    ///     
    /// </summary>
    public class Bank : PlayerAbstract, IPlayer
    {


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Bank;
        }



        [Display(Name = "Cateogry")]
        [MaxLength(128)]
        public virtual string BankCategoryId { get; set; }
        public virtual BankCategory BankCategory { get; set; }






        [NotMapped]
        public SelectList SelectListBankCategory { get; set; }




        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
            Bank banker = ic as Bank;
            banker.IsNullThrowException("Unable to unbox banker");

            PersonId = banker.PersonId;
            BankCategoryId = banker.BankCategoryId;

        }
    }
}