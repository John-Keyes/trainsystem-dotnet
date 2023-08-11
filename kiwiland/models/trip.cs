namespace KiwiLand.Models {
    public class Trip {
        /// <summary>
        /// Class <c>Trip</c> This class represents a Trip object. Each one has a string representation of the towns and the total distance.
        /// </summary>
        public Trip(string Symbols, uint Distance) {
            symbols = Symbols; // town symbols
            distance = Distance;
        }
        
        public string symbols { get; set; }

        public uint distance { get; set; }

        public override string ToString() => $"symbols: {symbols}, distance: {distance}";

    }
}