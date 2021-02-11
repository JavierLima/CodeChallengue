using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCodeChallenge
{
    public class BaseTest
    {
        internal const string _photoUrl = "https://www.thesprucepets.com/thmb/gs4SXkmCKH44qvve40sV9LPyQRY=/2578x2578/smart/filters:no_upscale()/AMRImage-E-GettyImages-171325224-56a26ba55f9b58b7d0ca0aa1.jpg";
        internal List<Dog> _dogList = new List<Dog>() 
                                      { 
                                            new Dog()
                                            {
                                                Id = "123456789",
                                                Name = "Yuma",
                                                Weight = 10.9,
                                                Age = 3,
                                                Photo = _photoUrl
                                            },
                                            new Dog()
                                            {
                                                Id = "1234567890",
                                                Name = "Pixi",
                                                Weight = 11.9,
                                                Age = 5,
                                                Photo = _photoUrl
                                            },
                                            new Dog()
                                            {
                                                Id = "0123456789",
                                                Name = "Tobi",
                                                Weight = 7.9,
                                                Age = 2,
                                                Photo = _photoUrl
                                            }
                                      };
    }
}
