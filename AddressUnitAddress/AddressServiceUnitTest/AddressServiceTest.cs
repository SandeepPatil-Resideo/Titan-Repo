using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.LocalizationResource;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.Repository;
using TitanTemplate.titanaddressapi.Service;
using Xunit;

namespace AddressUnitAddress.AddressServiceUnitTest
{
    public class AddressServiceTest
    {
        protected Mock<IAddressRepository> addressRepositoryMock { get; }
        protected AddressService addressServiceUnderTest { get; }
        protected Mock<ILogger<AddressService>> LoggerMock { get; }
        protected Mock<IStringLocalizer<SharedResource>> LocalizerMock { get; }

        public AddressServiceTest()
        {
            addressRepositoryMock = new Mock<IAddressRepository>();
            LoggerMock = new Mock<ILogger<AddressService>>();
            LocalizerMock = new Mock<IStringLocalizer<SharedResource>>();
            addressServiceUnderTest = new AddressService(addressRepositoryMock.Object, LocalizerMock.Object);
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

        public class Create : AddressServiceTest
        {
            [Fact]
            public async void should_create_address()
            {
                var expectedAddressObj = addressMockData;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CreateAddress(addressMockData)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.CreateAddress(addressMockData);
                Assert.Equal(expectedAddressObj, result);
            }
        }

        public class Update : AddressServiceTest
        {
            [Fact]
            public async void should_create_address()
            {
                var expectedAddressObj = addressMockData;
                var check_result = true;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.UpdateAddress(addressMockData.AddressId, addressMockData)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.UpdateAddress(addressMockData.AddressId.ToString(), addressMockData);
                Assert.Equal(expectedAddressObj, result);
            }
        }

        public class Delete : AddressServiceTest
        {
            [Fact]
            public async void should_delete_if_addressId_exists()
            {
                var expectedAddressObj = addressMockData;
                var check_result = true;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.DeleteAddress(addressMockData.AddressId)).Returns(Task.FromResult(expectedAddressObj.Id));
                var result = await addressServiceUnderTest.DeleteAddress(addressMockData.AddressId.ToString());
                Assert.Equal(expectedAddressObj.Id, result);
            }
        }

        public class Get : AddressServiceTest
        {
            [Fact]
            public async void should_get_addressId_if_exists()
            {
                var expectedAddressObj = addressMockData;
                var check_result = true;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.GetAddressById(addressMockData.AddressId)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.GetAddressById(addressMockData.AddressId.ToString());
                Assert.Equal(expectedAddressObj, result);
            }
        }
    }
}
