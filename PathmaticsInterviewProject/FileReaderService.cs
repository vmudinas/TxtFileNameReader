using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PathmaticsInterviewProject
{
    public class FileReaderService
    {
        public Dictionary<string, string> DuplicateValues = new();
        public List<string> IgnoreNameList = new();

        private readonly string _fileName;
        private readonly Dictionary<string, string> _mainList = new();

        public FileReaderService(string fileName)
        {
            _fileName = fileName;
            IgnoreNameList.AddRange(new List<string> { " Inc.", " Inc", " Ltd", " LLC.", " LLC", ".com", "Software" });
        }

        public async Task PrintDuplicates()
        {          

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(executableLocation, _fileName);

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            string content = string.Empty;

            while ((content = await sr.ReadLineAsync()) != null)
            {
                var key = GenerateKey(content);

                if (_mainList.ContainsKey(key))
                {
                    PrintDuplicate(key, content);
                }
                else
                {
                    _mainList.Add(key, content);
                }
            }

            Console.WriteLine($"Duplicate Count: {DuplicateValues.Count}");
            Console.WriteLine($"Main List Count: {_mainList.Count}");
        }

        private void PrintDuplicate(string contentKey, string content)
        {
            if (!DuplicateValues.ContainsKey(contentKey))
            {
                DuplicateValues.Add(contentKey, content);
                Console.WriteLine(content);
            }
        }
        private string GenerateKey(string content)
        {
            var contentToLower = content.ToLower();

            // Remove words in the generalWord List
            foreach (var valueToRemove in IgnoreNameList)
            {
                if (contentToLower.Contains(valueToRemove.ToLower()))
                {
                    contentToLower = contentToLower.Replace(valueToRemove.ToLower(),"");
                }
            }
            // Remove special characters
            Regex r = new("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var contentWithoutSpecialChar = r.Replace(contentToLower, String.Empty);
            var contentNoSpecialCharNoSpace = Regex.Replace(contentWithoutSpecialChar, @"\s+", "");
            
            return contentNoSpecialCharNoSpace;
        }

    }
}
