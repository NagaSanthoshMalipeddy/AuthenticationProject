using System.Data;
using Microsoft.Data.SqlClient;

namespace Authentication.Provider.KeyProvider
{
    public class KeyProvider(IConfiguration config): IKeyProvider
    {
        public string GetKey()
        {
            using var con = new SqlConnection(config.GetConnectionString("defaultConnString"));
            con.Open();
            var cmd = new SqlCommand("dbo.GetKey", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string result = cmd.ExecuteScalar()?.ToString() ?? string.Empty;
            con.Close();
            return result;
        }
    }
}
