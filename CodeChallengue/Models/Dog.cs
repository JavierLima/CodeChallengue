using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CodeChallenge.Models
{
    /// <summary>
    /// Clase que simula el modelo de la estructura de un perro.
    /// <param name="Id">String único identificador de un perro.</param>
    /// <param name="Name">String con el nombre del perro.</param>
    /// <param name="Weight">Double con el peso del perro.</param>
    /// <param name="Age">Int con la edad del perro.</param>
    /// <param name="Photo">String que contiene una imagen aleatoria de un perro según la API de imágenes de perros.</param>
    /// </summary>
    public class Dog
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        public string? Photo { get; set; }

    }
}
