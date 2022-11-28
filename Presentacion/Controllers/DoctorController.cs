using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Presentacion.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<DoctorController> logger)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //Consumiendo Servicio Web
            Negocio.Result resultDoctor = new Negocio.Result();
            resultDoctor.Objects = new List<object>();
            Negocio.Doctor doctor = new Negocio.Doctor();

            try
            {
                _logger.LogWarning("- GET Doctor Index -");

                using (var client = new HttpClient())
                {
                    string uriConnection = _configuration["applicationUrl"];
                    client.BaseAddress = new Uri(uriConnection);

                    var responseTask = client.GetAsync("Doctor");
                    responseTask.Wait();
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Negocio.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            Negocio.Doctor doctorList = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Doctor>(resultItem.ToString());
                            resultDoctor.Objects.Add(doctorList);
                        }
                    }
                }
                doctor.doctores = resultDoctor.Objects;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }


            return View(doctor);
        }

        [HttpGet]
        public ActionResult Form(int? idDoctor)
        {
            Negocio.Doctor doctor = new Negocio.Doctor();

            
            if (idDoctor > 0 || idDoctor != null)
            {
                Negocio.Result resultDoctor = new Negocio.Result();

                try
                {
                    _logger.LogWarning("- GET Formulario -");
                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var responseTask = client.GetAsync($"Doctor/Get/{idDoctor}");
                        responseTask.Wait();
                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<Negocio.Result>();
                            readTask.Wait();

                            Negocio.Doctor resultItem = new Negocio.Doctor();

                            resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Doctor>(readTask.Result.Object.ToString());
                            resultDoctor.Object = resultItem;
                            resultDoctor.Correct = true;
                        }
                        else
                        {
                            resultDoctor.Correct = false;
                            resultDoctor.ErrorMessage = "No se encontro la información del registro";
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultDoctor.Correct = false;
                    resultDoctor.ErrorMessage = ex.Message;

                    //log24net error
                    _logger.LogError(resultDoctor.ErrorMessage);
                }


                if (resultDoctor.Correct)
                {
                    //UNBOXING
                    doctor = (Negocio.Doctor)resultDoctor.Object;
                    return View(doctor);
                }
                else
                {
                    ViewBag.Message = "Algo salío mal... no se encontro el registro, " + resultDoctor.ErrorMessage;
                    return View("Modal");
                }
            }
            else
            {
                return View(doctor);
            }
        }

        [HttpPost]
        public ActionResult Form(Negocio.Doctor doctor)
        {
            IFormFile image = Request.Form.Files["IFImagen"];

            try
            {
                _logger.LogWarning("- POST Formulario -");

                if (image != null)
                {
                    //llamar al metodo que convierte a bytes la imagen
                    byte[] ImagenBytes = ConvertToBytes(image);
                    //convierto a base 64 la imagen y la guardo en mi objeto
                    doctor.Foto = Convert.ToBase64String(ImagenBytes);
                }
                else
                {
                    doctor.Foto = "";
                }

                if (doctor.IdDoctor == 0)
                {
                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var postTask = client.PostAsJsonAsync("Doctor/Add", doctor);
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

                        var postTask = client.PostAsJsonAsync("Doctor/Update", doctor);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            _logger.LogError(result.ToString());
                            ViewBag.Message = "Algo salio mal actualizar";
                        }
                    }


                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
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
        public ActionResult Delete(int IdDoctor)
        {
            try
            {
                _logger.LogWarning("- DELETE DOCTOR -");

                if (IdDoctor > 0)
                {
                    using (var client = new HttpClient())
                    {
                        string uriConnection = _configuration["applicationUrl"];
                        client.BaseAddress = new Uri(uriConnection);

                        var postTask = client.DeleteAsync("Doctor/Delete/" + IdDoctor);
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
                    ViewBag.Message = "No se elimino... Registro no encontrado";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return View("Modal");
        }


    }
}
