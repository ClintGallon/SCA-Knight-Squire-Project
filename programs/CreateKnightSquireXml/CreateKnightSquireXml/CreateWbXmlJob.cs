using System;
using System.Diagnostics;
using System.IO;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWbXmlJob
    {
        private readonly string _csvPathAndFilename;
        private readonly string _xmlPathAndFilenameOut;

        public CreateWbXmlJob(string csvPathAndFilename, string xmlPathAndFilenameOut)
        {
            _csvPathAndFilename    = csvPathAndFilename;
            _xmlPathAndFilenameOut = xmlPathAndFilenameOut;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DoWork()
        {
            Debug.WriteLine("CreateWbXmlJob.DoWork() ...");

            const string headerLine          = @"<?xml version=\""1.0\"" encoding=\""utf-8\""?>";
            const int    headerLinesToDelete = 1;
            int          lineCounter         = 0;

            try
            {
                var sr = new StreamReader(_csvPathAndFilename);
                var sw = new StreamWriter(_xmlPathAndFilenameOut);

                string line = sr.ReadLine();
                lineCounter++;

                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null) continue;
                    string[] col = line.Split(',');
                    lineCounter++;
                    if (lineCounter == headerLinesToDelete) sw.WriteLine(headerLine);

                    if (lineCounter <= headerLinesToDelete) continue;
                    const string knightstart = "<knight>";
                    sw.WriteLine(knightstart);
                    for (int i = 0; i < 12; i++)
                    {
                        string outline = i switch
                        {
                            0 => ("<society_precedence>" + col[0].Trim()    + "</society_precedence>"),
                            1 => ("<society_knight_number>" + col[1].Trim() + "</society_knight_number>"),
                            2 => ("<society_master_number>" + col[2].Trim() + "</society_master_number>"),
                            3 => ("<name>" + col[3].Trim()                  + "</name>"),
                            4 => ("<type>" + col[4].Trim()                  + "</type>"),
                            5 => ("<date_elevated>" + col[5].Trim()         + "</date_elevated>"),
                            6 => ("<anno_societatous>" + col[6].Trim()      + "</anno_societatous>"),
                            7 => ("<kingdom_of_elevation>" + col[7].Trim()  + "</kingdom_of_elevation>"),
                            8 => ("<kingdom_precedence>" + col[8].Trim()    + "</kingdom_precedence>"),
                            9 => ("<resigned_or_removed>" + col[9].Trim()   + "</resigned_or_removed>"),
                            10 => ("<passed_away>" + col[10].Trim()         + "</passed_away>"),
                            11 => ("<notes>" + col[11].Trim()               + "</notes>"),
                            _ => string.Empty
                        };

                        string replaced = outline.Replace("\"", string.Empty);

                        sw.WriteLine(replaced);
                    }

                    const string knightend = "</knight>";
                    sw.WriteLine(knightend);
                    Debug.WriteLine("CreateWbXmlJob.DoWork() - " + col[3].Trim());
                    Console.WriteLine(col[3].Trim());
                }

                //close the file
                sr.Close();
                sw.Flush();
                sw.Close();

                Debug.WriteLine("CreateWbXmlJob.DoWork() - COMPLETE - Created File: "   + _xmlPathAndFilenameOut);
                Console.WriteLine("CreateWbXmlJob.DoWork() - COMPLETE - Created File: " + _xmlPathAndFilenameOut);
            }
            catch (Exception e)
            {
                Debug.WriteLine("CreateWbXmlJob.DoWork() - Exception: " + e.Message);
            }
            finally
            {
                Debug.WriteLine("CreateWbXmlJob.DoWork() - Executing finally block.");
            }

            return 0;
        }
    }
}