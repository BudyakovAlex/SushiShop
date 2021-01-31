using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Plugins;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Profile
{
    public class EditProfileViewModel : BasePageViewModelResult<bool>
    {
        private readonly IProfileManager profileManager;
        private readonly IDialog dialog;

        private Data.Models.Profile.DetailedProfile? profile;

        public EditProfileViewModel(IProfileManager profileManager, IDialog dialog)
        {
            this.profileManager = profileManager;
            this.dialog = dialog;

            GenderTypes = new[] { GenderType.Male, GenderType.Female };

            SaveCommand = new SafeAsyncCommand(ExecutionStateWrapper, SaveAsync);
        }

        public GenderType[] GenderTypes { get; }

        private string? fullName;
        public string? FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private GenderType gender = GenderType.Unknown;
        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set => SetProperty(ref dateOfBirth, value);
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

        private bool canChangeDateOfBirth;
        public bool CanChangeDateOfBirth
        {
            get => canChangeDateOfBirth;
            set => SetProperty(ref canChangeDateOfBirth, value);
        }

        private bool canChangePhone;
        public bool CanChangePhone
        {
            get => canChangePhone;
            set => SetProperty(ref canChangePhone, value);
        }

        public IMvxCommand SaveCommand { get; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var responseProfile = await profileManager.GetProfileAsync();

            profile = responseProfile.Data;

            if (profile is null)
            {
                return;
            }

            FullName = profile.FullName;
            Gender = profile.Gender;
            DateOfBirth = profile.DateOfBirth;
            Phone = profile.Phone;
            Email = profile.Email;
            IsAllowSubscribe = profile.IsAllowSubscribe;
            IsAllowNotifications = profile.IsAllowNotifications;
            IsAllowPush = profile.IsAllowPush;
            CanChangePhone = !profile.CanChangePhone;
            CanChangeDateOfBirth = !profile.CanChangeDateOfBirth;
        }

        private async Task SaveAsync()
        {
            if (profile is null)
            {
                return;
            }

            var updatedProfile = new Data.Models.Profile.Profile(
                Email,
                Phone,
                DateOfBirth,
                string.Empty,
                string.Empty,
                FullName,
                Gender,
                IsAllowSubscribe,
                IsAllowNotifications,
                IsAllowPush,
                false);

            var response = await profileManager.SaveProfileAsync(updatedProfile);
            if (response.Data is null)
            {
                var error = response.Errors.FirstOrDefault();
                if (error.IsNullOrEmpty())
                {
                    return;
                }

                await dialog.ShowToastAsync(error);
                return;
            }

            await NavigationManager.CloseAsync(this, true);
        }
    }
}