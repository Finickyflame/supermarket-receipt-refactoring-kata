namespace SupermarketReceipt;

public sealed class WeightCounter : CartItemHandler
{
    private WeightCounter()
    {
    }
    
    private double Weight { get; set; }

    public override void Handle(WeightCartItem item) => this.Weight += item.Weight;

    public static double GetWeight(IEnumerable<ICartItem> items)
    {
        var counter = new WeightCounter();
        foreach (ICartItem item in items)
        {
            item.Accept(counter);
        }
        return counter.Weight;
    }
}