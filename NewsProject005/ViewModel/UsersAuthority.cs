using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject005.ViewModel
{
    public class UsersAuthority
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int authorityId { get; set; }
        public byte newsAuthorizeAdd { get; set; }
        public byte newsVideoAuthorizeAdd { get; set; }
        public byte CornerTextAuthorizeAdd { get; set; }
        public long newsAlwaysAuthorizeAdd { get; set; }
    }
}