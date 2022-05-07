using System;
using System.Drawing;
using System.Security.Cryptography;
using Emun.GAuth.Extensions;

namespace Emun.GAuth
{

    public class GoogleAuthenticator
    {

        private static readonly DateTime _epoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        private byte[] _byteArr = new byte[10];

        private long getCounter(int counter)
            => Convert.ToInt64((DateTime.UtcNow - _epoch).TotalSeconds / counter);

        public string GetStupCode(string account, string appName, byte[] secrectKey) {
            if(string.IsNullOrEmpty(account)) {
                throw new ArgumentNullException("Account name is null or empty.");
            }

            var normalizedAccount = account.RemoveWhiteSpaces();

            var encodedSecretKey = Base32Encoding.ToBase32String(secrectKey);

            //TODO 
            return null;

        }
    }
}
