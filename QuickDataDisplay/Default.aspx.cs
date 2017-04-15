using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuickDataDisplay.DAL;
using QuickDataDisplay.Model;

namespace QuickDataDisplay
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        private DataTable _results = null;
        protected DataTable Results
        {
            get
            {
                if (_results == null)
                {
                    var dataLayer = new DataLayer();
                    _results = dataLayer.ExecuteQueryFile();
                }
                return _results;
            }
        }
    }
}
