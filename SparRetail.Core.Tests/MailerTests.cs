using NUnit.Framework;
using SparRetail.Core.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Tests
{
    [TestFixture]
    public class MailerTests
    {
        [TestCase]
        public void test_email_function()
        {
            var mailer = new Mailer(null);
            mailer.SendMail("Pieter!", "Test", "pieter.roodt@gmail.com", "onlineretail@onlineretail.com");
        }
    }
}
