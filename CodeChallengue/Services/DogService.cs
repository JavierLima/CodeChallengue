using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CodeChallenge.Services
{
    /// <summary>
    /// Interfaz encargada del servicio de funcionalidades que debe disponer el almacenamiento de los perros
    /// </summary>
    public interface IDogService
    {
        List<Dog> GetDogs();
        Dog GetDog(string id);
        Dog PostDog(Dog dog);
        Dog PutDog(Dog dog);
        bool DeleteDog(Dog dogToRemove);

    }
    /// <summary>
    /// Clase encargada de la gestión del almacenamiento de los perros. Hereda de la interfaz IDogService.
    /// </summary>
    /// <remarks>
    /// Esta clase se encarga de añadir, eliminar, modificar y obtener perros.
    /// </remarks>
    /// <param name="_dogs"> Una lista con los perros.</param>
    public class DogService : IDogService
    {

        private List<Dog> _dogs;

        /// <summary>
        /// Constructor de la clase que crea una lista vacía de perros
        /// </summary>
        public DogService()
        {
            _dogs = new List<Dog>();
        }

        /// <summary>
        /// Función que se encarga de eliminar un perro de la lista
        /// <param name="dogToRemove"> El perro que se desea eliminar.</param>
        /// </summary>
        /// <returns>Devuelve un booleano diciendo si se ha eliminado "True" o no se podido eliminar "False" un perro</returns>
        public bool DeleteDog(Dog dogToRemove)
        {
            if (dogToRemove == null)
            {
                throw new Exception("El perro introducido no es válido");
            }
            if(_dogs.Count == 0)
            {
                throw new Exception("No se ha podido encontrar al perro");
            }
            try
            {
                var checkRemoved = _dogs.Remove(dogToRemove);
                if (checkRemoved)
                    return true;
                else
                    throw new Exception("El perro no se ha podido eliminar");
            }
            catch (Exception)
            {
                throw new Exception("El perro no se ha podido eliminar");
            }

        }
        /// <summary>
        /// Función que se encarga de obtener un perro según el id que se ofrezca.
        /// <param name="id"> Un string que contiene el id del perro a buscar.</param>
        /// </summary>
        /// <returns>Devuelve una estructura perro una vez encontrado.</returns>
        public Dog GetDog(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("El id introducido no es válido");
            }
            try
            {
                var dogSearched = _dogs.Find(x => x.Id.Equals(id));

                if (dogSearched != null)
                    return dogSearched;
                else
                    throw new Exception("El id introducido no pertenece a ningún perro");
            }
            catch(Exception)
            {
                throw new Exception("El id introducido pertenece a 2 perros distintos");
            }
        }

        /// <summary>
        /// Función que se encarga de obtener todos los perros que se encuentren en la lista.
        /// </summary>
        /// <returns>Devuelve la lista de perros que contiene la clase.</returns>
        public List<Dog> GetDogs()
        {
            return _dogs;
        }

        /// <summary>
        /// Función que se encarga de introducir un perro en la lista según el perro que se le pase a la función
        /// <param name="dog"> Un perro a introducir en la lista.</param>
        /// </summary>
        /// <returns>Devuelve una estructura perro una vez añadido.</returns>
        public Dog PostDog(Dog dog)
        {
            if (dog == null || string.IsNullOrEmpty(dog.Name) || dog.Weight < 0 || dog.Age < 0)
                throw new Exception("Los parámetros introducidos no son válidos");

            if (_dogs.Contains(dog))
                throw new Exception("El perro ya se encuentra posteado");

            var rng = new Random();
            int randomId = rng.Next();
            string id = randomId.ToString() + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss").Replace("-",string.Empty);
            dog.Id = id;
            _dogs.Add(dog);

            return dog;
        }

        /// <summary>
        /// Función que se encarga de modificar los datos de un perro, según el id que disponga el perro se buscará en la lista para su posterior modificación.
        /// <param name="dog"> Un perro modificado donde su id se encuentra en la lista para poder intercambiarlo.</param>
        /// </summary>
        /// <returns>Devuelve una estructura perro una vez modificado.</returns>
        public Dog PutDog(Dog dog)
        {
            if (dog == null || string.IsNullOrEmpty(dog.Name) || dog.Weight < 0 || dog.Age < 0)
            {
                throw new Exception("Los parámetros introducidos no son válidos");
            }
            var indexToModify = _dogs.FindIndex(x => x.Id == dog.Id);

            if (indexToModify < 0)
                throw new Exception("El perro no se ha podido identificar");

            _dogs[indexToModify] = dog;

            return dog;
        }
    }
}
