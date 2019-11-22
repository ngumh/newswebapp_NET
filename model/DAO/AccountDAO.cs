using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model.Entity;
namespace model.DAO
{
    public class AccountDAO
    {
        NewsContext db = null;
        public AccountDAO()
        {
            db = new NewsContext();
        }

        public bool log(String user, String pass)
        {
            var result = db.User.Count(x => x.Username == user && x.PasswordHash == pass);
            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetID(String id)
        {
            return db.User.SingleOrDefault(x => x.Username == id);
        }
    }
}
