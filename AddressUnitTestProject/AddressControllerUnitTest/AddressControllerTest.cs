using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Titan.Common.Diagnostics.State;
using Titan.UFC.Addresses.API.Controllers;
using Titan.UFC.Addresses.API.Entities;
using Titan.UFC.Addresses.API.Models;
using Titan.UFC.Addresses.API.Resources;
using Titan.UFC.Addresses.API.Service;
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
            AddressID = 1,
            AddressUID = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            Address1 = "Address Test Line 1",
            Address2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            PinCode = "560103",
            StateCode = "29",
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsVerified = 1,
            MailingAddressName = "TestName",
            CreatedDate = DateTime.Parse("05/08/2019"),
            UpdatedDate = DateTime.Parse("06/08/2019"),
            IsPrimary = true,
            StateID = 29,
            Type = 1
        };

        public static AddressEntity addressEntityMockData => new AddressEntity
        {
            AddressID = 1,
            AddressUID = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            Address1 = "Address Test Line 1",
            Address2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            PinCode = "560103",
            StateID = 29,
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsVerified = 1,
            MailingAddressName = "TestName",
            CreatedDate = DateTime.Parse("05/08/2019"),
            UpdatedDate = DateTime.Parse("06/08/2019"),
            isPrimary = true,
            Type = 1
        };
        public class Get : AddressControllerTest
        {
            [Fact]
            public async void should_return_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.GetAddressById(expectedAddressObj.AddressUID.Value.ToString())).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Get(expectedAddressObj.AddressUID.Value.ToString());
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
                addressServiceMock.Setup(s => s.UpdateAddress(expectedAddressObj.AddressUID.Value.ToString(), addressMockData)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Put(expectedAddressObj.AddressUID.Value.ToString(), addressMockData);
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
                addressServiceMock.Setup(s => s.DeleteAddress(expectedAddressObj.AddressUID.Value.ToString())).Returns(Task.FromResult(expectedAddressObj.AddressID));
                var result = await addressControllerUnderTest.Delete(expectedAddressObj.AddressUID.Value.ToString());
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
