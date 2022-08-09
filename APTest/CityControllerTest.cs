using API.Automapper;
using API.Controllers;
using API.Model;
using AutoMapper;
using BAL.Abstract;
using Domain.CommonEntity;
using Domain.Dto;
using Domain.EntityModel;
using Domain.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace APTest
{
    public class CityControllerTest
    {
        private readonly CityController _controller;
        private readonly ICityService _service;
        public CityControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            _service = new CityServiceFake();
            _controller = new CityController(_service, mapper);
        }
        [Fact]
        public async void Get_WhenCalled_ReturnsOkResult()
        {
            //arrange
            var searchModel = new CitySearchModel()
            {
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(100),
                Page=1,
                PageSize=100                
            };

            // Act
            var okResult =await _controller.Get(searchModel);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public async void Get_WhenCalled_ReturnsAllItems()
        {
            //arrange
            var searchModel = new CitySearchModel()
            {
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(100),
                Page = 1,
                PageSize = 100
            };
            // Act
            var okResult =await _controller.Get(searchModel);
            //var okResult = _controller.Get(searchModel).GetAwaiter().GetResult(); ;
            // Assert
            // var items = Assert.IsType<List<CityDetailEntityModel>>(((DataResult<CityDetailEntityModel>)((ApiResponseModel)((ObjectResult)okResult).Value).Data).list);
            var items = Assert.IsType<List<CityDetailEntityModel>>(((DataResult<CityDetailEntityModel>)((ApiResponseModel)((ObjectResult)okResult).Value).Data).list);
            Assert.Equal(2, items.Count);
        }
        [Fact]
        public async void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult =await _controller.Get(3);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public async void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = 1;
            // Act
            var okResult =await _controller.Get(testGuid);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public async void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = 1;
            // Act
            var okResult =await _controller.Get(testGuid) as OkObjectResult;
            // Assert
            Assert.IsType<CityDetailEntityModel>(((CityDetailEntityModel)((ApiResponseModel)((ObjectResult)okResult).Value).Data));
            Assert.Equal(testGuid, ((CityDetailEntityModel)((ApiResponseModel)((ObjectResult)okResult).Value).Data).Id);
        }

        [Fact]
        public async void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var cityMissingItem = new CityModel()
            {
               
                Price = 10
            };
            _controller.ModelState.AddModelError("City", "Required");
            // Act
            var badResponse =await _controller.Post(cityMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public async void Post_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var city = new CityModel()
            {

                Id = 3,
                City = "London",
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(10),
                Price = 55.82,
                Status = "Seldom",
                color = "#fd4e19"
            };
            // Act
            var createdResponse =await _controller.Post(city);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public async void Post_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var city = new CityModel()
            {

                Id = 3,
                City = "London",
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(10),
                Price = 55.82,
                Status = "Seldom",
                color = "#fd4e19"
            };
            // Act
            var createdResponse = await _controller.Post(city) as CreatedAtActionResult;
            var item = createdResponse.Value as CityDetailEntityModel;
            // Assert
            Assert.IsType<CityDetailEntityModel>(item);
            Assert.Equal("London", item.City);
        }

        [Fact]
        public async void Delete_InvalidIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var notExistingGuid =0;
            // Act
            var badResponse =await _controller.Delete(notExistingGuid);
            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }
        [Fact]
        public async void Delete_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingGuid = 100;
            // Act
            var noContentResponse =await _controller.Delete(existingGuid);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        [Fact]
        public async void Delete_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var searchModel = new CityDetailSearch()
            {
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(100),
                Page = 1,
                PageSize = 100
            };
            var existingGuid = 1;
            // Act
            var okResponse =await _controller.Delete(existingGuid);
            // Assert
            var result = await _service.GetAllCityAsync(searchModel);
            Assert.Equal(1, result.list.Count);
        }

        [Fact]
        public async void Put_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var cityMissingItem = new CityModel()
            {

                Price = 10
            };
            _controller.ModelState.AddModelError("City", "Required");
            // Act
            var badResponse = await _controller.Put(1,cityMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public async void Put_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var city = new CityModel()
            {

                Id = 1,
                City = "Noida",
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(10),
                Price = 55.82,
                Status = "Seldom",
                color = "#fd4e19"
            };
            // Act
            var createdResponse = await _controller.Put(1, city); 
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public async void Put_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var city = new CityModel()
            {

                Id = 1,
                City = "Noida",
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(10),
                Price = 55.82,
                Status = "Seldom",
                color = "#fd4e19"
            };
            // Act
            var createdResponse = await _controller.Put(1,city) as CreatedAtActionResult;
            var item = createdResponse.Value as CityDetailEntityModel;
            // Assert
            Assert.IsType<CityDetailEntityModel>(item);
            Assert.Equal("Noida", item.City);
        }
    }
}