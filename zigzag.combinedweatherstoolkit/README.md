# Combined Weathers Toolkit

### This small utility tool allows anyone to register combined/progressive weather effects from [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) without needing to write code !

- For **users**, this mod can be used to generate any kind of effects directly in the config.

- For **developers**, this is pretty much the same but it is done in .json files (note that those files have more customizations than the config).

- And finally, the mod also comes with **40+ predefined custom weathers**! So anyone can just install it and play.

Compatible with **v73** of Lethal Company.

##

### Preview images

The mod will automatically register some custom weather effects coming from multiple mods such as [Wesley's Weathers](https://thunderstore.io/c/lethal-company/p/Magic_Wesley/Wesleys_Weathers/), [Lethal Elements](https://thunderstore.io/c/lethal-company/p/pacoito/LethalElementsTheta/), [Black Fog Weather](https://thunderstore.io/c/lethal-company/p/DemonMae/Black_Fog_Weather/), [Legend Weathers](https://thunderstore.io/c/lethal-company/p/Zigzag/LegendWeathers/), [Code Rebirth](https://thunderstore.io/c/lethal-company/p/XuXiaolan/CodeRebirth/), [Mrov Weathers](https://thunderstore.io/c/lethal-company/p/mrov/MrovWeathers/) and even [Blue](https://thunderstore.io/c/lethal-company/p/Generic_GMD/Blue/).

If you want the full experience, make sure to install as many weather mods as you can/want !!

**<details><summary>Click on the section here to discover what the mod can do</summary>**

### Superstorm *(stormy + flooded + tornado + hurricane)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase2.PNG)

### Snowfall + Majora Moon

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase1.png)

### Hallowed Eclipse *(hallowed + eclipsed)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase3.PNG)

### Impending Storm *(none > rainy > cloudy > stormy > hurricane)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase4.PNG)

### Meteor Shower + Blood Moon

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase5.PNG)

### Blue Moon *(blue + bloodmoon)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase6.PNG)

### Toxic Winds *(toxicsmog + tornado)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase7.png)

### The End of the World *(all weathers)*

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Images/showcase8.PNG)

**There is a lot more to discover by yourself 🤗**

</details>

##

### How to use the tool

The mod supports multiple ways of registering a weather. The sections below explain how to use those methods.

<details><summary>Using the config</summary>

### Registering weathers using the config was meant to be used by users and modpacks makers.

**It is higly recommended to use [Gale](https://thunderstore.io/c/lethal-company/p/Kesomannen/GaleModManager/) to manage the configs of this mod.** Right now, it is the best mod manager when it comes to edit the configs and you'll see why shortly.

To register a new custom weather you need to open the `zigzag.combinedweatherstoolkit.cfg` config file, then navigate to the `Config weathers` section.

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/config1.PNG)

You will see a config named `Weather Config creator`: this is the place where you can create your own weather effects. Just below it is an example that can be used to learn what is the accepted data format when creating a weather and here is what it looks like :

**`Eclipsed + Foggy : Eclipsed : Foggy`**

The format is very simple as you can see, this line will actually register the combined weather ***Eclipsed + Foggy*** into the game and its effect is to combine the Eclipsed and Foggy weathers together.

You can actually add as much value as you want, each one separated by a comma `,`. To make the process easier, you can click on the little arrows icon on the right of the config to expand the field :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/config2.PNG)

Then you can very easily add each custom weathers in this new window by clicking on the `Edit as list` option. In this place you can add as much entries as you want and it's way easier to see what's going on :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/config3.PNG)

This is very nice but what can the config do more ? Well, the config can also accept a custom color and type. Here's all the possible parameters :

| Parameter name | Example | Accepted values | Max number | Position in the format | Is required? |
| -------------- | ------- | --------------- | ---------- | ---------------------- | ------------ |
| `Name` | Heavy rain | *anything you want* | 1 | 1st | **required** |
| `Name Color` | color(#ff0000) | *any color you want* | 1 | any | optional |
| `Type` | type(progressing) | progressing / combined | 1 | any | optional |
| `Weathers` | rainy | *any weather name* | no limit | any | **required** |

Each of these parameters needs to be separated with a colon `:`, and so if you combine everything you will get something like this in the config :

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/config4.PNG)

*<details><summary>Additional notes</summary>*

- if the type is not specified, it will default to a **combined weather** type
- if the color is not specified, it will default to the base color [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) uses for the weather names (will color the names if it is recognized around a symbol like `+` or `>`)
- the color can be anything from a [HEX color](https://www.google.com/search?&q=hex+color) or one of the [basic Unity colors](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Color.html)
- small writting mistakes in the config text such as a whitespace or capital letters should not cause any issues as long as it follows the format

</details>

</br>

When it is done, **launch the game and go into orbit** so [WeatherRegistry](https://thunderstore.io/c/lethal-company/p/mrov/WeatherRegistry/) can generate configs for your weathers. You can then go back in the config, this time the one from WeatherRegistry and edit your weather's configs as you want (weight, filtering, scrap multipliers).

Weathers created using this mod will be listed as `WeatherToolkit Weathers` in WeatherRegistry's config file.

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/config5.PNG)

That's pretty much it ! Hope you can create some crazy effects with this mod 🙂

</details>

</br>

<details><summary>Using .json files</summary>

### Registering weathers using .json files was meant to be used by developers.

It was made for developers that want to **add special weather combos to their mods**, or for moon makers that wants to **add a specific custom weather that only spawns on their custom moon**.

To register a new custom weather you need to create a json file with a name that will end with `.cwt.json`. The mod is made to detect and load all `.cwt.json` that are located in the `BepInEx/plugins` folder (no matter if it is located in a sub folder or not). This means you can **place your files wherever you want in your mod's folder**.

You can create as much .json files as you want but it is also possible to do everything inside the same file, it is up to you.

The weather creation in .json files is made in a very user friendly way where you can actually write a very minimal weather or completely configure it with all sorts of options. You can check some [json examples in the folder by clicking here](https://github.com/ZigzagAwaka/CombinedWeathersToolkit/tree/main/JsonExamples) so you can learn how to write it but let's still explain how it works :

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

This `example` property will actually register the combined weather ***Rainy + Eclipsed + Foggy*** into the game and its effect is to combine the Rainy, Eclipsed and Foggy weathers together.

If you want to make all your weathers in the same `.cwt.json` then you simply need to add other properties fields into the file.

A lot of parameters in this example are optional and if it is not specified the mod will calculate automatic default values. **The minimum you need to write for it to be valid is the `name` and the `weathers`.** Here's all the possible parameters :

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

*<details><summary>Additional notes</summary>*

- if `color` is not specified, it will default to the base color [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) uses for the weather names (will color the names if it is recognized around a symbol like `+` or `>`)
- the color can be anything from a [HEX color](https://www.google.com/search?&q=hex+color) or one of the [basic Unity colors](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Color.html)
- small writting mistakes when writting `weathers` names such as a whitespace or capital letters should not cause any issues
- if one of the following: `weight`, `scrap_amount` or `scrap_value` is not specified, it will default to the base values calculated by WeatherTweaks *(this will **dynamically apply a value based on your number of weather effects**)*
- the `filtering` and `level_filter` fields allows to define a blacklist (*"false"*) or whitelist (*"true"*) for moons that tries to spawn your weather
- the `level_weights` field allows to specify specific moons with specific weights in the format *"MoonName@50,OtherMoonName@9999"* so you can make the weather always spawn on specific moons or never spawn at all if the weight is 0
- the `weather_to_weather_weights` field allows to specify specific weathers names that will try to spawn your weather after them on the same moon (this uses the same format as `level_weights` but with weather names instead of moon names)
- and finally, `progressing_times` and `progressing_chances` are **exclusive properties to Progressing Weathers types**, it allows to specify at which time and chance the weather transition is going to happen (you need to enter as many values here as your number of weather effects **MINUS 1** because the first weather effect is the original weather on the moon)
- if these values are not specified when creating a progressing weather, **the tool will calculate them based on your number of weather effects**, so for example if you have 4 weathers (a base effect and 3 progressing effects), the time values are going to be set to 0.25, 0.5 and 0.75, and the chance values will all be 1

</details>

</br>

When it is done, you don't have anything else to do ! Just publish the json files with your mod, make sure that this tool is installed and it will work, **there is no need to soft depend on anything** !

WeatherRegistry will still generate a unique config for your weather under the `WeatherToolkit Weathers` section but there is nothing to adjust here since everything is already configured (this can then be customized by users of your mod).

That's pretty much it ! Hope you can create some crazy effects with this mod 🙂

</details>

</br>

<details><summary>Other utilities</summary>

</br>

<details><summary>Using code</summary>

- While this tool was not meant to be used by code, I guess you can still use it if you like. However if you really want to use code, well you can also use [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) directly.

- If you want to use code from this mod you can do so by instanciating a new `ToolkitWeather` class, then populate it with the data you want (name, color, weights, weathers, etc) then call `Register()` on this object. For an example on how it works in practice you can look at [this code](https://github.com/ZigzagAwaka/CombinedWeathersToolkit/blob/main/CombinedWeathersToolkit/Toolkit/PredefinedRegistery.cs).

</details>

</br>

<details><summary>Debug commands</summary>

#### This mod comes with some debug commands that can be used to help when making weathers.

You can activate the debug commands by activating `Debug commands` in the mod's config file. Then, when you are on a moon and the ship is landed you will be able to type the commands in the chat.

If you want to test how a specific weather combo will look like before creating it, you can make it spawn at runtime with the following commands :

| Command | Parameters | Effect |
| ------- | ---------- | ------ |
| `/cwt clear` | *none* | Remove all active weathers on the actual moon |
| `/cwt weathername` | a wanted valid weather name | Spawn the wanted weather as a combined weather effect with the previous ones |
| `/cwt list` | *none* | Display a message listing all active weathers |

***When using `/cwt weathername` make sure to enter the name with no whitespace and no capital letters!***

![Preview](https://raw.githubusercontent.com/ZigzagAwaka/CombinedWeathersToolkit/main/Previews/Doc/debug1.PNG)

</details>

</details>

##

### Compatibility with other mods

- This mod is compatible with any **modded weathers** that are registered using [WeatherRegistry](https://thunderstore.io/c/lethal-company/p/mrov/WeatherRegistry/)

- This mod uses custom classes that inherites from the custom weathers classes in [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) so anything that affects WeatherTweaks weathers will also affect this mod

- When using this mod, there is no need to add checks to see if a specific modded weather is installed and enabled because the registering function will do that automatically : **this means if a weather that contains an effect from a mod that is not installed tries to be registered then it will fail safely to avoid registering useless weathers**

- **Watch out**, as some weather combinations will not work well together, some might override the visuals of others completely ! To test if a specific weather combo is being rendered fine you can try to use the Debug commands (this is explained in the "Other utilities" section of this README)

### Planned features
**Evolutive weathers**: a mix of progressing and combined weathers, where each new weather is going to be added as a combined effect instead of replacing the previous effect

### Contact & Feedback
If you want to suggest new features, report issues or simply contact me please go to the mod release page in the [modding discord](https://discord.gg/XeyYqRdRGC) or post a [github issue](https://github.com/ZigzagAwaka/CombinedWeathersToolkit).

##

### Credits

- Thanks [Mrov](https://thunderstore.io/c/lethal-company/p/mrov/) for having created [WeatherRegistry](https://thunderstore.io/c/lethal-company/p/mrov/WeatherRegistry/) and [WeatherTweaks](https://thunderstore.io/c/lethal-company/p/mrov/WeatherTweaks/) !

- Thanks [Pacoito](https://thunderstore.io/c/lethal-company/p/pacoito/) for some advice !

- Thanks **ThecheeseXD** for massively helping at testing the mod !
