using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TitanTemplate.titanaddressapi.Entities;
using TitanTemplate.titanaddressapi.Models;
using TitanTemplate.titanaddressapi.LocalizationResource;
using TitanTemplate.titanaddressapi.Repository;
using Xunit;

namespace AddressUnitAddress.AddressRepositoryUnitTest
{
    public class AddressRepositoryTest
    {
        protected AddressRepository addressRepositoryUndertest { get; }
        protected IConfiguration ConfigurationMock { get; }
        protected AddressContext addressContextInMemory { get; }
        protected MapperConfiguration MappingConfig { get; }
        protected IMapper MapperMock { get; }
        protected Mock<ILogger<AddressRepository>> LoggerMock { get; }
        protected Mock<IStringLocalizer<SharedResource>> LocalizerMock { get; }
        public AddressRepositoryTest()
        {
            addressContextInMemory = GetAddressContextInMemory();

            LoggerMock = new Mock<ILogger<AddressRepository>>();
            LocalizerMock = new Mock<IStringLocalizer<SharedResource>>();

            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();
            ConfigurationMock = builder.Build();


            addressRepositoryUndertest = new AddressRepository(addressContextInMemory, MapperMock);
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

        public class Get : AddressRepositoryTest
        {
            [Fact]
            public async void should_retrive_address_if_exists()
            {
                var DbContextInMemory = GetAddressContextInMemory();
                var addressEntityInMemory = await DbContextInMemory.Set<AddressEntity>().FirstOrDefaultAsync();
                var LoggerMock = new Mock<ILogger<AddressRepository>>();
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.GetAddressById(addressEntityInMemory.AddressId);
                Assert.Equal(addressMockData, result);
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
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.CreateAddress(address);
                Assert.Equal(addressMockData, result);
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
                var addressRepositoryUndertest = new AddressRepository(DbContextInMemory, MapperMock);

                var result = await addressRepositoryUndertest.UpdateAddress(address.AddressId, address);
                Assert.Equal(addressMockData, result);
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

                var result = await addressRepositoryUndertest.DeleteAddress(addressEntityInMemory.AddressId);
                Assert.Equal(addressMockData.Id, result);
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

                var result = await addressRepositoryUndertest.CheckAddressId(addressEntityInMemory.AddressId);
                Assert.True(result == true);
            }
        }
    }
}
