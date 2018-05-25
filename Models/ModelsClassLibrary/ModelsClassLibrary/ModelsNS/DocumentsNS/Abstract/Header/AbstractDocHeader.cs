using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
	public abstract class AbstractDocHeader : CommonWithId, IAbstractDocHeader
	{

        
        #region Properties


        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }






        [Display(Name = "Doc Number")]
        public long DocNo { get; set; }

        


        
        #endregion

        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_Date();
            Check_DocNo();
        }

        #region SelfErrorCheck Helpers
        private void Check_DocNo()
        {
            if (DocNo == -1)
                throw new Exception("The Doc No cannot be -1. AbstractDocHeader.Check_DocNo");
        }

        private void Check_Date()
        {
            if (Date.IsMinOrMax())
                throw new Exception("The date is min/max. AbstractDocHeader.Check_Date");
        }


        //public void LoadFrom(IAbstractDocHeader a)
        //{
        //    ICommonWithId aCom = this as ICommonWithId;
        //    aCom.LoadFrom(a as ICommonWithId);

        //    Date = a.Date;
        //    DocNo = a.DocNo;
        //}
        #endregion
        
        #endregion

        #region Make...
        public override string MakeUniqueName()
        {
            Name = DocNo.ToString();
            return Name;

        }
        
        #endregion

        #region LoadFrom
        public void LoadFrom(IAbstractDocHeader a)
        {
            
            Date = a.Date;
            DocNo = a.DocNo;
            base.LoadFrom((ICommonWithId) a);
            //ICommonWithId c = a as ICommonWithId;
            //c.LoadFrom(a as (ICommonWithId) c);

        }
        
        #endregion

    }
}