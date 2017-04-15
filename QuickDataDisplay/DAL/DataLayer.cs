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

        public DataTable ExecuteQueryFile()
        {
            var results = new DataTable();
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var commandText = GetCommandText();
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

        private string GetCommandText()
        {
            var request = HttpContext.Current.Request;
            var sqlPath = Path.GetDirectoryName(request.PhysicalPath) + @"\sql\"; 
            var sqlFiles = Directory.EnumerateFiles(sqlPath, "*.sql");
            var file = sqlFiles.FirstOrDefault();
            if (file == null)
            {
                return "select 'No query files found in sql directory' as Error";
            }
            else
            {
                return File.ReadAllText(file);
            }
 
        }
    }
}