using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class EditProfileViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;
        private readonly IUserDialogs userDialogs;

        private Data.Models.Profile.Profile? profile;

        public EditProfileViewModel(IProfileManager profileManager, IUserDialogs userDialogs)
        {
            this.profileManager = profileManager;
            this.userDialogs = userDialogs;

            SaveCommand = new SafeAsyncCommand(ExecutionStateWrapper, SaveAsync);
        }

        private string? firstName;
        public string? FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        private string? lastName;
        public string? LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        private GenderType gender = GenderType.None;
        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        private DateTime dateOfBirdth;
        public DateTime DateOfBirdth
        {
            get => dateOfBirdth;
            set => SetProperty(ref dateOfBirdth, value);
        }

        private string? phone;
        public string? Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string? email;
        public string? Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private bool isAllowSubscribe;
        public bool IsAllowSubscribe
        {
            get => isAllowSubscribe;
            set => SetProperty(ref isAllowSubscribe, value);
        }

        private bool isAllowNotifications;
        public bool IsAllowNotifications
        {
            get => isAllowNotifications;
            set => SetProperty(ref isAllowNotifications, value);
        }

        private bool isAllowPush;
        public bool IsAllowPush
        {
            get => isAllowPush;
            set => SetProperty(ref isAllowPush, value);
        }

        public IMvxCommand SaveCommand { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var responseProfile = await profileManager.GetProfileAsync();

            profile = responseProfile.Data;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Gender = profile.Gender;
            DateOfBirdth = profile.DateOfBirth;
            Phone = profile.Phone;
            Email = profile.Email;
            IsAllowSubscribe = profile.IsAllowSubscribe;
            IsAllowNotifications = profile.IsAllowNotifications;
            IsAllowPush = profile.IsAllowPush;
        }

        private async Task SaveAsync()
        {
            if (profile is null)
            {
                return;
            }

            var userData = new Data.Models.Profile.Profile(
                profile.UserId,
                Email,
                Phone,
                DateOfBirdth,
                FirstName,
                LastName,
                $"{FirstName} {LastName}",
                Gender,
                IsAllowSubscribe,
                IsAllowNotifications,
                IsAllowPush,
                profile.IsNeedRegistration,
                profile.DateOfBirthFormated,
                profile.CanChangeDateOfBirth,
                profile.SubscribeSales,
                profile.Photo);

            var response = await profileManager.SaveProfileAsync(userData);
            if (response.Data is null)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }
                await userDialogs.AlertAsync(error);
                return;
            }

            await RefreshDataAsync();
            _ = NavigationManager.NavigateAsync<ProfileViewModel>();
        }
    }
}