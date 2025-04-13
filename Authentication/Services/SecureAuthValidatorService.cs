using System.Security.Cryptography;
using System.Text;
using Authentication.Model;
using Authentication.Provider;
using Authentication.Provider.KeyProvider;

namespace Authentication.Services
{
    public class SecureAuthValidatorService(IAuthProvider provider, IKeyProvider keyProvider): IAuthService
    {
        public bool Validate(User user)
        {
            string encryptedPwd = provider.FetchCredential(user.MailId);

            byte[] encryptedPwdBytes = Convert.FromHexString(encryptedPwd);
            byte[] encrSaltHashedPwd = new byte[encryptedPwdBytes.Length-16];
            byte[] IV = new byte[16];
            int ind = 0;
            for (int i = 0; i < 16; i++)
            {
                IV[i] = encryptedPwdBytes[ind++];
            }
            for (int i = 0; i < encryptedPwdBytes.Length - 16; i++)
            {
                encrSaltHashedPwd[i] = encryptedPwdBytes[ind++];
            }

            byte[] saltHashPwd = Decrypt(encrSaltHashedPwd, IV, Convert.FromHexString(keyProvider.GetKey()));
            byte[] salt = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                salt[i] = saltHashPwd[i];
            }

            return ValidatePassword(user.Password, salt, saltHashPwd);
        }

        private byte[] Decrypt(byte[] encryptedSaltHashedPwd, byte[] iv, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            return decryptor.TransformFinalBlock(encryptedSaltHashedPwd, 0, encryptedSaltHashedPwd.Length);
        }

        private bool ValidatePassword(string pwd, byte[] salt, byte[] saltHash)
        {
            using (var sha = SHA256.Create())
            {
                byte[] pwdBytes = Encoding.UTF8.GetBytes(pwd);
                byte[] saltPwdBytes = new byte[salt.Length + pwdBytes.Length];

                for (int i = 0; i < salt.Length; i++)
                {
                    saltPwdBytes[i] = salt[i];
                }

                for (int i = 0; i < pwdBytes.Length; i++)
                {
                    saltPwdBytes[i+16] = pwdBytes[i];
                }

                byte[] hashedPwd = sha.ComputeHash(saltPwdBytes);

                if (hashedPwd.Length != saltHash.Length - 16)
                {
                    return false;
                }

                for (int i = 0; i < hashedPwd.Length; i++)
                {
                    if (hashedPwd[i] != saltHash[i + 16])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
