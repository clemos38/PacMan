using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghostsList;
    public Pacman pacman;
    public Transform[] pellets;
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

    public void EatPellet(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(PlayerScore + pellet.points);
        if (!HasRemainingPellets())
        {
            //lancer l'UI pour rejouer ou quitter
        }
    }

    public void EatPowerPellet(PowerPellet powerPellet)
    {
        EatPellet(powerPellet);
        //changer etat fantomes
    }

    public bool HasRemainingPellets()
    {
        foreach(Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
