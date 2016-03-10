using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HuiYin.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }

        public long? LhUserId { get; set; }

        public LhUser LhUser { get; set; }


        public long UploadFileId { get; set; }

        [ForeignKey("UploadFileId")]
        public virtual UploadFile UploadFile { get; set; }

//         [Display(Name = "临时文档名称百度生成的")]
//        public string TempName { get; set; }

        [Display(Name = "文档名称")]
        public string Name { get; set; }

        [Display(Name = "份数")]
        public int Count { get; set; }

        [Display(Name = "单双面")]
        public bool IsDanMian { get; set; }

        [Display(Name = "是否彩打")]
        public bool IsCaiDa { get; set; } 


        [Display(Name = "纸张大小")]
        public PrintSize PrintSize { get; set; } //A1 A2 A3 A4

        
        public BaoZhuang BaoZhuang { get; set; } //无、胶装、打孔装


        [Display(Name = "共多少页")]
        public int TotalPage { get; set; }
        

        [Display(Name = "金额")]
        public decimal Money { get; set; }
  
        public Status Status { get; set; }
    }

    public enum Status
    {
        未下单 = 1,
        已下单 = 10,
        已过期 = 20
    }

    public enum BaoZhuang
    {
        无=1,
        胶装=10,
        打孔装=20
    }
    public enum PrintSize
    {
        A4=1,
        A3=10,
        A2=20,
        A1=30,
    }
}