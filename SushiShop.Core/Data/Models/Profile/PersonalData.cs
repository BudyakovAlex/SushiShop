using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Profile
{
    public class PersonalData
    {
        public PersonalData(string? dateOfBirth,
                            string? dateOfBirthFormated,
                            bool detaOfBirthReadonly,
                            string? email,
                            string? subscribeSales,
                            string? firstName,
                            string? lastName,
                            string? fullName,
                            GenderType? gender,
                            string? phone,
                            bool allowNotification,
                            bool allowPush,
                            ImageInfo? allowSubscribe,
                            int userId)
        {
            DateOfBirth = dateOfBirth;
            DateOfBirthFormated = dateOfBirthFormated;
            DetaOfBirthReadonly = detaOfBirthReadonly;
            Email = email;
            SubscribeSales = subscribeSales;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Phone = phone;
            AllowNotification = allowNotification;
            AllowPush = allowPush;
            AllowSubscribe = allowSubscribe;
            UserId = userId;
        }

        public string? DateOfBirth { get; set; }

        public string? DateOfBirthFormated { get; set; }

        public bool DetaOfBirthReadonly { get; set; }

        public string? Email { get; set; }

        public string? SubscribeSales { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public GenderType? Gender { get; set; }

        public string? Phone { get; set; }

        public bool AllowNotification { get; set; }

        public bool AllowPush { get; set; }

        public ImageInfo? AllowSubscribe { get; set; }

        public int UserId { get; set; }
    }
}