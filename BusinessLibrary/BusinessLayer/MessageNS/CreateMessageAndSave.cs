using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.MessagesNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;


namespace UowLibrary.PlayersNS.MessageNS
{
    public partial class MessageBiz
    {

        HashSet<Person> peopleInProductChildren;
        HashSet<Person> peopleInProducts;
        HashSet<Person> peopleInLikeUnlike;
        HashSet<Person> totalPeopleHashSet;
        HashSet<Product> productsBelongingToUserFrom;
        HashSet<ProductChild> childProductsBelongingToUserFrom;

        //HashSet<Person> toPeopleHashSet;


        bool countIsSet { get; set; }


        public long NumberOfProductsBelongingToUserFrom
        {
            get
            {
                if (!countIsSet)
                    throw new Exception("Count is not set");

                if (productsBelongingToUserFrom.IsNullOrEmpty())
                    return 0;

                return productsBelongingToUserFrom.LongCount();
            }
        }

        public long NumberOfChildProductsBelongingToUserFrom
        {
            get
            {
                if (!countIsSet)
                    throw new Exception("Count is not set");

                if (childProductsBelongingToUserFrom.IsNullOrEmpty())
                    return 0;

                return childProductsBelongingToUserFrom.LongCount();
            }
        }

        public long NumberOfProductPeople
        {
            get
            {
                if (!countIsSet)
                    throw new Exception("Count is not set");

                if (peopleInProducts.IsNullOrEmpty())
                    return 0;

                return peopleInProducts.LongCount();
            }
        }


        public long NumberOfPeopleInLikeUnlike
        {
            get
            {
                if (!countIsSet)
                    throw new Exception("Count is not set");

                if (peopleInLikeUnlike.IsNullOrEmpty())
                    return 0;

                return peopleInLikeUnlike.LongCount();
            }
        }

        public long NumberOfTotalPeople
        {
            get
            {
                if (!countIsSet)
                    throw new Exception("Count is not set");

                if (totalPeopleHashSet.IsNullOrEmpty())
                    return 0;

                return totalPeopleHashSet.LongCount();
            }
        }

        public List<Message> GetAllMessagesForUser(string userId)
        {
            throw new NotImplementedException();
            //userId.IsNullOrWhiteSpaceThrowException("userId");
            //Person person = UserBiz.GetPersonFor(userId);
            //person.IsNullThrowException("Person not found");
            //string personId = person.Id;

            ////now get all the Message for this person
            //List<Message> messages = FindAll().ToList();

            //if (messages.IsNullOrEmpty())
            //    return messages;

            //List<Message> selectedMsgs = new List<Message>();
            //foreach (Message msg in messages)
            //{
            //    if (msg.FromPersonId == personId)
            //    {
            //        selectedMsgs.Add(msg);
            //        continue;
            //    }

            //    if (msg.ToPeople.Any(x => x.Id == personId))
            //    {
            //        selectedMsgs.Add(msg);
            //    }
            //}


            //return selectedMsgs;

        }

        protected Owner OwnerForUser
        {
            get
            {
                if (UserId.IsNullOrWhiteSpace())
                    return null;

                Owner owner = OwnerBiz.GetOwnerForUser(UserId);
                return owner;
            }
        }

        /// <summary>
        /// Creates and saves the message
        /// </summary>
        /// <param name="fromUserId"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="menuPathMainId"></param>
        /// <param name="productId"></param>
        /// <param name="productChildId"></param>
        /// <param name="messageEnum"></param>
        /// <param name="menuEnum"></param>
        public void CreateMessageAndSave(MessageParameter param)
        {
            CreateMessageAndSave(param.Subject, param.Body, param.MenuPathMainId, param.ProductId, param.ProductChildId, param.MessageEnum, param.MenuEnum);
        }
        public void CreateMessageAndSave(string subject, string body, string menuPathMainId, string productId, string productChildId, MessageENUM messageEnum = MessageENUM.Unknown, MenuENUM menuEnum = MenuENUM.Unknown)
        {
            throw new NotImplementedException();
            //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

            //if (messageEnum == MessageENUM.Unknown)
            //    throw new Exception("messageEnum.Unknown");


            //MessageParameter messageParameter = GetPeopleListCount(menuPathMainId, productId, productChildId, menuEnum);
            //List<ProductChild> pChildSelectedForAdvertisment = getProductChildrenFromCheckItem(messageParameter);
            ////you need to fix the toPeople
            //toPeopleHashSet = new HashSet<Person>();
            //Message message = new Message(messageParameter.FromPerson, toPeopleHashSet.ToList(), subject, body, messageEnum, pChildSelectedForAdvertisment);
            //message.SelfErrorCheck();

            ////wire it up
            ////WireUpMessageWithPeople(mpm, productId, productChildId, toPeopleHashSet.ToList(), message, fromPerson, menuEnum);

            //CreateAndSave(message);
        }

        //this recieves a list of selected product children Ids from MessageParameter and then gets the 
        //product children and returns them
        private List<ProductChild> getProductChildrenFromCheckItem(MessageParameter msgParam)
        {
            List<ProductChild> selectedList = new List<ProductChild>();

            if (!msgParam.GetProductChildrenAddyFromCheckItems().IsNullOrEmpty())
            {
                foreach (string pChildAddy in msgParam.GetProductChildrenAddyFromCheckItems())
                {
                    ProductChild productChild = ProductChildBiz.Find(pChildAddy);
                    productChild.IsNullThrowException("productChild");
                    selectedList.Add(productChild);
                }
            }

            return selectedList;
        }

        /// <summary>
        /// This method assumes the current user is the sender.
        /// </summary>
        /// <param name="menuPathMainId"></param>
        /// <param name="productId"></param>
        /// <param name="productChildId"></param>
        /// <param name="menuEnum"></param>
        /// <returns></returns>
        //public MessageParameter GetPeopleListCount(string menuPathMainId, string productId, string productChildId, MenuENUM menuEnum)
        //{

        //    return makePeopleLists(productId, productChildId, menuEnum, menuPathMainId);
        //}

        /// <summary>
        /// This method assumes current user is sender. Note all parameters will not have value. It depends which menu level they are sent from
        /// </summary>
        /// <param name="menuPathMainId"></param>
        /// <param name="productId"></param>
        /// <param name="productChildId"></param>
        /// <param name="menuEnum"></param>
        /// <returns></returns>
        public MessageParameter GetPeopleListCount(string menuPathMainId, string productId, string productChildId, MenuENUM menuEnum)
        {
            countIsSet = false;
            if (menuEnum == MenuENUM.Unknown)
                throw new Exception("MenuENUM.Unknown");

            Person fromPerson = UserBiz.GetPersonFor(UserId);
            fromPerson.IsNullThrowException("fromPerson");


            //initialize the lists
            initializeHashSets();

            MenuPathMain mpm;

            switch (menuEnum)
            {

                case MenuENUM.IndexMenuPath1:
                    mpm = getMpm(menuPathMainId);
                    getPeopeFromMp1Etc(mpm.MenuPath1Id);
                    break;


                case MenuENUM.IndexMenuPath2:
                    mpm = getMpm(menuPathMainId);
                    getPeopleFromMp2Etc(mpm.MenuPath1Id, mpm.MenuPath2Id);
                    break;


                case MenuENUM.IndexMenuPath3:
                    mpm = getMpm(menuPathMainId);
                    getPeopleFromMp3Etc(mpm);
                    break;


                case MenuENUM.IndexMenuProduct:
                    getPeopleFromProductEtc(productId);
                    break;


                case MenuENUM.IndexMenuProductChild:
                    getPeopleFromProductChildOwners(productChildId);
                    break;


                case MenuENUM.IndexDefault:
                default:
                    break;
            }


            //Add to total people
            addupAllListsIntoTotalPeopleAndClean(fromPerson);

            MessageParameter msgParam = new MessageParameter();
            msgParam.FromPerson = fromPerson;

            msgParam.NumberOfProductPeople = NumberOfProductPeople;
            msgParam.NumberOfLikeUnlikePeople = NumberOfPeopleInLikeUnlike;
            msgParam.NumberOfTotalPeople = NumberOfTotalPeople;
            msgParam.NumberOfChildProductsBelongingToUserFrom = NumberOfChildProductsBelongingToUserFrom;
            msgParam.NumberOfProductsBelongingToUserFrom = NumberOfProductsBelongingToUserFrom;

            msgParam.ProductChildPeople = peopleInProductChildren;
            msgParam.ProductPeople = peopleInProducts;
            msgParam.LikeUnlikePeople = peopleInLikeUnlike;
            msgParam.TotalPeople = totalPeopleHashSet;
            msgParam.ProductsBelongingToUserFrom = productsBelongingToUserFrom;
            msgParam.ProductChildrenBelongingToUserFrom = childProductsBelongingToUserFrom;
            msgParam.ChildProductCheckItems = loadChildProductsIntoCheckItems(childProductsBelongingToUserFrom);
            //msgParam.OwnersChildProductsWithLinks = loadOwnersChildProductsWithLinks(childProductsBelongingToUserFrom);

            return msgParam;
        }

        /// <summary>
        /// This method loads the child products into the checklist for display in the Sale message
        /// </summary>
        /// <param name="childProductsBelongingToUserFrom"></param>
        /// <returns></returns>
        private ICollection<CheckBoxItem> loadChildProductsIntoCheckItems(HashSet<ProductChild> childProductsBelongingToUserFrom)
        {
            List<CheckBoxItem> lst = new List<CheckBoxItem>();

            if(childProductsBelongingToUserFrom.IsNullOrEmpty())
                return lst;

            foreach (ProductChild productChild in childProductsBelongingToUserFrom)
            {
                CheckBoxItem chkBx = new CheckBoxItem(productChild);
                lst.Add(chkBx);
            }

            return lst;


        }

        //private ICollection<OwnersChildProductsWithLink> loadOwnersChildProductsWithLinks(HashSet<ProductChild> childProductsBelongingToUserFrom)
        //{
        //    if (childProductsBelongingToUserFrom.IsNullOrEmpty())
        //        return null;

        //    List<OwnersChildProductsWithLink> ownersChildProductsWithLinkLst = new List<OwnersChildProductsWithLink>();

        //    foreach (ProductChild productChild in childProductsBelongingToUserFrom)
        //    {
        //        string saleLink = createLandingPageLinkForProductChild(productChild);
        //        OwnersChildProductsWithLink ownersChildProductsWithLink = new OwnersChildProductsWithLink(productChild, saleLink);
        //    }

        //    return ownersChildProductsWithLinkLst;
        //}

        //private string createLandingPageLinkForProductChild(ProductChild productChild)
        //{
        //    //Url.Action("ProductChildLandingPage", "ProductChilds", new { productChildId = menuVar.IdForEdit, menuEnum = Model.MenuManager.MenuState.NextMenu, returnUrl = returnUrl });
        //}

        private MenuPathMain getMpm(string menuPathMainId)
        {
            menuPathMainId.IsNullOrWhiteSpaceThrowArgumentException("menuPathMainId");
            MenuPathMain mpm = MenuPathMainBiz.Find(menuPathMainId);
            mpm.IsNullThrowException("mpm");
            return mpm;
        }

        private void initializeHashSets()
        {
            peopleInProducts = new HashSet<Person>();
            peopleInProductChildren = new HashSet<Person>();
            peopleInLikeUnlike = new HashSet<Person>();
            totalPeopleHashSet = new HashSet<Person>();
            productsBelongingToUserFrom = new HashSet<Product>();
            childProductsBelongingToUserFrom = new HashSet<ProductChild>();
        }

        /// <summary>
        /// This does the summation of all the people into the main list and removes the fromUser i.e. cleans it.
        /// </summary>
        /// <param name="fromPerson"></param>
        private void addupAllListsIntoTotalPeopleAndClean(Person fromPerson)
        {
            addToTotalPeople(peopleInProducts);
            addToTotalPeople(peopleInProductChildren);
            addToTotalPeople(peopleInLikeUnlike);

            //cleanup
            peopleInProducts.Remove(fromPerson);
            peopleInProductChildren.Remove(fromPerson);
            peopleInLikeUnlike.Remove(fromPerson);
            totalPeopleHashSet.Remove(fromPerson);

            countIsSet = true;
        }

        private void addToTotalPeople(ICollection<Person> people)
        {
            if (!people.IsNullOrEmpty())
                foreach (var person in people)
                    totalPeopleHashSet.Add(person);
        }



        private void WireUpMessageWithPeople(MenuPathMain mpm, string productId, string productChildId, List<Person> toPeople, Message message, Person fromPerson, MenuENUM menuEnum)
        {
            throw new NotImplementedException();

            //fromPerson.Messages.Add(message);

            //foreach (Person toPerson in toPeople)
            //{
            //    PeopleMessage pm = new PeopleMessage();

            //    pm.MessageId = message.Id;
            //    pm.PersonId = toPerson.Id;

            //    toPerson.MessagesToPeople.Add(pm);
            //    message.ToPeople.Add(pm);
            //}
            //switch (menuEnum)
            //{


            //    case MenuENUM.IndexMenuPath1:
            //        MenuPath1 mp1 = mpm.MenuPath1;
            //        mp1.IsNullThrowException("MenuPath1 not found");

            //        if (mp1.Messages.IsNull())
            //            mp1.Messages = new List<Message>();

            //        mp1.Messages.Add(message);
            //        message.MenuPath1Id = mp1.Id;
            //        break;


            //    case MenuENUM.IndexMenuPath2:
            //        MenuPath2 mp2 = mpm.MenuPath2;
            //        mp2.IsNullThrowException("MenuPath2 not found");

            //        if (mp2.Messages.IsNull())
            //            mp2.Messages = new List<Message>();

            //        mp2.Messages.Add(message);
            //        message.MenuPath2Id = mp2.Id;
            //        break;


            //    case MenuENUM.IndexMenuPath3:
            //        MenuPath3 mp3 = mpm.MenuPath3;
            //        mp3.IsNullThrowException("MenuPath3 not found");

            //        if (mp3.Messages.IsNull())
            //            mp3.Messages = new List<Message>();

            //        mp3.Messages.Add(message);
            //        message.MenuPath3Id = mp3.Id;
            //        break;


            //    case MenuENUM.IndexMenuProduct:
            //        Product product = ProductBiz.Find(productId);
            //        product.IsNullThrowException("Product not found");

            //        if (product.Messages.IsNull())
            //            product.Messages = new List<Message>();

            //        product.Messages.Add(message);
            //        message.MenuPath3Id = product.Id;

            //        break;


            //    case MenuENUM.IndexMenuProductChild:
            //        ProductChild productChild = ProductChildBiz.Find(productChildId);
            //        productChild.IsNullThrowException("Product Child not found");

            //        if (productChild.Messages.IsNull())
            //            productChild.Messages = new List<Message>();

            //        productChild.Messages.Add(message);
            //        message.MenuPath3Id = productChild.Id;
            //        break;


            //    case MenuENUM.IndexDefault:
            //    default:
            //        break;
            //}

        }





        //--------------------------------------------------------------------------------------------------------------------------


        public void getPeopeFromMp1Etc(string menuPath1Id)
        {
            menuPath1Id.IsNullOrWhiteSpaceThrowArgumentException("menuPath1Id");

            List<MenuPathMain> mpmList = MenuPathMainBiz.FindAll()
                .Where(x =>
                    x.MenuPath1Id == menuPath1Id)
                    .ToList();

            if (!mpmList.IsNullOrEmpty())
            {
                foreach (var mpm in mpmList)
                {
                    //this will also get the likes
                    getPeopleFromMp3Etc(mpm);

                    if (!mpm.MenuPath1.IsNull())
                        if (!mpm.MenuPath1.LikeUnlikes_Fixed.IsNull())
                            AddLikes(mpm.MenuPath1.LikeUnlikes);

                    if (!mpm.MenuPath2.IsNull())
                        if (!mpm.MenuPath2.LikeUnlikes.IsNull())
                            AddLikes(mpm.MenuPath2.LikeUnlikes);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuPath1Id"></param>
        /// <param name="menupath2Id"></param>
        public void getPeopleFromMp2Etc(string menuPath1Id, string menupath2Id)
        {
            menuPath1Id.IsNullOrWhiteSpaceThrowArgumentException("menuPath1Id");
            menupath2Id.IsNullOrWhiteSpaceThrowArgumentException("menupath2Id");

            List<MenuPathMain> mpmList = MenuPathMainBiz.FindAll()
                .Where(x =>
                    x.MenuPath1Id == menuPath1Id &&
                    x.MenuPath2Id == menupath2Id)
                    .ToList();

            if (!mpmList.IsNullOrEmpty())
            {
                foreach (var mpm in mpmList)
                {
                    //this will also get the likes
                    getPeopleFromMp3Etc(mpm);

                    if (!mpm.MenuPath2.IsNull())
                        if (!mpm.MenuPath2.LikeUnlikes.IsNull())
                            AddLikes(mpm.MenuPath2.LikeUnlikes);
                }
            }
        }




        //this would be same as the MenuPathMain
        private void getPeopleFromMp3Etc(MenuPathMain mpm)
        {
            mpm.IsNullThrowExceptionArgument("mpm");
            mpm.MenuPath3Id.IsNullOrWhiteSpaceThrowException("MenuPath3 is null");

            if (mpm.Products_Fixed.IsNullOrEmpty())
                return;

            foreach (var product in mpm.Products_Fixed)
            {
                getPeopleFromProductEtc(product);
            }


            //get the likes 
            MenuPath1 mp1 = mpm.MenuPath1;
            if (!mp1.LikeUnlikes.IsNullOrEmpty())
            {
                AddLikes(mp1.LikeUnlikes);
            }


            MenuPath2 mp2 = mpm.MenuPath2;
            if (!mp2.LikeUnlikes.IsNullOrEmpty())
            {
                AddLikes(mp2.LikeUnlikes);
            }


            MenuPath3 mp3 = mpm.MenuPath3;
            if (!mp3.LikeUnlikes.IsNullOrEmpty())
            {
                AddLikes(mp3.LikeUnlikes);
            }
        }


        private void getPeopleFromProductEtc(string productId)
        {
            productId.IsNullOrWhiteSpaceThrowArgumentException("productId");
            Product product = ProductBiz.Find(productId);
            product.IsNullThrowException("product not found");
            getPeopleFromProductEtc(product);
        }


        private void getPeopleFromProductEtc(Product product)
        {
            if (!product.Owner.IsNull())
            {
                product.Owner.Person.IsNullThrowException("Owner is missing person");
                peopleInProducts.Add(product.Owner.Person);

                if (!OwnerForUser.IsNull())
                    if (OwnerForUser.Id == product.OwnerId)
                        productsBelongingToUserFrom.Add(product);

            }


            if (!product.ProductChildren.IsNullOrEmpty())
            {
                foreach (ProductChild child in product.ProductChildren)
                {
                    getPeopleFromProductChildOwners(child);
                }
            }


            //get the likes/unlike people for the product
            if (!product.LikeUnlikes.IsNullOrEmpty())
            {
                AddLikes(product.LikeUnlikes);

            }


        }


        private void getPeopleFromProductChildOwners(string productChildId)
        {
            productChildId.IsNullOrWhiteSpaceThrowArgumentException("productChildId");
            ProductChild productChild = ProductChildBiz.Find(productChildId);

            getPeopleFromProductChildOwners(productChild);
        }
        private void getPeopleFromProductChildOwners(ProductChild productChild)
        {

            productChild.IsNullThrowExceptionArgument("productChild");
            productChild.Owner.IsNullThrowException("child owner is null");
            productChild.Owner.Person.IsNullThrowException("child owner person is null");

            peopleInProductChildren.Add(productChild.Owner.Person);

            //this is where we get all the child products owned by user in the current subset
            if (!OwnerForUser.IsNull())
                if (productChild.OwnerId == OwnerForUser.Id)
                {
                    productChild.Selected = true;
                    childProductsBelongingToUserFrom.Add(productChild);
                }

            //get the likes/unlike people for the product
            if (!productChild.LikeUnlikes_Fixed.IsNullOrEmpty())
            {
                AddLikes(productChild.LikeUnlikes_Fixed);
            }

        }

        private void AddLikes(ICollection<LikeUnlike> likeUnlikesCollection)
        {
            foreach (LikeUnlike likeUnlike in likeUnlikesCollection)
            {
                likeUnlike.Person.IsNullThrowException("No person for like unlike");
                peopleInLikeUnlike.Add(likeUnlike.Person);
            }
        }



    }
}
