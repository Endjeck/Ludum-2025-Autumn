using TMPro;
using UnityEngine;

public class TranslatableText : MonoBehaviour
{
    [SerializeField] private TranslationEnum _translation;
    [SerializeField] private TMP_Text _text;
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
        _text.text = _localizator.GetLocalizetedText(_translation);
    }
}
