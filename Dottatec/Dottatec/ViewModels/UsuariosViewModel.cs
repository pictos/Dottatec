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
                    //EditarCommand.ChangeCanExecute();
                }
            }
        }

        public ObservableCollection<Usuario> Usuarios { get; }

        public Command CadastrarCommand               { get; }

        //public Command<Usuario> EditarCommand         { get; }

        #endregion

        private Usuario user;

        public override async Task InitializeAsync(object[] args)
        {
            await RemovePage(typeof(LoginPage));
            //Obter dados do Banco e popular aqui
            if (args != null)
            {
                user = (Usuario)args[0];
                Titulo += $" - {user.Nome}";
                Settings.NomeL = user.Nome;
                //Em aplicação real, jamais guardar senha no dispositivo,
                //Exceto se for criptografada.
                Settings.SenhaL = user.Senha;
            }
           await AtualizarLista();
            
        }

        private async Task AtualizarLista()
        {
            var lista = await BDAzure.Current.ObterUsuariosAsync();

            Usuarios.Clear();

            foreach (var item in lista)
                Usuarios.Add(item);
        }

        public UsuariosViewModel()
        {
            Inscrever();
            Titulo           = "Lista de Usuários";
            Usuarios         = new ObservableCollection<Usuario>();
            CadastrarCommand = new Command(async () => await ExecuteCadastrarCommand(), () => !IsBusy);
            //EditarCommand    = new Command<Usuario>(async u => await ExecuteEditarCommand(u), u => !IsBusy);
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
