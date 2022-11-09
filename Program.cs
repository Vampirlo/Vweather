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
            int refreshTime = 0; 
            string vWeatheriniFileName = "Vweather.ini";
            string location = string.Empty;
            string key = string.Empty;
            string gamePath = string.Empty;
            string _VweatherScriptPath = string.Empty;
            string url = string.Empty;
            OpenWeather.OpenWeather oW;
            //ini
            INIManager manager = new INIManager(tools.GetFilePath(vWeatheriniFileName));
            
            location = manager.GetPrivateString("SETTINGS", "LOCATION");
            key = manager.GetPrivateString("SETTINGS", "API_KEY");
            gamePath = manager.GetPrivateString("SETTINGS", "Game_Folder");
            refreshTime = Convert.ToInt32(manager.GetPrivateString("SETTINGS", "Refresh_Time"));
            //ini
            tools.varEmpOrNull(refreshTime, nameof(refreshTime));
            tools.varEmpOrNull(location, nameof(location));
            tools.varEmpOrNull(key, nameof(key));
            tools.varEmpOrNull(gamePath, nameof(gamePath));

            _VweatherScriptPath = gamePath + "\\gamedata\\scripts\\Vweather.script";
            url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + key;


            while (true)
            {
                oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(await tools.GetJsonAsync(url));
                tools.selectWeatherToChange(oW, _VweatherScriptPath);
                Thread.Sleep(refreshTime);
            }
        }
    }
}
