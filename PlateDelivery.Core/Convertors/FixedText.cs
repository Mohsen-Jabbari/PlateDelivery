﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PlateDelivery.Core.Convertors
{
   public class FixedText
    {
        public static string FixedEmail(string email)
        {
            return toEnglishNumber(email.Trim().ToLower()).Replace("ي","ی").Replace("ك", "ک");
        }

        public static string FixedMobile(string mobile)
        {
            return toEnglishNumber(mobile.Trim());
        }

        public static string FixedTxt(string text)
        {
            return toEnglishNumber(text.Trim()).Replace("ي", "ی").Replace("ك", "ک");
        }

        public static string toEnglishNumber(string input)
        {
            string EnglishNumbers = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    EnglishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    EnglishNumbers += input[i].ToString();
                }
            }
            return EnglishNumbers;
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string ToRial(long Value)
        {
            return Value.ToString("##,# ریال");
        }
    }
}
