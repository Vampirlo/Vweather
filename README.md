# Vweather
 Synchronizes [Openweathermap](https://openweathermap.org/) with stalker.
 
 The program sends a request to the site, based on the data received, changes the weather in the game.
## Installation
1. Install the program setup file from the [appropriate folder](https://github.com/Vampirlo/Vweather/tree/main/Setup%20files).
2. Copy game scripts from [STALKERPATH](https://github.com/Vampirlo/Vweather/tree/main/STALKERPATH).
    * if you **don't** want a realistic length of day in the game, copy the files from the [respective folder](https://github.com/Vampirlo/Vweather/tree/main/STALKERPATH/!Without%20realistic%20time%20length/gamedata).


## How to use
### Setup you ini file
* `LOCATION` - the location where the weather data will be taken from. [Example](https://openweathermap.org/city/2643743).
* `API_KEY` - [your key](https://home.openweathermap.org/api_keys) to send a weather request to the site.
* `Game_Folder` - your way to the game.
* `Refresh_Time` - time in milliseconds between weather data updates.
* `Press_To_Update` - the program will wait for a key press to update the data. **In this case, nothing will depend on Refresh_Time**.
* `debug` - the program will not check the correctness of the specified path to the game.
### Log file
When you run the program, you may encounter errors in its operation. all foreseen errors will be recorded in **Ex.log** file.
* The path to the game must contain only latin letters.
* **If you are from Russia, North Korea or China**, then you may experience that the program is unable to communicate with the openweathermap server. In this case, you will need to **use VPN**.
## Tested builds
* [Reality 1.2](https://discord.gg/VUcJ2y4gkv)