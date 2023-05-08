using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string images { get; set; }
        public string nameSurname { get; set; }
        public string mail { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int authorityId { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string twitter { get; set; }
        public string linkedin { get; set; }
        public System.DateTime editingDate { get; set; }
        public System.DateTime createdDate { get; set; }
    }
}