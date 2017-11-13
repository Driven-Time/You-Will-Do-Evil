using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Launcher
{
    class Config
    {
        /// <summary>
        /// Saves the specified setting, provided by the parameter, to a .txt file.
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

                Writer.WriteLine (_toSave + ":" + _value + "\n");
                //MessageBox.Show ($"{_toSave} = {_value}");
                Writer.Close ();
            }
        }

        /// <summary>
        /// Loads a specified applied setting from the <see cref="SaveSetting(string, string)"/> generated file.
        /// </summary>
        /// <param name="_toLoad">Represents the keyword to look for</param>
        /// <returns></returns>
        public static bool LoadSettings (char[] _toLoad)
        {
            try
            {
                string toLoad = ReadAndReturn (_toLoad).TrimStart (_toLoad);
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
        /// Reads a file and returns it content as a string
        /// </summary>
        /// <param name="_toSearchFor"></param>
        /// <returns></returns>
        private static string ReadAndReturn (char[] _toSearchFor)
        {
            char[] buffer;
            using (var Reader = new StreamReader ("LauncherSetting.txt"))
            {
                buffer = new char[(int)Reader.BaseStream.Length];   //  Creating Char array of the size of the file.
                Reader.ReadBlock (buffer, 0, (int)Reader.BaseStream.Length);

                //  Converting file content from Char[] to string
                string toString = "";
                for (int i = 0; i < buffer.Length; i++)
                {
                    toString += buffer[i].ToString ();
                }

                //MessageBox.Show ($"ToString = {toString}");

                //  Converting Keyword to search for from Char[] to string
                string toSearhForString = "";
                for (int i = 0; i < _toSearchFor.Length; i++)
                {
                    toSearhForString += _toSearchFor[i];
                }

                int index = toString.IndexOf (toSearhForString);    //  Getting the first accurence of the Keyword to search for in the file and returns it's index value
                //MessageBox.Show ($"INdex = {index}");

                //  Formatting result
                string result = "";
                for (int i = index; i < 5 + index; i++)
                {
                    result += toString[i];
                }

                //MessageBox.Show ($"Result = {result}");


                Reader.Close ();

                return result;
            }
        }
    }
}
