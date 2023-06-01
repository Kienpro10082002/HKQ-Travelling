using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace HKQTravel.Assets.User
{
    public static class StringHelper
    {
        public static string Truncate(string input, int length)
        {
            if (input.Length < length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }

        public static string BreakDownLine(string input, int length)
        {
            if (input.Length < length)
            {
                return input;
            }
            else
            {
                string substring = input.Substring(0, length);
                int lastSpaceIndex = substring.LastIndexOf(' ');

                if (lastSpaceIndex != -1)
                {
                    substring = substring.Substring(0, lastSpaceIndex);
                }

                return substring + "\n" + BreakDownLine(input.Substring(substring.Length).TrimStart(), length);
            }
        }
    }
}