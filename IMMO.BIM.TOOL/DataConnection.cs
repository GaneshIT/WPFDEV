using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
namespace IMMO.BIM.TOOL
{
    class DataConnection
    {
        public static string sqlconstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Immo_BIM.mdb";

        public static DataTable GetData(string query)
        {
            DataTable dt = null;
            try
            {
                OleDbConnection con = new OleDbConnection(sqlconstr);
                OleDbDataAdapter adp = new OleDbDataAdapter(query, con);
                dt = new DataTable();
                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public static string ExecuteQuery(string query)
        {
            string msg = string.Empty;
            try
            {
                OleDbConnection con = new OleDbConnection(sqlconstr);
                OleDbDataAdapter adp = new OleDbDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                msg = "Executed";
            }
            catch (Exception ex)
            {

            }
            return msg;
        }
    }
}
