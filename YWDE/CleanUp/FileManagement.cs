using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace CleanUp
{
    class FileManagement
    {
        /// <summary>
        /// Deletes all files in the rootfolder (Note: Do not alter)
        /// </summary>
        /// <param name="_root"></param>
        public static void DeleteFiles (string _root)
        {
            List<FileInfo> fileList = new List<FileInfo> ();
            fileList = FindFile (_root);

            foreach (FileInfo file in fileList)
            {
                file.Delete ();
            }


        }

        /// <summary>
        /// Collects all files located in the root folder (Exception: Temp folder and CleanUp.exe)
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private static List<FileInfo> FindFile (string _path)
        {
            List<FileInfo> _fileList = new List<FileInfo> ();
            string path = ($"{AppDomain.CurrentDomain.BaseDirectory}{_path}");
            DirectoryInfo folder = new DirectoryInfo (path);
            FileInfo[] files = folder.GetFiles ("*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                if (file.Directory.Name == "Temp" || file.Name == "CleanUp.exe")
                {
                    Console.WriteLine ($"Exception: {file.Name}");
                    continue;                 
                }
                else
                {
                    Console.WriteLine ($"Deleting: {file.Name} in {file.Directory.Name}");
                    _fileList.Add (file);
                }
            }

            return _fileList;
        }
    }
}
