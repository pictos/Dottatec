using Dottatec.Models;
using Dottatec.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dottatec.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosPage : ContentPage
    {
        public UsuariosPage()
        {
            InitializeComponent();
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