# Combined Weathers Toolkit

### This small utility tool allows anyone to register combined/progressive weather effects from [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) without needing to write code !

- For **users**, this mod can be used to generate any kind of effects directly in the config.

- For **developers**, this is pretty much the same but it is done in .json files (note that those files have more customizations than the config).

- And finally, the mod also comes with **40+ predefined custom weathers**! So anyone can just install it and play.

Compatible with **v73** of Lethal Company.

##

### Preview images

The mod will automatically register some custom weather effects coming from multiple mods such as Wesley's Weathers, Lethal Elements, Black fog, Legend Weathers, Code Rebirth, Mrov Weathers and even Blue.

<details><summary>Click on the section here to discover what the mod can do</summary>

</details>

##

### How to use the tool

The mod supports multiple ways of registering a weather. The sections below explain how to use those methods.

<details><summary>Using the config</summary>

### Registering weathers using the config was meant to be used by users and modpacks makers.

**It is higly recommended to use [Gale](https://thunderstore.io/c/lethal-company/p/Kesomannen/GaleModManager/) to manage the configs of this mod.** Right now, it is the best mod manager when it comes to edit the configs and you'll see why shortly.

To register a new custom weather you need to open the `zigzag.combinedweatherstoolkit.cfg` config file, then navigate to the `Config weathers` section.

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/preview.PNG)

You will see a config named `Weather Config creator`: this is the place where you can create your own weather effects. Just below it is an example that can be used to learn what is the accepted data format when creating a weather and here is what it looks like :

`Eclipsed + Foggy : Eclipsed : Foggy`

The format is very simple as you can see, this line will actually register the combined weather *Eclipsed + Foggy* into the game and its effect is to combine the Eclipsed and Foggy weathers together.

You can actually add as much value as you want, each one separated by a comma `,`. To make the process easier, you can click on the little arrows icon on the right of the config to expand the field :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/preview.PNG)

Then you can very easily add each custom weathers in this new window by clicking on the `Edit as list` option. In this place you can add as much entries as you want and it's way easier to see what's going on :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/preview.PNG)

This is very nice but what can the config do more ? Well, the config can also accept a custom color and type. Here's all the possible parameters :

| Parameter name | Example | Accepted values | Max number | Position in the format | Is required? |
| -------------- | ------- | --------------- | ---------- | ---------------------- | ------------ |
| `Name` | Heavy rain | *anything you want* | 1 | 1st | **required** |
| `Name Color` | color(#ff0000) | *any color you want* | 1 | any | optional |
| `Type` | type(progressing) | progressing / combined | 1 | any | optional |
| `Weathers` | rainy | *any weather name* | no limit | any | **required** |

Each of these parameters needs to be separated with a colon `:`, and so if you combine everything you will get something like this in the config :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/preview.PNG)

*Additional notes*
- if the type is not specified, it will default to a combined weather type
- if the color is not specified, it will default to the base color [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) uses for the weather names (will color the names if it is recognized around a symbol like `+` or `>`)
- the color can be anything from a [HEX color](https://www.google.com/search?&q=hex+color) or one of the [basic Unity colors](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Color.html)
- small writting mistakes in the config text such as a whitespace or capital letters should not cause any issues as long as it follows the format

When it is done, launch the game and go into orbit so [WeatherRegistry](https://thunderstore.io/c/lethal-company/p/mrov/WeatherRegistry/) can generate configs for your weathers. You can then go back in the config, this time the one from WeatherRegistry and edit your weather's configs as you want (weight, filtering, scrap multipliers).

Weathers created using this mod will be listed as `WeatherToolkit Weathers` in WeatherRegistry's config file.

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/preview.PNG)

That's pretty much it ! Hope you can create some crazy effects with this mod 🙂

</details>

<details><summary>Using .json files</summary>

### Registering weathers using .json files was meant to be used by developers.

To register a new custom weather you need to create a json file with a name that will end with `.cwt.json`. The mod is made to detect and load all `.cwt.json` that are located in the `BepInEx/plugins` folder (no matter if it is located in a sub folder or not). This means you can place your files wherever you want in your mod's folder.

You can create as much .json files as you want but it is also possible to do everything inside the same file, it is up to you.

The weather creation in .json files is made in a very user friendly way where you can actually write a very minimal weather or completely configure it with all sorts of options. You can check some json examples [here]() so you can learn how to use it but let's still explain how it works :

```json
{
    "example": {
        "type": "Combined",
        "name": "Rainy + Eclipsed + Foggy",
        "color": "#FF0000",
        "weight": 100,
        "scrap_amount": 1.0,
        "scrap_value": 1.2,
        "filtering": false,
        "level_filter": "Company",
        "level_weights": "MoonName@50",
        "weather_to_weather_weights": "WeatherName@50",
        "weathers": [
            "Rainy",
            "Eclipsed",
            "Foggy"
        ]
    }
}
```

This `example` property will actually register the combined weather *Rainy + Eclipsed + Foggy* into the game and its effect is to combine the Rainy, Eclipsed and Foggy weathers together.

If you want to make all your weathers in the same `.cwt.json` then you simply need to add other properties fields into the file.

A lot of parameters in this example are optional and if it is not specified the mod will calculate automatic default values. Here's all the possible parameters :

| Parameter name | Accepted values | Default value | Is required? |
| -------------- | --------------- | ------------- | ------------ |
| `type` | Combined / Progressing | Combined | optional |
| `name` | *anything you want* | "" | **required** |
| `color` | *any color you want* | *default WT color* | optional |
| `weight` | *int* | *default WT weight* | optional |
| `scrap_amount` | *float* | *default WT multiplier* | optional |
| `scrap_value` | *float* | *default WT multiplier* | optional |
| `filtering` | *bool* | false | optional |
| `level_filter` | list of *moon names* | "Company" | optional |
| `level_weights` | list of *moon names with weight* | "" | optional |
| `weather_to_weather_weights` | list of *weather names with weight* | "" | optional |
| `progressing_times` | array of *floats* | *automatically calculated* | optional |
| `progressing_chances` | array of *floats* | *automatically calculated* | optional |
| `weathers` | array of *any weather names* | [] | **required** |

All of these options are actually coming from [WeatherRegistry](https://thunderstore.io/c/lethal-company/p/mrov/WeatherRegistry/), I just made them open for combined and progressive weathers if a developer wants to use them.

*Additional notes*
- if `color` is not specified, it will default to the base color [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) uses for the weather names (will color the names if it is recognized around a symbol like `+` or `>`)
- the color can be anything from a [HEX color](https://www.google.com/search?&q=hex+color) or one of the [basic Unity colors](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Color.html)
- small writting mistakes when writting weahter names such as a whitespace or capital letters should not cause any issues

When it is done, you don't have anything else to do ! Just publish the json files with your mod and it will work, **there is no need to soft depend on anything** !

WeatherRegistry will still generate a unique config for your weather under the `WeatherToolkit Weathers` section but there is nothing to adjust here since everything is already configured (this can then be customized by users of your mod).

That's pretty much it ! Hope you can create some crazy effects with this mod 🙂

</details>

<details><summary>Other utilities</summary>

<details><summary>Using code</summary>

- While this tool was not meant to be used by code, I guess you can still use it if you like. However if you really want to use code, well you can also use [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) directly.

- But anyways. If you want to use code from this mod you can do so by instanciating a new `ToolkitWeather` class, then populate it with the data you want (name, color, weights, weathers, etc) then call `Register()` on this object. For an example on how it works in practice you can look at [this](https://github.com/ZigzagAwaka/CombinedWeathersToolkit/blob/main/CombinedWeathersToolkit/Toolkit/PredefinedRegistery.cs).

</details>

<details><summary>Debug commands</summary>

#### This mod comes with some debug commands that can be used to help when making weathers.

You can activate the debug commands by activating `Debug commands` in the mod's config file. Then, when you are on a moon and the ship is landed you will be able to type the commands in the chat.

If you want to test how a specific weather combo will look like before creating it, you can make it spawn at runtime with the following commands :

| Command | Parameters | Effect |
| ------- | ---------- | ------ |
| `/cwt clear` | *none* | Remove all active weathers on the actual moon |
| `/cwt weathername` | a wanted valid weather name | Spawn the wanted weather as a combined weather effect with the previous ones |
| `/cwt list` | *none* | Display a message listing all active weathers |

</details>

</details>

##

### Compatibility with other mods
- compatible with modded weathers
- f

### Contact & Feedback
If you want to suggest new features, report issues or simply contact me please go to the mod release page in the [modding discord](https://discord.gg/XeyYqRdRGC) or post a [github issue](https://github.com/ZigzagAwaka/CombinedWeathersToolkit).

##

### Credits

credits goes here