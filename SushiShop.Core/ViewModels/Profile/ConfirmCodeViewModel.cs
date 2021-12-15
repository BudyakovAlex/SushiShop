using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Managers.Profile;
using SushiShop.Core.Plugins;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SushiShop.Core.ViewModels.Profile
{
    public class ConfirmCodeViewModel : BasePageViewModel<string, bool>
    {
        private const int TimerIntervalInMs = 1000;
        private readonly IProfileManager profileManager;
        private readonly IDialog dialog;

        private Timer? timer;
        private CompositeDisposable? disposables;
        private string login;

        public ConfirmCodeViewModel(
            IProfileManager profileManager,
            IDialog dialog)
        {
            login = string.Empty;

            this.profileManager = profileManager;
            this.dialog = dialog;

            ContinueCommand = new SafeAsyncCommand(ExecutionStateWrapper, ContinueAsync, () => Code.IsNotNullNorEmpty());
            SendCodeCommnad = new MvxAsyncCommand(SendNewCodeAsync);
        }

        private string? message;
        public string? Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private string? placeholder;
        public string? Placeholder
        {
            get => placeholder;
            set => SetProperty(ref placeholder, value);
        }

        private string? code;
        public string? Code
        {
            get => code;
            set => SetProperty(ref code, value, ContinueCommand.RaiseCanExecuteChanged);
        }

        private int secondsToSendNewMessage;
        public int SecondsToSendNewMessage
        {
            get => secondsToSendNewMessage;
            set => SetProperty(ref secondsToSendNewMessage, value);
        }

        public IMvxCommand ContinueCommand { get; }
        public IMvxAsyncCommand SendCodeCommnad { get; }

        public override void Prepare(string parameter)
        {
            login = parameter;

            RaisePropertyChanged(nameof(Message));
        }

        public override Task InitializeAsync()
        {
            return SendCodeCommnad.ExecuteAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                disposables?.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task ContinueAsync()
        {
            var response = await profileManager.AuthorizeAsync(login, Code!);
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

        private async Task SendNewCodeAsync()
        {
            var response = await profileManager.SendCodeAsync(login);
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

            Message = response.Data.Message;
            Placeholder = response.Data.Placeholder;
            SecondsToSendNewMessage = response.Data.RepeatCodeTimeout;

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            StopTimer();
            disposables = new CompositeDisposable();
            timer = new Timer(TimerIntervalInMs);
            timer.AutoReset = true;

            Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
                handler => timer.Elapsed += handler,
                handler => timer.Elapsed -= handler)
                .Subscribe(data => OnTimerElapsed(data.Sender, data.EventArgs))
                .DisposeWith(disposables);

            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (SecondsToSendNewMessage == 0)
            {
                StopTimer();
                return;
            }

            SecondsToSendNewMessage -= 1;
        }

        private void StopTimer()
        {
            timer?.Stop();
            disposables?.Dispose();
        }
    }
}