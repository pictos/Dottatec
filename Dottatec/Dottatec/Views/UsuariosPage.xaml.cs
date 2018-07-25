using Dottatec.Models;
using Dottatec.Utils;
using Dottatec.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dottatec.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosPage : ContentPage
    {
        UsuariosViewModel Vm => BindingContext as UsuariosViewModel;
        public UsuariosPage()
        {
            InitializeComponent();
            BindingContext = new UsuariosViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Settings.Logado)
                await Vm?.InitializeAsync(null);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Usuario;

            await((UsuariosViewModel)BindingContext).ExecuteEditarCommand(item);

            var listView = (ListView)sender;
            listView.SelectedItem = null;
        }
    }
}