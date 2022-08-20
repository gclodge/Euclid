// See https://aka.ms/new-console-template for more information
using Euclid.Time;

double offset = LeapSeconds.UTCtoGPS(DateTime.Now);

Console.WriteLine($"Current UTCtoGPS offset is: {offset}");
