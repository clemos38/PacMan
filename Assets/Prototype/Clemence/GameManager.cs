using Ghosts;
using Prototype.Luke;
using UnityEngine;

namespace CCLH
{
    public class GameManager : MonoBehaviour
    {
        #region SIngleton declaration

        public static GameManager Singleton;

        private void Awake()
        {
            if(Singleton != null && Singleton != this) Destroy(gameObject);

            Singleton = this;
        }

        #endregion
        
        [SerializeField] private GhostStateManager[] ghostsList;
        public Pacman pacman;
        public Transform pacgums;
        public int PlayerScore { get; private set; }
        public int PlayerLives { get; private set; }

        private UIManager _ui;

        float timeRemaining = 7;
        float weakTimer = 15;
        int cycle = 0;
        bool ghostWeak = false;


        public void Start()
        {
            _ui = UIManager.Singleton;
            NewGame();
            
        }
        
        public void NewGame()
        {
            SetScore(0);
            SetLives(2);
            foreach (Transform pacgum in pacgums)
            {
                pacgum.gameObject.SetActive(true);
            }
            _ui.Affichage();
        }
        public void ResetState()
        {
            ResetGhostsListState();
            ResetPacmanState();
            timeRemaining = 7;
            cycle = 0;
        }
        public void SetScore(int score)
        {
            PlayerScore = score;
            _ui.AffichageReady();
            if(score != 0) _ui.AffichageScore(score,PlayerScore);
            
        }
        public void SetLives(int lives)
        {
            PlayerLives = lives;
            _ui.AffichageLives(lives);
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
            _ui.AffichageGameOver();
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
            SetScore(PlayerScore + ghost.points);
        }
        public void EatPacgum(Pacgum pacgum)
        {
            pacgum.gameObject.SetActive(false);
            SetScore(PlayerScore + pacgum.points);
            if (!HasRemainingPacgums())
            {
                //TODO : lancer l'UI pour rejouer ou quitter
                Debug.Log("you win");
            }
        }
        public void EatSuperPacgum(SuperPacgum superPacgum)
        {
            EatPacgum(superPacgum);
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].ChangeState(ghostsList[i].WeakState);
                ghostWeak = true;
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

        void SwitchGhostScatter()
        {
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].ChangeState(ghostsList[i].NormalState);
            }
        }

        void SwitchGhostChase()
        {
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].ChangeState(ghostsList[i].ChaseState);
            }
        }

        public bool GhostPausedInScatter() => cycle % 2 == 0;
        

        void Update()
        {
            if(ghostWeak == true)
            {
                if(weakTimer >0)
                {
                    weakTimer -= Time.deltaTime;
                }
                else
                {
                    ghostWeak = false;
                }
            }
            else
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else if(cycle < 7)
                {
                    switch(cycle)
                    {
                        case 0:
                            timeRemaining = 20;
                            SwitchGhostChase();
                            break;
                        
                        case 1:
                            timeRemaining = 7;
                            SwitchGhostScatter();
                            break;

                        case 2:
                            timeRemaining = 20;
                            SwitchGhostChase();
                            break;

                        case 3:
                            timeRemaining = 5;
                            SwitchGhostScatter();
                            break;

                        case 4:
                            timeRemaining = 20;
                            SwitchGhostChase();
                            break;

                        case 5:
                            timeRemaining = 5;
                            SwitchGhostScatter();
                            break;

                        case 6:
                            timeRemaining = 10000;
                            SwitchGhostChase();
                            break;
                    }
                    cycle++;
                }
            }
        }
    }
}
