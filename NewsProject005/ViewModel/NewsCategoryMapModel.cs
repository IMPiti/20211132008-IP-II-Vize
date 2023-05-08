using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class NewsCategoryMapModel
    {
        public int Id { get; set; }
        public int haber_id { get; set; }
        public int kategori_id { get; set; }
        public int kayithaberId { get; set; }
        public NewsModel haberBilgi { get; set; }
    }
}