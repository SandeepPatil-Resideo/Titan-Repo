using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Titan.Common.Diagnostics.State;
using Titan.Ufc.Addresses.API.Controllers;
using Titan.Ufc.Addresses.API.Entities;
using Titan.Ufc.Addresses.API.Models;
using Titan.Ufc.Addresses.API.Resources;
using Titan.Ufc.Addresses.API.Service;
using Titan.UFC.Common.ExceptionMiddleWare;
using Xunit;

namespace AddressUnitTestProject.AddressControllerUnitTest
{
    public class AddressControllerTest
    {
        protected Mock<IStateObserver> stateObserverMock { get; }
        protected AddressController addressControllerUnderTest { get; }
        protected Mock<ILogger<AddressController>> LoggerMock { get; }
        protected Mock<IAddressService> addressServiceMock { get; }
        protected Mock<ISharedResource> LocalizerMock { get; }
        protected IMapper mapperMock { get; }
        protected AddressContext addressContextInMemory { get; }
        public AddressControllerTest()
        {
            addressContextInMemory = GetAddressContextInMemory();
            stateObserverMock = new Mock<IStateObserver>();
            LoggerMock = new Mock<ILogger<AddressController>>();
            addressServiceMock = new Mock<IAddressService>();
            LocalizerMock = new Mock<ISharedResource>();
            addressControllerUnderTest = new AddressController(addressServiceMock.Object, LocalizerMock.Object, stateObserverMock.Object, mapperMock, addressContextInMemory);
        }
        private static AddressContext GetAddressContextInMemory()
        {
            var options = new DbContextOptionsBuilder<AddressContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new AddressContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            AddressEntity addressEntity = addressEntityMockData;
            context.Add<AddressEntity>(addressEntity);
            context.SaveChanges();

            return context;
        }
        public static Address addressMockData => new Address
        {
            Id = 1,
            AddressId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            AddressLine1 = "Address Test Line 1",
            AddressLine2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            ZipPostalCode = "560103",
            StateProvinceRegion = "29",
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsValidated = true,
            MailingAddressName = "TestName"
        };

        public static AddressEntity addressEntityMockData => new AddressEntity
        {
            ID = 1,
            AddressId = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            AddressLine1 = "Address Test Line 1",
            AddressLine2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            ZipPostalCode = "560103",
            StateProvinceRegion = "29",
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsValidated = true,
            CreatedOn = DateTime.Parse("05/08/2019"),
            UpdatedOn = DateTime.Parse("06/08/2019"),
            MailingAddressName = "TestName"
        };
        public class Get : AddressControllerTest
        {
            [Fact]
            public async void should_return_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.GetAddressById(expectedAddressObj.AddressId.ToString())).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Get(expectedAddressObj.AddressId.ToString());
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, okResult.StatusCode);
            }
            [Fact]
            public async void should_return_null_if_addressId_does_not_exists()
            {
                string invalidAddressId = null;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.GetAddressById(invalidAddressId)).Throws(new TitanCustomException(StatusCodes.Status404NotFound, "Address Not Found"));
                try
                {
                    var result = await addressControllerUnderTest.Get(invalidAddressId);
                }
                catch(TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }
            }
        }

        public class Put : AddressControllerTest
        {
            [Fact]
            public async void should_update_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.UpdateAddress(expectedAddressObj.AddressId.ToString(), addressMockData)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Put(expectedAddressObj.AddressId.ToString(), addressMockData);
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, okResult.StatusCode);
            }

            [Fact]
            public async void should_return_null_for_invalid_addressId()
            {
                var expectedAddressObj = addressMockData;
                string invalidAddressId = null;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.UpdateAddress(invalidAddressId, expectedAddressObj)).Throws(new TitanCustomException(StatusCodes.Status404NotFound, "Address Doesn't Exists"));
                try
                {
                    var result = await addressControllerUnderTest.Put(invalidAddressId, expectedAddressObj);
                }
                catch(TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }
            }

            [Fact]
            public async void should_return_null_for_invalid_address()
            {
                Address expectedAddressObj = null;
                string invalidAddressId = "9245fe4a-d402-451c-b9ed-9c1a04247482";
                var LoggerMock = new Mock<ILogger<AddressController>>();
                //addressServiceMock.Setup(s => s.UpdateAddress(invalidAddressId, expectedAddressObj)).Throws(new ArgumentNullException("Address is Null"));
                try
                {
                    var result = await addressControllerUnderTest.Put(invalidAddressId, expectedAddressObj);
                }
                catch (ArgumentNullException argumentNullException)
                {
                    Assert.IsType<ArgumentNullException>(argumentNullException);
                }
            }
        }

        public class Delete : AddressControllerTest
        {
            [Fact]
            public async void should_delete_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.DeleteAddress(expectedAddressObj.AddressId.ToString())).Returns(Task.FromResult(expectedAddressObj.Id));
                var result = await addressControllerUnderTest.Delete(expectedAddressObj.AddressId.ToString());
                var okResult = Assert.IsType<NoContentResult>(result);
                Assert.Equal(204, okResult.StatusCode);
            }
            [Fact]
            public async void should_return_exception__if_addressId_does_not_exists()
            {
                string invalidAddressId = null;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.DeleteAddress(invalidAddressId)).Throws(new TitanCustomException(StatusCodes.Status404NotFound, "Address Doesn't Exists"));
                try
                {
                    var result = await addressControllerUnderTest.Delete(invalidAddressId);
                }
                catch (TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }
            }
        }

        public class Post : AddressControllerTest
        {
            [Fact]
            public async void should_create_if_address_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.CreateAddress(expectedAddressObj)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Post(expectedAddressObj);
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, okResult.StatusCode);
            }

            [Fact]
            public async void should_return_null_if_address_does_not_exists()
            {
                Address expectedAddressObj = null;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                //addressServiceMock.Setup(s => s.CreateAddress(expectedAddressObj)).Returns(Task.FromResult(expectedAddressObj));
                try
                {
                    var result = await addressControllerUnderTest.Post(expectedAddressObj);
                }
                catch (ArgumentNullException argumentNullException)
                {
                    Assert.IsType<ArgumentNullException>(argumentNullException);
                }
            }
        }
    }
}
