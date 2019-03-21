using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.MessagesNS
{
    [NotMapped]
    public class MessageParameter
    {
        public MessageParameter()
        {

        }
        public MessageParameter(string subject, string body, string menuPathMainId, string productId, string productChildId, string returnUrl, MessageENUM messageEnum = MessageENUM.Unknown, MenuENUM menuEnum = MenuENUM.Unknown)
        {
            MenuPathMainId = menuPathMainId;
            //UserId = userId;
            ProductId = productId;
            ProductChildId = productChildId;
            MessageEnum = messageEnum;
            MenuEnum = menuEnum;
            Subject = subject;
            Body = body;
            ReturnUrl = returnUrl;
        }

        public Person FromPerson { get; set; }
        public string ReturnUrl { get; set; }
        public string MenuPathMainId { get; set; }
        public string ProductId { get; set; }
        public string ProductChildId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MessageENUM MessageEnum { get; set; }
        public MenuENUM MenuEnum { get; set; }

        //these are the initial items that are printed in the view.
        public ICollection<CheckBoxItem> ProductChildCheckBoxItemList { get { return LoadProductChildrenIntoCheckItem(); } }

        /// <summary>
        /// The checked items return here
        /// </summary>
        public ICollection<CheckBoxItem> ChildProductCheckItems { get; set; }

        public List<string> GetProductChildrenAddyFromCheckItems()
        {
            List<string> productChildrenAddy = new List<string>();
            if (!ChildProductCheckItems.IsNullOrEmpty())
            {
                foreach (CheckBoxItem item in ChildProductCheckItems)
                {
                    if (item.IsTrue)
                    {
                        string addy = item.ProductChildId;
                        addy.IsNullOrWhiteSpaceThrowException("Child item in Check Box Item address is null");
                        productChildrenAddy.Add(addy);

                    }
                }
            }
            return productChildrenAddy;
        }
        /// <summary>
        /// This is the number of people who have liked any MenuPath, product, ProductChild in the subset.
        /// </summary>
        [Display(Name = "Number Of Like Unlike People")]
        public long NumberOfLikeUnlikePeople { get; set; }

        /// <summary>
        /// This is the number vendoes who own a product in this subset.
        /// </summary>
        [Display(Name = "Number Of Product People")]
        public long NumberOfProductPeople { get; set; }


        /// <summary>
        /// This is a subset of the people who own a product child in the subset.
        /// </summary>
        [Display(Name = "Number Of product child owners")]
        public long NumberOfProductChildPeople { get; set; }


        /// <summary>
        /// This is the total number of people
        /// </summary>
        [Display(Name = "Number Of Total People")]
        public long NumberOfTotalPeople { get; set; }

        /// <summary>
        /// These are the number of products belonging to the user/person that are a part of the subset
        /// </summary>
        [Display(Name = "Number Of Products Belonging To Person")]
        public long NumberOfProductsBelongingToUserFrom { get; set; }


        /// <summary>
        /// These are the number of Child Products belonging to the person/user in the subset
        /// </summary>
        [Display(Name = "Number Of Child Products Belonging To Person")]
        public long NumberOfChildProductsBelongingToUserFrom { get; set; }


        /// <summary>
        /// These are the people who like/unliked a Menu or product or product child in the subset.
        /// </summary>
        [Display(Name = "Like Unlike People")]
        public ICollection<Person> LikeUnlikePeople { get; set; }


        /// <summary>
        /// These are the people who own a product in the subset.
        /// </summary>
        [Display(Name = "Product People")]
        public ICollection<Person> ProductPeople { get; set; }


        /// <summary>
        /// These are the people who own a product child in the subset.
        /// </summary>
        [Display(Name = "Product Child People")]
        public ICollection<Person> ProductChildPeople { get; set; }


        /// <summary>
        /// This is the total number of unique people
        /// </summary>
        [Display(Name = "Total People")]
        public ICollection<Person> TotalPeople { get; set; }

        /// <summary>
        /// These are the products belonging to the person
        /// </summary>
        [Display(Name = "Products belonging to person")]
        public ICollection<Product> ProductsBelongingToUserFrom { get; set; }

        /// <summary>
        /// These are the child products belonging to the person
        /// </summary>
        [Display(Name = "Child Products belonging to person")]
        public ICollection<ProductChild> ProductChildrenBelongingToUserFrom { get; set; }

        /// <summary>
        /// this contains the product child and a link to its landing page.
        /// </summary>
        //public ICollection<OwnersChildProductsWithLink> OwnersChildProductsWithLinks { get; set; }

        /// <summary>
        /// This contains all the products of the FromTo Person which fit the users.
        /// </summary>
        [NotMapped]
        public SelectList SelectListMyProducts { get; set; }


        public List<CheckBoxItem> LoadProductChildrenIntoCheckItem()
        {
            List<CheckBoxItem> checkBoxesLst = new List<CheckBoxItem>();
            if (!ProductChildrenBelongingToUserFrom.IsNull())
            {
                foreach (ProductChild pc in ProductChildrenBelongingToUserFrom)
                {

                    CheckBoxItem cbi = new CheckBoxItem(pc);
                }
            }
            return checkBoxesLst;
        }
    }
}
