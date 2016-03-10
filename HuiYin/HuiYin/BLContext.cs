using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HuiYin.Models;

namespace HuiYin
{
    public class BLContext
    {
       
        public static LhUser LhUser {
            get
            {
                using (var db=new AppDbContext())
                {
                    var user = db.LhUsers.FirstOrDefault(x => x.Name == HttpContext.Current.User.Identity.Name);
                    return user;
                }
               
            }
        }

        public static long LhUserId {
            get { return LhUser.Id; }
        }
    }
}