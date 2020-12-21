using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models.Profile
{
    internal class RegistrationData
    {
        public RegistrationData(string? email,
                                string? phone,
                                string? dateOfBirth,
                                string? firstName,
                                string? lastName,
                                string? fullName,
                                GenderType? gender,
                                bool allowSubscribe,
                                bool allowNotificarions,
                                bool allowPush)
        {
            Email = email;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            AllowSubscribe = allowSubscribe;
            AllowNotificarions = allowNotificarions;
            AllowPush = allowPush;
        }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? DateOfBirth { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public GenderType? Gender { get; set; }

        public bool AllowSubscribe { get; set; }

        public bool AllowNotificarions { get; set; }

        public bool AllowPush { get; set; }
    }
}