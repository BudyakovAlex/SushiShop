using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class PersonalData : Profile
    {
        public PersonalData(
            int userId,
            string? email,
            string? phone,
            DateTime dateOfBirth,
            string? firstName,
            string? lastName,
            string? fullName,
            GenderType? gender,
            bool isAllowSubscribe,
            bool isAllowNotifications,
            bool isAllowPush,
            bool isNeedRegistration,
            DateTime dateOfBirthFormated,
            bool canChangeDateOfBirth,
            string? subscribeSales,
            ImageInfo? photo) : base(
                email,
                phone,
                dateOfBirth,
                firstName,
                lastName,
                fullName,
                gender,
                isAllowSubscribe,
                isAllowNotifications,
                isAllowPush,
                isNeedRegistration)
        {
            UserId = userId;
            Email = email;
            Phone = Phone;
            DateOfBirth = dateOfBirth;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            IsAllowSubscribe = isAllowSubscribe;
            IsAllowNotifications = isAllowNotifications;
            IsAllowPush = isAllowPush;
            IsNeedRegistration = isNeedRegistration;
            DateOfBirthFormated = dateOfBirthFormated;
            CanChangeDateOfBirth = canChangeDateOfBirth;
            SubscribeSales = subscribeSales;
            Photo = photo;
        }

        public int UserId { get; set; }

        public DateTime DateOfBirthFormated { get; set; }

        public bool CanChangeDateOfBirth { get; set; }

        public string? SubscribeSales { get; set; }

        public ImageInfo? Photo { get; set; }
    }
}