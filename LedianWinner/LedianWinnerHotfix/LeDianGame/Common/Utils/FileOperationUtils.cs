using BZFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileOperationUtils : Singleton<FileOperationUtils> {

    public static string FileRead(string path)
    {
        path = Application.persistentDataPath + "/" + path;
        DebugUtils.DebugerExtension.Log(path);
        if (File.Exists(path))
        {
            string fileContent = "";
            try
            {
                FileStream file = new FileStream(path, FileMode.Open);
                StreamReader streamReader = new StreamReader(file);
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    fileContent += line;
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (Exception)
            {
                return null;
            }
            return fileContent;
        }
        else
        {
            return null;
        }
    }

    public static void FileWrite(string path,string content)
    {
    
        path = Application.persistentDataPath + "/" + path;
        DebugUtils.DebugerExtension.Log(path);
        try
        {
            FileStream file = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(file);
            sw.Write(content);
            sw.Flush();
            sw.Close();

        }
        catch (Exception)
        {

           
        }
  

    }
}
