using System;
using Prototype.Clemence;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;



namespace CCLH
{
    public class GameManager : MonoBehaviour
    {

        #region Singleton declaration

        public static GameManager Singleton;

        private void Awake()
        {
            if (Singleton != null && Singleton != this) Destroy(gameObject);

            Singleton = this;
        }

        #endregion

        [SerializeField] private Ghost[] ghostsList;
        public Pacman pacman;
        public Transform pacgums;
        public int PlayerScore { get; private set; }
        public int HScore;
        public int PlayerLives { get; private set; }
        public Text ScoreText;
        public Text LP;
        public Text HighScore;
        public Image ready;
        public Image gameOver;



        public void Start()
        {
            NewGame();
            gameOver.enabled = false;

        }

        public void NewGame()
        {
            SetScore(0);
            SetLives(2);
            ResetGhostsListState();
            ResetPacmanState();
            HighScore.text = "High Score  : " + System.IO.File.ReadAllText("Score.txt"); //récupération du score
            HScore = int.Parse(System.IO.File.ReadAllText("Score.txt"));

            ready.enabled = true;




        }


        public void affichageReady()
        {
            //ready.enabled = true;

            float counter = 0;
            float waitTime = 200;
            while (counter < waitTime)
            {
                ready.enabled = true;
                counter += Time.deltaTime;


            }
            ready.enabled = false;

        }

        public void SetScore(int score)
        {
            PlayerScore = score;
            ScoreText.text = "Score : " + PlayerScore.ToString();
            ready.enabled = false;
    
            if (HScore<score)
            {
                HScore = score;
                HighScore.text = "High Score : " + HScore.ToString();
                System.IO.File.WriteAllText("Score.txt", HScore.ToString()); //ecriture dans le .txt


            }
        }

        

        public void SetLives(int lives)
        {
            PlayerLives = lives;
            LP.text = PlayerLives + " UP";
        }

        public void ResetPacmanState()
        {
            pacman.gameObject.SetActive(true);
        }

        public void ResetGhostsListState()
        {
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].gameObject.SetActive(true);
            }
        }
        public void ResetPelletsListState()
        {

        }
        private void GameOver()
        {
            pacman.gameObject.SetActive(false);
            //TODO : Désactiver les inputs de PacMan
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].gameObject.SetActive(false);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            gameOver.enabled = true;


        }
        public void PacmanDies()
        {
            pacman.gameObject.SetActive(false);
            SetLives(PlayerLives - 1);
            if (PlayerLives == 0)
            {
                GameOver();
            }
        }

        #region Score related

        public void EatGhost(Ghost ghost)
        {
            SetScore(PlayerScore + ghost.points);
        }

        public void EatPacgum(Pacgum pacgum)
        {
            pacgum.gameObject.SetActive(false);
            SetScore(PlayerScore + pacgum.points);
            if (!HasRemainingPacgums())
            {
                //lancer l'UI pour rejouer ou quitter
                Debug.Log("you win");
            }
        }

        public void EatSuperPacgum(SuperPacgum superPacgum)
        {
            EatPacgum(superPacgum);
            //changer etat fantomes
        }

        #endregion

        public bool HasRemainingPacgums()
        {
            //TODO : Checker avec le score.
            foreach (Transform pacgum in pacgums)
            {
                if (pacgum.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
