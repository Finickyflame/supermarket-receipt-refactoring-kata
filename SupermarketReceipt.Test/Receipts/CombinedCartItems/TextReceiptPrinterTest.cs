
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace SupermarketReceipt.Test.CombinedCartItems;

[UsesVerify]
public class TextReceiptPrinterTest
{
    private readonly Product _toothbrush = new("toothbrush", 0.99);
    private readonly WeightProduct _apples = new("apples", 1.99);
    private readonly Receipt _receipt = new();
    private readonly IReceiptPrinter _receiptPrinter = new TextReceiptPrinter();

    [Fact]
    public Task OneLineItem()
    {
        this._receipt.AddProduct(this._toothbrush, 1);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }
        
    [Fact]
    public Task QuantityTwo()
    {
        this._receipt.AddProduct(this._toothbrush, 2);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }
        
    [Fact]
    public Task LooseWeight()
    {
        this._receipt.AddProduct(this._apples, 1.5);
        this._receipt.AddProduct(this._apples, 1.5);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }

    [Fact]
    public Task Total()
    {
        this._receipt.AddProduct(this._toothbrush, 2);
        this._receipt.AddProduct(this._apples, 0.75);
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }

    [Fact]
    public Task Discounts()
    {
        this._receipt.AddDiscount(new Discount("3 for 2", 0.99));
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }

    [Fact]
    public Task PrintWholeReceipt()
    {
        this._receipt.AddProduct(this._toothbrush, 1);
        this._receipt.AddProduct(this._toothbrush, 2);
        this._receipt.AddProduct(this._apples, 0.75);
        this._receipt.AddDiscount(new Discount("3 for 2(toothbrush)", 0.99));
        return Verifier.Verify(this._receiptPrinter.PrintReceipt(this._receipt));
    }
}