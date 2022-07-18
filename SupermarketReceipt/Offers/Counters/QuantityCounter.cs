namespace SupermarketReceipt;

public sealed class QuantityCounter : CartItemHandler
{
    private QuantityCounter()
    {
    }
    
    private int Quantity { get; set; }

    public override void Handle(CartItem item) => this.Quantity += item.Quantity;

    public static int GetQuantity(IEnumerable<ICartItem> items)
    {
        var counter = new QuantityCounter();
        foreach (ICartItem item in items)
        {
            item.Accept(counter);
        }
        return counter.Quantity;
    }
}