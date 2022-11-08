using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Vweather
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            string vWeatheriniFileName = "Vweather.ini";
            string location = string.Empty;
            string key = string.Empty;
            string gamePath = string.Empty;
            //ini
            INIManager manager = new INIManager(tools.GetFilePath(vWeatheriniFileName));
            
            location = manager.GetPrivateString("SETTINGS", "LOCATION");
            key = manager.GetPrivateString("SETTINGS", "API_KEY");
            gamePath = manager.GetPrivateString("SETTINGS", "Game_Folder");
            //ini
            string _VweatherScriptPath = gamePath + "\\gamedata\\scripts\\Vweather.script";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + key;

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(await tools.GetJsonAsync(url));

            Console.WriteLine("Temp = {0}\nVisibility = {1}\nWind Speed = {2}\nWeatherID = {3}",
                oW.main.temp, oW.visibility, oW.wind.speed, oW.weather[0].id);



            string textForReplac = "w_storm1";
            tools.ChangeText(_VweatherScriptPath, textForReplac);
            Console.ReadKey();

        }
    }
}
