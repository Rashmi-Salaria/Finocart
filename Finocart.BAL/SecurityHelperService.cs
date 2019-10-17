using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Finocart.Services
{
    public class SecurityHelperService
    {
        #region "Password Encryption"
        private static string encryptionKey = "1234567890123451";

        /// <summary>
        /// Decryption method
        /// </summary>
        /// <param name="CipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string CipherText)
        {
            return Decrypt(CipherText, encryptionKey);
        }

        /// <summary>
        /// Decryption by private key
        /// </summary>
        /// <param name="CipherText"></param>
        /// <param name="EncryptionPrivateKey"></param>
        /// <returns></returns>
        protected static string Decrypt(string CipherText, string EncryptionPrivateKey)
        {
            if (String.IsNullOrEmpty(CipherText))
                return CipherText;

            TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(CipherText);
            string result = DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
            return result;
        }

        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="PlainText"></param>
        /// <returns></returns>
        public static string Encrypt(string PlainText)
        {
            return Encrypt(PlainText, encryptionKey);
        }

        /// <summary>
        /// Encryption with KEY
        /// </summary>
        /// <param name="PlainText"></param>
        /// <param name="EncryptionPrivateKey"></param>
        /// <returns></returns>
        protected static string Encrypt(string PlainText, string EncryptionPrivateKey)
        {
            if (String.IsNullOrEmpty(PlainText))
                return PlainText;

            TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(EncryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(PlainText, tDESalg.Key, tDESalg.IV);
            string result = Convert.ToBase64String(encryptedBinary);
            return result;
        }

        /// <summary>
        /// Encrypt text to memory
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
        {
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] toEncrypt = new UnicodeEncoding().GetBytes(Data);
            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();
            byte[] ret = mStream.ToArray();
            cStream.Close();
            mStream.Close();
            return ret;
        }

        /// <summary>
        /// Decrypt text from memory
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
        {
            MemoryStream msDecrypt = new MemoryStream(Data);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            StreamReader sReader = new StreamReader(csDecrypt, new UnicodeEncoding());
            return sReader.ReadLine();
        }

        #endregion
    }
}