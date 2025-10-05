using UnityEngine.SceneManagement;

public class SceneChanger 
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
