using System;
using System.IO;
using System.Reflection;

namespace Cemapa.Models
{
    static class GravaLog
    {
        private readonly static string NomeArquivo = $"log/{DateTime.Now.ToString("dd-M-yyy")}.log";

        public static void Log(string mensagem)
        {
            string caminhoLog = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                NomeArquivo
            );
            if (!File.Exists(caminhoLog))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoLog));
                FileStream novoLog = File.Create(caminhoLog);
                novoLog.Close();
            }

            StreamWriter eof = File.AppendText(caminhoLog);
            eof.WriteLine($"{DateTime.Now.ToShortTimeString()}" + ": " + $"{ mensagem}");
            eof.Dispose();
        }
    }
}
