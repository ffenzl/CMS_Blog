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
    public partial class Comment
    {
        public static readonly string GlobalDateFormat = "yyyy-MM-dd HH:mm:ss";

        public static Collection<Comment> GetAll(int post_id, int comment_state)
        {
            Collection<Comment> comments = new Collection<Comment>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                DataTable returnData = database.readSql(
                        "select * " +
                        "  from Comment " + 
                        " where post_id = " + post_id +
                        "   and comment_state = " + comment_state);

                if (returnData.Rows.Count > 0)
                {
                    foreach (DataRow row in returnData.Select())
                    {
                        Comment comment = new Comment();

                        comment.Id = Convert.ToInt32(row.ItemArray[0]);
                        comment.UserName = row.ItemArray[2].ToString();
                        comment.UserEmail = row.ItemArray[3].ToString();
                        comment.Date = Convert.ToDateTime(row.ItemArray[4]);
                        comment.Text = row.ItemArray[5].ToString();
                        comment.State = Convert.ToInt32(row.ItemArray[6]);

                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public static Collection<Comment> GetAll(int comment_state)
        {
            Collection<Comment> comments = new Collection<Comment>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                DataTable returnData = database.readSql(
                        "select * " +
                        "  from Comment " +
                        " where comment_state = " + comment_state);

                if (returnData.Rows.Count > 0)
                {
                    foreach (DataRow row in returnData.Select())
                    {
                        Comment comment = new Comment();

                        comment.Id = Convert.ToInt32(row.ItemArray[0]);
                        comment.UserName = row.ItemArray[2].ToString();
                        comment.UserEmail = row.ItemArray[3].ToString();
                        comment.Date = Convert.ToDateTime(row.ItemArray[4]);
                        comment.Text = row.ItemArray[5].ToString();
                        comment.State = Convert.ToInt32(row.ItemArray[6]);

                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public static bool Update(Comment comment)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement =
                        "update Comment " +
                            "set comment_state = '" + comment.State + " " +
                            "where comment_id = " + comment.Id;

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

        public static bool Create(Comment comment)
        {
            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection(Path.Combine(Startup.GetCurrentRootPath(), @"SQLite\CMS_BLOG.db")))
            {
                string statement =
                        "insert into Comment ( " +
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
