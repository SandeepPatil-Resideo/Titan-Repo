﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using Xunit;
using Titan.Ufc.Addresses.API.Repository;
using Titan.Ufc.Addresses.API.Entities;
using Titan.Ufc.Addresses.API.Resources;
using Titan.Ufc.Addresses.API.Models;

namespace AddressUnitTestProject.AddressRepositoryUnitTest
{
    public class AddressRepositoryTest
    {
        protected AddressRepository addressRepositoryUndertest { get; }
        protected IConfiguration ConfigurationMock { get; }
        protected AddressContext addressContextInMemory { get; }
        protected MapperConfiguration MappingConfig { get; }
        
        protected Mock<IMapper> mappermock;
        protected IMapper MapperMock { get; }
        protected Mock<ILogger<AddressRepository>> LoggerMock { get; }
        protected Mock<IStringLocalizer<SharedResource>> LocalizerMock { get; }
        public AddressRepositoryTest()
        {
            addressContextInMemory = GetAddressContextInMemory();

            LoggerMock = new Mock<ILogger<AddressRepository>>();
            LocalizerMock = new Mock<IStringLocalizer<SharedResource>>();
            mappermock = new Mock<IMapper>();
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();
            ConfigurationMock = builder.Build();


            addressRepositoryUndertest = new AddressRepository(addressContextInMemory, mappermock.Object);
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
            CountryStateEntity countryStateEntity = countryStateEntityMockData;
            context.Add<AddressEntity>(addressEntity);
            context.Add<CountryStateEntity>(countryStateEntity);
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
            StateCode = "KA",
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

        public static Address addressMockDataCreate => new Address
        {
            AddressUID = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            Address1 = "Address Test Line 1",
            Address2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            PinCode = "560103",
            StateCode = "KA",
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
        public static CountryStateEntity countryStateEntityMockData => new CountryStateEntity
        {
            CountryCode = "+91",
            StateId = 29,
            AbbreviatedName = "91-KA",
            StateName = "Karnataka",
            IsEnabled = true
        };
        public static AddressEntity addressEntityMockData => new AddressEntity
        {
            AddressUID = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            Address1 = "Address Test Line 1",
            Address2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            PinCode = "560103",
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsVerified = 1,
            MailingAddressName = "TestName",
            CreatedDate = DateTime.Parse("05/08/2019"),
            UpdatedDate = DateTime.Parse("06/08/2019"),
            StateID = 29,
            Type = 1,
            isPrimary = true
        };

        public static AddressEntity addressEntityMockDataCreate => new AddressEntity
        {
            AddressUID = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ContactName = "Suamnth",
            Address1 = "Address Test Line 1",
            Address2 = "Address Test Line 2",
            AddressLine3 = "Address Test Line 3",
            City = "Bangalore",
            PinCode = "560103",
            CountryCode = "+91",
            Latitude = decimal.Parse("17.231"),
            Longitude = decimal.Parse("78.123"),
            ContactNumber = "7075808080",
            IsVerified = 1,
            MailingAddressName = "TestName",
            CreatedDate = DateTime.Parse("05/08/2019"),
            UpdatedDate = DateTime.Parse("06/08/2019"),
            StateID = 29,
            Type = 1,
            isPrimary = true
        };

        public class Get : AddressRepositoryTest
        {
            [Fact]
            public async void should_retrive_address_if_exists()
            {
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                mappermock.Setup(x => x.Map<Address>(It.IsAny<AddressEntity>())).Returns(addressMockData);
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, mappermock.Object);
                
                Address result = await addressRepositoryUndertest.GetAddressById(addressEntityInMemory.AddressUID);
                Assert.Equal(addressMockData.AddressUID, result.AddressUID);
            }
        }

        public class Create : AddressRepositoryTest
        {
            [Fact]
            public async void create_when_Id_exists()
            {
                Address address = addressMockData;
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                mappermock.Setup(x => x.Map<AddressEntity>(It.IsAny<Address>())).Returns(addressEntityMockDataCreate);
                mappermock.Setup(x => x.Map<Address>(It.IsAny<AddressEntity>())).Returns(addressMockDataCreate);
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, mappermock.Object);
                var result = await addressRepositoryUndertest.CreateAddress(address);
                Assert.Equal(addressMockDataCreate.AddressUID, result.AddressUID);
            }
        }

        public class Update : AddressRepositoryTest
        {
            [Fact]
            public async void update_when_AddressId_exists()
            {
                Address address = addressMockData;
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                mappermock.Setup(x => x.Map<Address>(It.IsAny<AddressEntity>())).Returns(addressMockData);
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, mappermock.Object);
                var result = await addressRepositoryUndertest.UpdateAddress(address.AddressUID.Value, address);
                Assert.Equal(addressMockData.AddressUID, result.AddressUID);
            }
        }

        public class Delete : AddressRepositoryTest
        {
            [Fact]
            public async void delete_when_addressId_exists()
            {
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.DeleteAddress(addressEntityInMemory.AddressUID);
                Assert.Equal(addressMockData.AddressID, result);
            }
        }

        public class Check : AddressRepositoryTest
        {
            [Fact]
            public async void check_if_address_exists()
            {
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.CheckAddressId(addressEntityInMemory.AddressUID);
                Assert.True(result == true);
            }

            [Fact]
            public async void check_if_address_doesnot_exists()
            {
                string invalidAddressUID = "00000000-0000-0000-0000-000000000000";
                var invalidAddressGUID = new Guid(invalidAddressUID);
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.CheckAddressId(invalidAddressGUID);
                Assert.True(result == false);
            }
        }

        public class CheckCountryCode : AddressRepositoryTest
        {
            [Fact]
            public async void check_if_country_exists()
            {
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);
                var result = await addressRepositoryUndertest.CheckCountryCode(addressEntityInMemory.CountryCode);
                Assert.True(result == true);
            }

            [Fact]
            public async void return_false_if_country_doesnot_exists()
            {
                string invalidCountryCode = null;
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);
                var result = await addressRepositoryUndertest.CheckCountryCode(invalidCountryCode);
                Assert.True(result == false);
            }
        }
    }
}
