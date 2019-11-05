using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Titan.Ufc.Addresses.API.Entities;
using Titan.Ufc.Addresses.API.Models;
using Titan.Ufc.Addresses.API.Repository;
using Titan.Ufc.Addresses.API.Resources;
using Titan.Ufc.Addresses.API.Service;
using Titan.UFC.Common.ExceptionMiddleWare;
using Xunit;


namespace AddressUnitTestProject.AddressServiceUnitTest
{
    public class AddressServiceTest
    {
        protected Mock<IAddressRepository> addressRepositoryMock { get; }
        protected AddressService addressServiceUnderTest { get; }
        protected Mock<ILogger<AddressService>> LoggerMock { get; }
        protected Mock<ISharedResource> LocalizerMock { get; }

        public AddressServiceTest()
        {
            addressRepositoryMock = new Mock<IAddressRepository>();
            LoggerMock = new Mock<ILogger<AddressService>>();
            LocalizerMock = new Mock<ISharedResource>();
            addressServiceUnderTest = new AddressService(addressRepositoryMock.Object, LocalizerMock.Object);
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
            MailingAddressName = "TestName"
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
            UpdatedDate = DateTime.Parse("06/08/2019")
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
                addressRepositoryMock.Setup(i => i.CheckAddressId(expectedAddressObj.AddressUID.Value)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.UpdateAddress(expectedAddressObj.AddressUID.Value, expectedAddressObj)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.UpdateAddress(expectedAddressObj.AddressUID.ToString(), expectedAddressObj);
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
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressUID.Value)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.DeleteAddress(addressMockData.AddressUID.Value)).Returns(Task.FromResult(expectedAddressObj.AddressID));
                var result = await addressServiceUnderTest.DeleteAddress(addressMockData.AddressUID.Value.ToString());
                Assert.Equal(expectedAddressObj.AddressID, result);
            }

            [Fact]
            public async void should_not_delete_if_addressId_doesnot_exists()
            {
                string invalidAddressStr = "00000000-0000-0000-0000-000000000000";
                var expectedAddressObj = addressMockData;
                var invalidAddressId = new Guid(invalidAddressStr);
                var check_result = false;
                var LoggerLock = new Mock<ILogger<AddressService>>();
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressUID.Value)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.DeleteAddress(addressMockData.AddressUID.Value)).Returns(Task.FromResult(expectedAddressObj.AddressID));
                try
                {
                    var result = await addressServiceUnderTest.DeleteAddress(addressMockData.AddressUID.Value.ToString());
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
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressUID.Value)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.GetAddressById(addressMockData.AddressUID.Value)).Returns(Task.FromResult(expectedAddressObj));
                var result = await addressServiceUnderTest.GetAddressById(addressMockData.AddressUID.Value.ToString());
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
                addressRepositoryMock.Setup(i => i.CheckAddressId(addressMockData.AddressUID.Value)).Returns(Task.FromResult(check_result));
                addressRepositoryMock.Setup(i => i.GetAddressById(addressMockData.AddressUID.Value)).Returns(Task.FromResult(expectedAddressObj));
                try
                {
                    var result = await addressServiceUnderTest.GetAddressById(addressMockData.AddressUID.Value.ToString());
                }
                catch (TitanCustomException titanCustomException)
                {
                    Assert.IsType<TitanCustomException>(titanCustomException);
                }

            }
        }
    }
}
