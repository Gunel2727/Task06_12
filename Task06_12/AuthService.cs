using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task06_12
{
    public class AuthService
    {
        private const string userPath = "users.json";
        public Users Login(string username, string password)
        {
            var users = JsonHelper.Read<Users>(userPath);
            return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
        public void Register(Users user)
        {
            var users = JsonHelper.Read<Users>(userPath);
            user.Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;
            users.Add(user);
            JsonHelper.Write(userPath, users);
        }
        public void ChangeUserRole(int userId, Role newRole)
        {
            var users = JsonHelper.Read<Users>(userPath);
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Role = newRole;
                JsonHelper.Write(userPath, users);
            }
        }
        public List<Users> GetAllUsers() => JsonHelper.Read<Users>(userPath);

        public void DeleteUser(int userId)
        {
            var users = JsonHelper.Read<Users>(userPath);
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                users.Remove(user);
                JsonHelper.Write(userPath, users);
            }
        }
    }
}
