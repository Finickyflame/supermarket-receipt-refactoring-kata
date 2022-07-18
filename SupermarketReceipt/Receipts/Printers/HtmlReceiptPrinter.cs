using System.Globalization;
using System.Text;

namespace SupermarketReceipt;

public sealed class HtmlReceiptPrinter : IReceiptPrinter
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

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
        this.PrintTotal(receipt.TotalPrice);
        return this.ReceiptBuilder.ToString();
    }

    public void Print(Discount discount)
    {
        this.ReceiptBuilder.AppendLine("<div class=\"discount\">");
        this.ReceiptBuilder.AppendLine($"   <div class=\"label\">{discount.Description}</div>");
        this.ReceiptBuilder.AppendLine($"   <div class=\"amount\">{FormatPrice(-discount.Amount)}</div>");
        this.ReceiptBuilder.AppendLine("</div>");
    }

    public void Print(WeightReceiptItem item)
    {
        this.ReceiptBuilder.AppendLine("<div class=\"item\">");
        this.ReceiptBuilder.AppendLine($"   <div class=\"label\">{item.Product.Name}</div>");
        this.ReceiptBuilder.AppendLine($"   <div class=\"amount\">{FormatPrice(item.TotalPrice)}</div>");
        if (item.Weight > 0)
        {
            this.ReceiptBuilder.AppendLine($"   <div class=\"detail\">{FormatPrice(item.Product.Price)} * {item.Weight.ToString("N3", Culture)}</div>");
        }
        this.ReceiptBuilder.AppendLine("</div>");
    }

    public void Print(ReceiptItem item)
    {
        this.ReceiptBuilder.AppendLine("<div class=\"item\">");
        this.ReceiptBuilder.AppendLine($"   <div class=\"label\">{item.Product.Name}</div>");
        this.ReceiptBuilder.AppendLine($"   <div class=\"amount\">{FormatPrice(item.TotalPrice)}</div>");
        if (item.Quantity > 1)
        {
            this.ReceiptBuilder.AppendLine($"   <div class=\"detail\">{FormatPrice(item.Product.Price)} * {item.Quantity}</div>");
        }
        this.ReceiptBuilder.AppendLine("</div>");
    }

    private void PrintTotal(double totalPrice)
    {
        this.ReceiptBuilder.AppendLine("<div class=\"total\">");
        this.ReceiptBuilder.AppendLine("   <div class=\"label\">Total:</div>");
        this.ReceiptBuilder.AppendLine($"   <div class=\"amount\">{FormatPrice(totalPrice)}</div>");
        this.ReceiptBuilder.AppendLine("</div>");
    }

    private static string FormatPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}