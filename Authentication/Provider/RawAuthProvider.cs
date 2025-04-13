using System.Data;
using Microsoft.Data.SqlClient;

namespace Authentication.Provider
{
    public class RawAuthProvider(IConfiguration config): IAuthProvider
    {
        public string FetchCredential(string mailId)
        {
            using SqlConnection con = new SqlConnection(config.GetConnectionString("defaultConnString"));
            con.Open();
            SqlCommand cmd = new SqlCommand("dbo.FetchPassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mailId", mailId);
            string? result = cmd.ExecuteScalar()?.ToString();
            con.Close();
            return result ?? string.Empty;
        }
    }
}
