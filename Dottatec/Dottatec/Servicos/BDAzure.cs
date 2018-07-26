using Dottatec.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dottatec.Servicos
{
    public class BDAzure
    {
        private static Lazy<BDAzure> LazyAzure = new Lazy<BDAzure>(() => new BDAzure());

        public static BDAzure Current => LazyAzure.Value;

        BDAzure() { }

        public MobileServiceClient Cliente { get; private set; }
        IMobileServiceSyncTable<Usuario> usuarios;

        public async Task InitiAsync()
        {
            Cliente   = new MobileServiceClient(Const.Url);
            var local = new MobileServiceSQLiteStore("localUser.db");
            local.DefineTable<Usuario>();
            await Cliente.SyncContext.InitializeAsync(local,StoreTrackingOptions.NotifyLocalAndServerOperations);
            usuarios = Cliente.GetSyncTable<Usuario>();
        }

        public async Task<List<Usuario>> ObterUsuariosAsync()
        {
            try
            {

                await SincronizarAsync().ConfigureAwait(false);

                var itens = await usuarios.ToEnumerableAsync();


                return itens.ToList();

            }
            catch (MobileServiceInvalidOperationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SalvarUsuarioAsync(Usuario item)
        {
            try
            {
                if (item.Id == null)
                    await usuarios.InsertAsync(item).ConfigureAwait(false);
                else
                    await usuarios.UpdateAsync(item).ConfigureAwait(false);

                await SincronizarAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SincronizarAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErros = null;
            try
            {
                await Cliente.SyncContext.PushAsync().ConfigureAwait(false);
                

                await usuarios.PullAsync("todosUsuarios", usuarios.CreateQuery()).ConfigureAwait(false);
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErros = exc.PushResult.Errors;
                }
            }

            if (syncErros != null)
            {
                foreach (var erro in syncErros)
                {
                    if (erro.OperationKind == MobileServiceTableOperationKind.Update && erro.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await erro.CancelAndUpdateItemAsync(erro.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await erro.CancelAndDiscardItemAsync();
                    }

                }
            }
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarioAsync(string nome, string senha)
        {
            try
            {
                var query = from n in usuarios
                            where n.Nome == nome && n.Senha == senha
                            select n;

                var consulta = await usuarios.ReadAsync(query);
                return consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletarUsuario(Usuario item)
        {
            try
            {
                if (item.Id == null)
                    return;

                await usuarios.DeleteAsync(item).ConfigureAwait(false);
                await SincronizarAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}