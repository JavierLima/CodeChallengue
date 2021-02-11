using CodeChallenge.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestCodeChallenge
{
    public class TestPingController
    {

        [Test]
        public void TestPingGetPong()
        {
            var pingController = new PingController();
            var result = pingController.Get();

            Assert.AreEqual("Pong", result.Value);
        }
    }
}
