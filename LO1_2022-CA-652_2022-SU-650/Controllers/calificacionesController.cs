using LO1_2022_CA_652_2022_SU_650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LO1_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly blogContext _calificacionesContext;

        public calificacionesController(blogContext calificacionesContext)
        {
            _calificacionesContext = calificacionesContext;
        }

        /// <summary>
        /// EndPoint para obtener todos las calificaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<calificaciones> listaCalificaciones = (from c in _calificacionesContext.calificaciones
                                                        select c).ToList();

            if (listaCalificaciones.Count() == 0) return NotFound();

            return Ok(listaCalificaciones);
        }

        /// <summary>
        /// EndPoint para obtener calificacion ppor Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            calificaciones? calificacion = (from c in _calificacionesContext.calificaciones
                                            where c.calificacionId == id
                                            select c).FirstOrDefault();

            if (calificacion == null) return NotFound();

            return Ok(calificacion);
        }

        /// <summary>
        /// EndPoint para crear un registro de calificacion
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult AddCalificacion([FromBody] calificaciones calificacion)
        {
            try
            {
                _calificacionesContext.calificaciones.Add(calificacion);
                _calificacionesContext.SaveChanges();
                return Ok(calificacion);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// EndPoint para actualizar un registro de calificaciones
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] calificaciones calificacionModificar)
        {
            //Obtener el registro original
            calificaciones? calificacionActual = (from c in _calificacionesContext.calificaciones
                                                  where c.calificacionId == id
                                                  select c).FirstOrDefault();

            //Verificar si el registro existe
            if (calificacionActual == null) return NotFound();

            //Modificar si el registro existe 
            calificacionActual.publicacionId = calificacionModificar.publicacionId;
            calificacionActual.usuarioId = calificacionModificar.usuarioId;
            calificacionActual.calificacion = calificacionModificar.calificacion;

            //Marcar como modificado y enviar a la base de datos
            _calificacionesContext.Entry(calificacionActual).State = EntityState.Modified;
            _calificacionesContext.SaveChanges();

            return Ok(calificacionModificar);
        }

        /// <summary>
        /// EndPoint para eliminar un registro de calificacion
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id) 
        {
            //Obtener el registro original
            calificaciones? calificacion = (from c in _calificacionesContext.calificaciones
                                            where c.calificacionId == id
                                            select c).FirstOrDefault();

            //Verificar que exista el registro
            if(calificacion == null) return NotFound();

            //Ejecutar la eliminacion
            _calificacionesContext.calificaciones.Attach(calificacion);
            _calificacionesContext.calificaciones.Remove(calificacion);
            _calificacionesContext.SaveChanges();

            return Ok(calificacion);
        }

    }
}
