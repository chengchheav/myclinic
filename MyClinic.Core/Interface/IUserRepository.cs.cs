using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        IEnumerable<User> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords, string userType);
        void Add(User user);
        User GetUserByUsernameAndPassword(string username, string password);
        User GetUserByUsername(string username);
        User GetUserById(int id);
        User GetUserByCreateBy(int id);
        User GetUserByModifiedBy(int id);
        IEnumerable<UserType> getUserType();
        void UpdateFieldChangedOnly(User originalUser, User newUser);
    }
}
