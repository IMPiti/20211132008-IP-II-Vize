using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class CategorysModel
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public byte lockCategory { get; set; }
        public byte status { get; set; }
        public System.DateTime createdDate { get; set; }
        public System.DateTime editingDate { get; set; }
    }
}