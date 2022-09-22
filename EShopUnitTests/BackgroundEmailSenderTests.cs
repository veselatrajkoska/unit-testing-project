using EShop.Domain.DomainModels;
using EShop.Repository.Interface;
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
    public class BackgroundEmailSenderTests
    {
        private Mock<IBackgroundEmailSender> _backgroundEmailSender; 
        private EmailMessage emailMessage;
        private List<EmailMessage> messages;

        public BackgroundEmailSenderTests()
        {
            _backgroundEmailSender = new Mock<IBackgroundEmailSender>();
            emailMessage = new EmailMessage
            {
                MailTo = "test@mail.com",
                Subject = "Test",
                Content = "Email content",
                Status = true
            };
            messages = new List<EmailMessage>();
            messages.Add(emailMessage);
        }

        [Fact]
        public void DoWorkTestAsync()
        {
            _backgroundEmailSender.Setup(x => x.DoWork()).Returns(Task.FromResult(messages));

            var result = _backgroundEmailSender.Object.DoWork();

            Assert.NotNull(result);
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
