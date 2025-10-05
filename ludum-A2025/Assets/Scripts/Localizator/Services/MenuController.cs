using UnityEditor;
using UnityEngine;

public class MenuController 
{
    private ServiceLocator _serviceLocator = ServiceLocator.Container;
    private Localizator _localizator;
    private MenuView _menuView;
    private MenuModel _menuModel;

    public MenuController(MenuView menuView)
    {
        _localizator = _serviceLocator.Get<Localizator>();
        _menuModel = new MenuModel(_localizator);
        _menuView = menuView;
    }

    public void SetupMenu()
    {
        _menuView.LanguageToggle.isOn = _localizator.English;
        _menuView.LanguageToggle.onValueChanged.AddListener((g) => _menuModel.ChangeLanguage());
        _menuView.PlayButton.onClick.AddListener(_menuModel.SetupPlayButton);
    }
}
