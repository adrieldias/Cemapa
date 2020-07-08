using Cemapa.Controllers.UtilsServidor;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public ExcecaoContextual(DbEntityValidationException exception)
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
                
            foreach (DbEntityValidationResult entValidations in exception.EntityValidationErrors)
            {
                foreach (DbValidationError validation in entValidations.ValidationErrors)
                {
                    ChavesContextuais.Add(validation.ErrorMessage);
                }
            }

            Excecao = exception;
        }

        public ExcecaoContextual(Exception exception)
        {
            //Quando criamos uma exceção contextualizada, o construtor recebe a exceção padrão
            //e colhe alguns dados, como a linha em que foi gerado o erro, e em que arquivo.

            StackTrace trace = new StackTrace(exception, true);

            if (trace.FrameCount > 0)
            {
                StackFrame frame = trace.GetFrames().LastOrDefault();

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

    public class ControladorExcecoes
    {
        private readonly List<ExcecaoContextual> Excecoes = new List<ExcecaoContextual>();
        private bool PrintsAmigaveis = false;
        private readonly List<string> Filtros  = new List<string>
        {
            "A task was canceled",
            "Gateway Time-out",
            "The request was aborted: Could not create SSL/TLS secure channel",
            "The remote name could not be resolved"
        };

        public void ErrosPersonalizados(bool pa)
        {
            PrintsAmigaveis = pa;
        }

        public string Printa()
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

        public Dictionary<string, List<string>> FormatoJson(string mensagemPai)
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

        public void Adiciona(DbEntityValidationException except, List<string> Referencias = null)
        {
            if (Referencias == null)
            {
                Referencias = new List<string>();
            }

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

        public void Adiciona(Exception except, List<string> Referencias = null)
        {
            if (Referencias == null)
            {
                Referencias = new List<string>();
            }

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

        public bool SemExcecoes()
        {
            if (Excecoes.Count > 0)
                return false;
            else
                return true;
        }

        public void Limpa()
        {
            Excecoes.Clear();
        }
    }
}