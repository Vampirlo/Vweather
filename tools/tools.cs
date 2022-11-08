using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vweather
{
    internal class tools
    {
        public static void selectWeatherToChange(OpenWeather.OpenWeather oW, string path)
        {

        }
        // find weather type and change
        public static void ChangeText(string path, string textForReplac)
        {
            StreamReader reader = new StreamReader(path);
            string _GameCurrentWeather = reader.ReadLine();
            reader.Close();

            string[] spStr = _GameCurrentWeather.Split("\"");
            ReplaceInFile(path, spStr[1], textForReplac);
        }
        public static string GetFilePath(string FileName)
        {
            string exepath = Environment.CurrentDirectory;
            string Filepath = exepath + "\\" + FileName;
            return Filepath;
        }
        public static async Task<string> GetJsonAsync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-urlencoded";
            WebResponse response = await request.GetResponseAsync();

            string answer = string.Empty;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();
            return answer;
        }
        static public void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            StreamReader reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, searchText, replaceText);

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
            writer.Close();
        }
    }
}
