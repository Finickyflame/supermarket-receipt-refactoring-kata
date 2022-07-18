namespace SupermarketReceipt;

public interface ICartItem
{
    IProduct Product { get; }

    void Accept(ICartItemHandler handler);
}