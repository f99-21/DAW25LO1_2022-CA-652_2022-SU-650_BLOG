using LO1_2022_CA_652_2022_SU_650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LO1_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {

        private readonly blogContext _comentariosContext;

        public comentariosController(blogContext comentariosContext)
        {
            _comentariosContext = comentariosContext;
        }

        // devuelve lista de cometarios 
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listaComentario = (from c in _comentariosContext.comentarios
                                                 select c).ToList();

            if (listaComentario.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaComentario);

        }

        // buscar comenatarios por id
        [HttpGet]
        [Route("GetByID/{id}")]
        public IActionResult Get(int id)
        {
            comentarios? comentarios = (from c in _comentariosContext.comentarios
                                        where c.cometarioId == id
                                        select c).FirstOrDefault();

            if (comentarios == null)
            {
                return NotFound();
            }

            return Ok(comentarios);

        }

        //agregar comentarios
        [HttpPost]
        [Route("Agregar")]
        public IActionResult GuardaUsuario([FromBody] comentarios comentarios)
        {
            try
            {
                _comentariosContext.comentarios.Add(comentarios);
                _comentariosContext.SaveChanges();
                return Ok(comentarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        // modifica registros de comenatarios
        [HttpPut]
        [Route("Modificar/{id}")]
        public IActionResult ModificarComenatrio(int id, [FromBody] comentarios comenatarioModificar)
        {
            comentarios? comentarioActul = (from c in _comentariosContext.comentarios
                                    where c.cometarioId == id
                                    select c).FirstOrDefault();

            if (comentarioActul == null) { return NotFound(); }

            comentarioActul.publicacionId = comenatarioModificar.publicacionId;
            comentarioActul.comentario = comenatarioModificar.comentario;
            comentarioActul.usuarioId = comenatarioModificar.usuarioId;

            _comentariosContext.Entry(comentarioActul).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _comentariosContext.SaveChanges();

            return Ok (comenatarioModificar);
        }

        //eliminar registro
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            comentarios? comentario = (from c in _comentariosContext.comentarios
                                 where c.cometarioId ==id
                                 select c).FirstOrDefault();

            if (comentario == null) return NotFound();

            _comentariosContext.comentarios.Attach(comentario);
            _comentariosContext.Remove(comentario);
            _comentariosContext.SaveChanges();

            return Ok (comentario);


        }

        /// <summary>
        /// EndPoint para filtrar comentarios por un usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUsuario/{usuarioId}")]
        public IActionResult GetByUsuario(int usuarioId) 
        {
            var comentariosDeUsuario = (from u in _comentariosContext.usuarios
                                        where u.usuarioId == usuarioId
                                        select new
                                        {
                                            u.usuarioId,
                                            Comentarios = (from c in _comentariosContext.comentarios
                                                           where c.usuarioId == u.usuarioId
                                                           select c.comentario).ToList()
                                        }).FirstOrDefault();

            if (comentariosDeUsuario == null) return NotFound();

            return Ok(comentariosDeUsuario);
        }

    }
}
