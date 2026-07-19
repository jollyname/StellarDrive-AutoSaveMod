using MelonLoader;
using Core.Services;
using Saving.Interface.Services;
using UnityEngine;
using UI.Common.Options;

namespace AutoSaveMod
{
    public static class BuildInfo
    {
        public const string Name = "Jolly's AutoSave Mod";
        public const string Description = "Autosaves your game with a customizable interval";
        public const string Author = "Jollyname";
        public const string Company = null;
        public const string Version = "1.1.0";
        public const string DownloadLink = null;
    }

    public class AutoSaveMod : MelonMod
    {
        private static float saveTimer;

        private static GameObject selectedMenu;

        private static MelonPreferences_Category preferencesCategory;
        public static MelonPreferences_Entry<bool> autosaveEnabled;
        public static MelonPreferences_Entry<int> autosaveDelay;

        public override void OnInitializeMelon()
        {
            preferencesCategory = MelonPreferences.CreateCategory("AutoSaveMod");

            // AutoSave bool entry
            if (preferencesCategory.HasEntry("autosave"))
            {
                autosaveEnabled = preferencesCategory.GetEntry<bool>("autosave");
            }
            else
            {
                autosaveEnabled = preferencesCategory.CreateEntry(
                    "autosave",
                    true
                );
            }

            // SaveDelay int entry
            if (preferencesCategory.HasEntry("autosaveDelay"))
            {
                autosaveDelay = preferencesCategory.GetEntry<int>("autosaveDelay");
            }
            else
            {
                autosaveDelay = preferencesCategory.CreateEntry(
                    "autosaveDelay",
                    20
                );
            }
        }

        public static void ResetTimer()
        {
            saveTimer = autosaveDelay.Value * 60f;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 0)
            {
                selectedMenu = GameObject.Find("SelectedMenu");
            }
            else if (buildIndex == 1)
            {
                selectedMenu = GameObject.Find("PauseCanvas");
                ResetTimer();
            }
        }

        public override void OnUpdate()
        {
            if (selectedMenu != null)
            {
                var settings = selectedMenu.GetComponentInChildren<VolumeSettings>();

                if (settings != null && settings.GetComponent<AutoSaveSettings>() == null)
                {
                    settings.gameObject.AddComponent<AutoSaveSettings>();
                }
            }

            if (!autosaveEnabled.Value)
                return;

            saveTimer -= Time.deltaTime;

            if (saveTimer <= 0f)
            {
                var saver = ServiceLocator.GetService<IGameSaver>();

                if (saver != null) saver.SaveGame();

                ResetTimer();
            }
        }
    }
}