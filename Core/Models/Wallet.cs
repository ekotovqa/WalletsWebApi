namespace Core.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public TmpBalance TmpBalance { get; set; }
    }
}
