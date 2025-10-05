using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class TranslationConventer : Editor
{
    [MenuItem("Tools/Get translation objects")]
    private static void JsonToSOs()
    {
        string jsonObject = Resources.Load("Translations").ToString();
        TranslationObjects translationObjects = JsonConvert.DeserializeObject<TranslationObjects>(jsonObject);
        CreateSos(translationObjects);
    }

    private static void CreateSos(TranslationObjects translationObjects)
    {
        DeleteAllFile(TranslationConstants.PathToTranslationFolders + TranslationConstants.RuFolderName);
        DeleteAllFile(TranslationConstants.PathToTranslationFolders + TranslationConstants.EnFolderName);
        for (int i = 0; i < translationObjects.Translations.Length; i++)
        {
            CreateAsset(translationObjects, i, true);
            CreateAsset(translationObjects, i, false);
        }
    }

    private static void CreateAsset(TranslationObjects translationObjects, int i, bool en)
    {
        TranslationSO translationSO = ScriptableObject.CreateInstance<TranslationSO>();
        translationSO.name = translationObjects.Translations[i].kind.ToString();
        translationSO.SetKind(translationObjects.Translations[i].kind);
        translationSO.SetText(en ? translationObjects.Translations[i].en : translationObjects.Translations[i].ru);
        AssetDatabase.CreateAsset(translationSO, TranslationConstants.PathToTranslationFolders + @$"{(en ? TranslationConstants.EnFolderName : TranslationConstants.RuFolderName)}\{translationObjects.Translations[i].kind.ToString()}.asset");
    }

    private static bool DeleteAllFile(string fullPath)
    {
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                string FilePath = fullPath + "/" + files[i].Name;
                File.Delete(FilePath);
            }
            return true;
        }
        return false;
    }
}
