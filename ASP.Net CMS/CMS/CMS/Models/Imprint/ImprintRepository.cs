using CMS_Blog.SQLite;
using MVCIntegrationExample;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Imprint
    {
        public static readonly string GlobalDateFormat = "yyyy-MM-dd HH:mm:ss";

        public static string GetImprint()
        {
            string imprint = "";

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                DataTable returnData = database.readSql(
                        "select * " +
                        "  from Imprint where id = 1");

                if (returnData.Rows.Count > 0)
                {
                    imprint = returnData.Rows[0]["imprint"].ToString();
                }
            }
            return imprint;
        }

        public static bool Create(Comment comment)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement =
                        "insert into User ( " +
                            "comment_id, " +
                            "post_id, " +
                            "comment_username, " +
                            "comment_useremail, " +
                            "comment_date, " +
                            "comment_text, " +
                            "comment_state) " +
                        "values( " +
                            Comment.GetNextRef() + ", " +
                            comment.Post_id + ", " +
                            "'" + comment.UserName + "', " +
                            "'" + comment.UserEmail + "', " +
                            "'" + comment.Date.ToString(GlobalDateFormat) + "', " +
                            "'" + comment.Text + "', " +
                            Comment.STATE_OPEN + ");";

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


        public static int GetNextRef()
        {
            int nextRef = 1;

            SqlDatabase database = new SqlDatabase();
            database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db"));
            DataTable returnData =
                    database.readSql(
                            "select max(comment_id) comment_id from Comment;");

            if (returnData.Rows.Count > 0 &&
                returnData.Rows[0][0].GetType() != typeof(System.DBNull))
                nextRef = Convert.ToInt32(returnData.Rows[0][0]) + 1;

            return nextRef;
        }
    }
}
