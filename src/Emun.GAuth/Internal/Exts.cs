using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Emun.GAuth.Extensions
{
    public static class Exts
    {

        public static string RemoveWhiteSpaces(this string str) {
            return new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

    }
}
