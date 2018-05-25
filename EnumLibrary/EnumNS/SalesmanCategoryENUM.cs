using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EnumLibrary.EnumNS
{
 /// <summary>
 /// CityLeader- This is the sales leader of the city
 /// CountryLeader- This is the sales leader of the Country
 /// Customer-This is the person who wants to purchase the service of finding Taxi or Employee
 /// FieldMan-This is the person who will go out in the field and find workers and taxis
 /// Inputter-This is an inputter who will input only data in the office.
 /// PhoneMan-This person will receive calls from potential workers and enter partial data
 /// Salesman-This is the person who sells cards to the shops.
 /// Shop-This is a shop that buys cards.
 /// StateLeader-This is the leader of the State/Province
 /// Worker-This is the worker who is searching for a job
 /// WorldLeader-This is the world leader in the sales men
 /// Taxi-This person will offer taxi services

 /// </summary>

    public enum SalesmanCategoryENUM
    {
        Unknown,

        [Description("City Leader")]
        CityLeader,     //This is the sales leader of the city

        [Description("County Leader")]
        CountryLeader,  //This is the sales leader of the Country
        //Customer,       //This is the person who wants to purchase the service of finding Taxi or Employee

        [Description("Fieldman")]
        FieldMan,       //This is the person who will go out in the field and find workers and taxis
        
        Inputter,       //This is an inputter who will input only data in the office.

        [Description("Phone Person")]
        PhoneMan,       //This person will receive calls from potential workers and enter partial data

        Salesman,       //This is the person who sells cards to the shops.
        
        Shop,           //This is a shop that buys cards.

        [Description("State Leader")]
        StateLeader,    //This is the leader of the State/Province
        
        Worker,         //This is the worker who is searching for a job

        [Description("World Leader")]
        WorldLeader,    //This is the world leader in the sales men
        //Taxi            //This person will offer taxi services
        
    }
}