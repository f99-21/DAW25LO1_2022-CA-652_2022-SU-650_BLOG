using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LO1_2022_CA_652_2022_SU_650.Models;
using Microsoft.EntityFrameworkCore;

namespace LO1_2022_CA_652_2022_SU_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly blogContext _usuariosContxt;

        public UsuariosController (blogContext usuariosContxt)
        {
            _usuariosContxt = usuariosContxt;
        }

        //endpoin que retorna el lisado de todos lo existente

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listaUsuaro = (from u in _usuariosContxt.usuarios
                                          select u).ToList();

            if (listaUsuaro.Count == 0)
            {
                return NotFound();
            }
            return Ok (listaUsuaro);

        }

        [HttpGet]
        [Route("GetByID/{id}")]

        public IActionResult Get(int id)
        {
            usuarios? usuario = (from u in _usuariosContxt.usuarios
                                 where u.usuarioId == id
                                 select u).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok (usuario);

        }

        [HttpPost]
        [Route("Agregar")]

        public IActionResult GuardaUsuario([FromBody]usuarios usuarios)
        {
            try
            {
                _usuariosContxt.usuarios.Add(usuarios);
                _usuariosContxt.SaveChanges();
                return Ok(usuarios);

            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            
            }


        }

        [HttpPut]
        [Route ("modificar")]

        public IActionResult Modificar (int id , [FromBody]usuarios usuarioModificado)
            {

                usuarios? usuarioActual = (from u in _usuariosContxt.usuarios
                                          where u.usuarioId == id
                                          select u).FirstOrDefault();
                if (usuarioActual == null)
                { return NotFound(); }

                //sie encuentra registro se modifican los campos

               
                usuarioActual.nombreUsuario= usuarioModificado.nombreUsuario;
                usuarioActual.clave = usuarioModificado.clave;
                usuarioActual.nombre = usuarioModificado.nombre;
                usuarioActual.apellido = usuarioModificado.apellido;

                _usuariosContxt.Entry(usuarioActual).State = EntityState.Modified;
                _usuariosContxt.SaveChanges();

                return Ok (usuarioModificado);
            }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public IActionResult Eliminar(int id) 
        {
            usuarios? usuario = (from u in _usuariosContxt.usuarios
                                 where u.usuarioId == id
                                 select u).FirstOrDefault();

            if (usuario == null) return NotFound();

            _usuariosContxt.usuarios.Attach(usuario);
            _usuariosContxt.usuarios.Remove(usuario);
            _usuariosContxt.SaveChanges ();
            return NoContent ();
         
                    

                

                


        }
    }
}
