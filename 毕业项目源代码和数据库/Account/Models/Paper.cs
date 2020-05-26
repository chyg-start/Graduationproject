//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Account.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Paper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paper()
        {
            this.Answer = new HashSet<Answer>();
            this.Topic = new HashSet<Topic>();
        }

        [Display(Name = "试卷编号")]
        public int PaperID { get; set; }
        [Display(Name ="试卷名称")]
        [Required(ErrorMessage = "试卷名称必填")]
        public string PaperName { get; set; }
        [Display(Name = "试卷说明")]
        [StringLength(25,ErrorMessage ="字数超过了25")]
        public string PaperExplain { get; set; }
        [Display(Name = "时长")]
        [Required(ErrorMessage = "时长必填")]

        public int PaperTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic { get; set; }
    }
}
