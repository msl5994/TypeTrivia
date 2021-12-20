using System;
using System.Collections.Generic;
using System.Text;
using TypeTrivia.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TypeTrivia.ViewModels
{
    public class MainPageViewModel
    {
        public IAsyncCommand AttackCommand { get; }
        public IAsyncCommand DefenseCommand { get; }

        public MainPageViewModel()
        {
            AttackCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(AttackingPage)}");
            });
            DefenseCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(DefendingPage)}");
            });
        }
    }
}
