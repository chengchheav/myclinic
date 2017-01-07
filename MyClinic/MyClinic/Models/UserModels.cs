using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class UserModels
    {

        public IEnumerable<User> users { get; set; }
        public User user { get; set; }
        public User userCreate { get; set; }
        public User userModified { get; set; }
        public IEnumerable<UserType> usertypes { get; set; }
        public Pager Pager { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public string LicenseMessage { get; set; }

        public UserEdit userEdit { get; set; }
        public UserAdd userAdd { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
    }

}