﻿namespace Dottatec.Models
{
    public class Usuario : UsuarioBase
    {
        public string Nome   { get; set; } = string.Empty;

        public string Email  { get; set; } = string.Empty;

        public string CPF    { get; set; } = string.Empty;

        public string Senha  { get; set; } = string.Empty;
    }
}