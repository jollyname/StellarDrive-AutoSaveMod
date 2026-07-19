using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(AutoSaveMod.BuildInfo.Description)]
[assembly: AssemblyDescription(AutoSaveMod.BuildInfo.Description)]
[assembly: AssemblyCompany(AutoSaveMod.BuildInfo.Company)]
[assembly: AssemblyProduct(AutoSaveMod.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + AutoSaveMod.BuildInfo.Author)]
[assembly: AssemblyTrademark(AutoSaveMod.BuildInfo.Company)]
[assembly: AssemblyVersion(AutoSaveMod.BuildInfo.Version)]
[assembly: AssemblyFileVersion(AutoSaveMod.BuildInfo.Version)]
[assembly: MelonInfo(typeof(AutoSaveMod.AutoSaveMod), AutoSaveMod.BuildInfo.Name, AutoSaveMod.BuildInfo.Version, AutoSaveMod.BuildInfo.Author, AutoSaveMod.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("CuriousOwlGames", "StellarDrive")]