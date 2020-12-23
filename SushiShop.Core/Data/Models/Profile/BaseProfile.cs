using SushiShop.Core.Data.Enums;
using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class BaseProfile
    {
        public BaseProfile(
            string? fullname,
            DateTime dateOfBirth,
            string? phone,
            string? email)
        {
            FullName = fullname;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
        }

        public BaseProfile(
            string? email,
            string? phone,
            DateTime dateOfBirth,
            string? firstName,
            string? lastName,
            string? fullName,
            GenderType gender,
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

        public string? Email { get; }

        public string? Phone { get; }

        public DateTime DateOfBirth { get; }

        public string? FirstName { get; }

        public string? LastName { get; }

        public string? FullName { get; }

        public GenderType Gender { get; }

        public bool IsAllowSubscribe { get; }

        public bool IsAllowNotifications { get; }

        public bool IsAllowPush { get; }

        public bool IsNeedRegistration { get; }
    }
}