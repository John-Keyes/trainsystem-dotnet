using KiwiLand;

namespace KiwiLand.Tests {
    /// <summary>
    /// Class <c>TrainRoutes</c> This class contains the train system unit test
    /// </summary>
    public class KiwiLandTest {

        TrainSystem trainSystem = new TrainSystem();

        [Theory]
        [InlineData("A-B-C", "9")]
        [InlineData("A-D", "5")]
        [InlineData("A-D-C", "13")]
        [InlineData("A-E-B-C-D", "22")]
        [InlineData("A-E-D", "NO SUCH ROUTE")]
        public void FindDistanceTest(string tripInput, string expectedResult) {
            trainSystem.CreateTrainRoutes("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            string calculatedResult = trainSystem.FindDistance(tripInput);
            Assert.False(calculatedResult != expectedResult, $"FindDistanceTest() fail:\n{expectedResult} != {calculatedResult}");
            trainSystem.Clear();
        }

        [Theory]
        [InlineData("stop", "<=", 3, 'C', 'C', 2)]
        [InlineData("stop", "==", 4, 'A', 'C', 3)]
        [InlineData("dist", "<", 30, 'C', 'C', 5)]
        public void GetNumberOfTripsTest(string metricString, string conditionOperator, int comparedToThisInt, char startPoint, char endPoint, uint expectedResult) {
            trainSystem.CreateTrainRoutes("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            uint calculatedResult = trainSystem.GetNumberOfTrips(metricString, conditionOperator, comparedToThisInt, startPoint, endPoint);
            Assert.False(calculatedResult != expectedResult, $"GetNumberOfTripsTest() fail:\n{expectedResult} != {calculatedResult}");
            trainSystem.Clear();
        }

        [Theory]
        [InlineData('B', 'B', 9)]
        [InlineData('A', 'C', 9)]
        public void ShortestDistanceTest(char startPoint, char endPoint, uint expectedResult) {
            trainSystem.CreateTrainRoutes("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            uint calculatedResult = trainSystem.ShortestTotalDistance(startPoint, endPoint);
            Assert.False(calculatedResult != expectedResult, $"ShortestDistanceTest() fail:\n{expectedResult} != {calculatedResult}");
            trainSystem.Clear();

        }
    }
}