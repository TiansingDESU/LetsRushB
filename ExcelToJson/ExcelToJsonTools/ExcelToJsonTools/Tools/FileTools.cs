using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExcelToJsonTools
{
    class FileTools
    {
        public static List<FileInfo> GetFilesByDir(string path, string tail)
        {
            if (!Directory.Exists(path)) //判断文件夹是否存在
            {
                Console.WriteLine("文件夹不存在!!!! path = " + path);
                Console.Write("按任意键退出...");
                Console.ReadKey(true);
                Environment.Exit(0);
                return null;
            }
            DirectoryInfo di = new DirectoryInfo(path);
            //找到该目录下的文件
            FileInfo[] fi = di.GetFiles();
            //把FileInfo[]数组转换为List    
            List<FileInfo> list = new List<FileInfo>();
            for (int i = 0; i < fi.Length; i++)
            {
                string filestr = fi[i].Name;
                if ((filestr.Substring(filestr.LastIndexOf(".") + 1)) == tail)
                {
                    list.Add(fi[i]);
                }
            }
            return list;
        }

        public static string[] GetConfigTxt(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("配置文件不存在，手动创建TXT（第一行表格 path，第二行json path，第三行 c# path）!!!! path = " + path);
                Console.Write("按任意键退出...");
                Console.ReadKey(true);
                Environment.Exit(0);
                return null;
            }
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string excelPath = sr.ReadLine();
            string jsonPath = sr.ReadLine();
            string csPath = sr.ReadLine();
            if(excelPath == null || jsonPath == null || csPath == null)
            {
                Console.WriteLine("三行path配置有缺失，请假查");
                Console.Write("按任意键退出...");
                Console.ReadKey(true);
                Environment.Exit(0);
                return null;
            }
            return new string[] { excelPath, jsonPath, csPath };
        }

        public static void CreateFile(string jsonStr, string path, string fileName, string tail)
        {
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //生成json文件，将字符串数据保存到json文件
            string postPath = path + fileName + "." + tail;//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(jsonStr.ToString());
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
    }
}
