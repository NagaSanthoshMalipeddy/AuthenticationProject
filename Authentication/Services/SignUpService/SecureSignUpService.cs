using System.Security.Cryptography;
using System.Text;
using Authentication.Model;
using Authentication.Provider.KeyProvider;
using Authentication.Provider.SignUpProvider;

namespace Authentication.Services.SignUpService
{
    public class SecureSignUpService(IKeyProvider provider, ISignUpProvider signUpProvider): ISignUpService
    {
        public bool CompleteSignUp(User user)
        {
            byte[] salt = GenerateSalt(); // salt of 16 bytes
            byte[] hashedPwd = HashedPassword(user.Password, salt);

            byte[] saltHashedPwd = new byte[hashedPwd.Length+salt.Length];
            int ind = 0;
            foreach (var val in salt)
            {
                saltHashedPwd[ind++] = val;
            }
            foreach (var val in hashedPwd)
            {
                saltHashedPwd[ind++] = val;
            }

            byte[] IV = GenerateSalt();
            byte[] encryptedPwd = EncryptPassword(saltHashedPwd, Convert.FromHexString(provider.GetKey()), IV);

            byte[] pwdToStore = new byte[IV.Length + encryptedPwd.Length];
            ind = 0;
            foreach (var val in IV)
            {
                pwdToStore[ind++] = val;
            }
            foreach (var val in encryptedPwd)
            {
                pwdToStore[ind++] = val;
            }

            user.Password = Convert.ToHexString(pwdToStore);
            return signUpProvider.CompleteSignUp(user);
        }

        private byte[] EncryptPassword(byte[] hashedSaltPwd, byte[] key, byte[] IV)
        {
            using var aes = new AesCryptoServiceProvider();
            aes.Key = key;
            aes.IV = IV;
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            return encryptor.TransformFinalBlock(hashedSaltPwd, 0, hashedSaltPwd.Length);
        }

        private byte[] GenerateSalt(int size = 16)
        {
            using var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }

        private byte[] HashedPassword(string password, byte[] salt)
        {
            using var sha = SHA256.Create();
            byte[] pwd = Encoding.UTF8.GetBytes(password);
            byte[] saltPwd = new byte[salt.Length+pwd.Length];
            int ind = 0;
            foreach (var val in salt)
            {
                saltPwd[ind++] = val;
            }
            foreach (var val in pwd)
            {
                saltPwd[ind++] = val;
            }
            return sha.ComputeHash(saltPwd);
        }
    }
}
