//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewsProject005.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUsersAuthority
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int authorityId { get; set; }
        public byte newsAuthorizeAdd { get; set; }
        public byte newsVideoAuthorizeAdd { get; set; }
        public byte CornerTextAuthorizeAdd { get; set; }
        public long newsAlwaysAuthorizeAdd { get; set; }
    
        public virtual tblAuthority tblAuthority { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }
}