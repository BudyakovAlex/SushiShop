﻿using System.Linq;
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
using SushiShop.Core.ViewModels.Common.Items;

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

            Photos = new MvxObservableCollection<PhotoItemViewModel>();

            SendFeedbackCommand = new SafeAsyncCommand(ExecutionStateWrapper, SendFeedbackAsync);
            PickPhotoCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), PickPhotoAsync);
            TakePhotoCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), TakePhotoAsync);
            UploadPhotosCommand = new SafeAsyncCommand(new ExecutionStateWrapper(), UploadPhotosAsync);
        }

        public IMvxAsyncCommand SendFeedbackCommand { get; }
        public IMvxAsyncCommand UploadPhotosCommand { get; }

        public MvxObservableCollection<PhotoItemViewModel> Photos { get; }

        public string Title => AppStrings.Feedback;
        public string SendFeedbackTitle => AppStrings.Send;
        public string UploadPhotosTitle => AppStrings.UploadPhotos;

        public bool HasPhotos => Photos.IsNotEmpty();

        public string OrderNumberPlaceholder => IsOrderNumberEmpty ? $"{AppStrings.OrderNumber}*" : AppStrings.OrderNumber;

        public string QuestionPlaceholder => IsQuestionEmpty ? $"{AppStrings.Question}*" : AppStrings.Question;

        private string? orderNumber;
        public string? OrderNumber
        {
            get => orderNumber;
            set => SetProperty(ref orderNumber, value, () => IsOrderNumberEmpty = string.IsNullOrEmpty(orderNumber));
        }

        private string? question;
        public string? Question
        {
            get => question;
            set => SetProperty(ref question, value, () => IsQuestionEmpty = string.IsNullOrEmpty(question));
        }

        private IMvxAsyncCommand PickPhotoCommand { get; }
        private IMvxAsyncCommand TakePhotoCommand { get; }

        private bool isOrderNumberEmpty = true;
        private bool IsOrderNumberEmpty
        {
            get => isOrderNumberEmpty;
            set
            {
                if (isOrderNumberEmpty == value)
                {
                    return;
                }

                isOrderNumberEmpty = value;
                RaisePropertyChanged(nameof(OrderNumberPlaceholder));
                SendFeedbackCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isQuestionEmpty = true;
        private bool IsQuestionEmpty
        {
            get => isQuestionEmpty;
            set
            {
                if (isQuestionEmpty == value)
                {
                    return;
                }

                isQuestionEmpty = value;
                RaisePropertyChanged(nameof(QuestionPlaceholder));
                SendFeedbackCommand.RaiseCanExecuteChanged();
            }
        }

        private async Task SendFeedbackAsync()
        {
            var photos = Photos.Select(photo => photo.ImagePath).ToArray();
            var response = await feedbackManager.SendFeedbackAsync(OrderNumber!, Question!, photos);
            if (!response.IsSuccessful)
            {
                if (response.Errors.Any())
                {
                    await dialog.ShowToastAsync(response.Errors.First());
                }

                return;
            }

            await UserDialogs.Instance.AlertAsync(AppStrings.ThanksForFeedback);
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

        private PhotoItemViewModel ProduceFeedbackPhotoViewModel(string imageUrl) =>
            new PhotoItemViewModel(imageUrl, RemovePhotoItem);

        private void RemovePhotoItem(PhotoItemViewModel item)
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