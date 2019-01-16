using CMS_Blog.SQLite;
using System.Collections.ObjectModel;
using System.Data;

namespace CMS_Blog.Models
{
    public partial class User
    {
        public static Collection<User> GetAll()
        {
            Collection<User> users = new Collection<User>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable returnData =
                        database.readSql(
                                "select * from User");

                if (returnData.Rows.Count > 0)
                {
                    foreach (DataRow row in returnData.Select())
                    {
                        User user = new User();

                        user.Name = row.ItemArray[1].ToString();
                        user.Email = row.ItemArray[3].ToString();
                        user.Surname = row.ItemArray[4].ToString();
                        user.Alias = row.ItemArray[5].ToString();

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public static User Get(int user_id)
        {
            User user = new User();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable userData =
                        database.readSql("select * from User where user_id = " + user_id);

                if (userData.Rows.Count > 0)
                {
                    DataRow userRow = userData.Select()[0];
                    
                    user.Name = userRow.ItemArray[1].ToString();
                    user.Email = userRow.ItemArray[3].ToString();
                    user.Surname = userRow.ItemArray[4].ToString();
                    user.Alias = userRow.ItemArray[5].ToString();

                }
            }

            return user;
        }

    }
}
