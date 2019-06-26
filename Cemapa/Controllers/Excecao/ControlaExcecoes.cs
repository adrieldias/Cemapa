using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    //Classe que armazena diversas strings em uma lista para depois escreve-las todas de uma vez.
    //Utilizado para armazenar diversos erros e retorna-los ao fim da execução.

    public static class ControlaExcecoes
    {
        public static List<string> Excecoes;

        static ControlaExcecoes()
        {
            Excecoes = new List<string>();
        }

        public static void Add(string excecao1, string excecao2 = "", string excecao3 = "")
        {
            if ((excecao2 != "") && (excecao3 != ""))
            {

                Excecoes.Add($"{excecao1}, {excecao2}, {excecao3}");
            }
            else if (excecao2 != "")
            {
                Excecoes.Add($"{excecao1}, {excecao2}");
            }
            else
            {
                Excecoes.Add($"{excecao1}");
            }
        }

        public static bool SemExcecoes()
        {
            if (Excecoes.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void Limpa()
        {
            Excecoes.Clear();
        }
    }
    
    //Muitas vezes a exceção armazena a mensagem pedindo para consultar a InnerException, que também é uma Exception.
    //A classe a seguir é responsável por percorrer a Exception e encontrar uma mensagem de erro melhor.

    public static class ResolucaoExcecoes
    {
        public static string ErroAprofundado(Exception except)
        {
            while (except.Message == "An error occurred while updating the entries. See the inner exception for details.")
            {
                except = except.InnerException;
            }

            return except.Message;
        }
    }
}