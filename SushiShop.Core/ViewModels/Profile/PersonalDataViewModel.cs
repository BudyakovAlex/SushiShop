using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Managers.Profile;

namespace SushiShop.Core.ViewModels.Profile
{
    public class PersonalDataViewModel : BasePageViewModel
    {
        private readonly IProfileManager profileManager;

        public PersonalDataViewModel(IProfileManager profileManager)
        {
            this.profileManager = profileManager;

            SaveCommand = new SafeAsyncCommand(ExecutionStateWrapper, SaveAsync);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private GenderType gender = GenderType.None;
        public GenderType Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        private DateTime dateOfBirdth = DateTime.MinValue;
        public DateTime DateOfBirdth
        {
            get => dateOfBirdth;
            set => SetProperty(ref dateOfBirdth, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private bool acceptNotificationByPhone;
        public bool AcceptNotificationByPhone
        {
            get => acceptNotificationByPhone;
            set => SetProperty(ref acceptNotificationByPhone, value);
        }

        private bool acceptNotificationByEmail;
        public bool AcceptNotificationByEmail
        {
            get => acceptNotificationByEmail;
            set => SetProperty(ref acceptNotificationByEmail, value);
        }

        public IMvxCommand SaveCommand { get; }

        private Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
