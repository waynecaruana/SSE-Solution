using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace DataAccess
{
    public class SymmetricParameters
    {
        public byte[] IV { get; set; }
        public byte[] Key { get; set; }
    }


    public class EncryptionClass
    {
        public static string Sha512encrypt(string input)
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            SHA512 myAlg = SHA512.Create();
            byte[] digest = myAlg.ComputeHash(inputAsBytes);
            return Convert.ToBase64String(digest);
        }

        //static string salt = "1B045577-6839-42FE-BA40-7B23AD5876CC";

        public static SymmetricParameters GenerateSymmetricParameters(string password, string salt)
        {
            //Generation of KEY and IV //no encryption so far
            Rfc2898DeriveBytes myGenerator = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt));
            RijndaelManaged myalg = new RijndaelManaged();
            SymmetricParameters parameters = new SymmetricParameters();
            parameters.Key = myGenerator.GetBytes(myalg.KeySize / 8);
            parameters.IV = myGenerator.GetBytes(myalg.BlockSize / 8);
            return parameters;

        }

        public static string SymmetricEncrypt(string original, string password, string salt)
        {
            RijndaelManaged myAlg = new RijndaelManaged();

            SymmetricParameters parameters = GenerateSymmetricParameters(password, salt);
            myAlg.Key = parameters.Key;
            myAlg.IV = parameters.IV;

            MemoryStream msOut = new MemoryStream(); //the encyrpted data is written to msOut
            //cryptostream: it is the object that encrypts the data 
            CryptoStream myEncStream = new CryptoStream(msOut, myAlg.CreateEncryptor(), CryptoStreamMode.Write);


            //original >> CryptoStream >> msOut
            byte[] buffer = Encoding.UTF8.GetBytes(original);  // <<<<<<<<<<<<<<<<<<<<<
            myEncStream.Write(buffer, 0, buffer.Length);
            myEncStream.FlushFinalBlock();
            //streamwriter--writes in cyrptostream--then cypto writes in memory
            return Convert.ToBase64String(msOut.ToArray());   // <<<<<<<<<<<<<<<<<<<<< enc data to string
        }

        public static string SymmetricDecrypt(string cipherStr, string password, string salt)
        {
            RijndaelManaged myAlg = new RijndaelManaged();

            SymmetricParameters parameters = GenerateSymmetricParameters(password, salt);
            myAlg.Key = parameters.Key;
            myAlg.IV = parameters.IV;

            byte[] cipherAsBytes = Convert.FromBase64String(cipherStr);
            MemoryStream msIn = new MemoryStream(cipherAsBytes);
            CryptoStream stream = new CryptoStream(msIn, myAlg.CreateDecryptor(), CryptoStreamMode.Read);

            StreamReader sr = new StreamReader(stream);
            string originalString = sr.ReadToEnd();

            return originalString;
        }


        public static void SymmetricEncryptFile(string filename, string password, string salt)
        {
            FileStream fsIn = new FileStream(filename, FileMode.Open);

            RijndaelManaged myAlg = new RijndaelManaged();

            SymmetricParameters parameters = GenerateSymmetricParameters(password, salt);
            myAlg.Key = parameters.Key;
            myAlg.IV = parameters.IV;



            MemoryStream msOut = new MemoryStream(); //the encyrpted data is written to msOut


            //cryptostream: it is the object that encrypts the data 
            CryptoStream myEncStream = new CryptoStream(msOut, myAlg.CreateEncryptor(), CryptoStreamMode.Write);

            //byte[] buffer = new byte[100];
            //int read =0;
            //do
            //{
            //    read = fsIn.Read(buffer, 0, buffer.Length);
            //    myEncStream.Write(buffer, 0, read);
            //    myEncStream.FlushFinalBlock();
            //} while (read >= 100);

            fsIn.CopyTo(myEncStream);

            myEncStream.FlushFinalBlock();

            FileStream fsOut = new FileStream(filename.Replace(".","Enc."), FileMode.OpenOrCreate);
            msOut.Position = 0;
            msOut.CopyTo(fsOut);
            msOut.Flush();
            fsOut.Close();

        }

        public static void DecryptSymmetricFile(string filename, string password, string salt)
        {
            FileStream fsIn = new FileStream(filename, FileMode.Open);



            RijndaelManaged myAlg = new RijndaelManaged();

            SymmetricParameters parameters = GenerateSymmetricParameters(password, salt);
            myAlg.Key = parameters.Key;
            myAlg.IV = parameters.IV;

            MemoryStream msOut = new MemoryStream();
            //cryptostream: it is the object that encrypts the data 
            CryptoStream myEncStream = new CryptoStream(fsIn, myAlg.CreateDecryptor(), CryptoStreamMode.Read);

            myEncStream.CopyTo(msOut);


            FileStream fsOut = new FileStream(filename.Replace(".","Dec."), FileMode.OpenOrCreate);
            msOut.Position = 0;
            msOut.CopyTo(fsOut);
            msOut.Flush();
            fsOut.Close();


        }


        public static string HashString(string input)
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            SHA512 myAlg = SHA512.Create();
            byte[] digest = myAlg.ComputeHash(inputAsBytes);
            return Convert.ToBase64String(digest);
        }

        public static void GeneratePublicPrivateKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            //FileStream fsPublicKey = new FileStream(@"C:\Users\Wayne\Desktop\publicKey.txt", FileMode.OpenOrCreate);
            //FileStream fsPrivateKey = new FileStream(@"C:\Users\Wayne\Desktop\privateKey.txt", FileMode.OpenOrCreate);
            //StreamWriter swpublic = new StreamWriter(fsPublicKey);
            //StreamWriter swprivate = new StreamWriter(fsPrivateKey);

            publicKey = myAlg.ToXmlString(false);
            privateKey = myAlg.ToXmlString(true);



            //swpublic.Write(myAlg.ToXmlString(false));
            //swprivate.Write(myAlg.ToXmlString(true));
            //swpublic.Close(); swprivate.Close();
            //fsPrivateKey.Close(); fsPublicKey.Close();

        }

    
        

        public static string AsymmetricEncryptString(string input)
        {

            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            FileStream fsPublicKey = new FileStream(@"C:\ryan\publicKey.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsPublicKey);
            myAlg.FromXmlString(sr.ReadToEnd());

            byte[] cipher = myAlg.Encrypt(Encoding.UTF8.GetBytes(input), true);
            return Convert.ToBase64String(cipher);

        }


      
        public static int CheckPasswordStrength(string password)//1 Week - 6 Strong
        {
            

                int score = 1;

                if (password.Length < 1)
                    return score;
                if (password.Length < 4)
                    return score;

                if (password.Length >= 6)
                    score++;
                if (password.Length >= 8)
                    score++;
                if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
                    Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                    score++;
                if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                    score++;

                return score;
           
        }

        public static string GenerateSalt(int bytes)
        {
            byte[] buffer = new byte[bytes];
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            randomProvider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
        
    }
}
