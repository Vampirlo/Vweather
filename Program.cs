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
            string _VweatherMainScriptPath = string.Empty;
            string url = string.Empty;
            bool waitKey = false;
            bool _debug = false;

            OpenWeather.OpenWeather oW;

            //ini file check
            if (!File.Exists(tools.GetFilePath(vWeatheriniFileName)))
                tools._iniFileCreate(tools.GetFilePath(vWeatheriniFileName));

            //ini
            INIManager manager = new INIManager(tools.GetFilePath(vWeatheriniFileName));


            try
            {
                location = manager.GetPrivateString("SETTINGS", "LOCATION");
                key = manager.GetPrivateString("SETTINGS", "API_KEY");
                gamePath = manager.GetPrivateString("SETTINGS", "Game_Folder");
                refreshTime = Convert.ToInt32(manager.GetPrivateString("SETTINGS", "Refresh_Time"));
                waitKey = Convert.ToBoolean(manager.GetPrivateString("SETTINGS", "Press_To_Update"));
                _debug = Convert.ToBoolean(manager.GetPrivateString("SETTINGS", "debug"));
            }
            catch (Exception ex)
            {
                DateTime thisDay = DateTime.Now;
                tools.ExLog(thisDay.ToString(), ex.Message);
                Environment.Exit(0);
            }
            if (!_debug)
                tools.gamePathCheck(gamePath);
            tools.varEmpOrNull(refreshTime, nameof(refreshTime));
            tools.varEmpOrNull(location, nameof(location));
            tools.varEmpOrNull(key, nameof(key));
            tools.varEmpOrNull(gamePath, nameof(gamePath));

            _VweatherMainScriptPath = gamePath + "\\gamedata\\scripts\\Vweather.script";

            if (!File.Exists(_VweatherMainScriptPath))
                tools.initializeScript(_VweatherMainScriptPath);

                url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&units=metric&appid=" + key;


            if (waitKey)
            {

                while (true)
                {
                    invalidkey:

                    Console.WriteLine("Press 'U' button when you need to update weather. Or 'E' to close the program");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.U:
                            oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(await tools.GetJsonAsync(url));
                            tools.selectWeatherToChange(oW, _VweatherMainScriptPath);
                            Console.Clear();
                            Console.WriteLine("\nThe weather has been updated\n");
                            break;
                        case ConsoleKey.E:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\ninvalid key");
                            goto invalidkey;
                    }
                }
            }

            while (true)
            {
                oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(await tools.GetJsonAsync(url));
                tools.selectWeatherToChange(oW, _VweatherMainScriptPath);
                DateTime thisDay = DateTime.Now;
                Console.Clear();
                Console.WriteLine(thisDay.ToString() + "\nThe weather has been updated\n");
                Thread.Sleep(refreshTime);
            }
        }
    }
}
