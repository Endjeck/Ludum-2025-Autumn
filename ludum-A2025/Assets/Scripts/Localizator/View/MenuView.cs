using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Toggle _languageToggle;
    private MenuController _menuController;

    public Button PlayButton => _playButton;
    public Toggle LanguageToggle => _languageToggle;

    private void Start()
    {
        _menuController = new MenuController(this);
        _menuController.SetupMenu();
    }
} 
