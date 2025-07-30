namespace TradebackParser;

public class ItemModel
{
    public string Name { get; set; }
    public decimal Price1 { get; set; }
    public decimal Price2 { get; set; }
    public int Count1 { get; set; }
    public int Count2 { get; set; }
    public decimal ProposedPrice { get; set; }
    
    public ItemModel(string name, decimal price1, decimal price2, int count1, int count2, decimal proposedPrice)
    {
        Name = name;
        Price1 = price1;
        Price2 = price2;
        Count1 = count1;
        Count2 = count2;
        ProposedPrice = proposedPrice;
    }
}