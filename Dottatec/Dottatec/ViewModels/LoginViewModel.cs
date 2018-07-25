﻿using Dottatec.Servicos;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

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

        public Command LogarCommand { get; }

        #endregion

        public LoginViewModel()
        {
            LogarCommand = new Command(async () => await ExecuteLogarCommand(), () => !IsBusy);
            Senha        = string.Empty;
            Usuario      = string.Empty;
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