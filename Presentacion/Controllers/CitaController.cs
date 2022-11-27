using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Presentacion.Controllers
{
    public class CitaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CitaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public ActionResult Index()
        {
            //Consumiendo Servicio Web
            Negocio.Result resultCita = new Negocio.Result();
            resultCita.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                string uriConnection = _configuration["applicationUrl"];
                client.BaseAddress = new Uri(uriConnection);

                var responseTask = client.GetAsync("Cita");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Negocio.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        Negocio.Cita citaList = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Cita>(resultItem.ToString());
                        resultCita.Objects.Add(citaList);
                    }
                }
            }
            Negocio.Cita cita = new Negocio.Cita();
            cita.citas = resultCita.Objects;

            return View(cita);
        }

        [HttpGet]
        public ActionResult Form()
        {
            Negocio.Cita cita = new Negocio.Cita();
            cita.paciente = new Negocio.Paciente();


            Negocio.Result resultDoctor = new Negocio.Result();

            try
            {
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

                        resultDoctor.Objects = new List<object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            Negocio.Doctor doctorList = Newtonsoft.Json.JsonConvert.DeserializeObject<Negocio.Doctor>(resultItem.ToString());
                            resultDoctor.Objects.Add(doctorList);
                        }

                        resultDoctor.Correct = true;
                    }
                    else
                    {
                        ViewBag.Message = "Algo salío mal... no se encontro el registro, " + resultDoctor.ErrorMessage;
                        return View("Modal");
                    }
                }

                cita.doctor = new Negocio.Doctor();
                cita.doctor.doctores = resultDoctor.Objects;

            }
            catch (Exception ex)
            {
                resultDoctor.Correct = false;
                resultDoctor.ErrorMessage = ex.Message;
            }

            if (resultDoctor.Correct)
            {

                return View(cita);
            }
            else
            {
                ViewBag.Message = "Algo salío mal... no se encontro el registro, " + resultDoctor.ErrorMessage;
                return View("Modal");
            }
        }

        [HttpPost]
        public ActionResult Form(Negocio.Cita cita)
        {
            IFormFile image = Request.Form.Files["IFImagen"];

            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                cita.paciente.Foto = Convert.ToBase64String(ImagenBytes);
            }
            else
            {
                cita.paciente.Foto = "";
            }

            Negocio.Result rs = Negocio.Cita.Add(cita);
            if (rs.Correct)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Algo salio mal. " + rs.ErrorMessage;
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
        public ActionResult Delete(int IdCita)
        {
            if (IdCita > 0)
            {
                Negocio.Result result = Negocio.Cita.Delete(IdCita);
                if (result.Correct)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Algo salio mal al eliminar el registro. " + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "No se elimino... registro no encontrado";
            }
            return View("Modal");
        }


    }
}
