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
    
    public partial class infor
    {
        public int id { get; set; }
        public Nullable<int> users_id { get; set; }
        public string content { get; set; }
        public string publish_time { get; set; }
        public string title { get; set; }
        public string show_img { get; set; }
        public string profile { get; set; }
        public string url { get; set; }
        public string img_url { get; set; }
    
        public virtual users users { get; set; }
    }
}
