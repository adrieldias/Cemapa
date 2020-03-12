using Cemapa.Controllers.UtilsServidor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Cemapa.Models
{    
    // Um erro contextual, armazena a exceção ocorrida e também chaves que estavam
    // atribuidas em determinado contexto, ajudando assim a decifrar a causa do erro.
   
    internal class ExcecaoContextual
    {
        public Exception Excecao { get; set; }
        public Posicao Local { get; set; }
        public List<string> ChavesContextuais = new List<string>();

        public ExcecaoContextual(Exception exception)
        {
            try
            {
                //Quando criamos uma exceção contextualizada, o construtor recebe a exceção padrão
                //e colhe alguns dados, como a linha em que foi gerado o erro, e em que arquivo.
                
                StackFrame frame = new StackTrace(exception, true).GetFrames().LastOrDefault();

                if (frame != null)
                {
                    Local = new Posicao
                    {
                        Linha = frame.GetFileLineNumber(),
                        Coluna = frame.GetFileColumnNumber(),
                        Codigo = frame.GetFileName().Split(Path.DirectorySeparatorChar).Last()
                    };
                }
                
                //Busca por exceções internas para adiciona-las às chaves contextuais.
                //Tais chaves armazenam informações sobre o erro, ajudando na busca pela resolução.

                Exception inner = exception.InnerException;

                while (inner != null)
                {
                    ChavesContextuais.Add(inner.Message);
                    inner = inner.InnerException;
                }

                Excecao = exception;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public string Codigo { get; set; }
    }

    // Classe controladora de exceções, armazenada exceções em uma lista para
    // depois escreve-las todas de uma vez.
    // Utilizado para armazenar diversos erros e retorna-los ao fim da execução.

    public static class ControladorExcecoes
    {
        private static List<ExcecaoContextual> Excecoes = new List<ExcecaoContextual>();
        private static bool PrintsAmigaveis = false;
        private static List<string> Filtros  = new List<string>
        {
            "Internal Server Error",
            "A task was canceled",
            "Gateway Time-out",
            "Too Many Requests",
            "The request was aborted: Could not create SSL/TLS secure channel",
            "The remote name could not be resolved"
        };

        public static void ErrosPersonalizados(bool pa)
        {
            PrintsAmigaveis = pa;
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
                        StringBuilder MensagemFinal = new StringBuilder($"({index})");

                        if (PrintsAmigaveis)
                        {
                            if (exContext.Local != null)
                            {
                                MensagemFinal.Append($"{exContext.Local.Codigo}:{exContext.Local.Linha}");
                                MensagemFinal.Append(", ");
                            }

                            MensagemFinal.Append($"{exContext.Excecao.Message.Replace(Environment.NewLine, String.Empty).RemoveEnd()}");

                            if (exContext.ChavesContextuais.Count > 0)
                            {
                                MensagemFinal.Append(", ");
                                MensagemFinal.Append($"{string.Join(", ", exContext.ChavesContextuais).Replace(Environment.NewLine, String.Empty).RemoveEnd()}");
                            }
                        }
                        else
                        {
                            if (exContext.Local != null)
                            {
                                MensagemFinal.Append($"{exContext.Local.Codigo}:({exContext.Local.Linha},{exContext.Local.Linha})");
                                MensagemFinal.Append(", ");
                            }

                            MensagemFinal.Append($"{exContext.Excecao.Message}");

                            if (exContext.ChavesContextuais.Count > 0)
                            {
                                MensagemFinal.Append(", ");
                                MensagemFinal.Append($"{string.Join(", ", exContext.ChavesContextuais)}");
                            }
                        }

                        erros.Add(MensagemFinal.ToString());
                        index++;
                    }

                    return String.Format("[{0}]", string.Join(",", erros));
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

        public static Dictionary<string, List<string>> FormatoJson(string mensagemPai)
        {
            try
            {
                if (!SemExcecoes())
                {
                    List<string> erros = new List<string>();

                    foreach (ExcecaoContextual exContext in Excecoes)
                    {
                        StringBuilder MensagemFinal = new StringBuilder();

                        if (PrintsAmigaveis)
                        {
                            if (exContext.Local != null)
                            {
                                MensagemFinal.Append($"{exContext.Local.Codigo}:{exContext.Local.Linha}");
                                MensagemFinal.Append(", ");
                            }

                            MensagemFinal.Append($"{exContext.Excecao.Message.Replace(Environment.NewLine, String.Empty).RemoveEnd()}");

                            if (exContext.ChavesContextuais.Count > 0)
                            {
                                MensagemFinal.Append(", ");
                                MensagemFinal.Append($"{string.Join(", ", exContext.ChavesContextuais).Replace(Environment.NewLine, String.Empty).RemoveEnd()}");
                            }
                        }
                        else
                        {
                            if (exContext.Local != null)
                            {
                                MensagemFinal.Append($"{exContext.Local.Codigo}:({exContext.Local.Linha},{exContext.Local.Linha})");
                                MensagemFinal.Append(", ");
                            }

                            MensagemFinal.Append($"{exContext.Excecao.Message}");

                            if (exContext.ChavesContextuais.Count > 0)
                            {
                                MensagemFinal.Append(", ");
                                MensagemFinal.Append($"{string.Join(", ", exContext.ChavesContextuais)}");
                            }
                        }

                        erros.Add(MensagemFinal.ToString());
                    }

                    return new Dictionary<string, List<string>>()
                    {
                        { mensagemPai, erros }
                    };                    
                }
                else
                {
                    return new Dictionary<string, List<string>>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Adiciona(Exception except)
        {
            try
            {
                if (!Filtros.Exists(filtro => except.Message.Contains(filtro)))
                {
                    ExcecaoContextual exContx = new ExcecaoContextual(except);
                    Excecoes.Add(exContx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Adiciona(Exception except, List<string> Referencias)
        {
            try
            {
                if (!Filtros.Exists(filtro => except.Message.Contains(filtro)))
                {
                    ExcecaoContextual exContx = new ExcecaoContextual(except);

                    foreach (string referencia in Referencias)
                    {
                        if (!Filtros.Exists(filtro => filtro == referencia))
                        {
                            exContx.ChavesContextuais.Add(referencia);
                        }
                    }

                    Excecoes.Add(exContx);
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
    }
}