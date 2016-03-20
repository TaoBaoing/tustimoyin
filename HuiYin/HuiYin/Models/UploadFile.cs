using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HuiYin.Models
{
    public class UploadFile
    {
        public long Id { get; set; }

        [Display(Name = "原始文件名")]
        public string FileName { get; set; }

        [Display(Name = "保存后文件名")]
        public string FilePathName { get; set; }

        [Display(Name = "上传人")]
        public long? LhUserId { get; set; }

        [Display(Name = "上传人")]
        public LhUser LhUser { get; set; }

        private DateTime _createTime=DateTime.Now;
        public DateTime CreateTime {
            get { return _createTime; }
            set { _createTime = value; }
        }

        public long? WenKuId { get; set; }
 
        public WenKu WenKu { get; set; }



    }
}