using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuildApps.Core.Mobile.Common.Extensions;
using BuildApps.Core.Mobile.Common.Wrappers;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Plugin.Media.Abstractions;
using SushiShop.Core.Data.Models.Plugins;
using SushiShop.Core.Extensions;
using SushiShop.Core.Managers.Feedback;
using SushiShop.Core.Plugins;
using SushiShop.Core.Resources;
using SushiShop.Core.ViewModels.Feedback.Items;

namespace SushiShop.Core.ViewModels.Feedback
{
    public class FeedbackViewModel : BasePageViewModel
    {
        private readonly IFeedbackManager feedbackManager;
        private readonly IDialog dialog;
        private readonly IMedia media;

        public FeedbackViewModel(IFeedbackManager feedbackManager, IDialog dialog, IMedia media)
        {
            this.feedbackManager = feedbackManager;
            this.dialog = dialog;
            this.media = media;

            Photos = new MvxObservableCollection<FeedbackPhotoItemViewModel>();

            SendFeedbackCommand = new SafeAsyncCommand(
                ExecutionStateWrapper,
                SendFeedbackAsync,
                () => OrderNumber.IsNotNullNorEmpty() && Question.IsNotNullNorEmpty());

            PickPhotoCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), PickPhotoAsync);
            TakePhotoCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), TakePhotoAsync);
            UploadPhotosCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), UploadPhotosAsync);
        }

        public IMvxAsyncCommand SendFeedbackCommand { get; }
        public IMvxAsyncCommand UploadPhotosCommand { get; }

        public MvxObservableCollection<FeedbackPhotoItemViewModel> Photos { get; }

        public string Title => AppStrings.Feedback;
        public string OrderNumberTitle => AppStrings.OrderNumber;
        public string QuestionTitle => AppStrings.Question;
        public string SendFeedbackTitle => AppStrings.Send;
        public string UploadPhotosTitle => AppStrings.UploadPhotos;

        public bool HasPhotos => Photos.IsNotEmpty();

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

        private IMvxAsyncCommand PickPhotoCommand { get; }
        private IMvxAsyncCommand TakePhotoCommand { get; }

        private async Task SendFeedbackAsync()
        {
            var photos = Photos.Select(photo => photo.ImagePath).ToArray();
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
            var mediaFile = await media.PickPhotoOrDefaultAsync();
            if (mediaFile is null)
            {
                return;
            }

            AddPhoto(mediaFile.Path);
        }

        private async Task TakePhotoAsync()
        {
            var mediaFile = await media.TakePhotoOrDefaultAsync();
            if (mediaFile is null)
            {
                return;
            }

            AddPhoto(mediaFile.Path);
        }

        private Task UploadPhotosAsync()
        {
            return dialog.ShowActionSheetAsync(
                null,
                null,
                AppStrings.Cancel,
                new DialogAction(AppStrings.TakePhoto, TakePhotoCommand),
                new DialogAction(AppStrings.UploadFromGallery, PickPhotoCommand));
        }

        private FeedbackPhotoItemViewModel ProduceFeedbackPhotoViewModel(string imageUrl) =>
            new FeedbackPhotoItemViewModel(imageUrl, RemovePhotoItem);

        private void RemovePhotoItem(FeedbackPhotoItemViewModel item)
        {
            Photos.Remove(item);
            RaisePropertyChanged(nameof(HasPhotos));
        }

        private void AddPhoto(string imagePath)
        {
            Photos.Add(ProduceFeedbackPhotoViewModel(imagePath));
            RaisePropertyChanged(nameof(HasPhotos));
        }
    }
}