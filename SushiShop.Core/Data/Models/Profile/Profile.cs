﻿using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class Profile : BaseProfile
    {
        public Profile(
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
            SubscribeSales = subscribeSales;
            Photo = photo;
        }

        public int UserId { get; }

        public DateTime DateOfBirthFormated { get; }

        public bool CanChangeDateOfBirth { get; }

        public string? SubscribeSales { get; }

        public ImageInfo? Photo { get; }
    }
}