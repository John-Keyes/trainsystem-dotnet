namespace KiwiLand.Models {
    public class Town {
        /// <summary>
        /// Class <c>TrainRoutes</c> This class represents a Town object. Each one has a symbol and routes to other nodes.
        /// </summary>
        public Town(char Symbol) {
            symbol = Symbol;
            routeMap = new Dictionary<string, uint>(); //key contains both town symbols and the value is the distance
        }
        
        public char symbol { get; set; }

        public Dictionary<string, uint> routeMap { get; set; }

        public override string ToString() {
            string result = $"symbol: {symbol}, routeMap: ";
            foreach(var keyValuePair in routeMap) {
                result += $"Key = {keyValuePair.Key}, Value = {keyValuePair.Value}\n";
            }
            return result;
        }

    }
}