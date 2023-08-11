using KiwiLand.Models;
namespace KiwiLand { 
    /// <summary>
    /// Class <c>TrainRoutes</c> This class contains all of the methods that conduct operations for the train system.
    /// </summary>
    public class TrainSystem {

        private Dictionary<char, Town> townMap = new Dictionary<char, Town>();

        /// <summary>
        /// Method <c>CreateTrainRoutes</c> Creates the town objects(including the routes) when parsing the input.
        /// </summary>
        public void CreateTrainRoutes(string routeInput) {
            string[] routeArr = routeInput.Split(", ");
            foreach (string routeString in routeArr) {
                char firstSymbol = routeString[0];
                char secondSymbol = routeString[1];
                uint distance = (uint)routeString[2] - '0';
                string routeMapKey = $"{firstSymbol}{secondSymbol}";
                if(townMap.ContainsKey(firstSymbol)) { //If a town already exists, get it and add a route.
                    Town town = townMap[firstSymbol];
                    town.routeMap[routeMapKey] = distance;
                    townMap[firstSymbol] = town;
                }
                else { // create new town
                    Town town1 = new Town(firstSymbol);
                    town1.routeMap[routeMapKey] = distance;
                    townMap[firstSymbol] = town1;
                }
                if(!townMap.ContainsKey(secondSymbol)) { //Only create a new town if no match is found.
                    Town town2 = new Town(secondSymbol);
                    townMap[secondSymbol] = town2;
                }
            }
        }
        /// <summary>
        /// Method <c>ToString</c> Returns string representation of the train system (townMap).
        /// </summary>
        public override string ToString() {
            string result = "Train System\n";
            foreach(var keyValuePair in townMap) {
                result += $"Key = {keyValuePair.Key} Value = {keyValuePair.Value}\n";
            }
            return result;
        }
        /// <summary>
        /// Method <c>FindDistance</c> Finds the distance of a given trip as a string.
        /// </summary>
        public string FindDistance(string trip) {
            uint distance = 0;
            for(int i = 0; i < trip.Length; i++) {
                if((trip[i] != '-') && (i != 0) ) {
                    char currSymbol = trip[i - 2];
                    Town currTown = townMap[currSymbol];
                    string key = $"{currSymbol}{trip[i]}";
                    if (!currTown.routeMap.ContainsKey(key)) {
                        return "NO SUCH ROUTE";
                    }
                    distance += currTown.routeMap[key];
                }
            }
            return distance.ToString();
        }

        /// <summary>
        /// Method <c>FindTrips</c> Recursively finds the trips without multiple cycles. (GetTrips handles multiple cycles)
        /// </summary>
        private void FindTrips(Town currTown, List<char> visitedTowns, Trip currTrip, List<Trip> trips, char startPoint, char endPoint) {
            if(!visitedTowns.Contains(currTown.symbol)) { //This is to ensure we don't visit a previous node until we at least visited another.
                visitedTowns.Add(currTown.symbol);
                foreach(var currRoute in currTown.routeMap) { // For each town connected make a trip that extends the current trip
                    Trip currTripPlusThisRoute = new Trip(currTrip.symbols, currTrip.distance);
                    currTripPlusThisRoute.symbols += currRoute.Key[1];
                    currTripPlusThisRoute.distance += currRoute.Value;
                    if (currRoute.Key[1] == endPoint) { // If endPoint touched add as a trip
                        trips.Add(currTripPlusThisRoute);
                        visitedTowns.Remove(currRoute.Key[0]);
                    }
                    FindTrips(townMap[currRoute.Key[1]], visitedTowns, currTripPlusThisRoute, trips, startPoint, endPoint);
                }
                visitedTowns.Remove(currTown.symbol); //Remove so town is accessible
            }
        }

        /// <summary>
        /// Method <c>GetTrips</c> This function returns the number of trips by passing trips.
        /// </summary>
        private List<Trip> GetTrips(char startPoint, char endPoint) {
            List<Trip> trips = new List<Trip>();
            List<char> visitedTowns = new List<char>();
            Town currTown = townMap[startPoint];
            Trip currTrip = new Trip($"{startPoint}", 0);
            FindTrips(currTown, visitedTowns, currTrip, trips, startPoint, endPoint);
            return trips;
        }

        /// <summary>
        /// Method <c>CustomCondition</c> This function returns the boolean based on the conditionOperator.
        /// </summary>
        private Boolean CustomCondition(uint metric, string conditionOperator, int comparedToThisInt) {
            switch(conditionOperator) { // string correlates to the type of comparison
                case "<":
                    return metric < comparedToThisInt;
                case "<=":
                    return metric <= comparedToThisInt;
                case ">":
                    return metric > comparedToThisInt;
                case ">=":
                    return metric >= comparedToThisInt;
                default:
                    return metric == comparedToThisInt;
            }
        }

        /// <summary>
        /// Method <c>GetNumberOfTrips</c> Calling GetTrips to obtain the number of trips and using user inputted custom condition to filter the count.
        /// </summary>
        public uint GetNumberOfTrips(string metricString, string conditionOperator, int comparedToThisInt, char startPoint, char endPoint) {
            List<Trip> trips = GetTrips(startPoint, endPoint);
            uint tripsThatFallsUnderMetricCount = 0;
            Boolean metricStringComparisonOutCome = metricString == "dist";
            foreach(Trip trip in trips) {
                uint metric = metricStringComparisonOutCome ? trip.distance : (uint)trip.symbols.Length - 1;
                string tripSymbols = trip.symbols;
                uint tripDistance = trip.distance;
                while(true) { //Handles routes that cycle more than once.
                    if(!CustomCondition(metric, conditionOperator, comparedToThisInt)) { //if comparison
                        break;
                    }
                    tripsThatFallsUnderMetricCount++;
                    string symbols = trip.symbols.Substring(1, trip.symbols.Length - 1);
                    tripSymbols += symbols;
                    tripDistance += trip.distance;
                    metric = metricStringComparisonOutCome ? tripDistance : (uint)tripSymbols.Length - 1;
                }
            }
            return tripsThatFallsUnderMetricCount;
        }

        /// <summary>
        /// Method <c>ShortestTotalDistance</c> Find the min of the trips returned from GetTrips.
        /// </summary>
        public uint ShortestTotalDistance(char startPoint, char endPoint) {
            return GetTrips(startPoint, endPoint).Min(trip => trip.distance); 
        }

        /// <summary>
        /// Method <c>Clear</c> Reset the train system.
        /// </summary>
        public void Clear() => townMap.Clear();
    }
}