using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP_Predictor.Entities;


namespace PP_Predictor.Entities.Tests
{
    [TestClass()]
    public class PlateTests
    {
        [TestMethod()]
        public void Plate_Test()
        {
            // Arrange 
            string Plate = "PDB-032";

            // Act
            var testResult = new Plate(Plate);

            // Assert 
            Assert.AreEqual(testResult.PlateStatus, PlateStatus.Ok);
        }
    }
}