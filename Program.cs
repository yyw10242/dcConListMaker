using System.IO;
using System.Collections.Generic;

namespace DCConList_Maker
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string jsHeadWords = "dcConsData = [";
            string jsTailWords = "]";
            string jsonHeadWords = "{\"dccons\":[";
            string jsonTailWords = "]}";
        string targetDirectory;

            StreamWriter jsWriter;
            StreamWriter jsonWriter;

            FileStream dcconListJs;
            FileStream dcconListJson;

            FileStream dcConMakerSettings;

            StreamReader reader;

            string resultPath;
            

            if (File.Exists("dcConMakerSettings.txt"))
            {
                dcConMakerSettings = File.OpenRead("dcConMakerSettings.txt");
                reader = new StreamReader(dcConMakerSettings,System.Text.Encoding.Default);
            }
            else
            {
                File.Create("dcConMakerSettings.txt");
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }

            resultPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName +"/";
            //System.Console.WriteLine(resultPath);

            string temp;
            temp = reader.ReadLine();
            if(temp == null)
            {
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }
            targetDirectory = temp;
            temp = reader.ReadLine();
            if (temp == null)
            {
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }
            string githubRawAddress = temp;


            if (!File.Exists(resultPath + "dccon_list.js"))
            {
                dcconListJs = File.Create(resultPath + "dccon_list.js");
                jsWriter = new StreamWriter(dcconListJs, System.Text.Encoding.Default);
                dcconListJson = File.Create(resultPath + "dccon_list.json");
                jsonWriter = new StreamWriter(dcconListJson, System.Text.Encoding.Default);
            }
            else {
                jsWriter = new StreamWriter(resultPath + "dccon_list.js", true, System.Text.Encoding.Default);
                jsonWriter = new StreamWriter(resultPath + "dccon_list.json", true, System.Text.Encoding.Default);
            }

            
            jsWriter.Write(jsHeadWords);
            jsonWriter.Write(jsonHeadWords);

            DirectoryInfo directoryInfo = new DirectoryInfo(targetDirectory);

            FileInfo[] fileList = directoryInfo.GetFiles();
            foreach (FileInfo file in fileList)
            {
                if (file != fileList[fileList.Length - 1])
                {
                    jsWriter.WriteLine("{name:\"" + file.Name + "\", keywords:[\"" + file.Name.Split('.')[0] + "\"], tags:[\"\"]},");
                    jsonWriter.WriteLine("{\"path\":\"" + githubRawAddress + "/images/" + file.Name + "\", \"keywords\":[\"" + file.Name.Split('.')[0] + "\"], \"tags\":[\"\"]},");
                }
                else
                {
                    jsWriter.WriteLine("{name:\"" + file.Name + "\", keywords:[\"" + file.Name.Split('.')[0] + "\"], tags:[\"\"]}" + jsTailWords);
                    jsonWriter.WriteLine("{\"path\":\"" + githubRawAddress + "/images/" + file.Name + "\", \"keywords\":[\"" + file.Name.Split('.')[0] + "\"], \"tags\":[\"\"]}"+jsonTailWords);
                }
            }
            jsWriter.Close();
            jsonWriter.Close();
            reader.Close();
            System.Console.WriteLine("js파일과 json파일을 만들었습니다.");
            System.Console.ReadLine();
        }
    }
}
