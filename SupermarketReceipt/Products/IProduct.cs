namespace SupermarketReceipt;

public interface IProduct : IEquatable<IProduct>
{
    string Name { get; }

    double Price { get; }
}