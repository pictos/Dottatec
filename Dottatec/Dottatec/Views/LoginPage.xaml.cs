using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dottatec.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.LoginViewModel();
            usu.Text = string.Empty;
            sen.Text = string.Empty;
        }
    }
}