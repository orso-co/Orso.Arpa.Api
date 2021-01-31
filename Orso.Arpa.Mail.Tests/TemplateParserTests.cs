using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Mail.Tests
{
    [TestFixture]
    public class TemplateParserTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        private TemplateParser CreateTemplateParser()
        {
            return new TemplateParser();
        }

        [Test]
        public void Should_Parse_Email_Template()
        {
            // Arrange
            TemplateParser templateParser = CreateTemplateParser();
            ITemplate templateData = new ConfirmEmailTemplate();

            // Act
            var result = templateParser.Parse(templateData);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain("<title>");
            result.Should().Contain("</title>");
        }

        [Test]
        public void Should_Throw_Exception_If_Template_File_Could_Not_Be_Found()
        {
            // Arrange
            TemplateParser templateParser = CreateTemplateParser();
            ITemplate templateData = new InvalidTemplate();

            // Act
            Func<string> func = () => templateParser.Parse(templateData);

            // Assert
            func.Should().Throw<FileNotFoundException>();
        }

        private class InvalidTemplate : ITemplate
        {
            public string Name => "does_not_exist";
        }
    }
}
