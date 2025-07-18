using BusinessManagementSystem.BusinessLayer.Implementations;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using BusinessManagementSystem.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Test.BusinessLayerTest
{
    [TestFixture]
    public class BasicConfigurationImplTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IBasicConfiguration> _mockBasicConfig;
        private BasicConfigurationImpl _service;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBasicConfig = new Mock<IBasicConfiguration>();

            _mockUnitOfWork.Setup(u => u.BasicConfiguration).Returns(_mockBasicConfig.Object);

            _service = new BasicConfigurationImpl(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetBasicConfig_ReturnsResponseDto()
        {
            // Arrange
            var expectedResponse = new ResponseDto<BasicConfiguration>
            {
                Data = new BasicConfiguration
                {
                    Id = 1,
                    EmailAlias = "support",
                    Email = "support@example.com",
                    Password = "P@ssw0rd123!",
                    HostName = "smtp.example.com",
                    Port = 587,
                    ApplicationTitle = "My Business Management System",
                    EmployerName = "Awesome Tattoo Studio",
                    EmployerEmailAddress = "contact@tattoostudio.com",
                    EmployerAddress = "123 Ink Street, Ink City",
                    TattooPrice = 150.0,
                    PiercingPrice = 50.0,
                    DreadLockPrice = 120.0,
                    NewUserEmailTemplate = "Welcome to our service, {UserName}!",
                    NewAppointmentTemplateClient = "Dear {ClientName}, your appointment is confirmed.",
                    NewAppointmentTemplateArtist = "New appointment booked by {ClientName}.",
                    AppointmentUpdateTemplateClient = "Your appointment has been updated.",
                    AppointmentUpdateTemplateArtist = "Appointment updated for client {ClientName}.",
                    PaymentSettlementTemplateArtist = "Payment has been settled.",
                    AppointmentCompletedClient = "Your appointment was successfully completed.",
                    AppointmentCompletedArtist = "You have completed an appointment.",
                    AdvancePaymentArtistTemplate = "An advance payment has been received.",
                    AdvancePaymentSuperadminTemplate = "Advance payment notification for admin."
                },
                StatusCode = HttpStatusCode.OK,
                Message = "Success"
            };

            _mockBasicConfig
                .Setup(r => r.GetSingleOrDefaultAsync(null, false))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.GetBasicConfig();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedResponse.Data.Id, result.Data.Id);
            Assert.AreEqual(expectedResponse.Message, result.Message);
        }

        [Test]
        public void GetBasicConfig_ReturnsNull()
        {
            // Arrange
            var expectedResponse = new ResponseDto<BasicConfiguration>
            {
                Data = new BasicConfiguration
                {
                    Id = 1,
                    EmailAlias = "support",
                    Email = "support@example.com",
                    Password = "P@ssw0rd123!",
                    HostName = "smtp.example.com",
                    Port = 587,
                    ApplicationTitle = "My Business Management System",
                    EmployerName = "Awesome Tattoo Studio",
                    EmployerEmailAddress = "contact@tattoostudio.com",
                    EmployerAddress = "123 Ink Street, Ink City",
                    TattooPrice = 150.0,
                    PiercingPrice = 50.0,
                    DreadLockPrice = 120.0,
                    NewUserEmailTemplate = "Welcome to our service, {UserName}!",
                    NewAppointmentTemplateClient = "Dear {ClientName}, your appointment is confirmed.",
                    NewAppointmentTemplateArtist = "New appointment booked by {ClientName}.",
                    AppointmentUpdateTemplateClient = "Your appointment has been updated.",
                    AppointmentUpdateTemplateArtist = "Appointment updated for client {ClientName}.",
                    PaymentSettlementTemplateArtist = "Payment has been settled.",
                    AppointmentCompletedClient = "Your appointment was successfully completed.",
                    AppointmentCompletedArtist = "You have completed an appointment.",
                    AdvancePaymentArtistTemplate = "An advance payment has been received.",
                    AdvancePaymentSuperadminTemplate = "Advance payment notification for admin."
                },
                StatusCode = HttpStatusCode.OK,
                Message = "Success"
            };

            _mockBasicConfig
                .Setup(r => r.GetFirstOrDefault(p=>p.Id==2, null, false))
                .Returns(expectedResponse);

            // Act
            var result = _service.GetBasicConfig(1);

            // Assert
            Assert.IsNull(result);
            
        }

        [Test]
        public async Task Update_ReturnsResponseDto()
        {
            //Arrange
            var basicConfiguration = new BasicConfiguration
            {
                Id = 1,
                EmailAlias = "support Updated",
                Email = "support@example.com",
                Password = "P@ssw0rd123!",
                HostName = "smtp.example.com",
                Port = 587,
                ApplicationTitle = "My Business Management System",
                EmployerName = "Awesome Tattoo Studio",
                EmployerEmailAddress = "contact@tattoostudio.com",
                EmployerAddress = "123 Ink Street, Ink City",
                TattooPrice = 150.0,
                PiercingPrice = 50.0,
                DreadLockPrice = 120.0,
                NewUserEmailTemplate = "Welcome to our service, {UserName}!",
                NewAppointmentTemplateClient = "Dear {ClientName}, your appointment is confirmed.",
                NewAppointmentTemplateArtist = "New appointment booked by {ClientName}.",
                AppointmentUpdateTemplateClient = "Your appointment has been updated.",
                AppointmentUpdateTemplateArtist = "Appointment updated for client {ClientName}.",
                PaymentSettlementTemplateArtist = "Payment has been settled.",
                AppointmentCompletedClient = "Your appointment was successfully completed.",
                AppointmentCompletedArtist = "You have completed an appointment.",
                AdvancePaymentArtistTemplate = "An advance payment has been received.",
                AdvancePaymentSuperadminTemplate = "Advance payment notification for admin."
            };
            var expectedResponse = new ResponseDto<BasicConfiguration>
            {
                Data = basicConfiguration,
                StatusCode = HttpStatusCode.OK,
                Message = "Success"
            };

            _mockBasicConfig
                .Setup(r => r.UpdateBasicConfigurationDetail(It.IsAny<BasicConfiguration>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.Update(basicConfiguration);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedResponse.Message, result.Message);
            Assert.AreEqual(expectedResponse.Data, result.Data);

        }

    }


}
