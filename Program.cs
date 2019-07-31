using System.IO;
using System.Collections.Generic;

namespace DCConList_Maker
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsHeadWords = "{dcConsData = [";
            string jsTailWords = "]};";
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
                reader = new StreamReader(dcConMakerSettings);
            }
            else
            {
                File.Create("dcConMakerSettings.txt");
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }

            resultPath = System.IO.Directory.GetCurrentDirectory() +"/";

            string settingData;
            settingData = reader.ReadLine();
            if(settingData == null)
            {
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }
            targetDirectory = System.IO.Directory.GetCurrentDirectory() + "/images/";
            
            if (settingData == null)
            {
                System.Console.WriteLine("Please write dcConImage's path and raw github address");
                System.Console.ReadLine();
                return;
            }
            string githubRawAddress = settingData;


            if (!File.Exists(resultPath + "dccon_list.js"))
            {
                dcconListJs = File.Create(resultPath + "dccon_list.js");
                jsWriter = new StreamWriter(dcconListJs);
                dcconListJson = File.Create(resultPath + "dccon_list.json");
                jsonWriter = new StreamWriter(dcconListJson);
            }
            else {
                jsWriter = new StreamWriter(resultPath + "dccon_list.js");
                jsonWriter = new StreamWriter(resultPath + "dccon_list.json");
            }

            
            jsWriter.Write(jsHeadWords);
            jsonWriter.Write(jsonHeadWords);

            DirectoryInfo directoryInfo = new DirectoryInfo(targetDirectory);

            FileInfo[] fileList = directoryInfo.GetFiles();
            foreach (FileInfo file in fileList)
            {
                //
                string originalText = file.Name.Split('.')[0];//확장자 떼기
                string[] tempText = originalText.Split('_');
                string[] keywords = tempText[0].Split(',');
                string[] tags = null;
                if(tempText.Length != 1)
                {
                    tags = tempText[1].Split(',');
                }

                if (file != fileList[fileList.Length - 1])
                {
                    //js 입력
                    jsWriter.Write("{name:\"" + file.Name + "\", keywords:[");
                    //키워드 입력
                    foreach (string keyword in keywords)
                    {
                        if(!(keyword == keywords[keywords.Length - 1]))//마지막 요소가 아니면
                        {
                            jsWriter.Write("\"" + keyword + "\",");
                        }
                        else//마지막요소면
                        {
                            jsWriter.Write("\"" + keyword + "\"");
                        }
                    }
                    jsWriter.Write("], tags:[");
                    //태그입력
                    if (tags != null)
                    {
                        foreach (string tag in tags)
                        {
                            if (!(tag == tags[tags.Length - 1]))//마지막 요소가 아니면
                            {
                                jsWriter.Write("\"" + tag + "\",");
                            }
                            else//마지막요소면
                            {
                                jsWriter.Write("\"" + tag + "\"");
                            }
                        }
                    }
                    jsWriter.WriteLine("]},");


                    //JSON 입력
                    jsonWriter.Write("{\"path\":\"" + githubRawAddress + "/images/" + file.Name + "\", \"keywords\":[");
                    //키워드 입력
                    foreach (string keyword in keywords)
                    {
                        if (!(keyword == keywords[keywords.Length - 1]))//마지막 요소가 아니면
                        {
                            jsonWriter.Write("\"" + keyword + "\",");
                        }
                        else//마지막요소면
                        {
                            jsonWriter.Write("\"" + keyword + "\"");
                        }
                    }
                    jsonWriter.Write("], \"tags\":[");
                    //태그입력
                    if (tags != null)
                    {
                        foreach (string tag in tags)
                        {
                            if (!(tag == tags[tags.Length - 1]))//마지막 요소가 아니면
                            {
                                jsonWriter.Write("\"" + tag + "\",");
                            }
                            else//마지막요소면
                            {
                                jsonWriter.Write("\"" + tag + "\"");
                            }
                        }
                    }
                    jsonWriter.WriteLine("]},");
                }
                else
                {
                    jsWriter.Write("{name:\"" + file.Name + "\", keywords:[");
                    //키워드 입력
                    foreach (string keyword in keywords)
                    {
                        if (!(keyword == keywords[keywords.Length - 1]))//마지막 요소가 아니면
                        {
                            jsWriter.Write("\"" + keyword + "\",");
                        }
                        else//마지막요소면
                        {
                            jsWriter.Write("\"" + keyword + "\"");
                        }
                    }
                    jsWriter.Write("], tags:[");
                    //태그입력
                    if (tags != null)
                    {
                        foreach (string tag in tags)
                        {
                            if (!(tag == tags[tags.Length - 1]))//마지막 요소가 아니면
                            {
                                jsWriter.Write("\"" + tag + "\",");
                            }
                            else//마지막요소면
                            {
                                jsWriter.Write("\"" + tag + "\"");
                            }
                        }
                    }
                    jsWriter.WriteLine("]}" + jsTailWords);


                    //JSON 입력
                    jsonWriter.Write("{\"path\":\"" + githubRawAddress + "/images/" + file.Name + "\", \"keywords\":[");
                    //키워드 입력
                    foreach (string keyword in keywords)
                    {
                        if (!(keyword == keywords[keywords.Length - 1]))//마지막 요소가 아니면
                        {
                            jsonWriter.Write("\"" + keyword + "\",");
                        }
                        else//마지막요소면
                        {
                            jsonWriter.Write("\"" + keyword + "\"");
                        }
                    }
                    jsonWriter.Write("], \"tags\":[");
                    //태그입력
                    if (tags != null)
                    {
                        foreach (string tag in tags)
                        {
                            if (!(tag == tags[tags.Length - 1]))//마지막 요소가 아니면
                            {
                                jsonWriter.Write("\"" + tag + "\",");
                            }
                            else//마지막요소면
                            {
                                jsonWriter.Write("\"" + tag + "\"");
                            }
                        }
                    }
                    jsonWriter.WriteLine("]}" + jsonTailWords);
                    
                }
            }
            jsWriter.Close();
            jsonWriter.Close();
            

            //세팅 데이터 두번째줄 여기
            SetConfig_Js(reader.ReadLine(),
                         System.IO.Directory.GetCurrentDirectory(),
                         System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName+ "/BridgeBBCC-master/lib");


            reader.Close();
            System.Console.WriteLine("js파일과 json파일을 만들었습니다.");
            System.Console.ReadLine();

            
        }

        static private void SetConfig_Js(string channel, string folderpath, string bbccLibPath)
        {
            StreamWriter configJsWriter;
            if (Directory.Exists(bbccLibPath + "/config.js"))
            {
                configJsWriter = new StreamWriter(bbccLibPath + "/config.js");
                System.Console.Write("Test");

            }else{
                configJsWriter = new StreamWriter(File.Create(bbccLibPath + "/config.js"));
            }

            System.Console.WriteLine("BBCC config Set");
            System.Console.WriteLine(folderpath);
            System.Console.WriteLine(bbccLibPath);

            configJsWriter.WriteLine("configData = {");
	        configJsWriter.WriteLine("numChatMax	: 20,");
            configJsWriter.WriteLine("personalColor	: true,");
            configJsWriter.WriteLine("badgeVisible	: true,");
            configJsWriter.WriteLine("themeURI		: \"\",");
            configJsWriter.WriteLine("theme			: \"default\",");
            configJsWriter.WriteLine("themeName		: \"\",");
            configJsWriter.WriteLine("msgExistDuration: 0,");
            configJsWriter.WriteLine("msgAniDuration: 0,");
            configJsWriter.WriteLine("debugLevel		:	2,");
            configJsWriter.WriteLine("useDisplayName: true,");
            configJsWriter.WriteLine("loadCheerImgs	: true,");
            configJsWriter.WriteLine("loadTwitchCons: true,");
            configJsWriter.WriteLine("consRealSubsOnly	: true,");
            configJsWriter.WriteLine("loadDcCons	: true,");

            configJsWriter.Write("dcConsURI: \"");
            configJsWriter.Write(folderpath.Replace('\\', '/'));
            configJsWriter.WriteLine("\",//**경로구분자는 반드시 \\ 말고 /를 사용할 것");

            configJsWriter.WriteLine("subMonthsMsg	 : \"☆ {!0:{months} 개월 }구독{0: 시작}! ☆\",");
            configJsWriter.WriteLine("cheersMsg      : \"☆ {!0:{bits} 비트 }후원 ! ☆\",");
            configJsWriter.WriteLine("loadClipPreview: true,");
            configJsWriter.WriteLine("clipReplaceMsg : \"[ 클립 ]\",");
            configJsWriter.WriteLine("webSocket		 : \"wss://irc-ws.chat.twitch.tv:443\",");
            configJsWriter.WriteLine("nick			 : \"justinfan16831\",");
            configJsWriter.WriteLine("pass			 : \"foobar\", ");

            configJsWriter.Write("    channel: \"#");
            configJsWriter.WriteLine(channel+"\",");

	        configJsWriter.WriteLine("retryInterval		: 2,");
            configJsWriter.WriteLine("allMessageHandle	: /*false*/true,");
            configJsWriter.WriteLine("muteUser			: [\"Nightbot\"],");
            configJsWriter.WriteLine("deleteBanMsg		: true,");
            configJsWriter.WriteLine("commands			: [");
            configJsWriter.WriteLine("    {exe:\"clear\", msg:\"!!clear\"},");
            configJsWriter.WriteLine("    {exe:\"theme\", msg:\"!!theme\"}");
	        configJsWriter.WriteLine("],");
            configJsWriter.WriteLine("replaceMsgs		: [");
            configJsWriter.WriteLine("{orig:/^![a-zA-Z]+/, to:\"{no_display}\"}// 봇 호출 영문 메세지 미표시");
            configJsWriter.WriteLine("]");
            configJsWriter.WriteLine("};");

            configJsWriter.Close();
        }
    }
    
}
