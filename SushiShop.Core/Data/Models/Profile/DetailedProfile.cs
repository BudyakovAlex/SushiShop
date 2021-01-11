using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class DetailedProfile : Profile
    {
        public DetailedProfile(
            int userId,
            string? email,
            string? phone,
            DateTime? dateOfBirth,
            string? firstName,
            string? lastName,
            string? fullName,
            GenderType gender,
            bool isAllowSubscribe,
            bool isAllowNotifications,
            bool isAllowPush,
            bool isNeedRegistration,
            string? dateOfBirthFormated,
            bool canChangeDateOfBirth,
            ImageInfo? photo)
            : base(email,
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
            DateOfBirthFormated = dateOfBirthFormated;
            CanChangeDateOfBirth = canChangeDateOfBirth;
            Photo = photo;
        }

        public int UserId { get; }

        public string? DateOfBirthFormated { get; }

        public bool CanChangeDateOfBirth { get; }

        public ImageInfo? Photo { get; }
    }
}