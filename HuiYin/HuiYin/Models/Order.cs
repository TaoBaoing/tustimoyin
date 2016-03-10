using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace HuiYin.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public long Id { get; set; }

        public long LhUserId { get; set; }
        public virtual LhUser LhUser { get; set; }


        private DateTime _createTime = DateTime.Now;
        [Display(Name = "订单日期")]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [Display(Name = "订单状态")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "金额")]
        public decimal Money { get; set; }

    }

    public enum OrderStatus
    {
        全部=0,
        未打印 = 10,
        已打印 = 20
    }
}