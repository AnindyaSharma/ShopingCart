using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping_Cart.Models
{
    public class ok
    {
        public static List<cartitem> c = new List<cartitem>();
        
        public class cartitem
        {
            public int iid;
            public int iqty;
        }
    }
}