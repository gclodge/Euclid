// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

using Euclid.Las;

//< Disregard the hacky local test path (pls)
var source = @"C:\_test\1002_FOC22\1_Points";

var lasFiles = Directory.GetFiles(source, "*.las");

var histByLas = new Dictionary<string, uint[]>();

ulong totalCount = 0;
ulong totalBytes = 0;

var sw = Stopwatch.StartNew();
foreach (var las in lasFiles)
{
    using var reader = new LasReader(las);
    var lpt = new LasPoint();

    totalBytes += (ulong)(new FileInfo(las).Length);

    var hist = new uint[byte.MaxValue];

    while (!reader.EOF)
    {
        reader.GetNextPoint(ref lpt);

        totalCount++;
        hist[lpt.Classification]++;
    }

    histByLas.Add(Path.GetFileName(las), hist);
}
sw.Stop();

double totalMB = (double)totalBytes / (1024.0 * 1024.0);
double totalSeconds = sw.ElapsedMilliseconds / 1000.0;

Console.WriteLine($"Done reading {lasFiles.Length} total LAS files in {totalSeconds} seconds");
Console.WriteLine($"\t - Total Points: {totalCount}");
Console.WriteLine($"\t - Total MB Read: {totalMB:0.00} ({totalMB / totalSeconds:0.00} MB/s)");
