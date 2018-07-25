using Dottatec.Servicos;
using Dottatec.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Dottatec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //if (!Settings.Logado)
            MainPage = new NavigationPage(new Views.LoginPage());
            //else
            //MainPage = new NavigationPage(new Views.CadastrarUsuarioPage());
        }

        protected async override void OnStart()
        {
            // Handle when your app starts
            await BDAzure.Current.InitiAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
