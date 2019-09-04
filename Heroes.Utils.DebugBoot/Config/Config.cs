using System.IO;

namespace Heroes.Utils.DebugBoot.Config
{
    public class Config : JsonSerializable<Config>
    {
        private const string ConfigFileName = "Config.json";

        public LoadIntoLevel LoadIntoLevel                { get; set; }  = new LoadIntoLevel();
        public ReplaceMainMenu ReplaceMainMenuWithDebug   { get; set; }  = new ReplaceMainMenu();

        public static string FilePath(string modFolder) => Path.Combine(modFolder, ConfigFileName);
        public static Config FromJson(string modFolder) => Config.FromPath(FilePath(modFolder));
        public void ToJson(string modFolder)            => Config.ToPath(this, FilePath(modFolder));

    }
}
