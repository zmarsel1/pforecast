using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Documents
{
    public interface IGlobalValues
    {
        Dictionary<int, string> RPList { get; }
        Dictionary<int, string> FactoryList { get; }
        Dictionary<int, string> HourList { get; }
        Dictionary<int, string> PlanDocType { get; }
        Dictionary<int, string> ForecastDocType { get; }
        Dictionary<int, Dictionary<int, string>> ProductGroups { get; }
        DataTable RoleTable { get; }
        string ConnectionString { get; }
        string ActiveSchema { get; }
    }
    public class GlobalValues : IGlobalValues
    {
        GlobalValues() {}
        static GlobalValues instance = null;
        static public GlobalValues Instance
        {
            get 
            {
                if (instance == null)
                    instance = new GlobalValues();
                return instance;
            }
        }
        Dictionary<int, string> listForecastDocType = new Dictionary<int,string>();
        Dictionary<int, string> listPlanDocType = new Dictionary<int,string>();
        Dictionary<int, string> listRP = new Dictionary<int,string>();
        Dictionary<int, string> listFactory = new Dictionary<int,string>();
        Dictionary<int, string> listHour = new Dictionary<int,string>();
        Dictionary<int, Dictionary<int, string>> productGroups = new Dictionary<int, Dictionary<int, string>>();
        DataTable tableRole = new DataTable();
        string strRole = string.Empty;
        string strSchema = string.Empty;
        string connectionString = string.Empty;
        
        public Exception exception = null;

        public string ActiveSchema { get { return strSchema; } private set { strSchema = value; } }
        public string ActiveRole
        {
            get { return strRole; }
            set
            {
                if (value != strRole)
                {
                    strRole = value;
                    ActiveSchema = tableRole.Rows.Find(ActiveRole)["SchemaName"].ToString().Trim();
                }
            }
        }

        public Dictionary<int, string> RPList { get { return listRP; } }
        public Dictionary<int, string> FactoryList { get { return listFactory; } }
        public Dictionary<int, string> HourList { get { return listHour; } }
        public Dictionary<int, string> PlanDocType { get { return listPlanDocType; } }
        public Dictionary<int, string> ForecastDocType { get { return listForecastDocType; } }
        public Dictionary<int, Dictionary<int, string>> ProductGroups { get { return productGroups; } }
        public DataTable RoleTable { get { return tableRole; } }
        public string ConnectionString { get { return connectionString; } }

        public bool InitUser(string connection)
        {
            connectionString = connection;

            var sql = new SqlConnection(connection);

            try
            {
                sql.Open();
                string sql1 = @"SELECT RTRIM(rs.RoleID) as RoleID, RTRIM(rs.RoleName) as RoleName, RTRIM(rs.SchemaName) as SchemaName
                                FROM dbo.RoleSchemas as rs 
                                WHERE rs.RoleID in (SELECT groups.name FROM sysmembers membs
                                JOIN sysusers users on membs.memberuid = users.uid
                                JOIN sysusers groups on membs.groupuid = groups.uid
                                WHERE users.name = CURRENT_USER)";

                SqlCommand cmd = new SqlCommand(sql1, sql);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tableRole);
                if (tableRole.Rows.Count == 0) throw new Exception("У данного пользователя нет необходимых ролей.");
                DataColumn key = tableRole.Columns["RoleID"];
                tableRole.PrimaryKey = new DataColumn[] { key };
                if (ActiveRole == string.Empty)
                {
                    ActiveRole = tableRole.Rows[0]["RoleID"].ToString().Trim();
                }
            }
            catch (Exception exc)
            {
                this.exception = exc;
                return false;
            }
            sql.Close();
            return true;
        }
        public bool InitData()
        {
            SqlConnection sql = new SqlConnection(ConnectionString);
            listFactory.Clear();
            listHour.Clear();
            listPlanDocType.Clear();
            listRP.Clear();
            listForecastDocType.Clear();

            try
            {
                sql.Open();
            }
            catch (Exception exc)
            {
                exception = exc;
                return false;
            }
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sql;
                SqlDataReader reader = null;

                if (ActiveRole.Contains("energy"))
                {
                    cmd.CommandText = string.Format("SELECT RPID, RPName FROM {0}.RP", ActiveSchema);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listRP[reader.GetInt32(0)] = reader.GetString(1).Trim();
                    }
                    reader.Close();

                    cmd.CommandText = string.Format("SELECT DocTypeID, DocTypeName FROM dbo.ForecastDocumentType");
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listForecastDocType[reader.GetInt32(0)] = reader.GetString(1).Trim();
                    }
                    reader.Close();
                }
                
                cmd.CommandText = string.Format("SELECT FactoryID, FactoryName FROM {0}.Factory", ActiveSchema);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listFactory[reader.GetInt32(0)] = reader.GetString(1).Trim();
                }
                reader.Close();
                cmd.CommandText = "SELECT HourID, HourName FROM dbo.HourTable";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listHour[reader.GetInt32(0)] = reader.GetString(1).Trim();
                 }
                reader.Close();
                cmd.CommandText = string.Format("SELECT ProductID, ProductName, GroupID FROM {0}.Product",ActiveSchema);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!productGroups.ContainsKey(reader.GetInt32(2))) productGroups[reader.GetInt32(2)] = new Dictionary<int, string>();
                    productGroups[reader.GetInt32(2)][reader.GetInt32(0)] = reader.GetString(1).Trim();
                }

                reader.Close(); 
                cmd.CommandText = "SELECT DocTypeID, DocTypeName FROM dbo.DocumentType";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listPlanDocType[reader.GetInt32(0)] = reader.GetString(1).Trim();
                }
                reader.Close();
            }
            catch (Exception exc)
            {
                exception = exc;
                return false;
            }
            sql.Close();
            return true;
        }
    }
}
