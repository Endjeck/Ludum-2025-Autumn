public class MenuModel
{
    private const int FIRST_LEVEL_INDEX = 2;
    private SceneChanger _sceneChanger = new SceneChanger();
    private Localizator _localizator;

    public MenuModel(Localizator localizator)
    {
        _localizator = localizator;
    }

    public void SetupPlayButton()
    {
        _sceneChanger.ChangeScene(FIRST_LEVEL_INDEX);
    }

    public void ChangeLanguage()
    {
        _localizator.ChangeLanguage();
        _localizator.OnLanguageChange.Invoke();
    }
}
