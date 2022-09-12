using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Prototype.Luke
{
    public class UIManager : MonoBehaviour
    {
        #region SingletonDeclaration

        public static UIManager Singleton;

        private void Awake()
        {
            if(Singleton != null && Singleton != this) Destroy(gameObject);

            Singleton = this;
            
            _path = Application.streamingAssetsPath + "/Score.txt";
        }

        #endregion
        
        // Start is called before the first frame update
        public Text ScoreText;
        public Text LP;
        public Text HighScore;
        public Image ready;
        public Image gameOver;
        public int HScore;
        public Image Win;

        private string _path;

        public void Affichage()
        {
            gameOver.enabled = false;
            Win.enabled = false;

        }

        public void AffichageReady()
        {
            HScore = int.Parse(System.IO.File.ReadAllText(_path));
            HighScore.text = "High Score  : " + HScore; //r�cup�ration du score
            

            ready.enabled = true;
        }

        public void  AffichageScore(int score, int playerScore)
        {
            ScoreText.text = "Score : " + playerScore.ToString();
            ready.enabled = false;

            if (HScore < score)
            {
                HScore = score;
                HighScore.text = "High Score : " + HScore.ToString();
                System.IO.File.WriteAllText(_path, HScore.ToString()); //ecriture dans le .txt


            }
        }

        public void AffichageLives(int playerLives)
        {
            LP.text = playerLives + " UP";
        }

        public async void AffichageGameOver()
        {
            gameOver.enabled = true;
            await Task.Delay(5 * 1000);
            SceneManager.LoadScene(0);
        }

        public async void AffichageWin()
        {
            Win.enabled = true;
            await Task.Delay(5 * 1000);
            SceneManager.LoadScene(0);
        }
    }
}
