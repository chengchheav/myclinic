using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data;
using Utilities;
using System.Data.Entity.Validation;
using log4net;
using System.Data.Entity;

namespace MyClinic.Core
{
    public class UserRepository :IUserRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public UserRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<User> Get()
        {
            IEnumerable<User> users=null;
            try
            {
                users = db.User.ToList().OrderBy(p => p.Name);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return users;
        }

        public IEnumerable<UserType> getUserType()
        {
            IEnumerable<UserType> usertypes = null;
            try
            {
               // usertypes = db.UserType.ToList();
                usertypes = db.Database.SqlQuery<UserType>("select Id,TypeName,TypeCode from UserType").ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return usertypes;
        }

        public IEnumerable<User> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords, string userType)
        {
            IEnumerable<User> users = null;
            try
            {
                if (userType.Contains("SPU"))
                {
                    users = (from u in db.User select u).Where(u=>!u.UserType.Contains("ADM") && u.IsActived != 2).AsEnumerable();
                }
                else
                {
                    users = (from u in db.User select u).Where(u => u.IsActived != 2).AsEnumerable();
                }
                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            users = users.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "Email":
                            users = users.Where(p => p.Email.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "UserName":
                            users = users.Where(p => p.UserName.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }                

                totalRecords = users.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Name":
                        if (order == "desc")
                            users = users.OrderByDescending(p => p.Name);
                        else
                            users = users.OrderBy(p => p.Name);
                        break;                    
                    case "UserName":
                        if (order == "desc")
                            users = users.OrderByDescending(p => p.UserName);
                        else
                            users = users.OrderBy(p => p.UserName);
                        break;
                    case "Email":
                        if (order == "desc")
                            users = users.OrderByDescending(p => p.Email);
                        else
                            users = users.OrderBy(p => p.Email);
                        break;                    
                    case "Id":
                        if (order == "desc")
                            users = users.OrderByDescending(p => p.Id);
                        else
                            users = users.OrderBy(p => p.Id);
                        break;
                }

                var actualPageNo = pageNo - 1;
                users = users.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return users;
        }

        public void Add(User user)
        {
            try
            {
                user.Password = Common.EncryptString(user.Password);
                db.User.Add(user);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(User originalUser, User newUser)
        {
            try
            {
                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                db.Entry(originalUser).CurrentValues.SetValues(newUser);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*User In LogIn Controller*/
        public User GetUserByUsernameAndPassword(string username, string password)
        {            
            User logInUser = new User();
            try
            {
                var user = from o in db.User where o.UserName == username && o.IsActived !=2 select o; 
                if (user.ToList().Count > 0) { 
                    foreach (var record in user){

                        if (Common.DecryptString(record.Password) == password)
                        {
                            logInUser.Id = record.Id;
                            logInUser.UserName = record.UserName;
                            logInUser.Name = record.Name;
                            logInUser.Password = record.Password;
                            logInUser.UserType = record.UserType;
                            logInUser.Email = record.Email;
                            logInUser.IsActived = record.IsActived;
                            break;
                        }
                    }
                }
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return logInUser;                
        }

        public User GetUserByUsername(string username)
        {
            User logInUser = new User();
            try
            {
                var user = from o in db.User where o.UserName == username && o.IsActived != 2  select o;
                if (user.ToList().Count > 0)
                {
                    foreach (var record in user)
                    {
                            logInUser.Id = record.Id;
                            logInUser.UserName = record.UserName;
                            logInUser.Name = record.Name;
                            logInUser.Password = record.Password;
                            logInUser.UserType = record.UserType;
                            logInUser.Email = record.Email;
                            logInUser.IsActived = record.IsActived;
                            break;
                    }
                }
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return logInUser;
        }

        public User GetUserById(int id)
        {            
            User user = new User();
            try
            {
                var getUserById = from o in db.User where o.Id == id && o.IsActived != 2  select o;
                if (getUserById.ToList().Count > 0)
                {
                    foreach (var record in getUserById)
                    {
                        user = record;
                        break;
                    }
                }
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return user;                
        }

        public User GetUserByCreateBy(int id)
        {
            User user = new User();
            try
            {
                var getUserById = from o in db.User where o.CreatedBy == id && o.IsActived != 2  select o;
                if (getUserById.ToList().Count > 0)
                {
                    foreach (var record in getUserById)
                    {
                        user = record;
                        break;
                    }
                }
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return user;
        }

        public User GetUserByModifiedBy(int id)
        {
            User user = new User();
            try
            {
                var getUserById = from o in db.User where o.ModifiedBy == id && o.IsActived != 2  select o;
                if (getUserById.ToList().Count > 0)
                {
                    foreach (var record in getUserById)
                    {
                        user = record;
                        break;
                    }
                }
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return user;
        }
    }
}