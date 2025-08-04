using System.Collections.Generic;
using System.Text;

namespace TradebackParser;

public class QuotedStringParser
{
    public static List<string> ParseQuotedTokens(string input)
    {
        List<string> tokens = new ();
        if (string.IsNullOrEmpty(input))
            return tokens;

        bool inQuotes = false;
        StringBuilder currentToken = new ();

        foreach (char c in input)
        {
            if (c == '"')
            {
                if (inQuotes)
                {
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear();
                }
                inQuotes = !inQuotes;
            }
            else if (inQuotes)
            {
                currentToken.Append(c);
            }
        }
        return tokens;
    }
}