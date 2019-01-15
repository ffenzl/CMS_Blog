using CMS_Blog.SQLite;
using MVCIntegrationExample;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace CMS_Blog.Models
{
    public partial class User
    {
        public static bool Update(User user)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement =
                        "update User " +
                            "set user_name = '" + user.Name + "', " +
                                "user_surname = '" + user.Surname + "', " +
                                "user_email = '" + user.Email + "', " +
                                "user_alias = '" + user.Alias + "' " +
                            "where user_id = " + user.Id;

                database.BeginTransaction();
                bool result = database.executeSql(statement, false);

                if (result)
                {
                    database.TransCommit();
                    return true;
                }
            }
            return false;
        
        }

        public static bool Create(User user)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement =
                        "insert into User ( " +
                            "user_id, " +
                            "user_name, " +
                            "user_surname, " +
                            "user_email, " +
                            "user_password, " +
                            "user_alias) " +
                        "values( " +
                            User.GetNextRef() + ", " +
                            "'" + user.Name + "', " +
                            "'" + user.Surname + "', " +
                            "'" + user.Email + "', " +
                            "'" + user.Password + "', " +
                            "'" + user.Alias + "');";

                database.BeginTransaction();
                bool result = database.executeSql(statement, false);

                if (result)
                {
                    database.TransCommit();
                    return true;
                }
            }
            return false;
        }

        public static Collection<User> GetAll()
        {
            Collection<User> users = new Collection<User>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                DataTable returnData =
                        database.readSql(
                                "select * from User");

                if (returnData.Rows.Count > 0)
                {
                    foreach (DataRow row in returnData.Select())
                    {
                        User user = new User();

                        user.Id = Convert.ToInt32(row.ItemArray[0]);
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
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
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


        public static int GetNextRef()
        {
            int nextRef = 1;

            SqlDatabase database = new SqlDatabase();
            database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db"));
            DataTable returnData =
                    database.readSql(
                            "select max(user_id) user_id from User;");

            if (returnData.Rows.Count > 0 &&
                returnData.Rows[0][0].GetType() != typeof(System.DBNull))
                nextRef = Convert.ToInt32(returnData.Rows[0][0]) + 1;

            return nextRef;
        }
    }
}
