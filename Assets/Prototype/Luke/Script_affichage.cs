using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ScoreText;
    public Text LP;
    public Text HighScore;
    public Image ready;
    public Image gameOver;
    public int HScore;


     public void affichage()
    {
        gameOver.enabled = false;

    }

    public void affichageReady()
    {
        HighScore.text = "High Score  : " + System.IO.File.ReadAllText("Score.txt"); //récupération du score
        HScore = int.Parse(System.IO.File.ReadAllText("Score.txt"));

        ready.enabled = true;
    }

    public void  affichageScore(int score, int PlayerScore)
    {
        ScoreText.text = "Score : " + PlayerScore.ToString();
        ready.enabled = false;

        if (HScore < score)
        {
            HScore = score;
            HighScore.text = "High Score : " + HScore.ToString();
            System.IO.File.WriteAllText("Score.txt", HScore.ToString()); //ecriture dans le .txt


        }
    }

    public void affichageLives(int PlayerLives)
    {
        LP.text = PlayerLives + " UP";
    }

    public void affichageGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        gameOver.enabled = true;
    }
}
