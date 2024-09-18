using API_Aplicacion.Services;
using AplicacionData.Data;
using AplicacionData.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Aplicacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private IAplicacion service;        

        public ArticulosController()
        {
            service = new Aplicacion();
        }
        // GET: api/<ArticulosController>
        [HttpGet("consultar")]
        public IActionResult GetConsultar()
        {
            return Ok(service.Consultar());
        }

        // GET api/<ArticulosController>/5
        [HttpPost]
        public IActionResult PostAgregar([FromBody] Articulo oArticulo)
        {
            try
            {
                if (oArticulo == null)
                {
                    return BadRequest("Se esperaba un objeto valido");
                }
                if (service.Agregar(oArticulo))
                {
                    return Ok("Articulo registrado con exito");
                }
                else
                {
                    return StatusCode(500, "No se pudo registrar el articulo");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }
            
        }

        // PUT api/<ArticulosController>/5
        [HttpPut("{id}")]
        public IActionResult PutEditar(int id, [FromBody] string descripcion)
        {
            try
            {
                if (id > 0 && (descripcion != string.Empty && descripcion != null))
                {
                    if (service.Editar(id, descripcion))
                    {
                        return Ok("Se edito el articulo con exito");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo editar el articulo con exito");
                    }
                }
                else
                {
                    return BadRequest("Se esperaba un articulo valido");
                }
                
            }
            catch(Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

        }

        // DELETE api/<ArticulosController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistarBaja(int id)
        {
            try
            {
                if (id > 0)
                {
                    if (service.RegistarBaja(id))
                    {
                        return Ok("Se regirstro la baja del articulo con exito");
                    }
                    else
                    {
                        return StatusCode(500, "No se pudo registrar la baja del articulo");
                    }
                }
                else
                {
                    return BadRequest("Se esperaba una id mayor a cero");
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

        }
    }
}
