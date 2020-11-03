using System;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;

namespace SushiShop.Core.ViewModels.Common
{
    public class StepperViewModel : BaseViewModel
    {
        private const int MaxCount = 99;

        private readonly Action<int> onCountChanged;

        public StepperViewModel(string? title, int count, Action<int> onCountChanged)
        {
            Title = title;
            Count = count;
            this.onCountChanged = onCountChanged;

            AddCommand = new MvxCommand(Add, () => Count < MaxCount);
            RemoveCommand = new MvxCommand(Remove, () => Count > 0);
        }

        public StepperViewModel(int count, Action<int> onCountChanged)
            : this(title: null, count, onCountChanged)
        {
        }

        public IMvxCommand AddCommand { get; }
        public IMvxCommand RemoveCommand { get; }
        
        public string? Title { get; }

        private int count;
        public int Count
        {
            get => count;
            private set => SetProperty(ref count, value, () =>
            {
                AddCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
                onCountChanged(count);
            });
        }

        public void Reset()
        {
            Count = 0;
        }

        private void Add() => ++Count;

        private void Remove() => --Count;
    }
}
