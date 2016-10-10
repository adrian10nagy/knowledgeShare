using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Settings
{
    public class SqlDatabase<ConnectionType, CommandType, ParameterType>
        where ConnectionType : IDbConnection, new()
        where CommandType : IDbCommand, new()
        where ParameterType : IDbDataParameter, new()
    {
        #region variabiles

        public static string ConnectionStringSet { get; set; }
        public static string ConnectionStringSetLog { get; set; }
        public static System.Data.CommandType CommandTypeSet { get; set; }
        public delegate void ReadRow(IDataReader reader);

        #endregion

        public SqlDatabase(string connectionString, Dbs dbEnum)
        {
            switch (dbEnum)
            {
                case Dbs.MainDb:
                    Set(connectionString, System.Data.CommandType.StoredProcedure);
                    break;

                case Dbs.LogDb:
                    SetLog(connectionString, System.Data.CommandType.StoredProcedure);
                    break;
            }
        }

        void Set(string connectionString, System.Data.CommandType commandType)
        {
            ConnectionStringSet = connectionString;
            CommandTypeSet = commandType;
        }

        void SetLog(string connectionStringLog, System.Data.CommandType commandType)
        {
            ConnectionStringSetLog = connectionStringLog;
            CommandTypeSet = commandType;
        }

        //returns void OR list of Objects
        #region Execute
        public List<Object> Execute(string query, ParameterType[] parameters)
        {
            return Execute(ConnectionStringSet, CommandTypeSet, query, parameters);
        }

        public List<Object> Execute(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.ConnectionString = connectionString;
                command.CommandText = query;
                command.CommandType = commandType;
                connection.Open();
                //command.Connection = connection;


                using (var reader = command.ExecuteReader())
                {
                    var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    var Object = new Object();

                    List<Object> personList = new List<Object>();


                    while (reader.Read())
                    {
                        foreach (var column in columns)
                        {
                            var col = reader[column];
                        }
                    }
                }
                var results = new List<Object>();
                     
                connection.Close();
                return results;
            }
        }
        #endregion

        public void Execute(string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            Execute(ConnectionStringSet, System.Data.CommandType.StoredProcedure, query, parameters, rowMethod);
            //writeLog(query, parameters);
        }

        //public void Execute(System.Data.CommandType commandType, string query, ParameterType[] parameters, ReadRow rowMethod)
        //{
        //    Execute(ConnectionStringSet, commandType, query, parameters, rowMethod);
        //}

        //public void Execute(string connectionString, string query, ParameterType[] parameters, ReadRow rowMethod)
        //{
        //    Execute(connectionString, System.Data.CommandType.StoredProcedure, query, parameters, rowMethod);
        //}

        public void Execute(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            using (var connection = new ConnectionType())
            using (var command = new CommandType())
            {
                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = commandType;
                if (parameters != null)
                    Array.ForEach(parameters, p => command.Parameters.Add(p));

                connection.ConnectionString = connectionString;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        if (rowMethod != null)
                            rowMethod(reader);
                }
            }
        }

		public int ExecuteNonQuery(string query, ParameterType[] parameters)
		{
            //writeLog(query, parameters);
			return ExecuteNonQuery(ConnectionStringSet, System.Data.CommandType.StoredProcedure, query, parameters);
		}

        public int ExecuteNonQueryLog(string query, ParameterType[] parameters)
        {
            return ExecuteNonQuery(ConnectionStringSetLog, System.Data.CommandType.StoredProcedure, query, parameters);
        }

        //public int ExecuteNonQuery(System.Data.CommandType commandType, string query, ParameterType[] parameters)
        //{
        //    return ExecuteNonQuery(ConnectionStringSet, commandType, query, parameters);
        //}

		public int ExecuteNonQuery(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters)
		{
			var rowsAffected = 0;
			using (var connection = new ConnectionType())
			using (var command = new CommandType())
			{
				command.Connection = connection;
				command.CommandText = query;
				command.CommandType = commandType;
				if (parameters != null)
					Array.ForEach(parameters, p => command.Parameters.Add(p));

				connection.ConnectionString = connectionString;
				connection.Open();
				rowsAffected = command.ExecuteNonQuery();
			}
			return rowsAffected;
		}



        //public object ExecuteScalar(string query, ParameterType[] parameters)
        //{
        //    return ExecuteScalar(ConnectionStringSet, System.Data.CommandType.StoredProcedure, query, parameters);
        //}
        //public object ExecuteScalar(System.Data.CommandType commandType, string query, ParameterType[] parameters)
        //{
        //    return ExecuteScalar(ConnectionStringSet, commandType, query, parameters);
        //}
        //public object ExecuteScalar(string connectionString, string query, ParameterType[] parameters)
        //{
        //    return ExecuteScalar(connectionString, System.Data.CommandType.StoredProcedure, query, parameters);
        //}
        //public object ExecuteScalar(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters)
        //{
        //    object result = null;
        //    using (var connection = new ConnectionType())
        //    using (var command = new CommandType())
        //    {
        //        command.Connection = connection;
        //        command.CommandText = query;
        //        command.CommandType = commandType;
        //        if (parameters != null)
        //            Array.ForEach(parameters, p => command.Parameters.Add(p));

        //        connection.ConnectionString = connectionString;
        //        connection.Open();
        //        result = command.ExecuteScalar();
        //    }
        //    return result;
        //}

        // LogWrite 

        public void writeLog(string query, ParameterType[] parameters)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("exec " + query + " ");

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterName.ToString().Contains("date") || parameters[i].ParameterName.ToString().Contains("created"))
                    {
                        parameters[i].Value = ((DateTime)parameters[i].Value).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    sb.Append(parameters[i].ParameterName + " = '" + parameters[i].Value+"'");
                    if (i + 1 < parameters.Length)
                    {
                        sb.Append(", ");
                    }
                }
            }
            var trafficLog = new TrafficLog();
            trafficLog.Created = DateTime.Now;
            trafficLog.CodeText = sb.ToString();
            trafficLog.LogType = LogType.Write;

            DAL.SDK.Kit.Instance.TrafficLogs.WriteLog(trafficLog);
        }

    }
}
