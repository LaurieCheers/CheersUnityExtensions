using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheersStringExtensions
{
    public static T ToEnum<T>(this string s)
    {
        return (T)System.Enum.Parse(typeof(T), s);
    }

    public static object ParseOrDefault(Type enumType, string text)
    {
        try
        {
            object result = System.Enum.Parse(enumType, text);
            return result;
        }
        catch (ArgumentException)
        {
            Debug.LogError($"Parsing enum {enumType}: {text} is not a valid value");
            return Activator.CreateInstance(enumType); // default value
        }
    }

    public static object ParseWithFallback(Type enumType, string text, object fallback)
    {
        try
        {
            object result = System.Enum.Parse(enumType, text);
            return result;
        }
        catch (ArgumentException)
        {
            return fallback;
        }
    }

    public static string ToFriendlyString(this System.DateTime date)
    {
        System.DateTime now = System.DateTime.Now;
        System.TimeSpan span = (now - date);
        if (span.Days > 365 * 100)
        {
            return "Over 100 years ago";
        }
        else if (span.Days < 0)
        {
            return span.Days + " days in the future";
        }
        else if (span.Days == 0)
        {
            return date.TimeOfDay.Hours + ":" + date.TimeOfDay.Minutes.ToString("00") + " today";
        }
        else if (span.Days == 1)
        {
            return date.TimeOfDay.Hours + ":" + date.TimeOfDay.Minutes.ToString("00") + " yesterday";
        }
        else if (span.Days < 10)
        {
            return date.TimeOfDay.Hours + ":" + date.TimeOfDay.Minutes.ToString("00") + ", " + span.Days + " days ago";
        }
        else if (date.Year == now.Year)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            return mfi.GetMonthName(date.Month) + " " + date.Day;
        }
        else
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            return mfi.GetMonthName(date.Month) + " " + date.Day + " " + date.Year;
        }
    }

    public static string Pluralize(this int n, string noun, string pluralNoun)
    {
        return n + " " + (n == 1 ? noun : pluralNoun);
    }

    public static string Pluralize(this float n, string noun, string pluralNoun)
    {
        return n + " " + (n == 1 ? noun : pluralNoun);
    }

    public static string PluralizeOmitZero(this int n, string noun, string pluralNoun)
    {
        if (n == 0)
            return "";
        else
            return n + " " + (n == 1 ? noun : pluralNoun);
    }

    public static string CostToString(this int cost, string symbol = "$", string centSymbol = "¢")
    {
        if (cost < 0)
            return "-" + (-cost).CostToString();
        else if (cost >= 100)
            return symbol + (cost / 100) + "." + (cost % 100).ToString("D2");
        else
            return (cost) + centSymbol;
    }

    public static string ToLongString(this Vector3 vec)
    {
        return $"[{vec.x},{vec.y},{vec.z}]";
    }

    public static string ToLongString(this Vector2 vec)
    {
        return $"[{vec.x},{vec.y}]";
    }

}
