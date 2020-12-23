namespace SushiShop.Core.Data.Models.Profile
{
    public class ProfileDiscount
    {
        public ProfileDiscount(
            string? title,
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

        public string? Title { get; }

        public string? Phone { get; }

        public int BalanceToNextLevel { get; }

        public int Discount { get; }

        public int Certificate { get; }

        public int Bonuses { get; }
    }
}