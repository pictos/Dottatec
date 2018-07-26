using Android.App;
using Android.Widget;
using Dottatec.Droid.Dependencia;
using Dottatec.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NotificacaoAndroid))]
namespace Dottatec.Droid.Dependencia
{
    public class NotificacaoAndroid : INotificacao
    {
        public void Notificar(string msg) 
            => Toast.MakeText(Application.Context, msg, ToastLength.Short).Show();
    }
}