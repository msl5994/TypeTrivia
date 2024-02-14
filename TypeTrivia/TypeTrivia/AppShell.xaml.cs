using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeTrivia.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TypeTrivia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        // Separate Method for Creating Routes
        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(AttackingPage), typeof(AttackingPage));
            Routing.RegisterRoute(nameof(DefendingPage), typeof(DefendingPage));
            Routing.RegisterRoute(nameof(DualTypeAttackingPage), typeof(DualTypeAttackingPage));
            Routing.RegisterRoute(nameof(WebsiteView), typeof(WebsiteView));
        }
    }
}