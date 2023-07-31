using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodDemo.Models
{
    internal class global
    {
        //lưu ID người dùng
        public static string userID { get; private set; }
        public static void setUserID(string userid)
        {
            userID = userid;
        }

        

        //lưu username người dùng
        public static string username { get; private set; }
        public static void setUserName(string usernames)
        {
            username = usernames;
        }

        
    }
}
