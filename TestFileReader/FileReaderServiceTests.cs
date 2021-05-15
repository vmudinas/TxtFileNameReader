using PathmaticsInterviewProject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestFileReader
{
    public class FileReaderServiceTests
    {
        [Theory]
        [InlineData(@"Data\advertisers.txt", 85)]
        [InlineData(@"Data\testData.txt", 11)]
        public async Task TestPrintDuplicates_ExpectedBehavior(string path, int expected)
        {
            // Arrange
            var service = new FileReaderService(path, new List<string> { " Inc.", " Inc", " Ltd", " LLC.", " LLC", ".com", "Software" });

            // Act
            await service.PrintDuplicates();
            var actual = service.DuplicateValues.Count;
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(@"Data\advertisers.txt", 32)]
        [InlineData(@"Data\testData.txt", 10)]

        public async Task TestPrintDuplicates_EmptyIgnoreList(string path, int expected)
        {
            // Arrange
            var service = new FileReaderService(path, new());

            // Act
            //service._ignoreNameList = new();
            await service.PrintDuplicates();
          
            var actual = service.DuplicateValues.Count;
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
