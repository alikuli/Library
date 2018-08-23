using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebLibrary.Programs;

namespace BreadCrumbsLibraryNS.Programs
{
    /// <summary>
    /// The BreadCrumbManager keeps all the urls that the user visits in a stack.
    /// The Stack is saved in a Cache if available
    /// </summary>
    public class BreadCrumbManager
    {
        const int MAX_LENTH = 6;
        IMemoryMain _memory;
        SessionMemory _sessionMemory;

        public BreadCrumbManager(IMemoryMain memory)
        {
            _memory = memory;
            _sessionMemory = _memory.SessionMemory;
            _breadCrumbStack = GetFromMemory();
        }

        const string LOCATION_NAME = "BreadCrumbManagerMemory";

        public void Push(BreadCrumb bc)
        {
            if (bc.LinkName.IsNullOrWhiteSpace())
                return;
            if (bc.Url.IsNullOrWhiteSpace())
                return;
            if (bc.Url == "#")
                return;
            if(!BreadCrumbStack.IsNull())
            {
                if(BreadCrumbStack.Count > 0)
                {
                    if(BreadCrumbStack.Peek().Url == bc.Url)
                    {
                        return;
                    }
                }
            }

            BreadCrumbStack.Push(bc);
            SaveToMemory();

        }

        public void Push(string linkName, string url)
        {

            BreadCrumb bc = new BreadCrumb(url, linkName);
            Push(bc);

        }

        public BreadCrumb Pop()
        {
            if (BreadCrumbStack.Count() == 0)
            {
                Console.WriteLine("Stack is Empty!");
                return null;
            }

            BreadCrumb bc = BreadCrumbStack.Pop();
            SaveToMemory();
            Console.WriteLine("Pop!");
            return bc;

        }


        public List<BreadCrumb> ToList()
        {
            if (_breadCrumbStack.IsNull())
            {
                Console.WriteLine("BreadCrumbStack is null");
                return null;
            }
            return _breadCrumbStack.ToList();
        }

        public string HomeUrl { get; set; }
        public BreadCrumb[] ToArray()
        {
            if (BreadCrumbStack.IsNull())
            {
                Console.WriteLine("BreadCrumbStack is null");
                return null;
            }
            return BreadCrumbStack.ToArray();

        }
        private Stack<BreadCrumb> _breadCrumbStack;
        private Stack<BreadCrumb> BreadCrumbStack
        {
            get
            {
                if (_breadCrumbStack.IsNull())
                    _breadCrumbStack = new Stack<BreadCrumb>();

                return _breadCrumbStack;
            }
            set
            {
                _breadCrumbStack = value;

            }
        }

        public int Count()
        {
            return BreadCrumbStack.Count();
        }

        public bool IsNullOrEmpty()
        {
            if (_breadCrumbStack.IsNull())
                return true;

            if (_breadCrumbStack.Count() == 0)
                return true;

            return false;

        }

        private Stack<BreadCrumb> GetFromMemory()
        {

            if (!_sessionMemory.IsNull())
            {
                Stack<BreadCrumb> memoryStack = _sessionMemory.GetFrom(LOCATION_NAME) as Stack<BreadCrumb>;
                return memoryStack;
            }

            return null;
        }

        private void SaveToMemory()
        {
            if (_memory.IsNull())
                return;
            _sessionMemory.ClearFor(LOCATION_NAME);
            _sessionMemory.Add(LOCATION_NAME, _breadCrumbStack);
        }



        public string a(string url, string classString, string linkName)
        {
            if (url.IsNullOrWhiteSpace())
                url = "#";

            linkName.IsNullOrWhiteSpaceThrowException();
            string s;

            string id = linkName;

            if (classString.IsNullOrWhiteSpace())
                s = string.Format(string.Format("<a href=\"{0}\" id=\"{1}\">{2}</a>", url, id, linkName));
            else
                s = string.Format(string.Format("<a href=\"{0}\" id=\"{1}\" class=\"{3}\">{2}</a>", url, id, linkName, classString));

            return s;
        }

        public string a(BreadCrumb breadCrumb)
        {
            string s = a(breadCrumb.Url, "", breadCrumb.LinkName);
            return s;
        }

        public string nav(string ol)
        {
            string s = string.Format("<nav aria-label=\"breadcrumb\">{0}</nav>", ol);
            return s;
        }

        public string liActive(string anchor)
        {
            anchor.IsNullOrWhiteSpaceThrowException();
            string s = string.Format("<li class=\"breadcrumb-item active\" aria-current=\"page\">{0}</li>", anchor);
            return s;
        }

        public string li(string anchor)
        {
            anchor.IsNullOrWhiteSpaceThrowException();
            string s = string.Format("<li class=\"breadcrumb-item\">{0}</li>", anchor);
            return s;
        }
        public string ol(List<BreadCrumb> breadCrumbLst)
        {
            //<ol class="breadcrumb">
            //</ol>
            StringBuilder sb = new StringBuilder();

            string openOl = string.Format("<ol class=\"breadcrumb\">");
            string closeOl = string.Format("</ol>");

            sb.Append(openOl);

            if (!breadCrumbLst.IsNullOrEmpty())
            {
                string anchor = "";
                string _li = "";
                int startingPoint = (breadCrumbLst.Count() - 1);

                if (startingPoint > MAX_LENTH)
                {
                    anchor = a(breadCrumbLst[startingPoint]);
                    _li = li(anchor);

                    startingPoint = MAX_LENTH;
                    sb.Append(_li);

                    anchor = a("#", "", "..");
                    _li = li(anchor);
                    sb.Append(_li);
                }

                    for (int i = startingPoint; i >= 0; i--)
                    {
                        _li = "";
                        anchor = "";
                        if (i == 0) //this is the last one, not clickable
                        {
                            _li = liActive(breadCrumbLst[i].LinkName);
                        }
                        else
                        {
                            anchor = a(breadCrumbLst[i]);
                            _li = li(anchor);
                        }

                        sb.Append(_li);
                    }

            }

            sb.Append(closeOl);
            return sb.ToString();

        }

        public string Url_Curr
        {
            get
            {
                if (BreadCrumbStack.Count == 0)
                    return "";

                return BreadCrumbStack.Peek().Url;
            }
        }
        public string Url_CurrMinusOne
        {
            get
            {
                if (BreadCrumbStack.Count == 0)
                    return "";

                if (BreadCrumbStack.Count == 1)
                    return ToArray()[0].Url;

                return ToArray()[1].Url;
            }
        }
        public string Url_CurrMinusTwo
        {
            get
            {
                if (BreadCrumbStack.Count == 0)
                    return "";

                if (BreadCrumbStack.Count == 1)
                    return ToArray()[0].Url;


                if (BreadCrumbStack.Count == 2)
                    return ToArray()[1].Url;

                return ToArray()[2].Url;
            }
        }

        public string Url_CurrMinusThree
        {
            get
            {
                if (BreadCrumbStack.Count == 0)
                    return "";

                if (BreadCrumbStack.Count == 1)
                    return ToArray()[0].Url;


                if (BreadCrumbStack.Count == 2)
                    return ToArray()[1].Url;


                if (BreadCrumbStack.Count == 3)
                    return ToArray()[2].Url;

                return ToArray()[3].Url;
            }
        }

        public string Render()
        {

            if (IsNullOrEmpty())
            {
                return "";

            }

            var breadCrumbLst = ToList();
            var olComplete = ol(breadCrumbLst);
            var navComplete = nav(olComplete);
            return navComplete;


        }
    }
}
