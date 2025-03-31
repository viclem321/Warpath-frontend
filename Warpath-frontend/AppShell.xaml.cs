using Warpath_frontend.Views.LoadingPage;
using Warpath_frontend.Views.LoginPage;
using Warpath_frontend.Views.RegisterPage;
using Warpath_frontend.Views.PlayerPage;
using Warpath_frontend.Views.VillagePage;
using Warpath_frontend.Views.MapPage;

namespace Warpath_frontend
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("//LoadingPage", typeof(LoadingPage));
            Routing.RegisterRoute("//LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("//RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("//PlayerPage", typeof(PlayerPage));
            Routing.RegisterRoute("//VillagePage", typeof(VillagePage));
            Routing.RegisterRoute("//MapPage", typeof(MapPage));
        }
    }
}
