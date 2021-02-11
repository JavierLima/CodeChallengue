using CodeChallenge.Models;
using CodeChallenge.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCodeChallenge
{
    class TestDogService: BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPostDog()
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = 12.3, Age = 3, Photo = _photoUrl };
            var result = dogService.PostDog(dog);

            Assert.AreEqual(dog, result);
        }

        [Test]
        public void TestPostNullDog()
        {
            var dogService = new DogService();

            Assert.Throws<Exception>(() => dogService.PostDog(null));
        }
        
        [TestCase(null)]
        [TestCase("")]
        public void TestPostDogNullOrEmptyName(string name)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = name, Weight = 12.3, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PostDog(dog));
        }

        [TestCase(-4.3)]
        public void TestPostDogInvalidWeight(double weight)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = weight, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PostDog(dog));
        }

        [TestCase(-4)]
        public void TestPostDogInvalidAge(int age)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = 12.3, Age = age, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PostDog(dog));
        }

        [Test]
        public void TestPostDuplicateDog()
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = 12.3, Age = 4, Photo = _photoUrl };
            dogService.PostDog(dog);

            Assert.Throws<Exception>(() => dogService.PostDog(dog));
        }

        [Test]
        public void TestGetAllDogs()
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[0]);
            dogService.PostDog(_dogList[1]);
            dogService.PostDog(_dogList[2]);
            var result = dogService.GetDogs();

            Assert.AreEqual(_dogList, result);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestGetDog(int index)
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[index]);
            var result = dogService.GetDog(_dogList[index].Id);

            Assert.AreEqual(_dogList[index], result);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestGetDogNullOrEmptyId(string id)
        {
            var dogService = new DogService();

            Assert.Throws<Exception>(() => dogService.GetDog(id));
        }

        [TestCase("           ", 0)]
        [TestCase("2222", 1)]
        [TestCase("???", 2)]
        public void TestGetDogFalseId(string id, int index)
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[index]);

            Assert.Throws<Exception>(() => dogService.GetDog(id));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestPutDog(int index)
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[index]);

            var dog = new Dog() { Id = _dogList[index].Id, Name = "Pixi", Weight = 11.4, Age = 5, Photo = _photoUrl };
            var result = dogService.PutDog(dog);

            Assert.AreEqual(dog, result);
        }

        [Test]
        public void TestPutNullDog()
        {
            var dogService = new DogService();

            Assert.Throws<Exception>(() => dogService.PutDog(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestPutDogNullOrEmptyName(string name)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = name, Weight = 12.3, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PutDog(dog));
        }

        [TestCase(-4.3)]
        public void TestPutDogInvalidWeight(double weight)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = weight, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PutDog(dog));
        }

        [TestCase(-4)]
        public void TestPutDogInvalidAge(int age)
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "22222", Name = "Yuma", Weight = 12.3, Age = age, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PutDog(dog));
        }

        [TestCase("           ", 0)]
        [TestCase("2222", 1)]
        [TestCase("???", 2)]
        public void TestPutDogFalseId(string id, int index)
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[index]);

            var dog = new Dog() { Id = id, Name = "Yuma", Weight = 12.3, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.PutDog(dog));
        }

        [Test]
        public void TestDeleteDog()
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "222", Name = "Yuma", Weight = 12.3, Age = 3, Photo = _photoUrl };
            dogService.PostDog(dog);

            var result = dogService.DeleteDog(dog);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestDeleteNullDog()
        {
            var dogService = new DogService();

            Assert.Throws<Exception>(() => dogService.DeleteDog(null));
        }

        [Test]
        public void TestDeleteDogWithoutDogsPosted()
        {
            var dogService = new DogService();
            var dog = new Dog() { Id = "222", Name = "Yuma", Weight = 12.3, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.DeleteDog(dog));
        }

        [TestCase(0, "123")]
        [TestCase(1, "1234")]
        [TestCase(2, "133")]
        public void TestDeleteDogFalseDog(int index, string id)
        {
            var dogService = new DogService();
            dogService.PostDog(_dogList[index]);

            var dog = new Dog() { Id = id, Name = "Yuma", Weight = 12.3, Age = 3, Photo = _photoUrl };

            Assert.Throws<Exception>(() => dogService.DeleteDog(dog));
        }
    }
}
