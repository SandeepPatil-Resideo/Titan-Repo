using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Titan.UFC.Common.ExceptionMiddleWare;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.LocalizationResource;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.Repository;
using TitanTemplate.titanaddressapi.Service;
using Xunit;


namespace AddressUnitTestProject.AddressServiceUnitTest
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

        public class Create : AddressServiceTest
        {
            [Fact]
            public async void should_create_address()
            {
                var expectedAddressObj = addressMockData;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CreateAddress(expectedAddressObj)).Returns(Task.FromResult(expectedAddressObj));
                Address result = await addressServiceUnderTest.CreateAddress(expectedAddressObj);
                Assert.Equal(expectedAddressObj, result);
            }

            [Fact]
            public async void should_not_create_address_if_it_i()
            {
                var expectedAddressObj = addressMockData;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CreateAddress(expectedAddressObj)).Throws(new ArgumentNullException("Address is Null"));
                try
                {
                    Address result = await addressServiceUnderTest.CreateAddress(expectedAddressObj);
                }
                catch (ArgumentNullException argumentNullException)
                {
                    Assert.IsType<ArgumentNullException>(argumentNullException);
                }
            }
        }

        public class Update : AddressServiceTest
        {
            [Fact]
            public async void should_update_address()
            {
                var expectedAddressObj = addressMockData;
                var check_result = true;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(expectedAddressObj.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.UpdateAddress(expectedAddressObj.AddressId, expectedAddressObj)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.UpdateAddress(expectedAddressObj.AddressId.ToString(), expectedAddressObj);
                Assert.Equal(expectedAddressObj, result);
            }

            [Fact]
            public async void should_not_update_address_if_addressId_does_not_exist()
            {
                string invalidAddressStr = "00000000-0000-0000-0000-000000000000";
                var expectedAddressObj = addressMockData;
                var invalidAddressId = new Guid(invalidAddressStr);
                var check_result = false;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(invalidAddressId)).Returns(Task.FromResult(check_result));
                try
                {
                    var result = await addressServiceUnderTest.UpdateAddress(invalidAddressId.ToString(), expectedAddressObj);
                }
                catch (TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }
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

            [Fact]
            public async void should_not_delete_if_addressId_doesnot_exists()
            {
                string invalidAddressStr = "00000000-0000-0000-0000-000000000000";
                var expectedAddressObj = addressMockData;
                var invalidAddressId = new Guid(invalidAddressStr);
                var check_result = false;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.DeleteAddress(addressMockData.AddressId)).Returns(Task.FromResult(expectedAddressObj.Id));
                try
                {
                    var result = await addressServiceUnderTest.DeleteAddress(addressMockData.AddressId.ToString());
                }
                catch (TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }

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

            [Fact]
            public async void should_not_get_addressId_if_doesnot_exists()
            {
                string invalidAddressStr = "00000000-0000-0000-0000-000000000000";
                var expectedAddressObj = addressMockData;
                var invalidAddressId = new Guid(invalidAddressStr);
                var check_result = false;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressId)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.GetAddressById(addressMockData.AddressId)).Returns(Task.FromResult(expectedAddressObj));
                try
                {
                    var result = await addressServiceUnderTest.GetAddressById(addressMockData.AddressId.ToString());
                }
                catch (TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }

            }
        }
    }
}
