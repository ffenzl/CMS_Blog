using CMS_Blog.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Blog.Models
{
    public partial class Comment
    {
        public static Collection<Comment> GetAll(int post_id)
        {
            Collection<Comment> comments = new Collection<Comment>();

            SqlDatabase database = new SqlDatabase();
            if (database.OpenConnection("SQLite\\CMS_BLOG.db"))
            {
                DataTable returnData =
                        database.readSql("select * from Comment where post_id = " + post_id);

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

                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public static Comment Get(int comment_id)
        {
            return new Comment();
        }
    }
}
