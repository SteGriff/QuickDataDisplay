using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickDataDisplay.Model
{
    public class DataTable
    {
        public string QueryName { get; set; }
        public List<string> ColumnHeadings { get; set; }
        public List<object[]> Lines { get; set; }

        public DataTable()
        {
            QueryName = "Untitled Query";
            ColumnHeadings = new List<string>();
            Lines = new List<object[]>();
        }

    }
}