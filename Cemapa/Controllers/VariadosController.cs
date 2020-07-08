using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Cemapa.Controllers
{
    public class VariadosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage LinkParaJpeg(string linkImagem)
        {
            try
            {
                if (String.IsNullOrEmpty(linkImagem))
                {
                    throw new ArgumentNullException("linkImagem", "Link não informado");
                }

                WebClient client = new WebClient();
                Stream stream = client.OpenRead(linkImagem);

                Bitmap bitmap = new Bitmap(stream);

                MemoryStream memoryStream = new MemoryStream();

                bitmap.Save(memoryStream, ImageFormat.Jpeg);

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new ByteArrayContent(memoryStream.ToArray());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                response.StatusCode = HttpStatusCode.OK;

                stream.Flush();
                stream.Close();
                client.Dispose();

                return response;
            }
            catch (Exception except)
            {
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    except.Message
                );
            }
        }
    }
}
