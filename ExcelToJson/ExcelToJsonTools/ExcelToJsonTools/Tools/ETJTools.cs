using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace ExcelToJsonTools
{
    class ETJTools
    {
        /// <summary>
        /// 得到DataTable的方法
        /// </summary>
        /// <param name="filePath">传入Excel所在的路径</param>
        /// <param name="tableName">传入Table的name</param>
        /// <returns></returns>
        public static DataTable GetTable(string filePath, string tableName)
        {
            DataTable table = new DataTable(tableName);
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream fStream = File.OpenRead(filePath))
                    {
                        IWorkbook workBook = WorkbookFactory.Create(fStream);
                        ISheet sheet1 = workBook.GetSheetAt(0);
                        IRow headRow = sheet1.GetRow(0);
                        int cellCount = headRow.LastCellNum;
                        for (int m = headRow.FirstCellNum; m < cellCount; m++)
                        {
                            string headCellValue = headRow.GetCell(m).StringCellValue;
                            table.Columns.Add(headCellValue);
                        }
                        int rowCount = sheet1.LastRowNum;
                        for (int i = sheet1.FirstRowNum + 1; i < rowCount + 1; i++)
                        {
                            DataRow dataRow = table.NewRow();
                            IRow row = sheet1.GetRow(i);
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            table.Rows.Add(dataRow);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("文件可能被占用-path:"+filePath);
                    Console.WriteLine(e.ToString());
                    Console.Write("按任意键退出...");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }
                
            }
            return table;
        }
        /// <summary>
        /// 得到json字符串的类
        /// </summary>
        /// <param name="dt">传入要生成json字符串的Table</param>
        /// <returns></returns>
        public static string GetJsonStr(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            int count = dt.Rows.Count;
            if (count != 0)
            {
                jsonBuilder.Append("{");
                jsonBuilder.Append("\"");

                jsonBuilder.Append(dt.TableName);
                jsonBuilder.Append("\"");
                jsonBuilder.Append(":");
                jsonBuilder.Append("[");
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    
                    jsonBuilder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string colName = dt.Columns[j].ColumnName;
                        jsonBuilder.Append("");
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(colName);
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(":");

                        if (dt.Rows[i][j] == null)
                        {
                            Console.WriteLine("第{0}行{1}列的数据为空，请检查对应表格", i + 1, j + 1);
                            Console.Write("按任意键退出...");
                            Console.ReadKey(true);
                            Environment.Exit(0);
                        }

                        string content = dt.Rows[i][j].ToString();

                        string typeName = dt.Rows[0][j].ToString();

                        switch (typeName) 
                        {
                            case "int":
                            case "long":
                            case "double":
                            case "float":
                                jsonBuilder.Append(content);
                                break;
                            case "string":
                                jsonBuilder.Append("\"" + content + "\"");
                                break;
                            case "string[]":
                                jsonBuilder.Append("[");
                                string[] strList = content.Split(",");
                                foreach (var str in strList)
                                {
                                    jsonBuilder.Append("\"" + str + "\"");
                                    jsonBuilder.Append(",");
                                }
                                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                                jsonBuilder.Append("]");
                                break;
                            case "int[]":
                            case "long[]":
                            case "double[]":
                            case "float[]":
                                jsonBuilder.Append("[");
                                string[] numList = content.Split(",");
                                foreach (var str in numList)
                                {
                                    jsonBuilder.Append(str);
                                    jsonBuilder.Append(",");
                                }
                                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                                jsonBuilder.Append("]");
                                break;
                        }
                        jsonBuilder.Append(",");
                    }
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetCSClassStr(DataTable dt, string dataClassName, string className, string jsonFilePath)
        {
            StringBuilder classBuilder = new StringBuilder();
            int count = dt.Rows.Count;
            if (count != 0)
            {
                classBuilder.Append("using System.Collections.Generic;\n");
                classBuilder.Append("using UnityEngine;\n");
                classBuilder.Append("\nnamespace Assets.ConfigData\n");
                classBuilder.Append("{\n");
                classBuilder.Append("\t[System.Serializable]\n");
                classBuilder.Append("\tpublic class ");
                classBuilder.Append(dt.TableName);
                classBuilder.Append("\n");
                classBuilder.Append("\t{\n");
                //按列生成字段
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colName = dt.Columns[i].ColumnName;
                    string typeName = dt.Rows[0][i].ToString();
                    classBuilder.Append("\t\tpublic ");
                    switch (typeName)
                    {
                        case "int":
                        case "long":
                        case "double":
                        case "float":
                        case "string":
                            classBuilder.Append(typeName);
                            break;
                        case "string[]":
                        case "int[]":
                        case "long[]":
                        case "double[]":
                        case "float[]":
                            classBuilder.Append("List<");
                            classBuilder.Append(typeName);
                            classBuilder.Remove(classBuilder.Length - 1, 1);
                            classBuilder.Remove(classBuilder.Length - 1, 1);
                            classBuilder.Append(">");
                            break;
                    }
                    classBuilder.Append(" " + colName);
                    classBuilder.Append(";");
                    classBuilder.Append("\n");
                }
                classBuilder.Append("\t}\n\n");
                //List字段
                classBuilder.Append("\t[System.Serializable]\n");
                classBuilder.Append("\tpublic class " + dataClassName + "\n");
                classBuilder.Append("\t{\n");
                classBuilder.Append("\t\tpublic List<" + dt.TableName + "> " + dt.TableName + ";\n");
                classBuilder.Append("\t}\n\n");

                classBuilder.Append("\tpublic class " + className + "\n");
                classBuilder.Append("\t{\n\n");
                //instance
                classBuilder.Append("\t\tstatic " + dataClassName + " Instance;\n\n");
                //Dictionary
                //第一个属性作为索引
                string firstType = dt.Rows[0][0].ToString();
                classBuilder.Append("\t\tstatic Dictionary<" + firstType + ", " + dt.TableName + "> Dict;\n\n");
                //Get()
                classBuilder.Append("\t\tpublic static " + dt.TableName + " Get(" + firstType + " Idx)\n");
                classBuilder.Append("\t\t{\n");
                classBuilder.Append("\t\t\tif (Instance == null)\n");
                classBuilder.Append("\t\t\t\tReadJsonInfo();\n");
                classBuilder.Append("\t\t\tif(!Dict.ContainsKey(Idx))\n");
                classBuilder.Append("\t\t\t{\n");
                classBuilder.Append("\t\t\t\tTS.error(\"未从表" + dataClassName + "找到该索引: \"+Idx);\n");
                classBuilder.Append("\t\t\t}\n");
                classBuilder.Append("\t\t\treturn Dict[Idx];\n");
                classBuilder.Append("\t\t}\n");
                //GetList()
                classBuilder.Append("\t\tpublic static List<" + dt.TableName + "> GetList()\n");
                classBuilder.Append("\t\t{\n");
                classBuilder.Append("\t\t\tif (Instance == null)\n");
                classBuilder.Append("\t\t\t\tReadJsonInfo();\n");
                classBuilder.Append("\t\t\treturn Instance." + dt.TableName + ";\n");
                classBuilder.Append("\t\t}\n");
                //ReadJsonInfo()
                classBuilder.Append("\t\tstatic void ReadJsonInfo()\n");
                classBuilder.Append("\t\t{\n");
                classBuilder.Append("\t\t\tTextAsset text = Resources.Load<TextAsset>(\"" + jsonFilePath + "\");\n");
                classBuilder.Append("\t\t\tInstance = ConfigUtils.ReadJson<" + dataClassName + ">(text.text);\n");
                classBuilder.Append("\t\t\tDict = new Dictionary<" + firstType + ", " + dt.TableName + ">();\n");
                classBuilder.Append("\t\t\tforeach(var item in Instance."+dt.TableName+")\n");
                classBuilder.Append("\t\t\t{\n");
                classBuilder.Append("\t\t\t\tDict.Add(item."+dt.Columns[0].ColumnName+", item);\n");
                classBuilder.Append("\t\t\t}\n");
               classBuilder.Append("\t\t}\n");
                classBuilder.Append("\t}\n");
                classBuilder.Append("}");
            }
            return classBuilder.ToString();
        }
    }
}
