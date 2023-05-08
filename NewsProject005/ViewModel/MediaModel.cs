using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class MediaModel
    {
        public int Id { get; set; }
        public string path { get; set; }
        public int newsId { get; set; }
        public byte type { get; set; }
        public int order { get; set; }
        public System.DateTime createdDate { get; set; }
    }
}