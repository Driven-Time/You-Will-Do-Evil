using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using System.Windows;
using System.Windows.Markup;
using System.Diagnostics;
using System.IO;

/*Google Link
    https://drive.google.com/uc?export=download&id=1x0z39umiGw0bDKapBlpY_PmV3U4ahJXE

    https://drive.google.com/uc?export=download&id=1VTPnslFXPYTTKt0jJbq2JRh2Kx8Cxm4t
*/

namespace YWDE_Launcher
{
    static class Patching
    {
        /// <summary>
        /// Checks for updates --> Downloads Update (If any) --> Launches installer
        /// </summary>
        public static void Update ()
        {
            if (!VersionCheck())
            {
                MessageBox.Show ("There's an Update available. Click OK to proceed (This might take a little while)");
                Download ("https://drive.google.com/uc?export=download&id=0BzsykeCvI9XgZzFPZ1B3MWtqY0k", "YWDE.exe");
                MessageBox.Show ("Please relaunch the application after installation");
                Process.Start ($"{AppDomain.CurrentDomain.BaseDirectory}/Temp/YWDE.exe");
                /*DO NOT DELETE FILES! LAUNCH THE UNINSTALLER!*/

                Process.Start ("CleanUp.exe");
                Environment.Exit (0);

            }
        }

        /// <summary>
        /// Checks for updates
        /// </summary>
        /// <returns>True if any update is available</returns>
        public static bool VersionCheck ()
        {
            Download ("https://drive.google.com/uc?export=download&id=1VTPnslFXPYTTKt0jJbq2JRh2Kx8Cxm4t", "Version.txt");
            string version = Launcher.ReadAndReturn ($"{AppDomain.CurrentDomain.BaseDirectory}Temp/Version.txt");

            MessageBox.Show ($"Local client version: {Launcher.ClientVersion} <|> Remote client version: {version}");

            if (ConvertToInt(Launcher.ClientVersion) < ConvertToInt(version))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Converts a string to an int
        /// </summary>
        /// <param name="_toConvert">The string to convert</param>
        /// <returns></returns>
        private static int ConvertToInt (string _toConvert)
        {
            string NewString = "";
            for (int i = 0; i < _toConvert.Length; i++)
            {
                if (_toConvert[i] == '.')
                {
                    continue;
                }
                else
                {
                    NewString += _toConvert[i];
                }
            }

            //MessageBox.Show (NewString);

            int result = int.Parse (NewString);

            return result;
        }

        /// <summary>
        /// Donwloads the Update
        /// </summary>
        /// <param name="_source">The source of the Update</param>
        /// <param name="_name">The name the download should be stored with</param>
        public static void Download (string _source, string _name)
        {
            DriveService service = new DriveService ();
            string path = ($"{AppDomain.CurrentDomain.BaseDirectory}Temp/{_name}");
            //string filePath = @"C:\Users\Mike\Downloads\YWDE.exe";
            
            var stream = service.HttpClient.GetStreamAsync (_source);
            var result = stream.Result;


            if (stream.IsCompleted)
            {
                MessageBox.Show ($"Downloaded {path} with succes");
            }


            using (var fileStream = System.IO.File.Create (path))
            {
                result.CopyTo (fileStream);
            }
        }

        /// <summary>
        /// Deletes all files in at the specified path
        /// </summary>
        /// <param name="_root"></param>
        public static void DeleteFiles (string _root)
        {
            List<FileInfo> fileList = new List<FileInfo> ();
            fileList = FindFile (_root);

            foreach (FileInfo file in fileList)
            {
                file.Delete ();
                MessageBox.Show ($"Deleted: {file.Name}");
            }

            
        }

        /// <summary>
        /// Collects a list of all files in a folder
        /// </summary>
        /// <param name="_path">The end folder of the Root</param>
        /// <returns>A list of FileInfo</returns>
        private static List<FileInfo> FindFile (string _path)
        {
            //C:\Users\Mike\Documents\TestFolderDelete
            List<FileInfo> _fileList = new List<FileInfo> ();
            string path = ($"{AppDomain.CurrentDomain.BaseDirectory}{_path}");
            DirectoryInfo folder = new DirectoryInfo (path);
            FileInfo[] files = folder.GetFiles ("*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                _fileList.Add (file);
            }

            return _fileList;
        }
    }
}
