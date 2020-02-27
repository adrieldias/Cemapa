using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Componentes
{
    public class CompressionHelper
    {
        public static string Decompress(byte[] str)
        {
            if (str == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (
                    var Decompressor = new Ionic.Zlib.DeflateStream(
                    output, Ionic.Zlib.CompressionMode.Decompress,
                    Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    Decompressor.Write(str, 0, str.Length);
                }

                return System.Text.Encoding.UTF8.GetString(output.ToArray());
            }
        }
    }
}