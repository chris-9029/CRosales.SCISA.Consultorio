using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        // GET: api/<DoctorController>
        [HttpGet]
        //[Route("GetAll")]
        public IActionResult GetAll()
        {
            Negocio.Result result = Negocio.Doctor.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Negocio.Doctor doctor)
        {
            Negocio.Result result = Negocio.Doctor.Add(doctor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Get/{idDoctor}")]
        public IActionResult GetById(int idDoctor)
        {
            Negocio.Result result = Negocio.Doctor.GetById(idDoctor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] Negocio.Doctor doctor)
        {
            Negocio.Result result = Negocio.Doctor.Update(doctor);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpDelete]
        [Route("delete/{idDoctor}")]
        public IActionResult Delete(int idDoctor)
        {
            Negocio.Result result = Negocio.Doctor.Delete(idDoctor);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {   
                return NotFound(result);
            }

        }


    }
}
