using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HuiYin.Models
{
    public class WenKu
    {
        public long Id { get; set; }

        [Display(Name = "文件名称")]
        public string Name { get; set; }

        [Display(Name="存放位置")]
        public string FileName { get; set; }

        [Display(Name = "文库分类")]
        public long WenKuFenLeiId { get; set; }
        public virtual WenKuFenLei WenKuFenLei { get; set; }

        private DateTime _createTime = DateTime.Now;
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
    }
}