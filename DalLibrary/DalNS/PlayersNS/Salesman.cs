﻿using AliKuli.UtilitiesNS;
using AliKuli.Extentions;

using ModelsClassLibrary.ModelsNS.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Seller is someone who can sell the scratch cards / service in the company
    /// The Salesmen will have a salesman category which will contain a list of categories
    /// for which there will be a list of Enums. Whenever the program will be initialized then
    /// these enumbs will be initialized as salesmen category. In other words, hard coded categories and
    /// soft categories will be present.
    /// </summary>
    public class Salesman : PlayerAbstract, ISalesman
    {



        #region SalesmanCategory
        [Display(Name = "Category")]
        public ICategory SalesmanCategory { get; set; }
        public Guid? SalesmanCategoryId { get; set; }
        
        #endregion
        #region SelfErrorCheck

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_SalesmanCategoryIsNullDoNothing();
        }

        private void Check_SalesmanCategoryIsNullDoNothing()
        {
            if (IsSalesmanCategoryIdNullOrEmpty)
            {
                //throw new Exception ("Salesman Category is null or empty.")
            }

            if (IsSalesmanCategoryNull)
            {
                //throw new Exception ("Salesman Category is null or empty.")

            }
        }

        #endregion
        #region Is...
        public bool IsSalesmanCategoryNull
        {
            get
            {
                return SalesmanCategory == null;
            }
        }

        public bool IsSalesmanCategoryIdNullOrEmpty
        {
            get
            {
                return SalesmanCategoryId.IsNullOrEmpty();
            }
        } 
        #endregion

        public void LoadFrom(ISalesman s)
        {
            base.LoadFrom(s as PlayerAbstract);
            SalesmanCategory = s.SalesmanCategory;
            SalesmanCategoryId = s.SalesmanCategoryId;
        }
    }
}