using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    /// <summary>
    /// Clase controlador encargada de la lógica de los métodos de petición HTTP REST(Get,Put,Delete,Post) para su tratamiento.
    /// /// <param name="_dogApiService">Servicio que se encarga de la generación de imágenes aleatorias.</param>
    /// /// <param name="_dogService">Servicio que se encarga del almacenamiento de los datos de los perros.</param>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {

        private readonly IDogApiService _dogApiService;
        private readonly IDogService _dogService;

        /// <summary>
        /// Constructor de la clase que setea los servicios necesarios para elaborar las peticiones.
        /// </summary>
        public DogController(IDogApiService dogApiService, IDogService dogService)
        {
            _dogApiService = dogApiService;
            _dogService = dogService;
        }

        /// <summary>
        /// Función que se encarga de obtener todos los perros que se encuentran en la aplicación durante el tiempo de ejecución.
        /// </summary>
        /// <returns>Devuelve un Status200OK y una lista con todos los perros que se encuentren en ella</returns>
        /// <exception cref="Exception">En el caso de que la comunicación con el servicio de almacenamiento de perros 
        /// deje de funcionar se tomará en cuenta un Status500InternalServerError.</exception>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Dog>> Get()
        {
            try
            {
                return _dogService.GetDogs();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
        }
        /// <summary>
        /// Función que se encarga de obtener un perro según su id.
        /// <param name="id">String con el id del perro.</param>
        /// </summary>
        /// <returns>Devuelve un Status200OK si todo ha ido correctamente y el perro a buscar o las excepciones que puedan llegar a ocurrir</returns>
        /// <exception cref="Status400BadRequest">En el caso de que el id introducido no sea correcto(sea null o vacío) se tendrá
        /// en cuenta que el error ha sido del usuario por tanto se lanza un Bad Request(petición errónea).</exception>
        /// <exception cref="Status404NotFound">En el caso de que el id introducido no pertenezca a ningún perro se tomará
        /// el error como no encontrado, es decir Status404NotFound.</exception>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Dog> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            try
            {
                return _dogService.GetDog(id);
            }
            catch (Exception)
            {
                return StatusCode(404);
            }

        }
        /// <summary>
        /// Función que se encarga de guardar un perro según los datos del perro introducidos. Además cada vez que se cree un 
        /// perro se le añadirá una foto aleatoria gracias a la Api externa.
        /// <param name="dog">Una estructura perro a guardar.</param>
        /// </summary>
        /// <returns>Devuelve un Status201Created y el perro creado o las excepciones que puedan llegar a ocurrir</returns>
        /// <exception cref="Status400BadRequest">En el caso de que algún parámetro del perro introducido sea nulo o no coherente (peso o edad negativos)
        /// se tendrá en cuenta que el error ha sido del usuario por tanto se lanza un Bad Request(petición errónea).</exception>
        /// <exception cref="Status500InternalServerError">En el caso de que no sea posible crear un perro se generará un 
        /// Status500InternalServerError teniendo en cuenta que el error es del servidor.</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Dog> Post(Dog dog)
        {
            if (dog == null || string.IsNullOrEmpty(dog.Name) || dog.Weight < 0 || dog.Age < 0)
            {
                return BadRequest();
            }

            dog.Photo = _dogApiService.GetImage();
            try
            {
                _dogService.PostDog(dog);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Created(nameof(Post), dog);

        }
        /// <summary>
        /// Función que se encarga de eliminar un perro según el id introducido por el usuario.
        /// <param name="id">String con el id del perro a eliminar.</param>
        /// </summary>
        /// <returns>Devuelve un Status200OK y el perro eliminado o las excepciones que puedan llegar a ocurrir</returns>
        /// <exception cref="Status400BadRequest">En el caso de que el id introducido no sea correcto(sea null o vacío) se tendrá
        /// en cuenta que el error ha sido del usuario por tanto se lanza un Bad Request(petición errónea).</exception>
        /// <exception cref="Status404NotFound">En el caso de que el id introducido no pertenezca a ningún perro se tomará
        /// el error como no encontrado, es decir Status404NotFound.</exception>
        /// <exception cref="Status500InternalServerError">En el caso de que no sea posible eliminar un perro se generará un 
        /// Status500InternalServerError teniendo en cuenta que el error es del servidor, ya sea porque el id introducido 
        /// esté duplicado u otro tipo de error.</exception>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Dog> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            try
            {
                var dogToDelete = Get(id);

                if (dogToDelete.Value == null)
                    return StatusCode(404);
                else
                {
                    _dogService.DeleteDog(dogToDelete.Value);
                    return dogToDelete;
                }
                    
            }
            catch (Exception) //Se ha encontrado dos perros con el mismo ID, no debería pasar por la lógica de generación de ID, pero se contempla el posible error
            {
                return StatusCode(500);
            }

        }
        /// <summary>
        /// Función que se encarga de modificar los parámetros de un perro existente.
        /// <param name="dog">Una estructura perro a guardar, dog.Id contiene el id del perro a buscar para su posterior modificación.</param>
        /// </summary>
        /// <returns>Devuelve un Status200OK y el perro modificado o las excepciones que puedan llegar a ocurrir</returns>
        /// <exception cref="Status400BadRequest">En el caso de que el id introducido no sea correcto(sea null o vacío) se tendrá
        /// en cuenta que el error ha sido del usuario por tanto se lanza un Bad Request(petición errónea).</exception>
        /// <exception cref="Status404NotFound">En el caso de que el id introducido no pertenezca a ningún perro se tomará
        /// el error como no encontrado, es decir Status404NotFound.</exception>
        /// <exception cref="Status500InternalServerError">En el caso de que no sea posible modificar un perro se generará un 
        /// Status500InternalServerError teniendo en cuenta que el error es del servidor, ya sea porque el id introducido 
        /// esté duplicado u otro tipo de error.</exception>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Dog> Put(Dog dog)
        {
            if (string.IsNullOrEmpty(dog.Id) || dog.Age < 0 || string.IsNullOrEmpty(dog.Name) || dog.Weight < 0 || string.IsNullOrEmpty(dog.Photo))
            {
                return BadRequest();
            }
            try
            {
                var dogToModify = _dogService.GetDog(dog.Id);
            }
            catch(Exception)
            {
                return StatusCode(404);
            }
            try
            {
                _dogService.PutDog(dog);
                return dog;
            }
            catch (Exception) //Se ha encontrado dos perros con el mismo ID, no debería pasar por la lógica de generación de ID, pero se contempla el posible error
            {
                return StatusCode(500);
            }
        }
    }
}
