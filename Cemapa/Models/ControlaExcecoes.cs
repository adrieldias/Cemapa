using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
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
}