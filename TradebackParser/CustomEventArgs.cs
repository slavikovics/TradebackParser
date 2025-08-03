using System;

namespace TradebackParser;

public class CustomEventArgs : EventArgs
{
    public object Data { get; }

    public CustomEventArgs(object data)
    {
        Data = data;
    }
}