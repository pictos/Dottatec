using Xamarin.Forms;

namespace Dottatec.Behavior
{
    public class ValidarEmail : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableEmail;
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableEmail;
        }

        private void BindableEmail(object sender, TextChangedEventArgs e)
        {
            var email = e.NewTextValue;
            if (email.Length < 4)
                return;

            var resultado = Utils.Utilitarios.ValidaEmail(email);
            var entry = (Entry)sender;
            if (resultado)
                entry.BackgroundColor = Color.Transparent;
            else
                entry.BackgroundColor = Color.Red;
        }
    }
}
