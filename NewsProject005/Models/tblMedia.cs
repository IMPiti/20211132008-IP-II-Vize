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
    
    public partial class tblMedia
    {
        public int Id { get; set; }
        public string path { get; set; }
        public int newsId { get; set; }
        public byte type { get; set; }
        public int order { get; set; }
        public System.DateTime createdDate { get; set; }
    
        public virtual tblNews tblNews { get; set; }
    }
}
