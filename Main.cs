using MelonLoader;
using Core.Services;
using Saving.Interface.Services;
using UnityEngine;

namespace AutoSaveMod
{
    public static class BuildInfo
    {
        public const string Name = "Jolly's AutoSave Mod";
        public const string Description = "Autosaves every 20 minutes";
        public const string Author = "Jollyname";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class AutoSaveMod : MelonMod
    {
        private const float saveDelay = 1200f;
        private float saveTimer;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 1)
            {
                saveTimer = saveDelay;
            }
        }

        public override void OnUpdate()
        {
            saveTimer -= Time.deltaTime;

            if (saveTimer <= 0f)
            {
                var saver = ServiceLocator.GetService<IGameSaver>();

                if (saver != null)
                {
                    saver.SaveGame();
                }

                saveTimer = saveDelay;
            }
        }
    }
}