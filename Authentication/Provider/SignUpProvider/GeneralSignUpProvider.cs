using System.Data;
using Authentication.Model;
using Authentication.Services.SignUpService;
using Microsoft.Data.SqlClient;

namespace Authentication.Provider.SignUpProvider
{
    public class GeneralSignUpProvider(IConfiguration config, ILogger<ISignUpService> logger): ISignUpProvider
    {
        public bool CompleteSignUp(User user)
        {
            try
            {
                using SqlConnection con = new SqlConnection(config.GetConnectionString("defaultConnString"));
                con.Open();
                var cmd = new SqlCommand("dbo.InsertProc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mailId", user.MailId);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
