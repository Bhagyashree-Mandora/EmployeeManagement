//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class log
    {
        public int id { get; set; }
        public System.DateTime time { get; set; }
        public string change_type { get; set; }
        public int employee_id { get; set; }
        public string field_name { get; set; }
        public string old_value { get; set; }
        public string new_value { get; set; }
    
        public virtual employee employee { get; set; }
        public virtual employee employee1 { get; set; }
    }
}
