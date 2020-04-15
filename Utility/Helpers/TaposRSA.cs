
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PemUtils;

namespace Utility.Helpers
{
   public class TaposRSA
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public TaposRSA(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void GenerateRsaKey(string version)
        {
            var rsa = RSA.Create();

            rsa.KeySize = Convert.ToInt32(_configuration.GetSection("RSA:KeySize").Value);
            var directory = Path.Combine(_env.ContentRootPath, _configuration.GetSection("RSA:KeyLocation").Value, version);
            bool exists = System.IO.Directory.Exists(directory);
            if(!exists)
                System.IO.Directory.CreateDirectory(directory);
            var genpublicKey = Path.Combine(directory, @"publickey.pem");
            var genprivatekey = Path.Combine(directory, @"privatekey.pem");
            using (var fs = File.Create(genprivatekey))
            {
                using (var pem = new PemWriter(fs))
                {
                    pem.WritePrivateKey(rsa);
                }
            }

            using (var fs = File.Create(genpublicKey))
            {
                using (var pem = new PemWriter(fs))
                {
                    pem.WritePublicKey(rsa);
                }
            }
        }


        public string EncryptData(string plainText, string version)
        {
            using (var rsa = GetRSACryptoProvider(version,true))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                var cipherTextBytes = rsa.Encrypt(plainTextBytes, RSAEncryptionPadding.Pkcs1);
                var cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;
            }
        }


        public string Decrypt(string plainText, string version)
        {
            using (var rsa = GetRSACryptoProvider(version,false))
            {

                var cipherTextBytes = Convert.FromBase64String(plainText);
                var plainTextBytes = rsa.Decrypt(cipherTextBytes, RSAEncryptionPadding.Pkcs1);
                plainText = Encoding.UTF8.GetString(plainTextBytes);
            }

            return plainText;
        }

        



        public RSA GetRSACryptoProvider(string version,bool isPulicKey)
        {
            var rsa = RSA.Create();

            try
            {

                var directory = Path.Combine(_env.ContentRootPath, _configuration.GetSection("RSA:KeyLocation").Value, version, isPulicKey? @"publickey.pem": @"privatekey.pem");
                using (var privateKey = File.OpenRead(directory))
                {
                    using (var pem = new PemReader(privateKey))
                    {
                        var rsaParameters = pem.ReadRsaKey();
                        rsa.ImportParameters(rsaParameters);
                    }
                }
                return rsa;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetRSACryptoProvider(): {ex}");
                return null;
            }
        }
    }
}
