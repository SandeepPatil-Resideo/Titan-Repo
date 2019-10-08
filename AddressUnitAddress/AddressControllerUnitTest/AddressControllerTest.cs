using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanTemplate.titanaddressapi.Controllers;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.LocalizationResource;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.Service;
using Xunit;

namespace AddressUnitAddress.AddressControllerUnitTest
{
    class AddressControllerTest
    {
        protected Mock<IStateObserver> stateObserverMock { get; }
        protected AddressController addressControllerUnderTest { get; }
        protected Mock<ILogger<AddressController>> LoggerMock { get; }
        protected Mock<IAddressService> addressServiceMock { get; }
        protected Mock<IStringLocalizer<SharedResource>> LocalizerMock { get; }
        protected IMapper mapperMock { get; }
        protected AddressContext addressContextInMemory { get;  }
        public AddressControllerTest()
        {
            addressContextInMemory = GetAddressContextInMemory();
            stateObserverMock = new Mock<IStateObserver>();
            LoggerMock = new Mock<ILogger<AddressController>>();
            addressServiceMock = new Mock<IAddressService>();
            LocalizerMock = new Mock<IStringLocalizer<SharedResource>>();
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
            CreatedOn = DateTime.Parse("05/08/2019"),
            UpdatedOn = DateTime.Parse("06/08/2019"),
            MailingAddressName = "TestName"
        };
        public class Get : AddressControllerTest
        {
            public async void should_return_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var LoggerMock = new Mock<ILogger<AddressController>>();
                addressServiceMock.Setup(s => s.GetAddressById(expectedAddressObj.AddressId.ToString())).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressControllerUnderTest.Get(expectedAddressObj.AddressId.ToString());
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(addressMockData, okResult);
            }
        }
    }
}
