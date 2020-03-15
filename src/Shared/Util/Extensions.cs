﻿#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Net;

namespace Agonyl.Shared.Util
{
    public static class Extensions
    {
        /// <summary>
        /// Calculates differences between 2 strings.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compare"></param>
        /// <param name="caseSensitive"></param>
        /// <remarks>
        /// http://en.wikipedia.org/wiki/Levenshtein_distance
        /// </remarks>
        /// <example>
        /// <code>
        /// "test".LevenshteinDistance("test")       // == 0
        /// "test1".LevenshteinDistance("test2")     // == 1
        /// "test1".LevenshteinDistance("test1 asd") // == 4
        /// </code>
        /// </example>
        public static int LevenshteinDistance(this string str, string compare, bool caseSensitive = true)
        {
            if (!caseSensitive)
            {
                str = str.ToLower();
                compare = compare.ToLower();
            }

            var sLen = str.Length;
            var cLen = compare.Length;
            var result = new int[sLen + 1, cLen + 1];

            if (sLen == 0)
            {
                return cLen;
            }

            if (cLen == 0)
            {
                return sLen;
            }

            for (var i = 0; i <= sLen; result[i, 0] = i++)
            {
                ;
            }

            for (var i = 0; i <= cLen; result[0, i] = i++)
            {
                ;
            }

            for (var i = 1; i <= sLen; i++)
            {
                for (var j = 1; j <= cLen; j++)
                {
                    var cost = (compare[j - 1] == str[i - 1]) ? 0 : 1;
                    result[i, j] = Math.Min(Math.Min(result[i - 1, j] + 1, result[i, j - 1] + 1), result[i - 1, j - 1] + cost);
                }
            }

            return result[sLen, cLen];
        }

        /// <summary>
        /// Returns IP address as integer.
        /// </summary>
        /// <example>
        /// IPAddress.Parse("127.0.0.1").ToInt32(); // 0x0100007F
        /// </example>
        /// <param name="ipAddress"></param>
        public static int ToInt32(this IPAddress ipAddress)
        {
            return BitConverter.ToInt32(ipAddress.GetAddressBytes(), 0);
        }

        /// <summary>
        /// Returns DateTime as 32-bit unix timestamp.
        /// </summary>
        /// <param name="dt"></param>
        public static int ToUnixTimeSeconds(this DateTime dt)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)(dt.ToUniversalTime() - epoch).TotalSeconds;
        }
    }
}
