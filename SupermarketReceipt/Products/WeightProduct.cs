namespace SupermarketReceipt;

public sealed record WeightProduct(string Name, double Price) : IProduct
{
    public bool Equals(IProduct other) => this.Equals(other as WeightProduct);
}