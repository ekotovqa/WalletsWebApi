namespace Core.Dto.ViewModels
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public decimal? Balance { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
