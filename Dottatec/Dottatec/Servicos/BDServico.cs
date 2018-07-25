using Dottatec.Models;
using Dottatec.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dottatec.Servicos
{
    class BDServico
    {
        private static Lazy<BDServico> LazyBd = new Lazy<BDServico>(() => new BDServico());

        public static BDServico Current => LazyBd.Value;

        BDServico()
        {
            var nome = Path.Combine(Utilitarios.Raiz, "usuario.db3");
            Conexao  = new SQLiteConnection(nome);
            Conexao.CreateTable<Usuario>();
        }

        private readonly SQLiteConnection Conexao;

        //#region Consultas
        //public T GetItem<T>(int id) where T : IRegra, new() => 
        //    Conexao.Table<T>().FirstOrDefault(c => c.Id == id);

        //public IEnumerable<T> GetItems<T>() where T : IRegra, new()
        //{
        //    return (from i in Conexao.Table<T>()
        //            select i);
        //}

        //public int SaveItem<T>(T item) where T : IRegra
        //{
        //    if (item.Id != 0)
        //    {
        //        Conexao.Update(item);
        //        return item.Id;
        //    }
        //    return Conexao.Insert(item);
        //}

        //public void SaveItens<T>(IEnumerable<T> items) where T : IRegra
        //{
        //    Conexao.BeginTransaction();

        //    foreach (T item in items)
        //        SaveItem(item);
            

        //    Conexao.Commit();
        //}

        //public bool DeleteItem<T>(T item) where T : IRegra, new()
        //    => Conexao.Delete(item) > 0;

        //public void Dispose() =>
        //    Conexao.Dispose();


        //#endregion
    }
}