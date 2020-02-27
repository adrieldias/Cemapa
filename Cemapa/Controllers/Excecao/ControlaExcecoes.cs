using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cemapa.Models
{    
    // Um erro contextual, armazena a exceção ocorrida e também chaves que estavam
    // atribuidas em determinado contexto, ajudando assim a decifrar a causa do erro.

    internal class ExcecaoContextual
    {
        public Exception Excecao { get; set; }
        public List<string> ChavesContextuais = new List<string>();
    }

    // Classe controladora de exceções, armazenada exceções em uma lista para
    // depois escreve-las todas de uma vez.
    // Utilizado para armazenar diversos erros e retorna-los ao fim da execução.

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
                "A task was canceled.",
                "Gateway Time-out",
                "Too Many Requests"
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
                        if (exContext.ChavesContextuais.Count > 0)
                        {
                            erros.Add($"({index})[{exContext.Excecao.Message}, {string.Join(", ", exContext.ChavesContextuais)}]");
                        }
                        else
                        {
                            erros.Add($"({index})[{exContext.Excecao.Message}]");
                        }
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
            while (except.InnerException != null)
            {
                except = except.InnerException;
            }

            return except;
        }
    }
}