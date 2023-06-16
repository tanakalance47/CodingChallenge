using CodingChallenge.Models;
using CodingChallenge.Services.Implementation;
using CodingChallenge.Services.Interface;
using System.Collections.Generic;

IFileProcessor processor = new FileProcessor();

Console.WriteLine("Reading the files in the provided directory...");
Console.WriteLine("");

ServiceResponse<List<FileIdentity>> response = await processor.GetAllFilesInDirectory(@"C:\\Users\\tanak\\Documents\\CodingTest");

if (response.Success != true)
{
    Console.WriteLine(response.Message);
}

List<FileIdentity> files = response.Data;

Console.WriteLine("Number of files obtained: " + files.Count.ToString());
Console.WriteLine("");

ServiceResponse <List <List<FileIdentity>>> result = await processor.SplitFilesBasedOnSize(files);

Console.WriteLine("Divide the files to available threads...");
Console.WriteLine("");

if (result.Success != true || result.Data == null)
{
    Console.WriteLine(response.Message);
}

var splitFiles = result.Data;

for (int i = 0; i < splitFiles.Count; i++)
{
    Console.WriteLine("");
    Console.WriteLine("Thread: " + (i + 1).ToString());
    Console.WriteLine("");

    foreach (var file in splitFiles[i])
    {
        Console.WriteLine("Id: " + file.Id + " Filename: " + file.Name + " Length: " + file.Length);
    }
}

Console.Write("Press any key to exit...");
Console.ReadKey();
