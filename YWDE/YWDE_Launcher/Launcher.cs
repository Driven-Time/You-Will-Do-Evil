using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace YWDE_Launcher
{
    class Launcher
    {
        public const string Version = "1.2.1.1";
        public const string ClientVersion = "1.4.2.0";
        /// <summary>
        /// Launches the provided Executable file
        /// </summary>
        /// <example>
        /// This sample will show how to use the <see cref="LaunchGame(CheckBox, string)"/> method
        /// <code>
        /// void LaunchGame (true, "SampleName");
        /// </code>
        /// </example>
        /// <param name="_closeOnLaunch">Represents a boolean value specifeing wether to close on Load or not</param>
        /// <param name="_fileName">Represents the name of the executable file, which should be launched (.exe is provided)</param>
        public static void LaunchGame (CheckBox _closeOnLaunch, string _fileName)
        {
            try
            {
                SaveSetting ("COL" ,_closeOnLaunch.IsChecked.Value.ToString(), false);
                SaveSetting ("OTL", true.ToString(), true);

                Process.Start ($"{_fileName}.exe");

                if (_closeOnLaunch.IsChecked == true)
                {
                    Environment.Exit (0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show ($"ERROR: No executable file named '{_fileName}.exe' was found!");
                return;
            }
        }

        /// <summary>
        /// This will Launch a Website
        /// </summary>
        /// <param name="_url">The URL for the Website</param>
        public static void LaunchWebsite (string _url)
        {
            Process.Start (_url);
        }

        /// <summary>
        /// Save the specified setting, provided by the parameter, to a .txt file.
        /// </summary>
        /// <param name="_toSave">Represents the Keyword to save setting as</param>
        /// <param name="_value">Represents the value of the setting to save</param>
        /// <param name="_append">If true the writer will append to an existing Textfile</param>
        public static void SaveSetting (string _toSave, string _value, bool _append)
        {
            using (StreamWriter Writer = new StreamWriter ("LauncherSetting.txt", _append))
            {
                if (_value == "True")
                {
                    _value = "1";
                }
                else
                {
                    _value = "0";
                }

                Writer.WriteLine ($"{_toSave}:{_value}\n");
                //MessageBox.Show ($"{_toSave} = {_value}");
                Writer.Close ();
            }
        }

        /// <summary>
        /// Loads all currently applied settings from the <see cref="SaveSetting(string, string)"/> generated file.
        /// </summary>
        /// <param name="_toLoad">Represents the keyword to look for</param>
        /// <returns></returns>
        public static bool LoadSettings (char[] _toLoad)
        {
            try
            {
                string toLoad = ReadAndReturn ("LauncherSetting.txt" ,_toLoad).TrimStart (_toLoad);
                //MessageBox.Show ($"{_toLoad} = {toLoad}");

                if (toLoad == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read and returns a string from a file
        /// </summary>
        /// <param name="_searchIn">Represents the file search in</param>
        /// <param name="_toSearchFor">Represents the sequens of characters to search for</param>
        /// <returns></returns>
        public static string ReadAndReturn (string _searchIn , char[] _toSearchFor = null)
        {
            char[] buffer;
            using (var Reader = new StreamReader (_searchIn))
            {
                buffer = new char[(int)Reader.BaseStream.Length];
                Reader.ReadBlock (buffer, 0, (int)Reader.BaseStream.Length);

                string toString = "";
                for (int i = 0; i < buffer.Length; i++)
                {
                    toString += buffer[i].ToString ();
                }

                //MessageBox.Show ($"ToString = {toString}");
                string result = "";
                if (_toSearchFor != null)
                {
                    string toSearhForString = "";
                    for (int i = 0; i < _toSearchFor.Length; i++)
                    {
                        toSearhForString += _toSearchFor[i];
                    }

                    int index = toString.IndexOf (toSearhForString);
                    //MessageBox.Show ($"INdex = {index}");

                   
                    for (int i = index; i < 5 + index; i++)
                    {
                        result += toString[i];
                    }
                }
                else
                {
                    result = toString;
                }


                

                //MessageBox.Show ($"Result = {result}");


                Reader.Close ();

                return result;
            }
        }
    }
}
