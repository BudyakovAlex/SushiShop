﻿using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.Commands;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class CategoryMenuItemViewModel : BaseViewModel
    {
        public CategoryMenuItemViewModel(Category category)
        {
            Title = category.PageTitle;
            ImageUrl = category.CategoryIcon?.JpgUrl ?? string.Empty;
            ShowDetailsCommand = new SafeAsyncCommand(ExecutionStateWrapper, ShowDetailsAsync);
        }

        public string Title { get; }
        public string ImageUrl { get; }
        public IMvxCommand ShowDetailsCommand { get; }

        private Task ShowDetailsAsync()
        {
            return Task.CompletedTask;
        }
    }
}