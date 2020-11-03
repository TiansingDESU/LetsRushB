using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ExcelToJsonTools
{
    class Program
    {
        static string ProgrmeConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"PathConfig.txt";
        static void Main(string[] args)
        {

            string[] paths = FileTools.GetConfigTxt(ProgrmeConfigPath);

            string dirPath = @paths[0];
            string jsonOutputPath = @paths[1];
            string csOutputPath = @paths[2];

            string readTail = "xlsx";

            Console.WriteLine("DirPath : " + dirPath);
            Console.WriteLine("JsonOutPutPath : "+jsonOutputPath);
            Console.WriteLine("csOutPutPath : "+csOutputPath);

            List<FileInfo> excelFileList = FileTools.GetFilesByDir(dirPath, readTail);
            foreach(var file in excelFileList)
            {
                Console.WriteLine("read excel file : "+file.Name);
                string filePath = dirPath + file.Name;
                string fileName = file.Name.Split(".")[0];
                string JsonFileName = "Config" + fileName;
                DataTable newTable = ETJTools.GetTable(filePath, "DUnit"+fileName);
                
                string jsonOutPut = ETJTools.GetJsonStr(newTable);

                string className = "Conf" + fileName;
                string classOutPut = ETJTools.GetCSClassStr(newTable, fileName, className, "ConfigData/" + JsonFileName);

                FileTools.CreateFile(jsonOutPut, jsonOutputPath, JsonFileName, "json");
                FileTools.CreateFile(classOutPut, csOutputPath, className, "cs");
            }
            Console.WriteLine("Write Complete!\n\n\n");
            Console.Write("按任意键退出...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        
    }
}
