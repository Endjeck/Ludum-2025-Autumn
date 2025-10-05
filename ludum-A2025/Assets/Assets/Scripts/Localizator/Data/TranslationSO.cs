using UnityEngine;

public class TranslationSO : ScriptableObject
{
    private TranslationEnum _kind;
    [SerializeField] private string _text;

    public TranslationEnum Kind => _kind;

    public string Text => _text;

    public void SetKind(TranslationEnum kind)
    {
        _kind = kind;
    }
    public void SetText(string text)
    {
        _text = text;
    }
}
