using System;
using System.Diagnostics;

namespace CreateKnightSquireXml
{
    public class CreateHtmlJob
    {
        private readonly string _pathFilenameOutputHtml;
        private readonly string _pathFilenameXml;
        private readonly string _pathFilenameXsl;

        public CreateHtmlJob(string xslPathAndFilename, string xmlPathAndFilename, string htmlPathAndFilename)
        {
            _pathFilenameXsl        = xslPathAndFilename;
            _pathFilenameXml        = xmlPathAndFilename;
            _pathFilenameOutputHtml = htmlPathAndFilename;
        }

        public int DoWork()
        {
            var firstProc = new Process
                            {
                                StartInfo           = {FileName = "C:\\windows\\msxsl.exe", Arguments = _pathFilenameXml + " " + _pathFilenameXsl + " -o " + _pathFilenameOutputHtml, WindowStyle = ProcessWindowStyle.Normal},
                                EnableRaisingEvents = true
                            };

            firstProc.Start();
            firstProc.WaitForExit();

            //You may want to perform different actions depending on the exit code.
            Console.WriteLine("Process exited: " + firstProc.ExitCode);

            return firstProc.ExitCode;
        }
    }
}