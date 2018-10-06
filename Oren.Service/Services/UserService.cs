using Oren.Model.Model.Entities;
using Oren.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Service.Services
{
   public class UserService:BaseService<AppUser>
    {
        public void DeleteUser()
        {

        }
        public bool UserControl(string Email, string password) => Any(user => user.Email == Email && user.Password == password);

        public AppUser UserFromName(string name) => GetByExpSingle(x => x.Nick == name);

        public AppUser GetByName(string name) => GetByExpSingle(x => x.Nick.ToLower() == name.ToLower());

        public AppUser GetByMail(string email) => GetByExpSingle(x => x.Email == email);


    }
}
