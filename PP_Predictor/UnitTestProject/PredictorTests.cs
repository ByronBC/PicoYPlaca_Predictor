using Microsoft.VisualStudio.TestTools.UnitTesting;
using PP_Predictor;

namespace PP_Predictor.Tests
{
    [TestClass()]
    public class PredictorTests
    {
        [TestMethod()]
        public void CanBeOnTheRoad_Test()
        {
            // Arrange 
            string Plate = "PDB-1032";
            string Date = "2017-06-19";
            string Time = "15:36";

            // Act
            Predictor predictor = new Predictor(Plate, Date, Time);
            var testResult = predictor.CanBeOnTheRoad();

            // Assert 
            Assert.AreEqual(testResult, true);
        }

        [TestMethod()]
        public void CanBeOnTheRoad_BacthTest()
        {
            // Arrange 
            string[,] Params = new string[,] {
                { "PDB-1032", "2017-06-19", "07:40", "false"},
                { "PDB-1032", "2017-06-19", "17:36", "false"},
                { "PDB-1032", "2017-06-19", "22:40", "true"},
                { "PDB-1032", "2017-06-20", "17:36", "true"},
                { "INVALID2", "2017-06-19", "17:36", "false"},
                { "PDB-1035", "2017-06-19", "17:36", "true"}
            };

            // Act
            bool bResult = true;
            for (int i = 0; i < Params.GetLength(0); i++)
            {
                Predictor predictor = new Predictor(Params[i, 0], Params[i, 1], Params[i, 2]);
                var testResult = predictor.CanBeOnTheRoad();
                bResult = bResult & (testResult.ToString().ToLower() == Params[i, 3]);

            }

            // Assert 
            Assert.AreEqual(bResult, true);
        }
    }
}