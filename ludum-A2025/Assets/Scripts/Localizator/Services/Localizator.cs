using System;
using UnityEngine;

public class Localizator :IGameService
{
    private bool _english = true;
    private Action _action;
    public Action OnLanguageChange { 
        get 
        { return _action; }
        set { _action = value; }
    }

    public bool English => _english;

    public Localizator(bool english = true)
    {
        _english = english;
    }

    public void ChangeLanguage()
    {
        _english = !English;
        PlayerPrefs.SetInt(TranslationConstants.TranslateParametr, _english ? 1 : 0);
    }

    public string GetLocalizetedText(TranslationEnum translation)
    {
        TranslationSO translationSO = Resources.Load<TranslationSO>(TranslationConstants.PathToTranslationFoldersResources 
            + (_english ? TranslationConstants.EnFolderName : TranslationConstants.RuFolderName)
            + @"\" + translation.ToString());
        return translationSO.Text;
    }
    
}
