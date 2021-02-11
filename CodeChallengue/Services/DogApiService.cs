using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Interfaz encargada del servicio de funcionalidades que debe disponer la obtención de imágenes de perros.
    /// </summary>
    public interface IDogApiService
    {
        string GetImage();
    }
    /// <summary>
    /// Clase encargada de la gestión para la obtención de una imagen aleatoria de un perro desde una API externa. 
    /// Hereda de la interfaz IDogApiService.
    /// </summary>
    /// <remarks>
    /// Esta clase se encarga solo de obtener imágenes de perros.
    /// </remarks>
    /// <param name="_url">String con la URL de la API donde hacer las peticiones, dicha URL se encuentra en el archivo appsettings.json.</param>
    public class DogApiService : IDogApiService
    {
        private readonly string _url;

        /// <summary>
        /// Constructor de la clase que setea la URL según el parámetro introducido en el archivo appsettings.json en el valor ApiDogURL.
        /// <param name="configuration">Configuración que nos permite acceder al archivo appsettings.json.</param>
        /// </summary>
        public DogApiService(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("ApiDogURL");
        }

        /// <summary>
        /// Función que se encarga de obtener una imagen de un perro aleatoria, gracias a la API (Dog Api).
        /// </summary>
        /// <returns>Un string que representa la imagen de perro</returns>
        /// <exception cref="Exception">En el caso de que la API no esté funcionando se devolverá una imagen de un perro triste.</exception>
        public string GetImage()
        {
            try
            {
                var client = new WebClient();
                var stringJson = client.DownloadString(_url);
                var json = JsonSerializer.Deserialize<ImageStructure>(stringJson);
                return json.message;
            }
            catch (Exception)
            {
                //Introducimos una imagen por defecto por si la API de perros está caída
                return "https://www.thesprucepets.com/thmb/gs4SXkmCKH44qvve40sV9LPyQRY=/2578x2578/smart/filters:no_upscale()/AMRImage-E-GettyImages-171325224-56a26ba55f9b58b7d0ca0aa1.jpg";
            } 
        }
    }
}
