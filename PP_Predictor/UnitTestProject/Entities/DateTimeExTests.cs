using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP_Predictor.Entities;

namespace PP_Predictor.Entities.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void DateTimeEx_Test()
        {
            // Arrange 
            string Date = "2017-01-25";
            string Time = "15:36";

            // Act
            var testResult = new DateTimeEx(Date, Time);

            // Assert 
            Assert.AreEqual(testResult.DateTimeStatus, DateStatus.Ok);
        }
    }
}