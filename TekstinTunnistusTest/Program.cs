using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spire; // Get these via the Spire OCR package
using Spire.OCR; // Get these via the Spire OCR package

namespace OCR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OcrScanner _scanner = new OcrScanner(); // Make a variable for scanner
            ConfigureOptions configureOptions = new ConfigureOptions(); // Make an variable for options

            // Set options for the variable
            configureOptions.ModelPath = @"C:\\Users\\****\\*****\\win-x64"; // First download the Spire OCR Executable and point this to where it's located. I didn't bother including it with this so the package wont get too big :).
            configureOptions.Language = "English"; // Set the model language to English |||| I didn't make this Finnish as at the time of writing it was not supported. See: https://www.e-iceblue.com/Introduce/ocr-for-net.html

            _scanner.ConfigureDependencies(configureOptions); // Apply these options to your scanner

            // The file that the scanner will scan
            _scanner.Scan(@"C:\\Users\\****\\****\\OCRSolution\\OCR\\kuva.png"); // Again, point this towards where your photo is.
            
            // Outputs the text that the scanner read
            string textOutput = _scanner.Text.ToString(); // Turn the scanner output into string format
            Console.WriteLine(textOutput); // Write the output to a console

            Console.WriteLine("---------- Regex result ----------"); // Write this into the console
            
            Regex itemPricePat = new Regex(@"([A-Za-z0-9\s\-_]+)\s*(\d{1,3},\d{2})", RegexOptions.Multiline); // Regex to remove unnecessary stuff from the scan result
            Console.WriteLine("Item; Price"); // Write these into the console

            //Search for match in the scanned text and store it in "matches"
            var matches = itemPricePat.Matches(textOutput);

            // If there are matches then do this
            if (matches.Count > 0)
            {
                foreach (Match match in matches) // For each match in the matches variable, run these
                {
                    string item = match.Groups[1].Value;
                    string prices = match.Groups[2].Value;
                    Console.WriteLine($"{item} = {prices}");
                }
            }
            // Else write this
            else Console.WriteLine("No results");



            // Display at the end
            Console.WriteLine("---------- End of OCR Result ----------");
            Console.WriteLine(); 

        }
    }
}
