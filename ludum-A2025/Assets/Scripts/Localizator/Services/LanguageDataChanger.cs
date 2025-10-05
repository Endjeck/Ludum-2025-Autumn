using System.IO;
//using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class LanguageDataChanger
{
    public void Change(bool english)
    {
        try
        {

         //   string jsonObject = Resources.Load("GameData").ToString();
         //  GameData data = JsonConvert.DeserializeObject<GameData>(jsonObject);
         //   data.English = english;
         //   string json_data = JsonUtility.ToJson(data);
         //   File.WriteAllText(TranslationConstants.PathToTranslationFoldersResources +@"GameData.json", json_data);
        }
        catch 
        {
            //AssetDatabase.CreateAsset(translationSO, TranslationConstants.PathToTranslationFolders + @$"{(en ? TranslationConstants.EnFolderName : TranslationConstants.RuFolderName)}\{translationObjects.Translations[i].kind.ToString()}.asset");
        }
    }
}