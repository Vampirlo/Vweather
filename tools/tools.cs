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
        public static void _iniFileCreate(string vWeatheriniFileName)
        {
            using (File.Create(vWeatheriniFileName));
            INIManager  manager = new INIManager(vWeatheriniFileName);
            manager.WritePrivateString("SETTINGS", "LOCATION", "");
            manager.WritePrivateString("SETTINGS", "API_KEY", "");
            manager.WritePrivateString("SETTINGS", "Game_Folder", "");
            manager.WritePrivateString("SETTINGS", "Refresh_Time", "");
            manager.WritePrivateString("SETTINGS", "Press_To_Update", "");
            DateTime thisDay = DateTime.Now;
            tools.ExLog(thisDay.ToString(), "ini file was not found. File has been created. Please enter the settings in the file");
            Environment.Exit(0);
        }
        public static void initializeScript(string _VweatherMainScriptPath)
        {
            using (File.Create(_VweatherMainScriptPath));
            StreamWriter writer = new StreamWriter(_VweatherMainScriptPath, true);
            string firstWeather = "level.set_weather(\"w_cloudy1\", true)";
            writer.WriteLine(firstWeather);
            writer.Close();
            DateTime thisDay = DateTime.Now;
            string str = "required file was not found in the game directory. File has been created. File path - " + _VweatherMainScriptPath;
            tools.ExLog(thisDay.ToString(), str);
        }

        //проверка пустой строки
        public static void varEmpOrNull(string str, string nameOfvar)
        {
            if (string.IsNullOrEmpty(str))
            {
                string _str = nameOfvar + " is null or empty";
                DateTime thisDay = DateTime.Now;
                ExLog(thisDay.ToString(), _str);
                Environment.Exit(0);
            }
        }
        // перегрузка для int
        public static void varEmpOrNull(int mSec, string nameOfvar)
        {
            if (mSec <=0)
            {
                string _str = nameOfvar + " is less than zero or empty";
                DateTime thisDay = DateTime.Now;
                ExLog(thisDay.ToString(), _str);
                Environment.Exit(0);
            }
        }
        // создание лога и запись
        public static void ExLog(string date, string EX)
        {
            if (!File.Exists(GetFilePath("Ex.log")))
            {
                using (File.Create(GetFilePath("Ex.log")));
            }
            StreamWriter writer = new StreamWriter(GetFilePath("Ex.log"), true);
            writer.WriteLine(date+" - "+EX);
            writer.Close();

        }
        // выбор погоды в зависимости от полученного json
        public static void selectWeatherToChange(OpenWeather.OpenWeather oW, string path)
        {
            string textForReplac = string.Empty;
            // Thunderstorm
            if ((oW.weather[0].id == 200) | (oW.weather[0].id == 201) | (oW.weather[0].id == 210) |
                (oW.weather[0].id == 211) | (oW.weather[0].id == 230) | (oW.weather[0].id == 231))
            {
                textForReplac = "w_storm1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 202) | (oW.weather[0].id == 212) | (oW.weather[0].id == 221) | (oW.weather[0].id == 232))
            {
                textForReplac = "w_storm2";
                ChangeText(path, textForReplac);
            }
            // Drizzle
            else if ((oW.weather[0].id == 300) | (oW.weather[0].id == 301))
            {
                textForReplac = "w_rain1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 300) | (oW.weather[0].id == 310) | (oW.weather[0].id == 311))
            {
                textForReplac = "w_rain2";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 302) | (oW.weather[0].id == 312) | (oW.weather[0].id == 313) | (oW.weather[0].id == 314) | (oW.weather[0].id == 321))
            {
                textForReplac = "w_rain3";
                ChangeText(path, textForReplac);
            }
            // Rain
            else if ((oW.weather[0].id == 500) | (oW.weather[0].id == 501) | (oW.weather[0].id == 511))
            {
                textForReplac = "w_rain1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 502) | (oW.weather[0].id == 503) | (oW.weather[0].id == 520) | (oW.weather[0].id == 521))
            {
                textForReplac = "w_rain2";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 504) | (oW.weather[0].id == 522) | (oW.weather[0].id == 531))
            {
                textForReplac = "w_rain3";
                ChangeText(path, textForReplac);
            }
            // Snow
            else if ((oW.weather[0].id == 600) | (oW.weather[0].id == 611) | (oW.weather[0].id == 615))
            {
                textForReplac = "w_rain1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 601) | (oW.weather[0].id == 612) | (oW.weather[0].id == 616))
            {
                textForReplac = "w_rain2";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 602) | (oW.weather[0].id == 613) | (oW.weather[0].id == 620) | (oW.weather[0].id == 621) | (oW.weather[0].id == 622))
            {
                textForReplac = "w_rain3";
                ChangeText(path, textForReplac);
            }
            // Atmosphere
            else if ((oW.weather[0].id == 701) | (oW.weather[0].id == 711) | (oW.weather[0].id == 751))
            {
                textForReplac = "w_foggy1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 721) | (oW.weather[0].id == 731) | (oW.weather[0].id == 741) | (oW.weather[0].id == 761) | (oW.weather[0].id == 762) |
                (oW.weather[0].id == 771) | (oW.weather[0].id == 781))
            {
                textForReplac = "w_foggy2";
                ChangeText(path, textForReplac);
            }
            // clear
            else if (oW.weather[0].id == 800)
            {
                textForReplac = "w_clear1";
                ChangeText(path, textForReplac);
            }
            // Clouds
            else if ((oW.weather[0].id == 801))
            {

                textForReplac = "w_clear2";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 802) | (oW.weather[0].id == 803))
            {
                textForReplac = "w_cloudy1";
                ChangeText(path, textForReplac);
            }
            else if ((oW.weather[0].id == 804))
            {
                textForReplac = "w_cloudy2_dark";
                ChangeText(path, textForReplac);
            }
        }
        // find weather type in file and change
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
            WebResponse response = null;
            try
            {
                response = await request.GetResponseAsync();
            }
            catch (Exception ex)
            {
                DateTime thisDay = DateTime.Now;
                ExLog(thisDay.ToString(), ex.Message);
                Environment.Exit(0);
            }

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
