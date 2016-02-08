using System;
using System.IO;

namespace Differ
{
    public class Snapshot : CommandProcess
    {
        string[] command;
        StreamWriter textFile = null;
        public void process(string[] command)
        {
            this.command = command;
            DirectoryInfo mainDir = new DirectoryInfo(command[1]); //Checking folder;
            File.Delete(command[2]); //deleting file if it exists;
            
            WalkDirectoryTree(mainDir);

            if (File.Exists(command[2]))
                Console.WriteLine("File " + command[2].ToString() + " was created.");
            else
                Console.WriteLine("File " + command[2].ToString() + " was not created");            
        }

        public void WalkDirectoryTree(DirectoryInfo mainDir)
        {
            FileInfo[] subFiles = null;
            DirectoryInfo[] subDirs = null;           

            try
            {
                subFiles = mainDir.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.                
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (subFiles != null)
            {
                foreach (FileInfo fi in subFiles)
                {
                    textFile=File.AppendText(command[2]);
                    textFile.WriteLine(fi.FullName + "|" + File.GetLastWriteTime(fi.FullName));                    
                    textFile.Close();
                }
                subDirs = mainDir.GetDirectories();
                foreach (DirectoryInfo dirInfo in subDirs)
                {                       
                    textFile=File.AppendText(command[2]);
                    textFile.WriteLine(dirInfo.FullName + "|" + File.GetLastWriteTime(dirInfo.FullName));                       
                    textFile.Close();
                    // Recursive call for each subdirectory.  
                    WalkDirectoryTree(dirInfo);
                }                
            }            
        }
    }
}
