using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{
    public class GameSave : MonoBehaviour, ISaveDataWriteService, ISaveDataReadService
    {
        [SerializeField] private SaveData _saveData;

        public SaveData SaveDataWrirtable => _saveData;
        public ISaveData SaveData => _saveData;
        private string SaveFilePath => $"{Application.dataPath}/Save.save";
        
        public void Save()
        {
            FileStream stream = new FileStream(SaveFilePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _saveData);
            stream.Close();
        }

        private void Load() 
        {
            if ( ! File.Exists(SaveFilePath))
                return;
            FileStream stream = new FileStream(SaveFilePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            _saveData = (SaveData) formatter.Deserialize(stream);
            stream.Close();
        }

        private void Awake()
        {
            Load();
        }

    }
}