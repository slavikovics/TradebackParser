using System;
using System.Collections.Generic;
using System.Globalization;

namespace TradebackParser;

public class Parser
{
    private void ReshapeString(string pageSource, double multiplier)
    {
        CultureInfo myUSCulture = new CultureInfo("en-US");
        //items = new List<Item>();
        string name;
        double price;
        string source = pageSource;
        int position = 0;
        int endi = 0;
        double price1 = 0, price2 = 0;
        double priceMin = 0;
        double lastSale = 0;
        double orderPrice = 0;
        while (position < source.Length - 100)
        {
            try
            {
                // title
                position = source.IndexOf("Copy", position);
                position = source.IndexOf('>', position);
                position++;
                endi = source.IndexOf('<', position);
                endi--;
                name = source.Substring(position, endi - position + 1);
                Console.WriteLine(name);

                //first-line
                position = source.IndexOf("first-line", position);
                position = source.IndexOf("data-price", position);
                position = source.IndexOf("\"", position);
                position++;
                endi = source.IndexOf('\"', position);
                endi--;
                Console.WriteLine(source.Substring(position, endi - position + 1));

                price1 = Convert.ToDouble(source.Substring(position, endi - position + 1));


                // second-line
                position = source.IndexOf("price usd", position); // change type of currency usd, cny ... 
                position = source.IndexOf("data-price", position);
                position = source.IndexOf("\"", position);
                position++;
                endi = source.IndexOf('\"', position);
                endi--;
                Console.WriteLine(source.Substring(position, endi - position + 1));
                price2 = Convert.ToDouble(source.Substring(position, endi - position + 1));

                //// min
                //position = source.IndexOf("min", position);
                //position = source.IndexOf("<div>", position);
                //position = source.IndexOf(">", position);
                //position++;
                //endi = source.IndexOf('<', position);
                //endi -= 2;
                //Console.WriteLine(source.Substring(position, endi - position + 1));
                //try
                //{
                //    priceMin = Convert.ToDouble(source.Substring(position, endi - position + 1));
                //}
                //catch
                //{
                //    priceMin = 0;
                //    continue;
                //}

                //// avg
                //position = source.IndexOf("avg", position);
                //position = source.IndexOf("<div>", position);
                //position = source.IndexOf(">", position);
                //position++;
                //endi = source.IndexOf('<', position);
                //endi-=2;
                //Console.WriteLine(source.Substring(position, endi - position + 1));
                //try
                //{
                //    price1 = Convert.ToDouble(source.Substring(position, endi - position + 1));
                //}
                //catch
                //{
                //    price1 = 0;
                //    continue;
                //}

                ////class="last-sales-row"
                //position = source.IndexOf("class=\"last-sales-row\"", position);
                //position = source.IndexOf("span", position);
                //position = source.IndexOf(">", position);
                //position++;
                //endi = source.IndexOf('<', position);
                //endi -= 2;
                //Console.WriteLine(source.Substring(position, endi - position + 1));
                //try
                //{
                //    lastSale = Convert.ToDouble(source.Substring(position, endi - position + 1));
                //}
                //catch
                //{
                //    lastSale = 0;
                //    continue;
                //}


                //// steam order
                //position = source.IndexOf("data-price", position);
                //position = source.IndexOf("\"", position);
                //position++;
                //endi = source.IndexOf('\"', position);
                //endi--;
                //Console.WriteLine(source.Substring(position, endi - position + 1));
                //try
                //{
                //    orderPrice = Convert.ToDouble(source.Substring(position, endi - position + 1));
                //}
                //catch
                //{
                //    orderPrice = 0;
                //    continue;
                //}

                //if (price1 < 0.07) continue;

                //int i1 = Convert.ToInt32(Math.Round(((price1 + priceMin + orderPrice) / 3) * 100));

                //price1 = i1;



                // SteamPrice
                //position = source.IndexOf("first-line", position);
                //position = source.IndexOf("data-price", position);
                //position = source.IndexOf("=", position);
                //position += 2;
                //endi = source.IndexOf('\"', position);
                //endi--;
                //Console.WriteLine(source.Substring(position, endi - position + 1));
                //price1 = Convert.ToDouble(source.Substring(position, endi - position + 1));

                // DmarketPrice
                //position = source.IndexOf("first-line", position);
                //position = source.IndexOf("data-price", position);
                //position = source.IndexOf("=", position);
                //position += 2;
                //endi = source.IndexOf('\"', position);
                //endi--;
                //price2 = Convert.ToDouble(source.Substring(position, endi - position + 1));

                if (price1 < price2) continue; // CHANGE HERE !!!!!!!!!!!!!!!!!!!!!!
                else
                {
                    int p;
                    if (multiplier > 0.6) return;
                    p = Convert.ToInt32(Math.Round((price1 + price2) / 2 * 100 * multiplier)); // change
                    //p = Convert.ToInt32(Math.Round((price2) * 100 * 0.81)); // change
                    // p = Convert.ToInt32(Math.Round(((price1 + price2) / 2) * 100 * 0.3));
                    // p = Convert.ToInt32(Math.Round(((price1 + price2) / 2) * 100 * 0.4)); // CHANGE HERE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //if (CopenhagenCheck(name)) items.Add(new Item(name, p, (p - 1) * 7.14 / 100));
                }

            }
            catch
            {
                return;
            }
        }
    }
}