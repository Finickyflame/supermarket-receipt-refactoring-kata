namespace SupermarketReceipt;

public class FakeCatalog : ISupermarketCatalog
{
    private readonly HashSet<IProduct> _products = new();
    
    private readonly HashSet<IOffer> _offers = new();

    public IEnumerable<IProduct> Products => this._products;

    public IEnumerable<IOffer> Offers => this._offers;

    public void AddProduct(IProduct product) => this._products.Add(product);

    public void AddSpecialOffer(IOffer offer) => this._offers.Add(offer);
}