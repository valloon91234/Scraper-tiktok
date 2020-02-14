using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scraper_tiktok
{
    public partial class Form1 : Form
    {
        private static String[] ID_ARRAY;
        private int NextProxyIndex = 0;
        private String[] PROXY_ARRAY = null;

        public Form1()
        {
            InitializeComponent();
            {
                String idString = @"No Noise
Viral
Funny
Prank
Comedy
Perfect Landing
Conspiracy
Suchacoincidence
Crushedit
Breaktherecord
Voiceeffects
Randomfacts
Tiktokpets
Artclub
Fashionfails
Meme
Dadjokes
Jokes
Thatssotiktok
Scienceexperiments
Cutedog
Failvideo
Epicfail
Fart
Prankcall
Foryou
Viral
Laughing
Art
Jokes
Dadjokes
Funnyjokes
Practicaljokes
Random
Randomfacts
Randomvideos
Randomthoughts
Breakdancing
Actionsports
Flips
Artsy
Lifehacks
Foodhacks
Meme
Memes
Magic
Impossible
Trickshots
Funnypet
Funnymeme
Dance
Handshake
Justkidding
Goviral
Viralvibes
Funnyvid
Lolz
Extremesports
Greenscreenvideo
";
                String[] array = idString.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<String> list = new List<string>();
                foreach (String s in array)
                {
                    if (list.Contains(s)) continue;
                    list.Add(s);
                }
                ID_ARRAY = list.ToArray();
            }
        }

        private String RequestGet(String url, int proxyIndex = -1)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 10000;
            httpWebRequest.ReadWriteTimeout = 10000;
            if (proxyIndex >= 0)
            {
                String p = PROXY_ARRAY[proxyIndex];
                String[] pArray = p.Split(':');
                WebProxy myproxy = new WebProxy(pArray[0], Convert.ToInt32(pArray[1]));
                myproxy.BypassProxyOnLocal = false;
                httpWebRequest.Proxy = myproxy;
            }
            httpWebRequest.Method = "GET";
            //httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36";
            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream receiveStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                //var httpWebResponse = (HttpWebResponse)ex.Response;
                //if (httpWebResponse != null)
                //{
                //    HttpStatusCode statusCode = httpWebResponse.StatusCode;
                //    if (statusCode == (HttpStatusCode)422)
                //    {
                //        using (Stream receiveStream = httpWebResponse.GetResponseStream())
                //        {
                //            using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                //            {
                //                return streamReader.ReadToEnd();
                //            }
                //        }
                //    }
                //}
                throw;
            }
        }

        private static void PrintGreen(String text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintRed(String text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idCount = ID_ARRAY.Length;
            for (int idIndex = 0; idIndex < idCount; idIndex++)
            {
                String listID = ID_ARRAY[idIndex];
                String urlListFilename = listID + ".list";
                List<String> urlPageList = new List<string>(); ;
                FileInfo urlListFileInfo = new FileInfo(urlListFilename);
                if (!urlListFileInfo.Exists)
                {
                    List<String> urlList = new List<string>();
                    {
                        String jsonString = File.ReadAllText(listID + ".har");
                        var root = JObject.Parse(jsonString);
                        JArray array = (JArray)root["log"]["entries"];
                        foreach (var item in array)
                        {
                            String url = (String)item["request"]["url"];
                            if (url.StartsWith("https://m.tiktok.com/share/item/list?secUid="))
                            {
                                if (urlList.Contains(url)) continue;
                                urlList.Add(url);
                            }
                        }
                    }
                    {
                        int count = urlList.Count;
                        for (int i = 0; i < count; i++)
                        {
                            String url = urlList[i];
                            String jsonString = RequestGet(url);
                            var root = JObject.Parse(jsonString);
                            JArray array = (JArray)root["body"]["itemListData"];
                            foreach (var item in array)
                            {
                                String id = (String)item["itemInfos"]["id"];
                                String author = (String)item["authorInfos"]["uniqueId"];
                                String url2 = $"https://www.tiktok.com/@{author}/video/{id}";
                                urlPageList.Add(url2);
                            }
                            Console.WriteLine($"{i + 1}/{count}\t{urlPageList.Count}\t{url}");
                        }
                    }
                    using (StreamWriter file = new StreamWriter(urlListFilename, false, Encoding.UTF8))
                    {
                        foreach (String url in urlPageList)
                        {
                            file.WriteLine(url);
                        }
                    }
                }
                Console.WriteLine($"{idIndex + 1}/{idCount}");
            }
        }

        private int ThreadCount1 = 0;
        private int ThreadCount2 = 0;
        private int CountAll = 0;

        private int GetNextProxyIndex()
        {
            int index = NextProxyIndex;
            NextProxyIndex++;
            if (NextProxyIndex >= PROXY_ARRAY.Length)
            {
                PrintRed("--- PROXY ARRAY OVERFLOW ---");
                NextProxyIndex = 0;
            }
            return index;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button_BuildPage_Click(object sender, EventArgs e)
        {
            int listStartIndex = (int)numericUpDown_IDStart.Value;
            int listEndIndex = (int)numericUpDown_IDEnd.Value;
            if (listEndIndex >= ID_ARRAY.Length) listEndIndex = ID_ARRAY.Length - 1;
            for (int idIndex = listStartIndex; idIndex <= listEndIndex; idIndex++)
            {
                String listID = ID_ARRAY[idIndex];
                listID = "Artclub";
                String urlPageFilename = "page\\" + listID + ".list";
                FileInfo urlPageFileInfo = new FileInfo(urlPageFilename);
                if (urlPageFileInfo.Exists)
                {
                    Console.WriteLine($"<Already>\t{idIndex + 1}/{ID_ARRAY.Length}\t{listID}");
                    continue;
                }
                List<String> urlList = new List<string>();
                JsonSerializer serializer = new JsonSerializer();
                JObject root = null;
                using (FileStream s = File.Open("har\\" + listID + ".har", FileMode.Open))
                using (StreamReader sr = new StreamReader(s))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    while (reader.Read())
                    {
                        // deserialize only when there's "{" character in the stream
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            root = serializer.Deserialize<JObject>(reader);
                        }
                    }
                }
                JArray array = (JArray)root["log"]["entries"];
                foreach (var item in array)
                {
                    String url = (String)item["request"]["url"];
                    if (url.StartsWith("https://m.tiktok.com/share/item/list?secUid="))
                    {
                        if (urlList.Contains(url)) continue;
                        urlList.Add(url);
                    }
                }
                using (StreamWriter file = new StreamWriter(urlPageFilename, false, Encoding.UTF8))
                {
                    foreach (String url in urlList)
                    {
                        file.WriteLine(url);
                    }
                }
                Console.WriteLine($"{idIndex + 1}/{ID_ARRAY.Length}\t{listID}");
                break;
            }
        }

        private void button_BuildList_Click(object sender, EventArgs e)
        {
            PROXY_ARRAY = File.ReadAllText("proxy.txt").Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            NextProxyIndex = (int)numericUpDown_Proxy1.Value;
            int listStartIndex = (int)numericUpDown_IDStart.Value;
            int listEndIndex = (int)numericUpDown_IDEnd.Value;
            if (listEndIndex >= ID_ARRAY.Length) listEndIndex = ID_ARRAY.Length - 1;
            for (int idIndex = listStartIndex; idIndex <= listEndIndex; idIndex++)
            {
                int k = idIndex;
                Thread thread = new Thread(() => workBuildList(k));
                thread.Start();
            }
        }

        private void workBuildList(int idIndex)
        {
            ThreadCount1++;
            const int TRY_MAX = 500;
            int failedCount = 0;
            int proxyIndex = GetNextProxyIndex();
            String listID = ID_ARRAY[idIndex];
            try
            {
                String urlPageFilename = "page\\" + listID + ".list";
                String urlListFilename = "list\\" + listID + ".list";
                List<String> urlList = new List<string>();
                FileInfo urlListFileInfo = new FileInfo(urlListFilename);
                if (urlListFileInfo.Exists)
                {
                    Console.WriteLine($"<Already>\t{idIndex + 1}/{ThreadCount1}\t{listID}");
                    goto line_workBuildList_end;
                }
                List<String> urlPageList = new List<string>();
                FileInfo urlPageFileInfo = new FileInfo(urlPageFilename);
                if (!urlPageFileInfo.Exists)
                {
                    PrintRed($"{idIndex + 1}/{ThreadCount1}\t{listID}\t\tPageFile Not Found");
                    //MessageBox.Show($"{idIndex + 1}/{ThreadCount}\t{listID}\t\tPageFile Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto line_workBuildList_end;
                }
                {
                    using (StreamReader reader = File.OpenText(urlPageFilename))
                    {
                        String line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            urlPageList.Add(line);
                        }
                    }
                }
                {
                    int count = urlPageList.Count;
                    //int maxCursor = 0;
                    for (int i = 0; i < count; i++)
                    {
                        String url = urlPageList[i];
                        try
                        {
                            //if (maxCursor > 0)
                            //{
                            //    int urlc = 0, urlm = 0;
                            //    {
                            //        String w = "&count=";
                            //        int w1 = url.IndexOf(w);
                            //        if (w1 < 0) throw new NullReferenceException();
                            //        w1 += w.Length;
                            //        int w2 = url.IndexOf("&", w1);
                            //        String s1 = url.Substring(w1, w2 - w1);
                            //        urlc = Convert.ToInt32(s1);
                            //    }
                            //    {
                            //        String w = "&maxCursor=";
                            //        int w1 = url.IndexOf(w);
                            //        if (w1 < 0) throw new NullReferenceException();
                            //        w1 += w.Length;
                            //        int w2 = url.IndexOf("&", w1);
                            //        String s1 = url.Substring(w1, w2 - w1);
                            //        urlm = Convert.ToInt32(s1);
                            //    }
                            //    if (urlc + urlm > maxCursor)
                            //    {
                            //        Console.WriteLine($"{idIndex + 1}/{ThreadCount1}\t{listID}\t<{proxyIndex}/{NextProxyIndex}>\t\t{i + 1}/{count}\t{urlList.Count}\t<Overflow>\t{urlc} + {urlm} > {maxCursor}");
                            //        break;
                            //    }
                            //}
                            String jsonString = RequestGet(url, proxyIndex);
                            var root = JObject.Parse(jsonString);
                            if (root["body"] == null) throw new Exception("<Limit>" + root.ToString());
                            String region = (String)root["body"]["pageState"]["region"];
                            if (region != "US")
                            {
                                throw new Exception("Region is not US. Region = " + region);
                            }
                            //if (maxCursor == 0)
                            //{
                            //    maxCursor = Convert.ToInt32((String)root["body"]["maxCursor"]);
                            //}
                            var itemListData = root["body"]["itemListData"];
                            JArray array = (JArray)itemListData;
                            try
                            {
                                foreach (var item in array)
                                {
                                    String id = (String)item["itemInfos"]["id"];
                                    String author = (String)item["authorInfos"]["uniqueId"];
                                    String url2 = $"https://www.tiktok.com/@{author}/video/{id}";
                                    urlList.Add(url2);
                                }
                                Console.WriteLine($"{idIndex + 1}/{ThreadCount1}\t{listID}\t<{proxyIndex}/{NextProxyIndex}>\t\t{i + 1}/{count}\t{urlList.Count}");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine(root.ToString());
                                throw;
                            }
                        }
                        catch (WebException ex)
                        {
                            if (i > 0 && TRY_MAX > ++failedCount)
                            {
                                MessageBox.Show($"{idIndex + 1}/{ThreadCount1}\t{listID}\t\t{i + 1}/{count}\t<TRY OUT>", "TRY OUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            var httpWebResponse = (HttpWebResponse)ex.Response;
                            int statusCode = 0;
                            if (httpWebResponse != null) statusCode = (int)httpWebResponse.StatusCode;
                            PrintRed($"{idIndex + 1}/{ThreadCount1}\t{listID}\t\t{i + 1}/{count}\t<Changing Proxy> {proxyIndex} ->  {NextProxyIndex}\t({failedCount})\tStatusCode = {statusCode}\r\n\t{ex.Message}");
                            proxyIndex = GetNextProxyIndex();
                            i--;
                            continue;
                        }
                        catch (Exception ex)
                        {
                            if (i > 0 && TRY_MAX > ++failedCount)
                            {
                                MessageBox.Show($"{idIndex + 1}/{ThreadCount1}\t{listID}\t\t{i + 1}/{count}\t<TRY OUT>", "TRY OUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            PrintRed($"{idIndex + 1}/{ThreadCount1}\t{listID}\t\t{i + 1}/{count}\t<Changing Proxy> {proxyIndex} ->  {NextProxyIndex}\r({failedCount})\r\n\t{ex.ToString()}");
                            proxyIndex = GetNextProxyIndex();
                            i--;
                            continue;
                        }
                    }
                }
                {
                    using (StreamWriter file = new StreamWriter(urlListFilename, false, Encoding.UTF8))
                    {
                        foreach (String url in urlList)
                        {
                            file.WriteLine(url);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        line_workBuildList_end:;
            ThreadCount1--;
            if (ThreadCount1 < 1)
            {
                PrintGreen("-------- All done --------");
            }
        }

        private void button_BuildResult_Click(object sender, EventArgs e)
        {
            PROXY_ARRAY = File.ReadAllText("proxy2.txt").Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            NextProxyIndex = (int)numericUpDown_Proxy2.Value;
            int listStartIndex = (int)numericUpDown_IDStart.Value;
            int listEndIndex = (int)numericUpDown_IDEnd.Value;
            if (listEndIndex >= ID_ARRAY.Length) listEndIndex = ID_ARRAY.Length - 1;
            for (int idIndex = listStartIndex; idIndex <= listEndIndex; idIndex++)
            {
                int k = idIndex;
                Thread thread = new Thread(() => workBuildResult(k));
                thread.Start();
            }
        }

        private void workBuildResult(int idIndex)
        {
            ThreadCount2++;
            int proxyIndex = GetNextProxyIndex();
            String listID = ID_ARRAY[idIndex];
            try
            {
                String urlListFilename = "list\\" + listID + ".list";
                String resultFilename = "result\\" + listID + ".result";
                String finishedFilename = "result\\_" + listID + ".result";
                FileInfo resultFileInfo = new FileInfo(resultFilename);
                FileInfo finishedFileInfo = new FileInfo(finishedFilename);
                if (finishedFileInfo.Exists)
                {
                    Console.WriteLine($"<Already>\t{idIndex + 1}/{ThreadCount2}\t{listID}");
                    CountAll += File.ReadAllLines(finishedFilename).Length;
                    goto line_workBuildResult_end;
                }
                List<String> urlList = new List<string>(); ;
                FileInfo urlListFileInfo = new FileInfo(urlListFilename);
                if (!urlListFileInfo.Exists)
                {
                    PrintRed($"{idIndex + 1}/{ThreadCount2}\t{listID}\t\tListFile Not Found");
                    goto line_workBuildResult_end;
                }
                {
                    using (StreamReader reader = File.OpenText(urlListFilename))
                    {
                        String line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            urlList.Add(line);
                        }
                    }
                }

                List<String> oldResultList = null;
                if (resultFileInfo.Exists)
                {
                    oldResultList = new List<string>();
                    using (StreamReader reader = File.OpenText(resultFilename))
                    {
                        String line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            oldResultList.Add(line);
                        }
                    }
                    resultFileInfo.Delete();
                }
                {
                    int count = urlList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        String url = urlList[i];
                        if (oldResultList != null)
                        {
                            int oldLineCount = oldResultList.Count;
                            for (int j = 0; j < oldLineCount; j++)
                            {
                                String oldLine = oldResultList[j];
                                if (oldLine.Contains(url))
                                {
                                    String[] oldLineSplit = oldLine.Split('\t');
                                    if (oldLineSplit.Length == 4)
                                    {
                                        using (StreamWriter file = new StreamWriter(resultFilename, true, Encoding.UTF8))
                                            file.WriteLine(oldLine);
                                        CountAll++;
                                        Console.WriteLine($"<Already>\t{idIndex}/{ThreadCount2}\t{listID}\t\t{i + 1}/{count}/{CountAll}\t" + oldLineSplit[3].Substring(0, 48));
                                        oldResultList.RemoveAt(j);
                                        goto line_workBuildResult_NextURL;
                                    }
                                    oldResultList.RemoveAt(j);
                                    break;
                                }
                            }
                        }
                        String sourceUrl = "view-source:" + url;
                        try
                        {
                            String response = RequestGet(url, proxyIndex);
                            const String pattern = "\"contentUrl\":\"";
                            int m = response.IndexOf(pattern);
                            if (m < 0)
                            {
                                PrintRed($"{idIndex}/{ThreadCount2}\t{listID}\t\t{i + 1}/{count}\t<Changing Proxy> {proxyIndex} -> {NextProxyIndex}");
                                proxyIndex = GetNextProxyIndex();

                                i--;
                                continue;
                            }
                            m += pattern.Length;
                            int n = response.IndexOf("\"", m);
                            String contentUrl = response.Substring(m, n - m);
                            using (StreamWriter file = new StreamWriter(resultFilename, true, Encoding.UTF8))
                                file.WriteLine(listID + "\t" + url + "\t" + sourceUrl + "\t" + contentUrl);
                            CountAll++;
                            Console.WriteLine($"{idIndex}/{ThreadCount2}\t{listID}\t\t{i + 1}/{count}/{CountAll}\t<{proxyIndex}/{NextProxyIndex}>\t{contentUrl.Substring(0, 48)}");
                        }
                        catch (WebException ex)
                        {
                            var httpWebResponse = (HttpWebResponse)ex.Response;
                            int statusCode = 0;
                            if (httpWebResponse != null) statusCode = (int)httpWebResponse.StatusCode;
                            PrintRed($"{idIndex}/{ThreadCount2}\t{listID}\t\t{i + 1}/{count}\t<Changing Proxy> {proxyIndex} -> {NextProxyIndex}\tStatusCode = {statusCode}");
                            proxyIndex = GetNextProxyIndex();

                            i--;
                            continue;
                        }
                        catch (Exception ex)
                        {
                            PrintRed($"{idIndex}/{ThreadCount2}\t{listID}\t\t{i + 1}/{count}\t<Changing Proxy> {proxyIndex} -> {NextProxyIndex}\r\n\t{ex.Message}");
                            proxyIndex = GetNextProxyIndex();

                            i--;
                            continue;
                        }
                    line_workBuildResult_NextURL:;
                    }
                }
                PrintGreen($"{idIndex}\t{listID}\t\t--- Completed ---");
                resultFileInfo.MoveTo(finishedFileInfo.FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        line_workBuildResult_end:;
            ThreadCount2--;
            if (ThreadCount2 < 1)
            {
                PrintGreen("-------- All done --------");
            }
        }

        private void button_Merge_Click(object sender, EventArgs e)
        {
            const String resultFilename = "result\\__All.result";
            using (StreamWriter file = new StreamWriter(resultFilename, false, Encoding.UTF8))
            {
                foreach (String listID in ID_ARRAY)
                {
                    String urlFinishedFilename = "result\\_" + listID + ".result";
                    FileInfo urlFinishedFileInfo = new FileInfo(urlFinishedFilename);
                    if (urlFinishedFileInfo.Exists)
                    {
                        using (StreamReader reader = File.OpenText(urlFinishedFilename))
                        {
                            String line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                file.WriteLine(line);
                            }
                        }
                        Console.WriteLine(listID + " - Merged");
                    }
                }
            }
            Console.WriteLine("--- Completed ---");
        }

    }
}
