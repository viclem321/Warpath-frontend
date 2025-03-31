using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warpath_frontend.Views.LoadingPage
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            await Task.Delay(50);
            // Navigation vers la page de connexion
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
