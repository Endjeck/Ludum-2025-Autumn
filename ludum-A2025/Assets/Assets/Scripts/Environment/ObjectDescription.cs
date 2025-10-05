using TMPro;
using UnityEngine;

public class ObjectDescription : MonoBehaviour
{
    [SerializeField] private TranslationEnum _translation;
    public string Description { get; private set; }
    private ServiceLocator _locator => ServiceLocator.Container;
    private Localizator _localizator;
    private void Awake()
    {
        _localizator = _locator.Get<Localizator>();
        _localizator.OnLanguageChange += ChangeText;
        ChangeText();
    }
    private void OnDisable()
    {
        _localizator.OnLanguageChange -= ChangeText;
    }

    private void ChangeText()
    {
        Description = _localizator.GetLocalizetedText(_translation);
    }
}