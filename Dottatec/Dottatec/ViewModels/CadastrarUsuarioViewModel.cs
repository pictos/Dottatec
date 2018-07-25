using Dottatec.Mensagens;
using Dottatec.Models;
using Dottatec.Servicos;
using Dottatec.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dottatec.ViewModels
{
    public class CadastrarUsuarioViewModel : BaseViewModel
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
                    AddCommand.ChangeCanExecute();
                };
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

        public Command AddCommand { get; }

        #endregion

        public CadastrarUsuarioViewModel()
        {
            AddCommand = new Command(async () => await ExecuteAddCommand(), () => !IsBusy);
        }

        async Task ExecuteAddCommand()
        {
            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;

                    if (!Utilitarios.ValidaGeral(CPF, Email))
                    {
                        await DisplayAlert("Erro", "CPF ou Email inválido!");
                        return;
                    }

                    await BDAzure.Current.SalvarUsuarioAsync(new Usuario
                    {
                        Senha = Senha,
                        Nome  = Nome,
                        Email = Email,
                        CPF   = CPF
                    });

                    Senha = string.Empty;
                    Nome  = string.Empty;
                    Email = string.Empty;
                    CPF   = string.Empty;

                    MessagingCenter.Send<Atualizar>(new Atualizar (), nameof(Atualizar));

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
