using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace HuiYin.Models
{
    public enum UserType
    {
        Admin=10,
        Customer=20
    }

    public class LhUser : IIdentity, IPrincipal
    {
        public LhUser()
        {
        }

        public LhUser(string name)
        {
            Name = name;
        }

        public long Id { get; set; }


        [Display(Name = "手机号")]
        public string Name { get; set; }


        [Display(Name = "密码")]
        public string Pwd { get; set; }

        private string _xingMing = "";
        [Display(Name = "姓名")]
        [Required(AllowEmptyStrings = true,ErrorMessage = "姓名不能为空")]
        public string XingMing {
            get { return _xingMing; }
            set { _xingMing = value; }
        }

        private string _address = "";
        [Display(Name = "配送地址")]
        public string Address {   
            get { return _address; }
            set { _address = value; } }

        [Display(Name = "性别")]
        public string Sex { get; set; }
        
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "用户类型")]
        public UserType UserType { get; set; }

        [NotMapped]
        public IIdentity Identity
        {
            get { return this; }
        }

        
        public bool IsInRole(string role)
        {
            return false;
        }

        [NotMapped]
        public string AuthenticationType
        {
            get { return "LhUser"; }
        }

        [NotMapped]
        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}