using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Kiosk_Managment.Models
{
    public class Cryption
    {
        public static string Encrypt(HashAlgorithm hashAlgorithm, string value)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }
    }
}