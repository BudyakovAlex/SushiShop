using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SushiShop.Core.Managers.Feedback;
using SushiShop.Core.ViewModels.Feedback.Items;
using System.Linq;
using System.Threading.Tasks;

namespace SushiShop.Core.ViewModels.Feedback
{
    public class FeedbackViewModel : BasePageViewModel
    {
        private readonly IFeedbackManager feedbackManager;

        public FeedbackViewModel(IFeedbackManager feedbackManager)
        {
            this.feedbackManager = feedbackManager;

            Photos = new MvxObservableCollection<FeedbackPhotoItemViewModel>();

            SendFeedbackCommand = new SafeAsyncCommand(
                ExecutionStateWrapper,
                SendFeedbackAsync,
                () => OrderNumber.IsNotNullNorEmpty() && Question.IsNotNullNorEmpty());

            PickPhotoCommand = new SafeAsyncCommand(ExecutionStateWrapper, PickPhotoAsync);
            TakePhotoCommand = new SafeAsyncCommand(ExecutionStateWrapper, TakePhotoAsync);
        }

        public MvxObservableCollection<FeedbackPhotoItemViewModel> Photos { get; }

        public IMvxCommand SendFeedbackCommand { get; }

        public IMvxCommand PickPhotoCommand { get; }

        public IMvxCommand TakePhotoCommand { get; }

        private string? orderNumber;
        public string? OrderNumber
        {
            get => orderNumber;
            set => SetProperty(ref orderNumber, value, SendFeedbackCommand.RaiseCanExecuteChanged);
        }

        private string? question;
        public string? Question
        {
            get => question;
            set => SetProperty(ref question, value, SendFeedbackCommand.RaiseCanExecuteChanged);
        }

        private async Task SendFeedbackAsync()
        {
            var photos = Photos.Select(photo => photo.ImageUrl).ToArray();
            var response = await feedbackManager.SendFeedbackAsync(OrderNumber, Question, photos);
            if (!response.IsSuccessful)
            {
                if (response.Errors.Any())
                {
                    await UserDialogs.Instance.AlertAsync(response.Errors.First());
                }

                return;
            }

            await NavigationManager.CloseAsync(this);
        }

        private async Task PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            var mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (mediaFile is null)
            {
                return;
            }

            Photos.Add(ProduceFeedbackPhotoViewModel(mediaFile.Path));
        }

        private async Task TakePhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
            if (mediaFile is null)
            {
                return;
            }

            Photos.Add(ProduceFeedbackPhotoViewModel(mediaFile.Path));
        }

        private FeedbackPhotoItemViewModel ProduceFeedbackPhotoViewModel(string imageUrl)
            => new FeedbackPhotoItemViewModel(imageUrl, (vm) => Photos.Remove(vm));
    }
}