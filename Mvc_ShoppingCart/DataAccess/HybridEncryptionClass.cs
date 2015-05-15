using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web;
using Common;

namespace DataAccess
{
    public class HybridEncryptionClass
    {
        public string salt = Guid.NewGuid().ToString();
        private const int bufferSize = 100;
        public byte[] key;
        public byte[] IV;
        public void Encryption(string path, string outputPath, string pass)
        {
            var algo = new RijndaelManaged() { KeySize = 128, BlockSize = 128 };
            FileStream inputFile = new FileStream(path, FileMode.Open, FileAccess.Read);
            var KEYS = new Rfc2898DeriveBytes(pass, Encoding.UTF8.GetBytes(salt));
            algo.Key = KEYS.GetBytes(algo.KeySize / 8);
            algo.IV = KEYS.GetBytes(algo.BlockSize / 8);
            key = algo.Key;
            IV = algo.IV;
            MemoryStream msOut = new MemoryStream();
            CryptoStream cryStream = new CryptoStream(msOut, algo.CreateEncryptor(), CryptoStreamMode.Write);
            inputFile.CopyTo(cryStream);
            inputFile.Flush();
            inputFile.Close();
            cryStream.FlushFinalBlock();
            FileStream outputFile = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
            msOut.Position = 0;
            msOut.CopyTo(outputFile);
            msOut.Flush();
            msOut.Close();
        }


        public MemoryStream Decryption(string fileName, string outputPath, byte[] Key, byte[] IV, int productid)
        {
            FileStream fsIn = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            RijndaelManaged algo = new RijndaelManaged() { KeySize = 128, BlockSize = 128 };
            algo.Key = Key;
            algo.IV = IV;
            MemoryStream msOut = new MemoryStream();
            CryptoStream decrStream = new CryptoStream(fsIn, algo.CreateDecryptor(), CryptoStreamMode.Read);
            decrStream.CopyTo(msOut);

           
            FileStream fsOut = new FileStream(outputPath, FileMode.OpenOrCreate);
            //get product signiture
            Product p = new ProductRepository().GetProductsByID(productid);
            User u = new UserRepository().GetUserByEmail(p.SellerEmail);
            MemoryStream temp = msOut;
            msOut.Position = 0;
            msOut.CopyTo(fsOut);
            msOut.Flush();
            msOut.Close();
            fsOut.Flush();
            fsOut.Close();
            fsIn.Flush();
            fsIn.Close();
            bool ver = VerifyFile(outputPath, u.PublicKey, p.Signiture);
            if (ver)
            {
                return temp;
            }
             else return null;
        }

        public string AsymmetricEncryption(string PublicKey, byte[] keys)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PublicKey);
            byte[] cipher = rsa.Encrypt(keys, false);
            return Convert.ToBase64String(cipher);
        }
        public byte[] AsymmetricDecryption(string PrivateKey, string keys)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);
            byte[] data = rsa.Decrypt(Convert.FromBase64String(keys), false);
            return data;
        }


        //signing

        public static string Hashing(string input)
        {
            SHA256 myAlg = SHA256.Create();
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);
            byte[] digest = myAlg.ComputeHash(data);
            return Convert.ToBase64String(digest);
        }

        public static string SignFile(string inputData, string privateKeyfilename)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(privateKeyfilename);
            
            byte[] digest = Convert.FromBase64String(Hashing(System.IO.File.ReadAllText(inputData)));
            byte[] signature = myAlg.SignHash(digest, "SHA256");
            return Convert.ToBase64String(signature);
        }
        public static bool VerifyFile(string inputData, string publicKey, string signature)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(publicKey);
            string pubKey = myAlg.ToXmlString(false);
            //sr = new StreamReader(pubKey);
            myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(pubKey);
            byte[] digest = Convert.FromBase64String(Hashing(System.IO.File.ReadAllText(inputData)));
            return myAlg.VerifyHash(digest, "SHA256", Convert.FromBase64String(signature));
        }
    }
}
