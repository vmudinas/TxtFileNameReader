using System;
using System.Threading.Tasks;

namespace PathmaticsInterviewProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // We can pass file name as args param
            Console.WriteLine("Printing Duplicate Names from advertisers.txt file");
            var fileService = new FileReaderService(@"Data\advertisers.txt");
        
            await fileService.PrintDuplicates();
            // We can add more words to be ignored in  the ignore list
            // By accesing  fileService.IgnoreNameList
            // Ignore list can be reset to empty
            // fileService.IgnoreNameList = new();

        }

    }
}
