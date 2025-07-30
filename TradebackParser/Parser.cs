using System;
using System.Collections.Generic;
using System.Globalization;

namespace TradebackParser;

public class Parser
{
    private static string _pageSource = "";
    
    public Parser(string pageSource)
    {
        _pageSource = pageSource;
    }

    public List<ItemModel> Parse()
    {
        return ReshapeString(1);
    }
    
    private List<ItemModel> ReshapeString(double multiplier)
    { 
        var items = new List<ItemModel>();
        int position = 0;

        while (position < _pageSource.Length - 100)
        {
            try
            {
                var name = FindName(ref position);
                var price1 = FindPrice1(ref position);
                var count1 = FindCount1(ref position);
                var price2 = FindPrice2(ref position);
                var count2 = FindCount2(ref position);
                var proposedPrice = Convert.ToInt32(Math.Round((price1 + price2) / 2 * 100 * multiplier));
                items.Add(new ItemModel(name, (decimal)price1, (decimal)price2, count1, count2, proposedPrice));
            }
            catch
            {
                return items;
            }
        }

        return items;
    }

    private static double FindPrice2(ref int position)
    {
        position = _pageSource.IndexOf("price usd", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("data-price", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("\"", position, StringComparison.Ordinal);
        position++;
        var endIndex = _pageSource.IndexOf('\"', position);
        endIndex--;
        var price2 = Convert.ToDouble(_pageSource.Substring(position, endIndex - position + 1));
        return price2;
    }

    private static double FindPrice1(ref int position)
    {
        position = _pageSource.IndexOf("first-line", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("data-price", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("\"", position, StringComparison.Ordinal);
        position++;
        var endIndex = _pageSource.IndexOf('\"', position);
        endIndex--;
        var price1 = Convert.ToDouble(_pageSource.Substring(position, endIndex - position + 1));
        return price1;
    }

    private static string FindName(ref int position)
    {
        position = _pageSource.IndexOf("Copy", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf('>', position);
        position++;
        var endIndex = _pageSource.IndexOf('<', position);
        endIndex--;
        var name = _pageSource.Substring(position, endIndex - position + 1);
        return name;
    }
    
    private static int FindCount1(ref int position)
    {
        position = _pageSource.IndexOf("\"second-line\"", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("span", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf('>', position);
        position++;
        var endIndex = _pageSource.IndexOf('<', position);
        endIndex--;
        var count1 = _pageSource.Substring(position, endIndex - position + 1);
        count1 = count1.Replace(" pcs.", "");
        return Convert.ToInt32(count1);
    }
    
    private static int FindCount2(ref int position)
    {
        position = _pageSource.IndexOf("\"second-line\"", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf("span", position, StringComparison.Ordinal);
        position = _pageSource.IndexOf('>', position);
        position++;
        var endIndex = _pageSource.IndexOf('<', position);
        endIndex--;
        var count2 = _pageSource.Substring(position, endIndex - position + 1);
        count2 = count2.Replace(" pcs.", "");
        return Convert.ToInt32(count2);
    }
}