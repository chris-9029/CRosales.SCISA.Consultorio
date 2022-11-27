using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/cita/")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        [HttpGet]
        public IActionResult CitaList()
        {
            Negocio.Result result = Negocio.Cita.CitaList();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        //[HttpGet]
        //[Route("GetAll")]
        //public IActionResult GetAll()
        //{
        //    Negocio.Result result = Negocio.Cita.GetAll();
        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}
        
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]Negocio.Cita cita)
        {
            //Negocio.Result result = Negocio.Cita.Add(cita);
            Negocio.Result result = Negocio.Cita.Add(cita);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPost]
        //[Route("Update")]
        //public IActionResult Update([FromBody]Negocio.Cita cita)
        //{
        //    Negocio.Result result = Negocio.Cita.Update(cita);
        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}

        //[HttpDelete]
        //[Route("delete/{idCita}")]
        //public IActionResult Delete(int idCita)
        //{
        //    Negocio.Result result = Negocio.Cita.Delete(idCita);

        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound(result);
        //    }

        //}


    }
}
