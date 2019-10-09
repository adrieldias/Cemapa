using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{
    //Classe que armazena exceções em uma lista para depois escreve-las todas de uma vez.
    //Utilizado para armazenar diversos erros e retorna-los ao fim da execução.

    internal class ExcecaoContextual
    {
        public Exception Excecao { get; set; }
        public List<string> ChavesContextuais = new List<string>();
    }

    public static class ControladorExcecoes
    {
        private static List<ExcecaoContextual> Excecoes = new List<ExcecaoContextual>();
        private static List<string> Filtros;

        static ControladorExcecoes()
        {
            Filtros = new List<string>
            {
                "Internal Server Error",
                "Bad Request",
                "A task was canceled."
            };
        }

        public static string Printa()
        {
            try
            {
                if (!SemExcecoes())
                {
                    List<string> erros = new List<string>();
                    int index = 1;

                    foreach (ExcecaoContextual exContext in Excecoes)
                    {
                        erros.Add($"({index})[{exContext.Excecao.Message}, {string.Join(", ", exContext.ChavesContextuais)}]");
                        index++;
                    }

                    return string.Join(";", erros);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Adiciona(Exception except, List<string> Referencias = null)
        {
            try
            {
                except = ExcecaoInterna(except);

                if (!Filtros.Exists(e => except.Message == e))
                {
                    if (Referencias == null)
                    {
                        Excecoes.Add(
                            new ExcecaoContextual
                            {
                                Excecao = except
                            }
                        );
                    }
                    else
                    {
                        if (!Filtros.Any(x => Referencias.Any(y => y == x)))
                        {
                            ExcecaoContextual exContx = new ExcecaoContextual
                            {
                                Excecao = except
                            };

                            foreach (string Referencia in Referencias)
                            {
                                exContx.ChavesContextuais.Add(Referencia);
                            }

                            Excecoes.Add(exContx);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool SemExcecoes()
        {
            if (Excecoes.Count > 0)
                return false;
            else
                return true;
        }

        public static void Limpa()
        {
            Excecoes.Clear();
        }

        private static Exception ExcecaoInterna(Exception except)
        {
            Exception wUltimaExcecao = except;

            while (except.InnerException != null)
            {
                except = except.InnerException;
                wUltimaExcecao = except;
            }

            return wUltimaExcecao;
        }
    }
}