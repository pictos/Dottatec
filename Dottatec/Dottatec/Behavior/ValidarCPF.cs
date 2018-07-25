using Dottatec.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dottatec.Behavior
{
    public class ValidarCPF : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableCPF;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableCPF;
        }

        private void BindableCPF(object sender, TextChangedEventArgs e)
        {
            var cpf = e.NewTextValue;

            if (cpf.Length < 8)
                return;

            var resultado = Utilitarios.ValidaCPF(cpf);

            var entry = (Entry)sender;

            if (resultado)
                entry.BackgroundColor = Color.Transparent;
            else
                entry.BackgroundColor = Color.Red;
        }
    }
}
