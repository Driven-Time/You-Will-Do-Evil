using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CleanUp
{
    class Program
    {
        const string VERSION = "1.0.1.0";
        static void Main (string[] args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo ();
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";

            FileManagement.DeleteFiles ("");
        }
    }
}
