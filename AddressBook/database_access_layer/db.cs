using AddressBook.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.database_access_layer
{
    
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-LTUHE89;Initial Catalog=AddressBooks;Integrated Security=True");
        //AddressBookDbContext con=new AddressBookDbContext();
        public DataSet GetCountry()
        {
            SqlCommand com = new SqlCommand("Sp_Country", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetDivision(int cid)
        {
            SqlCommand com = new SqlCommand("Sp_Division", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Country_id",cid);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetDistrict(int did)
        {
            SqlCommand com = new SqlCommand("Sp_District", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Division_id", did);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
