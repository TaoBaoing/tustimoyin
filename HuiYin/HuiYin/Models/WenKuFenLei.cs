using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HuiYin.Models
{
    public class WenKuFenLei
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "文库分类")]
        public string Name { get; set; }


        [Display(Name = "父类名称")]
        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual WenKuFenLei ParentWenKuFenLei { get; set; }

        [Display(Name = "分类级别")]
        public FenLeiType FenLeiType    { get; set; }
    }

    public enum FenLeiType
    {
        一级=1,
        二级=2,
        三级=3,
        四级=4,
        五级=5
    }
}