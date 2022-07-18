namespace SupermarketReceipt;

public record WeightCartItem(WeightProduct Product, double Weight) : ICartItem
{
    IProduct ICartItem.Product => this.Product;
    
    public void Accept(ICartItemHandler handler) => handler.Handle(this);
}