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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.collection = new HashSet<collection>();
            this.comment = new HashSet<comment>();
            this.user_history = new HashSet<user_history>();
            this.complain = new HashSet<complain>();
        }
    
        public int id { get; set; }
        public Nullable<int> users_id { get; set; }
        public string title { get; set; }
        public string profile { get; set; }
        public string signature { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> old_price { get; set; }
        public string tags { get; set; }
        public string depart { get; set; }
        public string wechat { get; set; }
        public string qq { get; set; }
        public string phone { get; set; }
        public string imgs_url { get; set; }
        public string publish_time { get; set; }
        public Nullable<int> view_count { get; set; }
        public int isadmin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<collection> collection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comment { get; set; }
        public virtual users users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_history> user_history { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complain> complain { get; set; }
    }
}
