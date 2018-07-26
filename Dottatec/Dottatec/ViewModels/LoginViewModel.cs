using Dottatec.Servicos;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Dottatec.Utils;
using Dottatec.Views;

namespace Dottatec.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Propriedades

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (SetProperty(ref isBusy, value))
                {
                    LogarCommand.ChangeCanExecute();
                }
            }
        }

        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { SetProperty(ref usuario, value); }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set { SetProperty(ref senha, value); }
        }

        public Command LogarCommand     { get; }

        public Command CadastrarCommand { get; }

        #endregion
        public async override Task InitializeAsync(object[] args)
        {
            await RemovePage(typeof(UsuariosPage));
        }
        public LoginViewModel()
        {
            LogarCommand     = new Command(async () => await ExecuteLogarCommand(), () => !IsBusy);
            CadastrarCommand = new Command(async () => await ExecuteCadastrarCommand(), () => !IsBusy);
            Senha            = string.Empty;
            Usuario          = string.Empty;
        }

        async Task ExecuteCadastrarCommand()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await PushAsync<CadastrarUsuarioViewModel>();

                }
                catch (Exception ex)
                {

                    await DisplayAlert("Erro", $"Erro:{ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return; 
        }

        async Task ExecuteLogarCommand()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    if (Senha == "admin" && Usuario == "admin")
                    {
                        await PushAsync<UsuariosViewModel>(null);
                        return;
                    }


                    var lista = await BDAzure.Current.ObterUsuariosAsync();
                    var teste = lista.FirstOrDefault(x => x.Nome == Usuario && x.Senha == Senha);

                    if (teste != null)
                    {
                        await PushAsync<UsuariosViewModel>(teste);
                        return;
                    }

                    await DisplayAlert("Erro", "Usuario ou senha inválido!");
                }
                catch (Exception ex)
                {
                    ex.Report();
                    await DisplayAlert("Erro", $"Erro:{ex.Message}", "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            return;
        }
    }
}