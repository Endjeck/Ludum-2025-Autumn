using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootSetupter : MonoBehaviour
{
    private SceneChanger _changer = new SceneChanger();
    private ServiceLocator _serviceLocator => ServiceLocator.Container;
    private void Awake()
    {
        RegisterLocaliztionService();
    }
    void Start()
    {
        _changer.ChangeScene(1);
    }

    private void RegisterLocaliztionService()
    {
        bool english = PlayerPrefs.GetInt(TranslationConstants.TranslateParametr) == 1;
        _serviceLocator.RegisterSingle(new Localizator(english));
    }

}
