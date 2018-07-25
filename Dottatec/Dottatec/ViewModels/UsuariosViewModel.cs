using Dottatec.Mensagens;
using Dottatec.Models;
using Dottatec.Servicos;
using Dottatec.Utils;
using Dottatec.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dottatec.ViewModels
{
    public class UsuariosViewModel : BaseViewModel
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
                    CadastrarCommand.ChangeCanExecute();
                }
            }
        }

        public ObservableCollection<Usuario> Usuarios { get; }

        public Command CadastrarCommand               { get; }

        public Command LogOffCommand                  { get; }

        #endregion

        private Usuario user;

        public override async Task InitializeAsync(object[] args)
        {
            IsBusy = true;

            await RemovePage(typeof(LoginPage));
            if (args != null)
            {
                user = (Usuario)args[0];
                Settings.NomeL = user.Nome;
                //Em aplicação real, jamais guardar senha no dispositivo,
                //Exceto se for criptografada.
                Settings.SenhaL = user.Senha;
            }
            Titulo += $" - {Settings.NomeL}";
            await AtualizarLista();

            IsBusy = false;
        }

        public UsuariosViewModel()
        {
            Inscrever();
            Titulo           = "Lista de Usuários";
            Usuarios         = new ObservableCollection<Usuario>();
            CadastrarCommand = new Command(async () => await ExecuteCadastrarCommand(), () => !IsBusy);
            LogOffCommand    = new Command(async () => await ExecuteLogOffCommand(), () => !isBusy);
        }

        async Task ExecuteLogOffCommand()
        {

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    Settings.NomeL = string.Empty;
                    Settings.SenhaL = string.Empty;
                    await PushAsync<LoginViewModel>();

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

        private async Task AtualizarLista()
        {
            var lista = await BDAzure.Current.ObterUsuariosAsync();

            Usuarios.Clear();

            foreach (var item in lista)
                Usuarios.Add(item);
        }

        private void Inscrever()
        {
            MessagingCenter.Subscribe<Atualizar>(this, nameof(Atualizar), async sender =>
            {
                await AtualizarLista();
            });
        }

        public async Task ExecuteEditarCommand(Usuario usuario)
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await PushAsync<EditarUsuarioViewModel>(usuario);

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