namespace SushiShop.Core.Data.Models.Profile
{
    public class ProfileDiscount
    {
        public ProfileDiscount(string? title,
                            string? phone,
                            int balanceToNextLevel,
                            int discount,
                            int certificate,
                            int bonuses)
        {
            Title = title;
            Phone = phone;
            BalanceToNextLevel = balanceToNextLevel;
            Discount = discount;
            Certificate = certificate;
            Bonuses = bonuses;
        }

        public string? Title { get; set; }

        public string? Phone { get; set; }

        public int BalanceToNextLevel { get; set; }

        public int Discount { get; set; }

        public int Certificate { get; set; }

        public int Bonuses { get; set; }
    }
}