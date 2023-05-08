using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string newsSmallTitle { get; set; }
        public string newsTitle { get; set; }
        public string newsSummary { get; set; }
        public string newsDetail { get; set; }
        public int newsViewing { get; set; }
        public int newsTypeId { get; set; }
        public int newsUserAddId { get; set; }
        public int newsCuff { get; set; }
        public byte newsStatus { get; set; }
        public int newsConfirmation { get; set; }
        public System.DateTime createdDate { get; set; }
        public System.DateTime editingDate { get; set; }

        public int categoryId { get; set; } // ben ekledim dboda yok
    }
}