using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Profile
{
    public class PersonalData
    {
        public PersonalData(
            int userId,
            string? dateOfBirth,
            string? dateOfBirthFormated,
            bool canChangeDateOfBirth,
            string? email,
            string? subscribeSales,
            string? firstName,
            string? lastName,
            string? fullName,
            GenderType? gender,
            string? phone,
            bool isAllowNotification,
            bool isAllowPush,
            ImageInfo? photo)
        {
            UserId = userId;
            DateOfBirth = dateOfBirth;
            DateOfBirthFormated = dateOfBirthFormated;
            CanChangeDateOfBirth = canChangeDateOfBirth;
            Email = email;
            SubscribeSales = subscribeSales;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Phone = phone;
            IsAllowNotification = isAllowNotification;
            IsAllowPush = isAllowPush;
            Photo = photo;
        }

        public int UserId { get; set; }

        public string? DateOfBirth { get; set; }

        public string? DateOfBirthFormated { get; set; }

        public bool CanChangeDateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? SubscribeSales { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public GenderType? Gender { get; set; }

        public string? Phone { get; set; }

        public bool IsAllowNotification { get; set; }

        public bool IsAllowPush { get; set; }

        public ImageInfo? Photo { get; set; }
    }
}