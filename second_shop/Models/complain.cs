//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace second_shop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class complain
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<System.DateTime> complaint_time { get; set; }
        public string content { get; set; }
    
        public virtual product product { get; set; }
        public virtual users users { get; set; }
    }
}
