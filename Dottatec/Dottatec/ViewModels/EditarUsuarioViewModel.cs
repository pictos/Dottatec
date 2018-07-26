using Dottatec.Interfaces;
using Dottatec.Mensagens;
using Dottatec.Models;
using Dottatec.Servicos;
using Dottatec.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dottatec.ViewModels
{
    public class EditarUsuarioViewModel : BaseViewModel
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
                    SalvarCommand.ChangeCanExecute();
                    ExcluirCommand.ChangeCanExecute();
                }
            }
        }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { SetProperty(ref nome, value); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string cpf;

        public string CPF
        {
            get { return cpf; }
            set { SetProperty(ref cpf, value); }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set { SetProperty(ref senha, value); }
        }

        public Command SalvarCommand  { get; }

        public Command ExcluirCommand { get; }

        #endregion

        private Usuario user;

        public async override Task InitializeAsync(object[] args)
        {
            IsBusy = true;
            await Task.Delay(100);

            if(args[0] is Usuario)
            {
                user  = (Usuario)args[0];
                Senha = user.Senha;
                Nome  = user.Nome;
                Email = user.Email;
                CPF   = user.CPF;
            }

            IsBusy = false;
        }

        public EditarUsuarioViewModel()
        {
            SalvarCommand  = new Command(async () => await ExecuteSalvarCommand(),  () => !IsBusy);
            ExcluirCommand = new Command(async () => await ExecuteExcluirCommand(), () => !IsBusy);
        }

        async Task ExecuteExcluirCommand()
        {

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    await BDAzure.Current.DeletarUsuario(user);
                    DependencyService.Get<INotificacao>().Notificar("Usuario Excluído.");
                    MessagingCenter.Send<Atualizar>(new Atualizar(), nameof(Atualizar));
                    await PopAsync();
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

        async Task ExecuteSalvarCommand()
        {

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    //Editar item.

                     user.Senha = Senha ;
                     user.Nome  = Nome  ;
                     user.Email = Email ;
                     user.CPF   = CPF;

                    if(!Utilitarios.ValidaGeral(CPF,Email))
                    {
                        await DisplayAlert("Erro","CPF ou Email inválido!");
                        return;
                    }


                    await  BDAzure.Current.SalvarUsuarioAsync(user);

                    DependencyService.Get<INotificacao>().Notificar("Alterações salvas.");

                    MessagingCenter.Send<Atualizar>(new Atualizar(), nameof(Atualizar));

                    await PopAsync();

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