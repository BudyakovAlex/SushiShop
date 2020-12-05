using System;
using System.Threading.Tasks;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace SushiShop.Core.ViewModels.Common
{
    public class StepperViewModel : BaseViewModel
    {
        private const int MaxCount = 99;

        private readonly Func<int, int, Task> onCountChangedFunc;

        public StepperViewModel(string? title, int count, Func<int, int, Task> onCountChangedFunc)
        {
            Title = title;
            this.count = count;
            this.onCountChangedFunc = onCountChangedFunc;

            Interaction = new MvxInteraction();

            AddCommand = new MvxAsyncCommand(AddAsync, () => Count < MaxCount);
            RemoveCommand = new MvxAsyncCommand(RemoveAsync, () => Count > 0);
        }

        public StepperViewModel(int count, Func<int, int, Task> onCountChangedFunc)
            : this(title: null, count, onCountChangedFunc)
        {
        }

        public IMvxCommand AddCommand { get; }
        public IMvxCommand RemoveCommand { get; }
        
        public string? Title { get; }

        private int count;
        public int Count
        {
            get => count;
            set => SetProperty(ref count, value, () =>
            {
                AddCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            });
        }

        public MvxInteraction Interaction { get; private set; }

        public void Reset()
        {
            Count = 0;
            Interaction.Raise();
        }

        private Task AddAsync()
        {
            var previousValue = count;
            ++Count;
            Interaction.Raise();

            return onCountChangedFunc?.Invoke(previousValue, Count) ?? Task.CompletedTask;
        }

        private Task RemoveAsync()
        {
            var previousValue = Count;
            --Count;
            Interaction.Raise();

            return onCountChangedFunc?.Invoke(previousValue, Count) ?? Task.CompletedTask;
        }
    }
}
