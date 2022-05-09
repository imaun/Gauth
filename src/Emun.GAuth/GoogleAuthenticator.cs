﻿using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography;
using Emun.GAuth.Extensions;

namespace Emun.GAuth
{

    /// <summary>
    /// Helper class to Generate PINs that matches PINs generated by the Google Authenticator for an Account with an already
    /// generated setup key in an application.
    /// </summary>
    public class GoogleAuthenticator
    {

        private static readonly DateTime _epoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private int _defaultSeconds = 30;
        private byte[] _byteArr = new byte[10];

        private long getCounter(long counter)
            => Convert.ToInt64((DateTime.UtcNow - _epoch).TotalSeconds / counter);

        private long getDefaultCounter() => getCounter(_defaultSeconds);

        public string GetStupCode(string account, string appName, string secrectKey) {
            if(string.IsNullOrEmpty(account)) {
                throw new ArgumentNullException("Account name is null or empty.");
            }

            var encodedSecretKey = secrectKey.ConvertToBytes(isBase32: true);
            var normalizedAccount = Uri.EscapeUriString(account.RemoveWhiteSpaces());
            //TODO generate qrcode

            var result = Base32Encoding.ToBase32String(encodedSecretKey);
            
            return result.Trim('=');
        }
        

        public string GeneratePIN(string secretKey, long counter, int codeLength = 6) {
            var secretKeyBytes = secretKey.ConvertToBytes(isBase32: true);
            return secretKeyBytes.ComputeHashCode(getCounter(counter), codeLength);
        }

        public string GetCurrentPIN(string secrectKey, int codeLength = 6) {
            var secretKeyBytes = secrectKey.ConvertToBytes(isBase32: true);
            return secretKeyBytes.ComputeHashCode(getDefaultCounter(), codeLength);
        }


        /// <summary>
        /// Get All the possible PIN codes for the given timeWindow.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="timeWindow"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public List<string> GetAllPINS(string secretKey, TimeSpan timeWindow, int codeLength = 6) {
            var result = new List<string>();
            var counter = getDefaultCounter();
            var offset = 0;

            if(timeWindow.TotalSeconds > _defaultSeconds) {
                offset = Convert.ToInt32(timeWindow.TotalSeconds / _defaultSeconds);
            }

            var start = counter - offset;
            var end = counter + offset;

            //generates all PINs within the given timeWindow
            for(var c = start; c <= end; c++) {
                result.Add(GeneratePIN(secretKey, c, codeLength));
            }

            return result;
        }

        /// <summary>
        /// Validates a User input PIN against a List of possible PINs in the given timeWindow, based on secretKey.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="inputPIN"></param>
        /// <param name="timeWindow"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public bool ValidatePIN(string secretKey, string inputPIN, TimeSpan timeWindow, int codeLength = 6) {
            var PINs = GetAllPINS(secretKey, timeWindow, codeLength);
            return PINs.Any(_=> _ == inputPIN);
        }
        
    }
}
