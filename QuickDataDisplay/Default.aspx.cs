using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuickDataDisplay.DAL;
using QuickDataDisplay.Model;
using System.Web.UI.WebControls;

namespace QuickDataDisplay
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        private List<DataTable> _tables = null;
        protected List<DataTable> Tables
        {
            get
            {
                if (_tables == null)
                {
                    var dataLayer = new DataLayer();
                    _tables = dataLayer.GetTables();
                }
                return _tables;
            }
        }
    }
}
