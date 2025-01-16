using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanToptan_Fatura_Control
{
    class SqlConnectionClass
    {
        public SqlConnection baglanti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
            SqlConnection baglan = new SqlConnection(connectionString);
            baglan.Open();
            return baglan;
        }

    }
}
