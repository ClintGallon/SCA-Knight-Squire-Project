﻿using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;

namespace CreateKnightSquireXml
{
    internal static class Program
    {
        public static void Main(string[] args)
        {

            string JobType = String.Empty;

            if (args.Length == 0)
            {
                Console.WriteLine("sca-ksp is the command line utility for the SCA-Knight-Squire-Project to get a list of commands please enter 'sca-ksp -commands'");
            }

            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-commands":
                        Console.WriteLine("sca-ksp commands: ");
                        Console.WriteLine("     2) -prepcsv -csv 'pathAndFilename'");
                        Console.WriteLine("     This preps the csv file by making sure the first row is just the column names we are expecting. ");
                        Console.WriteLine(" ");
                        Console.WriteLine("     2) -createwbxml -csv 'wbPathAndFilename.csv' -out 'wbPathAndFilename.xml' ");
                        Console.WriteLine("     This creates the out file.");
                        Console.WriteLine(" ");
                        Console.WriteLine("     3) -merge -wbxml 'wbPathAndFilename.xml' -relationships 'relationshipsPathAndFilename.xml' -out 'outputPathAndFilename.xml' ");
                        Console.WriteLine("     This merges the relationships.xml file with the whitebelt spreadsheet xml creating the file which will output the resulting HTML");
                        Console.WriteLine(" ");
                        Console.WriteLine("     4) -createhtml -mergedrandwb 'mergedRelationshipsWhiteBelt.xml' -xsl 'stylesheet.xsl' -outputhtml 'outPathAndFilename.html' ");
                        Console.WriteLine("     This creates the resulting html file in output filename");
                        Console.WriteLine(" ");
                        Console.WriteLine("     5) -missing -wbxml 'wbPathAndFilename.xml' -relationships 'relationshipsPathAndFilename.xml'");
                        Console.WriteLine("     This walks all names in the whitebelt spreadsheet xml (SCA_ChivList-Latest.xml) then looks up the names in the ks_relationship.xml. If the name is not found it puts the name in a missing.txt file in the data directory.");
                        Console.WriteLine(" ");
                        Console.WriteLine("     6) -addrelationships -newrelss 'pathandfilename' ");
                        Console.WriteLine("     This walks all relationships in the new relationships spreadsheet finds the knights in ks_relationship.xml and adds the squires appropraitely if the squire isnt already there.");
                        Console.WriteLine(" ");
                        break;
                    default:
                        Console.WriteLine("'sca-ksp -commands' for all the commands you can run.");
                        break;

                }
            }

            if ((args[0].ToLower() == "-prepcsv") && (args[1].ToLower() == "-csv"))
            {
                string csvPathAndFilename = args[2].ToLower();
                PrepCsvJob job = new PrepCsvJob(csvPathAndFilename);
                int ret = job.DoWork();

                Environment.Exit(ret);
            }

            if ((args[0].ToLower() == "-createwbxml") && (args[1].ToLower() == "-csv") && (args[3].ToLower() == "-out"))
            {
                string csvPathAndFilename = args[2].ToLower();
                string xmlPathAndFilenameOut = args[4].ToLower();
                CreateWbXmlJob job = new CreateWbXmlJob(csvPathAndFilename, xmlPathAndFilenameOut);
                int ret = job.DoWork();

                Environment.Exit(ret);
            }

            if ((args[0].ToLower() == "-merge") && (args[1].ToLower() == "-wbxml") && (args[3].ToLower() == "-relationships") && (args[5].ToLower() == "-out"))
            {
                string wbPathAndFilename = args[2].ToLower();
                string relPathAndFilename = args[4].ToLower();
                string outPathAndFilename = args[6].ToLower();
                
                MergeJob job = new MergeJob(wbPathAndFilename, relPathAndFilename, outPathAndFilename);
                int ret = job.DoWork();

                Environment.Exit(ret);
            }

            if ((args[0].ToLower() == "-createhtml") && (args[1].ToLower() == "-xsl") && (args[3].ToLower() == "-xml") && (args[5].ToLower() == "-out"))
            {
                string xslPathAndFilename  = args[2].ToLower();
                string xmlPathAndFilename = args[4].ToLower();
                string htmlPathAndFilename = args[6].ToLower();
                CreateHtmlJob job = new CreateHtmlJob(xslPathAndFilename, xmlPathAndFilename, htmlPathAndFilename);
                int      ret = job.DoWork();

                Environment.Exit(ret);
            }
            
            if ((args[0].ToLower() == "-missing") && (args[1].ToLower() == "-wbxml") && (args[3].ToLower() == "-relationships") )
            {
                string wbPathAndFilename  = args[2].ToLower();
                string relPathAndFilename = args[4].ToLower();
                string outPathAndFilename = args[6].ToLower();
                MissingJob job = new MissingJob(wbPathAndFilename, relPathAndFilename, outPathAndFilename);
                int           ret = job.DoWork();

                Environment.Exit(ret);
            }
            
        }
    }
}