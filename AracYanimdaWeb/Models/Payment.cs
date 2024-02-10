public class Payment
{
    public int PaymentId { get; set; }
    public int RezervationId { get; set; }
    public int PaymentMethodId { get; set; }
    public decimal Miktar { get; set; }
    public DateTime OdemeTarihi { get; set; }
    public string OdemeDurumu { get; set; }
}