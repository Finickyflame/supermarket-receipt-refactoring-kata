using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace SupermarketReceipt.Test;

[UsesVerify]
public class SupermarketTest
{
    private readonly ISupermarketCatalog _catalog;
    private readonly Teller _teller;
    private readonly ShoppingCart _shoppingCart;
    private readonly Product _toothbrush;
    private readonly Product _toothpaste;
    private readonly Product _rice;
    private readonly WeightProduct _apples;
    private readonly Product _cherryTomatoes;
    private readonly IReceiptPrinter _receiptPrinter;

    public SupermarketTest()
    {
        this._catalog = new FakeCatalog();
        this._teller = new Teller(this._catalog);
        this._shoppingCart = new ShoppingCart();
        this._receiptPrinter = new TextReceiptPrinter();

        this._toothbrush = new Product("toothbrush", 0.99);
        this._catalog.AddProduct(this._toothbrush);
        this._toothpaste = new Product("toothpaste", 1.79);
        this._catalog.AddProduct(this._toothpaste);
        this._rice = new Product("rice", 2.99);
        this._catalog.AddProduct(this._rice);
        this._apples = new WeightProduct("apples", 1.99);
        this._catalog.AddProduct(this._apples);
        this._cherryTomatoes = new Product("cherry tomato box", 0.69);
        this._catalog.AddProduct(this._cherryTomatoes);
    }

    [Fact]
    public Task an_empty_shopping_cart_should_cost_nothing()
    {
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task one_normal_item()
    {
        this._shoppingCart.AddItem(this._toothbrush);
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task two_normal_items()
    {
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._rice);
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task buy_two_get_one_free()
    {
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._catalog.AddSpecialOffer(new ThreeForTwoOffer(this._toothbrush));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task buy_five_get_one_free()
    {
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._shoppingCart.AddItem(this._toothbrush);
        this._catalog.AddSpecialOffer(new ThreeForTwoOffer(this._toothbrush));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task loose_weight_product()
    {
        this._shoppingCart.AddItem(this._apples, .5);
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task percent_discount()
    {
        this._shoppingCart.AddItem(this._rice);
        this._catalog.AddSpecialOffer(new TenPercentDiscount(this._rice));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task xForY_discount()
    {
        this._shoppingCart.AddItem(this._cherryTomatoes);
        this._shoppingCart.AddItem(this._cherryTomatoes);
        this._catalog.AddSpecialOffer(new TwoForAmountOffer(this._cherryTomatoes, .99));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task FiveForY_discount()
    {
        this._shoppingCart.AddItem(this._apples, 5);
        this._catalog.AddSpecialOffer(new FiveForAmountOffer(this._apples, 6.99));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task FiveForY_discount_withSix()
    {
        this._shoppingCart.AddItem(this._apples, 6);
        this._catalog.AddSpecialOffer(new FiveForAmountOffer(this._apples, 6.99));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task FiveForY_discount_withSixteen()
    {
        this._shoppingCart.AddItem(this._apples, 16);
        this._catalog.AddSpecialOffer(new FiveForAmountOffer(this._apples, 6.99));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task FiveForY_discount_withFour()
    {
        this._shoppingCart.AddItem(this._apples, 4);
        this._catalog.AddSpecialOffer(new FiveForAmountOffer(this._apples, 6.99));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task Bundle_No_Discount()
    {
        this._shoppingCart.AddItem(this._toothbrush, 2);
        this._catalog.AddSpecialOffer(new TenPercentBundleDiscount(this._toothbrush, this._toothpaste));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task Bundle_One_Discount()
    {
        this._shoppingCart.AddItem(this._toothbrush, 2);
        this._shoppingCart.AddItem(this._toothpaste);
        this._catalog.AddSpecialOffer(new TenPercentBundleDiscount(this._toothbrush, this._toothpaste));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }

    [Fact]
    public Task Bundle_Two_Discounts()
    {
        this._shoppingCart.AddItem(this._toothbrush, 2);
        this._shoppingCart.AddItem(this._toothpaste, 2);
        this._catalog.AddSpecialOffer(new TenPercentBundleDiscount(this._toothbrush, this._toothpaste));
        Receipt receipt = this._teller.ChecksOutArticlesFrom(this._shoppingCart);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(receipt));
    }
}