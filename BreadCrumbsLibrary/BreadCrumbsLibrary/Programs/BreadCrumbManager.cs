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
        IMemoryMain _memory;
        CacheMemory _cache;

        public BreadCrumbManager()
        {
            _breadCrumbStack = GetFromMemory();
        }
        public BreadCrumbManager(IMemoryMain memory)
            : this()
        {
            _memory = memory;
            _cache = _memory.CacheMemory;
        }

        Stack<BreadCrumb> _breadCrumbStack;
        const string LOCATION_NAME = "BreadCrumbManagerMemory";

        private Stack<BreadCrumb> breadCrumbStack
        {
            get
            {
                if (!_cache.IsNull())
                    _breadCrumbStack = GetFromMemory();

                if (_breadCrumbStack.IsNull())
                    _breadCrumbStack = new Stack<BreadCrumb>();

                return _breadCrumbStack;
            }
            set
            {
                _breadCrumbStack = value;
            }
        }

        public void Push(BreadCrumb bc)
        {
            if (bc.IsNull())
                return;
            breadCrumbStack.Push(bc);
            SaveToMemory();

        }


        public BreadCrumb Pop()
        {
            if (breadCrumbStack.IsNull())
            {
                Console.WriteLine("BreadCrumbStack is null");
                return null;
            }
            if (breadCrumbStack.Count() == 0)
            {
                Console.WriteLine("Stack is Empty!");
                return null;
            }

            Console.WriteLine("Pop!");
            BreadCrumb bc = breadCrumbStack.Pop();
            SaveToMemory();
            return bc;

        }


        public List<BreadCrumb> ToList()
        {
            if (breadCrumbStack.IsNull())
            {
                Console.WriteLine("BreadCrumbStack is null");
                return null;
            }
            return breadCrumbStack.ToList();
        }

        public string HomeUrl { get; set; }
        public BreadCrumb[] ToArray()
        {
            if (breadCrumbStack.IsNull())
            {
                Console.WriteLine("BreadCrumbStack is null");
                return null;
            }
            return breadCrumbStack.ToArray();

        }

        public int Count()
        {
            if (breadCrumbStack.IsNull())
                return 0;
            return breadCrumbStack.Count();
        }

        public bool IsNullOrEmpty()
        {
            if (breadCrumbStack.IsNull())
                return true;

            if (breadCrumbStack.Count() == 0)
                return true;

            return false;

        }

        private Stack<BreadCrumb> GetFromMemory()
        {

            if (!_cache.IsNull())
            {
                Stack<BreadCrumb> stack = _cache.GetFrom(LOCATION_NAME) as Stack<BreadCrumb>;
                return stack;
            }

            return null;
        }

        private void SaveToMemory()
        {
            if (_memory.IsNull())
                return;
            _cache.ClearFor(LOCATION_NAME);
            _cache.Add(LOCATION_NAME, _breadCrumbStack);
        }

        //private void ClearMemory()
        //{
        //    _cache.Clear();
        //}


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
            string s = a(breadCrumb.CurrentUrl, "", breadCrumb.LinkName);
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
                int startingPoint = (breadCrumbLst.Count() - 1);
                for (int i = startingPoint; i >= 0; i--)
                {
                    string _li = "";
                    if (i == 0) //this is the last one, not clickable
                    {
                        _li = liActive(breadCrumbLst[i].LinkName);
                    }
                    else
                    {
                        string anchor = a(breadCrumbLst[i]);
                        _li = li(anchor);
                    }

                    sb.Append(_li);
                }
            }

            sb.Append(closeOl);
            return sb.ToString();

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
