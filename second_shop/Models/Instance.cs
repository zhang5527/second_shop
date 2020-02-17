
using second_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace second_shop
{
    public class Instance
    {
        private static second_shopEntities sample;
        private Instance() { }
        public static second_shopEntities getSingal
        {
            get
            {
                if (sample == null) sample = new second_shopEntities();
                return sample;          
            }
        }
    }
}