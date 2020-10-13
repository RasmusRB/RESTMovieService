using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using MovieLib.Model;
using RESTMovieService.Controllers;

namespace ControllerTests
{
    [TestClass]
    public class UnitTest1
    {
        private MoviesController controller = null;

        [TestInitialize]
        public void BeforeEachTest()
        {
            controller = new MoviesController();
        }

        [TestMethod]
        public void TestMoviesInListByGet()
        {
            // arrange
            // iniatialize before each test

            // act
            List<Movies> movies = new List<Movies>(controller.Get());

            // assert
            Assert.AreEqual(6, movies.Count);
        }
    }
}
