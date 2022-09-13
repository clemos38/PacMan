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
        
        private void NewGame()
        {
            AudioManager.Singleton.PlaySound("start");
            SetScore(0);
            SetLives(2);
            foreach (Transform pacgum in pacgums)
            {
                pacgum.gameObject.SetActive(true);
            }
            _ui.Affichage();
            //Pause everything (disable normal inputs)

        }

        #region Setters

        private void SetScore(int score)
        {
            PlayerScore = score;
            _ui.AffichageReady();
            if(score != 0) _ui.AffichageScore(score,PlayerScore);
            
        }
        private void SetLives(int lives)
        {
            PlayerLives = lives;
            _ui.AffichageLives(lives);
        }

        #endregion


        #region Reset related

        public void ResetState()
        {
            ResetGhostsListState();
            ResetPacmanState();
            timeRemaining = 7;
            cycle = 0;
        }
        private void ResetPacmanState()
        {
            pacman.ResetState();
        }
        private void ResetGhostsListState()
        {
            foreach (var ghost in ghostsList)
            {
                ghost.gameObject.SetActive(true);
                ghost.ChangeState(ghost.RespawnState);
            }
        }

        #endregion
        private void GameOver()
        {
            pacman.gameObject.SetActive(false);
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].gameObject.SetActive(false);
            }
            AudioManager.Singleton.PlaySound("death");
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

        #region Interaction

        public void EatGhost(GhostStateManager ghost)
        {
            AudioManager.Singleton.PlaySound("ghost");
            SetScore(PlayerScore + ghost.points);
        }
        public void EatPacgum(Pacgum pacgum)
        {
            pacgum.gameObject.SetActive(false);
            SetScore(PlayerScore + pacgum.points);
            if (!HasRemainingPacgums())
            {
                //TODO : lancer l'UI pour rejouer ou quitter
                //Debug.Log("you win");
                _ui.AffichageWin();
            }
        }
        public void EatSuperPacgum(SuperPacgum superPacgum)
        {
            EatPacgum(superPacgum);
            AudioManager.Singleton.PlaySound("powered");
            for (int i = 0; i < ghostsList.Length; i++)
            {
                ghostsList[i].ChangeState(ghostsList[i].WeakState);
                ghostWeak = true;
            }
        }

        #endregion
        
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

        #region Ghost states related

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

        private bool StillWeakGhost()
        {
            foreach (var ghost in ghostsList)
            {
                if (ghost.IsWeak()) return true;
            }

            return false;
        }

        #endregion
        void Update()
        {
            if(ghostWeak)
            {
                if(weakTimer <= 0 || !StillWeakGhost()) //! pas très optimisé
                {
                    AudioManager.Singleton.PlaySound("powered", false);
                    weakTimer = 15;
                    ghostWeak = false;
                }
                else
                {
                    
                    weakTimer -= Time.deltaTime;
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
