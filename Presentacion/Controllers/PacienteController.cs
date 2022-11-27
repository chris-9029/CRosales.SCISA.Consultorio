using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Presentacion.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PacienteController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Consumiendo Servicio Web
            Negocio.Result resultPaciente = new Negocio.Result();
            resultPaciente.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                string uriConnection = _configuration["applicationUrl"];
                client.BaseAddress = new Uri(uriConnection);

                var responseTask = client.GetAsync("Paciente");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Negocio.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        Negocio.Paciente pacienteList = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Paciente>(resultItem.ToString());
                        resultPaciente.Objects.Add(pacienteList);
                    }
                }
            }
            Negocio.Paciente paciente = new Negocio.Paciente();
            paciente.pacientes = resultPaciente.Objects;

            return View(paciente);

        }

        [HttpGet]
        public ActionResult Form(int? idPaciente)
        {
            Negocio.Paciente paciente = new Negocio.Paciente();

            //ADD - cuando insertamos, manda la vista vacia(solo cargara los DDL)
            if (idPaciente > 0 || idPaciente != null)
            {
                Negocio.Result resultPaciente = new Negocio.Result();

                try
                {
                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var responseTask = client.GetAsync($"Paciente/Get/{idPaciente}");
                        responseTask.Wait();
                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<Negocio.Result>();
                            readTask.Wait();

                            Negocio.Paciente resultItem = new Negocio.Paciente();

                            resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Paciente>(readTask.Result.Object.ToString());
                            resultPaciente.Object = resultItem;
                            resultPaciente.Correct = true;
                        }
                        else
                        {
                            resultPaciente.Correct = false;
                            resultPaciente.ErrorMessage = "No se encontro la información del registro";
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultPaciente.Correct = false;
                    resultPaciente.ErrorMessage = ex.Message;
                }


                if (resultPaciente.Correct)
                {
                    //UNBOXING
                    paciente = (Negocio.Paciente)resultPaciente.Object;
                    return View(paciente);
                }
                else
                {
                    ViewBag.Message = "Algo salío mal... no se encontro el registro, " + resultPaciente.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(paciente);
            }
        }

        [HttpPost]
        public ActionResult Form(Negocio.Paciente paciente)
        {
            IFormFile image = Request.Form.Files["IFImagen"];

            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto
                paciente.Foto = Convert.ToBase64String(ImagenBytes);
            }
            else
            {
                paciente.Foto = "";
            }

            if (ModelState.IsValid)
            {
                if (paciente.IdPaciente == 0)
                {
                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var postTask = client.PostAsJsonAsync("Paciente/Add", paciente);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = "Algo salio mal al agregar";
                        }
                    }
                }
                else
                {

                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var postTask = client.PostAsJsonAsync("Paciente/Update", paciente);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = "Algo salio mal actualizar";
                        }
                    }


                }
            }
            else
            {
                return View(paciente);
            }

            return View("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpGet]
        public ActionResult Delete(int IdPaciente)
        {
            if (IdPaciente > 0)
            {
                using (var client = new HttpClient())
                {
                    string uriConnection = _configuration["applicationUrl"];
                    client.BaseAddress = new Uri(uriConnection);

                    var postTask = client.DeleteAsync("Paciente/Delete/" + IdPaciente);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");

                    }
                    {
                        ViewBag.Message = "Algo salio mal... consulta con el administrador del servidor";
                    }
                }

            }
            else
            {
                ViewBag.Message = "No se elimino... Usuario no encontrado";
            }
            return View("Modal");
        }



    }
}
