using Microgroove.CustomerApi.DataAccess.Extensions;
using NUnit.Framework;


namespace Microgroove.CustomerApi.Tests._3___Persistence__Integration_Layer.DataAccess.Extensions
{
    [TestFixture]
    public class DateExtensionsTests
    {
        public DateExtensionsTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GetBirthDateRange_Test1()
        {
            //Arrange test

            //Act test
            var result = 
                30.GetBirthDateRange(() => new DateOnly(2022, 1, 22));

            //Assert test
            Assert.AreEqual(new DateOnly(1991, 1, 23), result.MinDateOfBirth);
            Assert.AreEqual(new DateOnly(1992, 1, 22), result.MaxDateOfBirth);
        }
    }
}
