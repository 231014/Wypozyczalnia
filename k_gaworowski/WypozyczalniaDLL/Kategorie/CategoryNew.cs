﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaDane
{
    public class CategoryNew : Category
    {
        private static CategoryNew instance;
        public static CategoryNew Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoryNew();
                return instance;
            }
        }

        private CategoryNew()
        {
            movies = new List<Movie>();
        }

        public override string ReturnCategoryName()
        {
            return "New";
        }
        public override string ToString()
        {
            return "Nowy";
        }

    }
}
