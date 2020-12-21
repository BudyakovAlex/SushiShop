using SushiShop.Core.Data.Enums;
using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class Profile
    {
        public Profile(
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
            bool isNeedRegistration)
        {
            Email = email;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            IsAllowSubscribe = isAllowSubscribe;
            IsAllowNotifications = isAllowNotifications;
            IsAllowPush = isAllowPush;
            IsNeedRegistration = isNeedRegistration;
        }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public GenderType? Gender { get; set; }

        public bool IsAllowSubscribe { get; set; }

        public bool IsAllowNotifications { get; set; }

        public bool IsAllowPush { get; set; }

        public bool IsNeedRegistration { get; set; }
    }
}