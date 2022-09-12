using Prototype.Clemence;
using Ghosts;
using UnityEngine;

namespace CCLH
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GhostStateManager[] ghostsList;
        public Pacman pacman;
        public Transform pacgums;
        public int PlayerScore { get; private set; }
        public int PlayerLives { get; private set; }

        public void Start()
        {
            NewGame();
        }
        public void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                PacmanDies();
            }
        }
        public void NewGame()
        {
            SetScore(0);
            SetLives(2);
            foreach (Transform pacgum in pacgums)
            {
                pacgum.gameObject.SetActive(true);
            }
        }
        public void ResetState()
        {
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
            pacman.ResetState();
        }
        public void ResetGhostsListState()
        {
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].gameObject.SetActive(true);
                ghostsList[i].ChangeState(ghostsList[i].RespawnState);
            }
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
            pacman.DeathSequence();

            SetLives(PlayerLives - 1);
            if (PlayerLives > 0)
            {
                Invoke(nameof(ResetState), 3f);
            }
            else
            {
                GameOver();
            }
        }
        public void EatGhost(GhostStateManager ghost)
        {
            SetScore(PlayerScore + ghost._points);
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
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].ChangeState(ghostsList[i].WeakState);
            }
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
