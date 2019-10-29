using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Configuration;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.MenuNS
{
    public abstract class MenuPathAbstract : CommonWithId
    {

        public static int MaxNumberOfPicturesInMenu()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["menu.max_menu_pictures.number"];
            int noOfDays = noOfDaysString.ToInt();
            return noOfDays;

        }

    }
}