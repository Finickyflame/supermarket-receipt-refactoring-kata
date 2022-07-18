namespace SupermarketReceipt;

public record CartItem(Product Product, int Quantity) : ICartItem
{
    IProduct ICartItem.Product => this.Product;
    
    public void Accept(ICartItemHandler handler) => handler.Handle(this);
}