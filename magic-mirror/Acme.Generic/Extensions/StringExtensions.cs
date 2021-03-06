﻿using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a copy of this string with the first letter of each word converted to Upper case.
        /// </summary>
        /// <param name="input">The string to format</param>
        /// <param name="whiteSpaces">Does this string need to have whitespaces between the words? Defaults to true</param>
        /// <returns>A copy of the input string, whitespace optional, first letters uppercased</returns>
        public static string ToCamelCase(this string input, bool whiteSpaces = true)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("Input string cannot be empty");

            string[] words = input.Split(' ');

            foreach (string word in words)
            {
                sb.Append(word.First().ToString().ToUpper() + string.Join("", word.Skip(1)));
                if (whiteSpaces)
                {
                    sb.Append(" ");
                }
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Returns a copy of this string with each first letter uppercased, except for the first one
        /// </summary>
        /// <param name="input">The string to format</param>
        /// <param name="whiteSpaces">Does this string need to have whitespaces between the words? Defaults to true</param>
        /// <returns>A copy of the input string, whitespace optional, first letters uppercased with the first letter of the very first word in lowercase.</returns>
        public static string ToPascalCase(this string input, bool whiteSpaces = true)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("Input string cannot be empty");

            string[] words = input.Split(' ');
            sb.Append(words[0].First().ToString().ToLower());
            if (whiteSpaces)
            {
                sb.Append(" ");
            }

            for (var i = 1; i < words.Length; i++)
            {
                string word = words[i];
                sb.Append(word.First().ToString().ToUpper() + string.Join("", word.Skip(1)));
                if (whiteSpaces)
                {
                    sb.Append(" ");
                }
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Returns the first 3 letters of this string. Used to shorten the name of a day. Eg: Monday -> Mon.
        /// </summary>
        public static string ToShortDayNotation(this DayOfWeek day)
        {
            string shortDay = day.ToString().Substring(0, 3);
            return shortDay;
        }

        /// <summary>
        /// Replace all the commas (,) in a string with a dot (.)
        /// </summary>
        public static string ConvertCommaToDot(this string input)
        {
            var result = input.Replace(',', '.');

            return result;
        }

        /// <summary>
        /// Replace all the dots (.) in a string with a comma (,)
        /// </summary>
        public static string ConvertDotToComma(this string input)
        {
            var result = input.Replace('.', ',');

            return result;
        }

        /// <summary>
        /// Convert an object to a Json object
        /// </summary>
        public static string ToJson(this object input)
        {
            string json = JsonConvert.SerializeObject(input);
            return json;
        }
    }
}