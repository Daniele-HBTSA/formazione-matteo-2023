using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HBTSA.Libraries.ExtensionMethods
{
    public static partial class StringExtension
    {
        public static string ConvertNullToEmpty(this string? str)
        {
            if (str == null) str = "";
            return str;
        }
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";
            else
                return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static bool ContainsIgnoreCase(this List<string> input, string element)
        {
            List<string> inputTrimTolower = new List<string>();
            input.ForEach(x => inputTrimTolower.Add(x.TrimTolower()));
            return inputTrimTolower.Contains(element.TrimTolower());
        }

        public static bool ContainsIgnoreCase(this string input, string element)
        {
            input = input.TrimTolower();
            return input.Contains(element.TrimTolower());
        }

        public static string TrimTolower(this string input)
        {
            string a = input.Trim().ToLower();
            return a;
        }

        public static bool EqualsIgnoreCase(this string input, string element)
        {
            return input.TrimTolower() == element.TrimTolower();
        }

        public static bool StartsWithIgnoreCase(this string input, string element)
        {
            input = input.TrimTolower();
            return input.StartsWith(element.TrimTolower());
        }

        public static string Truncate(this string input, int len)
        {
            if (String.IsNullOrEmpty(input))
                return "";
            else
            {
                int lenTmp = input.Length;
                if (lenTmp <= len)
                {
                    return input.PadLeft(len - lenTmp) + "   ";
                }
                else
                {
                    return string.Format("{0}...", input.Substring(0, len - 1));
                }
            }
        }

        public static string ComposeQueryStringUrl<T>(this string input, string queryStringVarName, T queryStringVarValue)
        {
            string valueTmp = "";
            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                DateTime? convertedValue = Convert<DateTime?>(queryStringVarValue);
                if (convertedValue != null)
                    valueTmp = $"{queryStringVarName}={convertedValue.Value.Year}-{convertedValue.Value.Month}-{convertedValue.Value.Day}";
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                valueTmp = $"{queryStringVarName}={queryStringVarValue}";
            else if (typeof(T) == typeof(string))
                valueTmp = $"{queryStringVarName}={queryStringVarValue}";
            else
                throw new TypeAccessException($"{typeof(T)} is not supportend");
            if (input.Length > 0) 
                return $"{input}&{valueTmp}";
            else
                return valueTmp;
        }

        public static object? Convert(this object value, Type t)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(t);

            if (underlyingType != null && value == null)
            {
                return null;
            }
            Type basetype = underlyingType == null ? t : underlyingType;
            return System.Convert.ChangeType(value, basetype);
        }

        public static T Convert<T>(this object value)
        {
            return (T)value.Convert(typeof(T));
        }

        public static decimal Similarity(this string input, string stringToCompare)
        {
            if (input == null || stringToCompare == null)
            {
                throw new ArgumentException("Strings must not be null");
            }
          
            decimal maxLength = Math.Max(input.Length, stringToCompare.Length);
            if (maxLength > 0)
            {
                // facoltativamente ignora le maiuscole se necessario
                return (maxLength - getEditDistance(input, stringToCompare)) / maxLength;
            }
            return 1.0m;
        }

        private static int getEditDistance(string X, string Y)
        {
            int m = X.Length;
            int n = Y.Length;

            int[][] T = new int[m + 1][];
            for (int i = 0; i < m + 1; ++i)
            {
                T[i] = new int[n + 1];
            }

            for (int i = 1; i <= m; i++)
            {
                T[i][0] = i;
            }
            for (int j = 1; j <= n; j++)
            {
                T[0][j] = j;
            }

            int cost;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    cost = X[i - 1] == Y[j - 1] ? 0 : 1;
                    T[i][j] = Math.Min(Math.Min(T[i - 1][j] + 1, T[i][j - 1] + 1),
                            T[i - 1][j - 1] + cost);
                }
            }

            return T[m][n];
        }
    }
}
