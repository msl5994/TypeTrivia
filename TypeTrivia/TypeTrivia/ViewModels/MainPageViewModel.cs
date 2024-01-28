using System;
using System.Collections.Generic;
using System.Text;
using TypeTrivia.Models;
using TypeTrivia.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TypeTrivia.ViewModels
{
    public class MainPageViewModel
    {
        public IAsyncCommand AttackCommand { get; }
        public IAsyncCommand DefenseCommand { get; }
        public IAsyncCommand DualTypeAttackingCommand { get; }

        public MainPageViewModel()
        {
            AttackCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(AttackingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 0);
            });
            DefenseCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(DefendingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 1);
            });
            DualTypeAttackingCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(DualTypeAttackingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 2);
            });
        }
    }
}
