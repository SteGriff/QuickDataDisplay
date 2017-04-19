using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using QuickDataDisplay.Model;
using System.IO;

namespace QuickDataDisplay.DAL
{
    public class DataLayer
    {
        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
            }
        }

        public DataTable ExecuteSql(string queryName, string commandText)
        {
            var results = new DataTable();
            results.QueryName = queryName;
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            results.ColumnHeadings = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                            
                            while (reader.Read())
                            {
                                var resultLine = new object[reader.VisibleFieldCount];
                                reader.GetValues(resultLine);
                                results.Lines.Add(resultLine);
                            }
                        }
                    }
                }
            }

            return results;
        }

        public List<DataTable> GetTables()
        {
            var results = new List<DataTable>();
            var queryNamesAndContent = GetQueries();

            foreach (var queryNameContent in queryNamesAndContent)
            {
                var thisTable = ExecuteSql(queryNameContent.Key, queryNameContent.Value);
                results.Add(thisTable);
            }

            return results;
        }

        private Dictionary<string,string> GetQueries()
        {
            var commands = new Dictionary<string, string>();

            var request = HttpContext.Current.Request;
            var sqlPath = Path.GetDirectoryName(request.PhysicalPath) + @"\sql\";
            var sqlFiles = Directory.EnumerateFiles(sqlPath, "*.sql");
            foreach (var sqlFile in sqlFiles)
            {
                var thisContent = File.ReadAllText(sqlFile);
                var tidyFileName = Path.GetFileNameWithoutExtension(sqlFile);
                commands.Add(tidyFileName, thisContent);
            }
            if (!sqlFiles.Any())
            {
                commands.Add("Error", "select 'No query files found in sql directory' as Error");
            }

            return commands;
        }
    }
}