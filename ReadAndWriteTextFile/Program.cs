using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ReadAndWriteTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var debugDirectory = System.Environment.CurrentDirectory;  // 获取当前Debug目录
            var sourceDirectory = Directory.GetParent(debugDirectory).Parent.FullName.ToString();
            
            var fileName = "Cylinder025.txt";
            var filePath = Path.Combine(sourceDirectory, "Files", fileName);
            var originLines = File.ReadLines(filePath);

            var AllLines = new List<string>();
            AllLines.Clear();
            foreach (string line in originLines)
            {
                AllLines.Add(line);
            }

            var fileName2 = "Cylinder500.txt";
            string path = Path.Combine(sourceDirectory, "Files", fileName2); 
            try
            {
                if (File.Exists(path))
                {
                    // 如果文件为可读属性，则将其设为一般属性，然后删除
                    FileInfo fs = new FileInfo(path);
                    if (fs.IsReadOnly)
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                    }

                    File.Delete(path);
                }

                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < AllLines.Count / 20; i++)
                    {
                        string temp = AllLines[20 * i];
                        sw.WriteLine(temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }

    public class TextFile
    {
        public static void ClearTextContent(string txtPath)
        {
            FileStream stream = File.Open(txtPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();
        }

        public static void SaveTextContent(string txtPath, string content, bool saOrAp = false)
        {
            StreamWriter sw = new StreamWriter(txtPath, saOrAp); // True: Append, False: overwrite
            sw.WriteLine(content);
            sw.Close();
        }

        public static void ClearFolderFiles(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            // Delete files
            foreach (var fs in dInfo.GetFiles())
            {
                var filePath = Path.Combine(path, fs.Name.ToString());

                if (fs.IsReadOnly)
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                }

                File.Delete(filePath);
            }
        }

        public static string GetVersionInformation(string filePath)
        {
            //Assembly assembly = Assembly.LoadFile(filePath);			
            //var text =  assembly.GetName().Version.ToString();

            FileVersionInfo info = FileVersionInfo.GetVersionInfo(filePath);
            return info.FileVersion;
        }

        public static string GetEnvironmentVariable()
        {
            // 环境变量 TEMP
            return Environment.GetEnvironmentVariable("TEMP");
        }
    }
}
