namespace SupermarketReceipt;

public class Discount
{
    public Discount(string description, double amount)
    {
        this.Description = description;
        this.Amount = amount;
    }

    public string Description { get; }
    
    public double Amount { get; }
        
    public void Accept(IReceiptPrinter printer) => printer.Print(this);
}