using CodeChallenge.Controllers;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NUnitTestCodeChallenge
{
    public class TestDogController: BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetAllDogs()
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDogs()).Returns(_dogList);

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Get();

            Assert.AreEqual(_dogList, result.Value);
        }

        [Test]
        public void TestGetAllDogsFail()
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDogs()).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Get();

            Assert.AreEqual(500, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);

        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestGetOneDog(int index)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(_dogList[index].Id)).Returns(_dogList[index]);

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Get(_dogList[index].Id);

            Assert.AreEqual(_dogList[index], result.Value);
        }

        [TestCase(null, 400)]
        [TestCase("", 400)]
        [TestCase("                  ", 404)]
        [TestCase("123", 404)]
        public void TestGetOneDogFail(string id, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(id)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Get(id);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestPostDog(int index)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.PostDog(_dogList[index])).Returns(_dogList[index]);

            var dogApiServiceMock = new Mock<IDogApiService>();
            dogApiServiceMock.Setup(item => item.GetImage()).Returns(_photoUrl);

            var dogController = new DogController(dogApiServiceMock.Object, dogServiceMock.Object);
            var result = dogController.Post(_dogList[index]);

            Assert.AreEqual(201, ((CreatedResult)result.Result).StatusCode);
            Assert.AreEqual(_dogList[index], ((CreatedResult)result.Result).Value);
        }

        [TestCase(null, 400)]
        public void TestPostNullDog(Dog dog, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.PostDog(dog)).Throws(new Exception());

            var dogApiServiceMock = new Mock<IDogApiService>();
            dogApiServiceMock.Setup(item => item.GetImage()).Returns(_photoUrl);

            var dogController = new DogController(dogApiServiceMock.Object, dogServiceMock.Object);
            var result = dogController.Post(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(null, 400)]
        [TestCase("", 400)]
        public void TestPostDogNameFail(string name, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = null, Name = name, Weight = 2.4, Age = 2, Photo = null };
            dogServiceMock.Setup(item => item.PostDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Post(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(-4.5, 400)]
        public void TestPostDogWeightFail(double weight, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = null, Name = "Yuma", Weight = weight, Age = 2, Photo = null };
            dogServiceMock.Setup(item => item.PostDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Post(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(-3, 400)]
        public void TestPostDogAgeFail(int age, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = null, Name = "Yuma", Weight = 3.5, Age = age, Photo = null };
            dogServiceMock.Setup(item => item.PostDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Post(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0, 500)]
        [TestCase(1, 500)]
        [TestCase(2, 500)]
        public void TestPostDogInternalServerError(int index, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.PostDog(_dogList[index])).Throws(new Exception());

            var dogApiServiceMock = new Mock<IDogApiService>();
            dogApiServiceMock.Setup(item => item.GetImage()).Returns(_photoUrl);

            var dogController = new DogController(dogApiServiceMock.Object, dogServiceMock.Object);
            var result = dogController.Post(_dogList[index]);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestDeleteOneDog(int index)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(_dogList[index].Id)).Returns(_dogList[index]);
            dogServiceMock.Setup(item => item.DeleteDog(_dogList[index])).Returns(true);

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Delete(_dogList[index].Id);

            Assert.AreEqual(_dogList[index], result.Value);
        }

        [TestCase(null, 400)]
        [TestCase("", 400)]
        public void TestDeleteDogBadRequest(string id, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = id, Name = "Yuma", Weight = 3.5, Age = 3, Photo = null };
            dogServiceMock.Setup(item => item.DeleteDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Delete(id);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase("           ", 404)]
        [TestCase("123", 404)]
        public void TestDeleteDogThrowsExceptionByGet(string id, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(id)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Delete(id);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0, 500)]
        [TestCase(1, 500)]
        [TestCase(2, 500)]
        public void TestDeleteDogThrowsExceptionByDelete(int index, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(_dogList[index].Id)).Returns(_dogList[index]);
            dogServiceMock.Setup(item => item.DeleteDog(_dogList[index])).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Delete(_dogList[index].Id);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestPutOneDog(int index)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(_dogList[index].Id)).Returns(_dogList[index]);
            dogServiceMock.Setup(item => item.PutDog(_dogList[index])).Returns(_dogList[index]);

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(_dogList[index]);

            Assert.AreEqual(_dogList[index], result.Value);
        }

        [TestCase(null, 400)]
        [TestCase("", 400)]
        public void TestPutDogBadRequestById(string id, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = id, Name = "Yuma", Weight = 3.5, Age = 3, Photo = null };
            dogServiceMock.Setup(item => item.PutDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(null, 400)]
        [TestCase("", 400)]
        public void TestPutDogBadRequestByName(string name, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = "", Name = name, Weight = 3.5, Age = 3, Photo = null };
            dogServiceMock.Setup(item => item.PutDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(-12.2, 400)]
        public void TestPutDogBadRequestByWeight(double weight, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = "", Name = "Yuma", Weight = weight, Age = 3, Photo = null };
            dogServiceMock.Setup(item => item.PutDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(-1, 400)]
        public void TestPutDogBadRequestByAge(int age, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = "", Name = "Yuma", Weight = 12.3, Age = age, Photo = null };
            dogServiceMock.Setup(item => item.PutDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        //Falta modificar el test, no debería poder meter una URL no existente
        [TestCase(null, 400)]
        [TestCase("", 400)]
        [TestCase("Hola", 400)]
        public void TestPutDogBadRequestByPhotoURL(string url, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = "", Name = "Yuma", Weight = 3.5, Age = 3, Photo = url };
            dogServiceMock.Setup(item => item.PutDog(dog)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase("           ", 404)]
        [TestCase("123", 404)]
        public void TestPutDogThrowsExceptionByGet(string id, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            var dog = new Dog() { Id = id, Name = "Yuma", Weight = 3.5, Age = 3, Photo = _photoUrl };
            dogServiceMock.Setup(item => item.GetDog(id)).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(dog);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

        [TestCase(0, 500)]
        [TestCase(1, 500)]
        [TestCase(2, 500)]
        public void TestPutDogThrowsExceptionByPut(int index, int statusCodeExpected)
        {
            var dogServiceMock = new Mock<IDogService>();
            dogServiceMock.Setup(item => item.GetDog(_dogList[index].Id)).Returns(_dogList[index]);
            dogServiceMock.Setup(item => item.PutDog(_dogList[index])).Throws(new Exception());

            var dogController = new DogController(null, dogServiceMock.Object);
            var result = dogController.Put(_dogList[index]);

            Assert.AreEqual(statusCodeExpected, ((StatusCodeResult)result.Result).StatusCode);
            Assert.AreEqual(null, result.Value);
        }

    }
}