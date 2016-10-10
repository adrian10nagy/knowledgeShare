using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.Settings
{
    public static class Configure
    {
        /// <summary>
        /// Connection String
        /// </summary>
        private static string _connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        private static string _connectionStringLog = System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringLog"].ToString();

        public static string ConnectionString
        {
            get { return _connectionString; }
            private set { _connectionString = value; }
        }

        public static string ConnectionStringLog
        {
            get { return _connectionStringLog; }
            private set { _connectionStringLog = value; }
        }

        /// <summary>
        /// Sql Connection
        /// </summary>
        private static SqlConnection _SqlConnection = new SqlConnection(ConnectionString);
        public static SqlConnection SQLConnection
        {
            get { return _SqlConnection; }
            private set { _SqlConnection = value; }
        }

        private static SqlConnection _SqlConnectionLog = new SqlConnection(ConnectionStringLog);
        public static SqlConnection SQLConnectionLog
        {
            get { return _SqlConnectionLog; }
            private set { _SqlConnectionLog = value; }
        }
    }

    public enum Dbs
    {
        MainDb =1,
        LogDb = 2
    }
}