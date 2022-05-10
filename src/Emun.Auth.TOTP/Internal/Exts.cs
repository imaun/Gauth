using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Emun.Auth.Extensions {

    public static class Exts {

        public static string RemoveWhiteSpaces(this string str) {
            return new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }


        public static byte[] ConvertToBytes(this string key, bool isBase32) {
            return isBase32 
                ? Base32Encoding.ToBytes(key)
                : Encoding.UTF8.GetBytes(key);
        }
        
        public static string ComputeHashCode(this byte[] key, long step, int codeLength) {
            var counter = BitConverter.GetBytes(step);

            if(BitConverter.IsLittleEndian) {
                Array.Reverse(counter);
            }

            var hMACSH1 = new HMACSHA1(key);
            var hash = hMACSH1.ComputeHash(counter);
            var offset = hash[hash.Length - 1] & 0xf;

            var intnum = ((hash[offset] & 0x7f) << 24)
                | (hash[offset + 1] << 16)
                | (hash[offset + 2] << 8)
                | hash[offset + 3];
                
            var result = intnum % Convert.ToInt32(Math.Pow(10, codeLength));
            return result.ToString(new string('0', codeLength));
        }

    }
}
