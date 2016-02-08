using System;
using System.IO;
using System.Collections;

namespace Differ
{
    public class Compare : CommandProcess
    {        
        public void process(string[] command)
        {            
            int i=0,j=0; //indexs for loops;
            ArrayList firstFile = new ArrayList(), secondFile=new ArrayList(); //arrays for saving files data;        
            try
            {
                //it is reading the first text file;
                StreamReader textFile = new StreamReader(command[1]);
                while (textFile.Peek() != -1)
                    firstFile.Add(textFile.ReadLine());
                textFile.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File " + command[1] + " did not find. It can not be continued");
                return;
            }
            try
            {
                //it is reading the seconf text file;                
                StreamReader textFile = new StreamReader(command[2]);
                while (textFile.Peek() != -1)
                    secondFile.Add(textFile.ReadLine());
                textFile.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File " + command[1] + " did not find. It can not be continued");
                return;
            }

            File.Delete(command[3]); //deleting file if it exists;
            StreamWriter diffFile = null;
            //it is removing files and dirs names do not have differents
            for (i = 0; i < firstFile.Count; i++)
            {
                for (j = 0; j < secondFile.Count; j++)
                    if ((firstFile[i].ToString().Split('|')[0] == secondFile[j].ToString().Split('|')[0]) && (firstFile[i].ToString().Split('|')[1] == secondFile[j].ToString().Split('|')[1]))
                    {
                        firstFile.RemoveAt(i);                        
                        secondFile.RemoveAt(j);
                        j--;
                    }                
            }            
            //modified files and dirs
            for (i = 0; i < firstFile.Count; i++)
                for (j = 0; j < secondFile.Count; j++)
                    if ((firstFile[i].ToString().Split('|')[0] == secondFile[j].ToString().Split('|')[0]) && (firstFile[i].ToString().Split('|')[1] != secondFile[j].ToString().Split('|')[1]))
                    {
                        diffFile = File.AppendText(command[3]);
                        diffFile.WriteLine("Modified " + firstFile[i].ToString().Split('|')[0]);
                        diffFile.Close();
                        firstFile.RemoveAt(i);                        
                        secondFile.RemoveAt(j);
                        j--;
                    }              
            //Removed files and dirs
            for (i = 0; i < firstFile.Count; i++)
            {
                diffFile = File.AppendText(command[3]);
                diffFile.WriteLine("Removed " + firstFile[i].ToString().Split('|')[0]);
                diffFile.Close();
            }
            //Created files and dirs
            for (j = 0; j < secondFile.Count; j++)
            {
                diffFile = File.AppendText(command[3]);
                diffFile.WriteLine("Created " + secondFile[j].ToString().Split('|')[0]);
                diffFile.Close();
            } 

            if (File.Exists(command[3]))
                Console.WriteLine("File "+command[3]+" was created.");
            else
                Console.WriteLine("File " + command[3]+" was not created.");
        }
    }
}
