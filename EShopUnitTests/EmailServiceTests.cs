using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Service.Implementation;
using EShop.Service.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EShopUnitTests
{
    public class EmailServiceTests
    {
        private Mock<IEmailService> _mockService;
        private EmailMessage mockEmailMessage;
        private List<EmailMessage> mockMessagesList;

        public EmailServiceTests()
        {
            _mockService = new Mock<IEmailService>();
            mockEmailMessage = new EmailMessage
            {
                MailTo = "test@mail.com",
                Subject = "Test",
                Content = "Email content",
                Status = true
            };
            mockMessagesList = new List<EmailMessage>();
            mockMessagesList.Add(mockEmailMessage);
        }

        [Fact]
        public void SendEmailAsyncTest()
        {
            _mockService.Setup(x => x.SendEmailAsync(mockMessagesList)).Returns(Task.FromResult(true));

            var emailSent = _mockService.Object.SendEmailAsync(mockMessagesList);

            Assert.NotNull(emailSent);
            Assert.True(emailSent.IsCompletedSuccessfully);
        }
    }
}
