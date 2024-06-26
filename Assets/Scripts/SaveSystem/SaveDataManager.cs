using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveDataManager
    {
        private static readonly string USER_DATA_NAME = "UserData.json";
        
        private static void SaveData<T>(T data, string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }

        private static T LoadData<T>(string fileName, T defaultData)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            else
            {
                SaveData(defaultData, fileName);
                return defaultData;
            }
        }
        
        public static void SaveUserData(UserData data)
        {
            SaveData(data, USER_DATA_NAME);
        }
        
        public static UserData LoadUserData()
        {
            return LoadData(USER_DATA_NAME, new UserData());
        }
        
    }
}