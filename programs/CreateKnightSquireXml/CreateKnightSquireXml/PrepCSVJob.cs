using System;
using System.Diagnostics;
using System.IO;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    public class PrepCsvJob
    {
        private readonly string _csvPathAndFilename;
        private readonly string _csvPathAndFilenameOut;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvPathAndFilename"></param>
        public PrepCsvJob(string csvPathAndFilename)
        {
            _csvPathAndFilename    = csvPathAndFilename;
            _csvPathAndFilenameOut = csvPathAndFilename.Replace(".csv", "-out.csv");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DoWork()
        {
            Debug.WriteLine("PrepCsvJob.DoWork() ...");

            const char charDoubleQuote = (char) 34;
            const char charComma       = (char) 44;
            string headerLine = charDoubleQuote + "PRECEDENCE"    + charDoubleQuote + charComma + charDoubleQuote + "KNIGHT" + charDoubleQuote + charComma + charDoubleQuote + "MASTER" + charDoubleQuote +
                                charComma       + charDoubleQuote + "NAME"          +
                                charDoubleQuote + charComma       + charDoubleQuote + "TYPE" + charDoubleQuote + charComma + charDoubleQuote + "DATE ELEVATED" + charDoubleQuote + charComma + charDoubleQuote +
                                "A.S."          + charDoubleQuote +
                                charComma       + charDoubleQuote + "KINGDOM" + charDoubleQuote + charComma + charDoubleQuote + "KINGDOM PRECEDENCE" + charDoubleQuote + charComma + charDoubleQuote + "RESIGNED OR REMOVED" +
                                charDoubleQuote +
                                charComma       + charDoubleQuote + "PASSED AWAY" + charDoubleQuote + charComma + charDoubleQuote + "NOTES";


            const int headerLinesToDelete = 12;
            int       lineCounter         = 0;
            try
            {
                var sr = new StreamReader(_csvPathAndFilename);
                var sw = new StreamWriter(_csvPathAndFilenameOut);

                string line = sr.ReadLine();
                lineCounter++;
                while (line != null)
                {
                    line = sr.ReadLine();
                    lineCounter++;
                    if (lineCounter == headerLinesToDelete) sw.WriteLine(headerLine);

                    if (lineCounter > headerLinesToDelete) sw.WriteLine(line);
                }

                sr.Close();
                sw.Flush();
                sw.Close();

                File.Delete(_csvPathAndFilename);
                File.Copy(_csvPathAndFilenameOut, _csvPathAndFilename);
                File.Delete(_csvPathAndFilenameOut);

                Debug.WriteLine("PrepCsvJob.DoWork() Created: " + _csvPathAndFilename);
            }
            catch (Exception e)
            {
                Debug.WriteLine("PrepCsvJob Exception: " + e.Message);
            }
            finally
            {
                Debug.WriteLine("Executing finally block.");
            }

            return 0;
        }
    }
}