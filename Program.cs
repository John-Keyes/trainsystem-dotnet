using KiwiLand;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

TrainSystem trainSystem = new TrainSystem();

//Copy and paste the input.
trainSystem.CreateTrainRoutes("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");

//Add a route as a string just how it is in the problem.
Console.WriteLine(trainSystem.FindDistance("A-B-C"));
Console.WriteLine(trainSystem.FindDistance("A-D"));
Console.WriteLine(trainSystem.FindDistance("A-D-C"));
Console.WriteLine(trainSystem.FindDistance("A-E-B-C-D"));
Console.WriteLine(trainSystem.FindDistance("A-E-D"));

//Get Number of trips accepts a custom condition 
//stop (for the number of stops) or dist (for the distance)
//The comparison operator
//A character for the start point and the end point
Console.WriteLine(trainSystem.GetNumberOfTrips("stop", "<=", 3, 'C', 'C'));
Console.WriteLine(trainSystem.GetNumberOfTrips("stop", "==", 4, 'A', 'C'));

//Params are a character for the start point and end point
Console.WriteLine(trainSystem.ShortestTotalDistance('B', 'B'));
Console.WriteLine(trainSystem.ShortestTotalDistance('A', 'C'));

Console.WriteLine(trainSystem.GetNumberOfTrips("dist", "<", 30, 'C', 'C'));