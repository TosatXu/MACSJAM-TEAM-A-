using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitleScreen : MonoBehaviour
{
    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}