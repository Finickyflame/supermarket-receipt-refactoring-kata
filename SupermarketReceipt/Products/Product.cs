namespace SupermarketReceipt;

public sealed record Product(string Name, double Price) : IProduct
{
    public bool Equals(IProduct other) => this.Equals(other as Product);
}