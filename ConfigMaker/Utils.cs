using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfigMaker
{
    public static class Utils
    {
        public static string CreateTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        public static bool SafelyDeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName)){
                    File.Delete(fileName);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Debug("Failed to delete " + fileName, e);
                return false;
            }
        }
        public static bool SafelyDeleteFolder(DirectoryInfo directory, bool recursive = true)
        {
            try
            {
                if (directory.Exists)
                {
                    Directory.Delete(directory.FullName, recursive);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Debug("Failed to delete " + directory, e);
                return false;
            }
        }
        public static bool IsValidFilename(string testName)
        {
            try
            {
                var path = System.IO.Path.GetFullPath(testName);
                var fileName = System.IO.Path.GetFileName(testName);
                string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
                Regex regFixFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
                if (regFixFileName.IsMatch(fileName)) { return false; };
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

    }
}
