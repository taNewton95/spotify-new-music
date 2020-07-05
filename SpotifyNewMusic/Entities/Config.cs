using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpotifyNewMusic.Entities
{
    public class Config
    {

        private static Config _Current;
        public static Config Current
        {
            get
            {
                if (_Current == null)
                {
                    Load();
                }

                return _Current;
            }
        }

        public readonly static string _ConfigDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SpotifyNewMusic";
        public const string _FileName = "Config.json";

        public string client_id, client_secret;

        private static void Load()
        {
            if (!Directory.Exists(_ConfigDir))
            {
                Directory.CreateDirectory(_ConfigDir);
            }

            string fullFilePath = GetFullFilePath();

            if (!File.Exists(fullFilePath))
            {
                _Current = new Config();
                Save();
            }
            else
            {
                _Current = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullFilePath));
            }

        }

        private static void Save()
        {
            string fullFilePath = GetFullFilePath();
            File.WriteAllText(fullFilePath, JsonConvert.SerializeObject(_Current));
        }

        private static string GetFullFilePath()
        {
            return _ConfigDir + "\\" + _FileName;
        }

    }
}
