using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    /// <summary>
    /// Clase que simula el modelo de la estructura json que dispone la API externa de perros para la obtención de la imagen.
    /// <param name="message">String que según la API de imágenes de perros contiene la URL con la imagen.</param>
    /// <param name="status">String que según la API de imágenes de perros contiene el estado de la operación al pedir una imagen random.</param>
    /// </summary>
    public class ImageStructure
    {
        public string message { get; set; }

        public string status { get; set; }
    }
}
