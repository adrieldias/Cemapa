using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Cemapa.Controllers.UtilsServidor
{
    public static class StringUtils
    {
        //Por Gian, 06/02/2020
        //Trunca uma string

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        //Por Gian, 10/03/2020
        //Remove o caracter 'ponto final' de uma string se existir

        public static string RemoveEnd(this string value)
        {
            return value[value.Length - 1] == '.' ? value.Remove(value.Length - 1, 1) : value;
        }

        //Por Gian, 06/02/2020
        //Utilizado para descompactar respostas compactadas de chamadas Http.

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    byte[] bytes2 = new byte[4096];

                    int cnt;

                    while ((cnt = gs.Read(bytes2, 0, bytes2.Length)) != 0)
                    {
                        mso.Write(bytes2, 0, cnt);
                    }
                }
                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        //Por Gian, 27/02/2020
        //Utilizado para formatar objetos em arquivos Json.

        public static object ObjetoJsonFormatado(object objeto)
        {
            JsonSerializerSettings jsonConfigs = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(objeto, jsonConfigs));
        }
    }
}