﻿using Dottatec.Models;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dottatec.Utils
{
    public class Utilitarios
    {
        //public static bool CPFValido { get; private set; } = false;

        public static string Raiz { get; set; } = string.Empty;

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);

        }

        public static bool ValidaEmail(string email)
        {
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return EmailRegex.IsMatch(email);
        }

        public static bool ValidaGeral(string cpf, string email) 
            => (ValidaCPF(cpf) && ValidaEmail(email));

        public static async Task<IEnumerable<Usuario>> ObterUsuarios()
        {
            await Task.Delay(1000);

            var usuarios = new Usuario[]
            {
                new Usuario
                {
                    CPF = "10737317663",
                    Nome = "Pedro",
                    Email = "pedro.pj.souza@hotmail.com",
                    Senha = "pedro123"
                },
                new Usuario
                {
                    CPF = "12345678909",
                    Nome = "Carlos",
                    Email = "teste@hotmail.com",
                    Senha = "pedro123"
                },
                new Usuario
                {
                    CPF = "10737317663",
                    Nome = "Luciene",
                    Email = "eu@hotmail.com",
                    Senha = "pedro123"
                },
                new Usuario
                {
                    CPF = "10737317663",
                    Nome = "Gabriel",
                    Email = "ele@hotmail.com",
                    Senha = "pedro123"
                },
                new Usuario
                {
                    CPF = "10737317663",
                    Nome = "Carla",
                    Email = "ela@hotmail.com",
                    Senha = "pedro123"
                },
            };

            return usuarios;
        }
    }
}