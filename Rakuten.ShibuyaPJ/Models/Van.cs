//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rakuten.ShibuyaPJ.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Van
    {
        public int VanId { get; set; }
        public string VanNumber { get; set; }
        public Nullable<int> NumberOfOpenOrders { get; set; }
        public Nullable<int> NumberOfDeliveredOrders { get; set; }
        public Nullable<int> NumberOfRedelivered { get; set; }
        public Nullable<System.DateTime> NextETA { get; set; }
        public Nullable<System.DateTime> LastETA { get; set; }
        public string NDI { get; set; }
        public string Status { get; set; }
    }
}