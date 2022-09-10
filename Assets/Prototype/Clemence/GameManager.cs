using Prototype.Clemence;
using UnityEngine;

namespace CCLH
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Ghost[] ghostsList;
        public Pacman pacman;
        public Transform pacgums;
        public int PlayerScore { get; private set; }
        public int PlayerLives { get; private set; }

        public void Start()
        {
            NewGame();
        }

        public void NewGame()
        {
            SetScore(0);
            SetLives(2);
            ResetGhostsListState();
            ResetPacmanState();
        }

        public void SetScore(int score)
        {
            PlayerScore = score;
        }

        public void SetLives(int lives)
        {
            PlayerLives = lives;
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
        public void GameOver()
        {
            pacman.gameObject.SetActive(false);
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].gameObject.SetActive(false);
            }
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

        public bool HasRemainingPacgums()
        {
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
