namespace SupermarketReceipt;

public interface ICartItemHandler
{
    void Handle(CartItem item);
    
    void Handle(WeightCartItem item);
}

public abstract class CartItemHandler : ICartItemHandler
{
    public virtual void Handle(CartItem item)
    {
        // override to add behavior
    }

    public virtual void Handle(WeightCartItem item)
    {
        // override to add behavior
    }
}