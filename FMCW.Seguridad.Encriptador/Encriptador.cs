﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FMCW.Seguridad
{
    public static class Encriptador
    {
        #region Settings
        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";

        #endregion
        public static string Encriptar(
            string value, 
            string password, 
            string salt = "aseweias38490a76", 
            string vector = "8947az44a5wlkwyt")
        {
            return Encriptar<AesManaged>(value, password, salt, vector);
        }

        public static string Encriptar<T>(string value, string password,
                string salt = "aseweias38490a76",
                string vector = "8947az44a5wlkwyt")
                where T : SymmetricAlgorithm, new()
        {

            byte[] vectorBytes = Encoding.ASCII.GetBytes(vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Desencriptar(string value, string password, 
            string salt = "aseweias38490a76",
            string vector = "8947az44a5wlkwyt")
        {
            return Desencriptar<AesManaged>(value, password, salt, vector);
        }

        public static string Desencriptar<T>(string value, string password,
            string salt = "aseweias38490a76",
            string vector = "8947az44a5wlkwyt") where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;
                using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream from = new MemoryStream(valueBytes))
                    {
                        using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[valueBytes.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
    }
}
