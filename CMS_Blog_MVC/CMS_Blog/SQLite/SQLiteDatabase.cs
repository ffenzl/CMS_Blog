namespace CMS_Blog.SQLite
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Threading;

    using CMS_Blog.Logging;


    public class SqlDatabase
    {
        SQLiteConnection m_conn = null;
        SQLiteTransaction m_transaction = null;
        SQLiteCommand m_cmd = null;

        public bool OpenConnection(string iSqlFile)
        {
            m_conn = new SQLiteConnection("Data Source=" + iSqlFile + ";Version=3", true);
            m_conn.Open();
            return true;
            /*
            if (File.Exists(iSqlFile))
            {
                m_conn = new SQLiteConnection("Data Source=" + iSqlFile + ";Version=3", true);
                m_conn.Open();

                return true;
            }
            else
            {
                return false;
            }*/
        }

        internal bool executeSql(string sqlText, KeyValuePair<string, object> blobParams)
        {
            Dictionary<string, object> dbParams = new Dictionary<string, object>();
            dbParams.Add(blobParams.Key, blobParams.Value);

            return executeSql(sqlText, true, dbParams);
        }

        internal bool executeSql(string sqlText, Dictionary<string, object> blobParams)
        {

            return executeSql(sqlText, true, blobParams);
        }

        internal bool executeSql(string sqlText)
        {
            return executeSql(sqlText, true);
        }


        internal bool executeSql(string sqlText, bool hideException)
        {
            return executeSql(sqlText, hideException, null);
        }

        internal bool executeSql(string sqlText, bool hideException, Dictionary<string, object> blobParams)
        {
            SQLiteCommand cmd = m_conn.CreateCommand();
            cmd.CommandText = sqlText;

            if (blobParams != null &&
                blobParams.Count > 0)
            {
                foreach (KeyValuePair<string, object> paramValue in blobParams)
                {

                    cmd.Parameters.AddWithValue("@" + paramValue.Key, paramValue.Value);
                }
            }

            if (!hideException)
            {
                return executeSql(cmd);
            }
            else
            {
                try
                {
                    return executeSql(cmd);
                }
                catch
                {
                    return false;
                }
            }

        }

        private bool executeSql(SQLiteCommand sqlCmd)
        {
            DateTime dtStartTime = DateTime.Now;

            int retVal = sqlCmd.ExecuteNonQuery();
            bool fCallOk = false;

            if (sqlCmd.CommandText.StartsWith("ALTER", StringComparison.OrdinalIgnoreCase) ||
                sqlCmd.CommandText.StartsWith("CREATE", StringComparison.OrdinalIgnoreCase) ||
                sqlCmd.CommandText.StartsWith("DROP", StringComparison.OrdinalIgnoreCase))
            {
                fCallOk = retVal != 0;
            }
            else
            {
                fCallOk = retVal > 0;
            }

            CMS_Blog.Logging.Logging.writeInfo("executeSql [ Return: " + fCallOk + "] [ Time: " + Math.Round(DateTime.Now.Subtract(dtStartTime).TotalMilliseconds / 1000.0, 3) + "] [" + sqlCmd.CommandText + "]");

            return fCallOk;
        }


        public void BeginTransaction()
        {
            DateTime dtStartTime = DateTime.Now;

            m_transaction = m_conn.BeginTransaction();
            m_cmd = m_conn.CreateCommand();

            Logging.writeInfo("executeSql [ Time: " + Math.Round(DateTime.Now.Subtract(dtStartTime).TotalMilliseconds / 1000.0, 3) + "] [Transaction Begin]");
        }

        public void ExceuteSqlCommit(string sqlCmd)
        {
            DateTime dtStartTime = DateTime.Now;

            m_cmd.CommandText = sqlCmd;
            m_cmd.ExecuteNonQuery();

            Logging.writeInfo("executeSql [ Time: " + Math.Round(DateTime.Now.Subtract(dtStartTime).TotalMilliseconds / 1000.0, 3) + "] [" + sqlCmd + "]");
        }

        public void TransCommit()
        {
            DateTime dtStartTime = DateTime.Now;

            this.m_transaction.Commit();

            Logging.writeInfo("executeSql [ Time: " + Math.Round(DateTime.Now.Subtract(dtStartTime).TotalMilliseconds / 1000.0, 3) + "] [Transaction Commit]");
        }


        internal DataTable readSql(string sqlText)
        {
            SQLiteCommand cmd = m_conn.CreateCommand();
            cmd.CommandText = sqlText;

            return readSql(cmd);
        }


        public static bool CreateDataBase(string iSdfFile)
        {

            SQLiteConnection.CreateFile(iSdfFile);
            int i = 0;
            while (!File.Exists(iSdfFile) && i < 100)
            {
                Thread.Sleep(100);
                i++;
            }

            return File.Exists(iSdfFile);

        }

        private DataTable readSql(SQLiteCommand sqlCmd)
        {
            DateTime dtStartTime = DateTime.Now;

            SQLiteDataAdapter dr = new SQLiteDataAdapter(sqlCmd);
            DataSet dt = new DataSet();
            dt.EnforceConstraints = false;
            dr.Fill(dt, "Result");

            Logging.writeInfo("executeSql [ Return Rows: " + dt.Tables["Result"].Rows.Count + "] [ Time: " + Math.Round(DateTime.Now.Subtract(dtStartTime).TotalMilliseconds / 1000.0, 3) + "] [" + sqlCmd.CommandText + "]");

            return dt.Tables["Result"];

        }
    }
}
