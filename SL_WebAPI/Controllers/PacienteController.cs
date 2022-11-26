using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            Negocio.Result result = Negocio.Paciente.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("Get/{idPaciente}")]
        public IActionResult GetById(int idPaciente)
        {
            Negocio.Result result = Negocio.Paciente.GetById(idPaciente);

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
        public IActionResult Add([FromBody] Negocio.Paciente paciente)
        {
            Negocio.Result result = Negocio.Paciente.Add(paciente);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] Negocio.Paciente paciente)
        {
            Negocio.Result result = Negocio.Paciente.Update(paciente);
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
        [Route("delete/{idPaciente}")]
        public IActionResult Delete(int idPaciente)
        {
            Negocio.Result result = Negocio.Paciente.Delete(idPaciente);

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
