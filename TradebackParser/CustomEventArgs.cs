using System;

namespace TradebackParser;

public class CustomEventArgs : EventArgs
{
    public string Data { get; set; }

    public CustomEventArgs(string data)
    {
        Data = data;
    }
}