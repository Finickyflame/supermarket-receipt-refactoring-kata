using System.Globalization;
using System.Text;

namespace SupermarketReceipt;

public sealed class TextReceiptPrinter : IReceiptPrinter
{
    private const int DefaultColumnsLength = 40;

    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    private readonly int _columnsLength;

    public TextReceiptPrinter(int columnsLength = DefaultColumnsLength)
    {
        this._columnsLength = columnsLength;
    }

    private StringBuilder ReceiptBuilder { get; } = new();

    public string PrintReceipt(Receipt receipt)
    {
        foreach (IReceiptItem item in receipt.Items)
        {
            item.Accept(this);
        }

        foreach (Discount discount in receipt.Discounts)
        {
            discount.Accept(this);
        }
        this.ReceiptBuilder.AppendLine();
        this.PrintTotal(receipt.TotalPrice);
        return this.ReceiptBuilder.ToString();
    }

    public void Print(Discount discount)
    {
        this.PrintLineWithWhitespace(discount.Description, FormatPrice(-discount.Amount));
    }

    public void Print(WeightReceiptItem item)
    {
        this.PrintLineWithWhitespace(item.Product.Name, FormatPrice(item.TotalPrice));
        if (item.Weight > 0)
        {
            this.ReceiptBuilder.AppendLine($"  {FormatPrice(item.Product.Price)} * {item.Weight.ToString("N3", Culture)}");
        }
    }

    public void Print(ReceiptItem item)
    {
        this.PrintLineWithWhitespace(item.Product.Name, FormatPrice(item.TotalPrice));
        if (item.Quantity > 1)
        {
            this.ReceiptBuilder.AppendLine($"  {FormatPrice(item.Product.Price)} * {item.Quantity}");
        }
    }


    private void PrintTotal(double totalPrice)
    {
        this.PrintLineWithWhitespace("Total: ", FormatPrice(totalPrice));
    }

    private void PrintLineWithWhitespace(string name, string value)
    {
        this.ReceiptBuilder.Append(name);
        int whitespaceSize = this._columnsLength - name.Length - value.Length;
        this.ReceiptBuilder.Append(' ', whitespaceSize);
        this.ReceiptBuilder.Append(value);
        this.ReceiptBuilder.AppendLine();
    }

    private static string FormatPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}