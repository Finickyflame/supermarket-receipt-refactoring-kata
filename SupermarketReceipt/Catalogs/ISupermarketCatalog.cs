namespace SupermarketReceipt;

public interface ISupermarketCatalog
{
    IEnumerable<IProduct> Products { get; }

    IEnumerable<IOffer> Offers { get; }

    void AddProduct(IProduct product);

    void AddSpecialOffer(IOffer offer);
}