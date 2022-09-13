using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_play : MonoBehaviour
{
    [SerializeField] private Text highscoreTxt;
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

   private void Start()
   {
       SetHighScore();
   }

   private void SetHighScore()
   {
       highscoreTxt.text = "High Score  : " + System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Score.txt"); //r�cup�ration du score   
   }

    public void QuitGame()
    {
        Application.Quit();
    }
}
