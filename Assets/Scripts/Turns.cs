using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turns : MonoBehaviour
{
    public int currentPlayers;
    private int[] players;
    public int currentTurn;
    public bool IsClickable = true;
    int currentTurnIndex;
    public BoardManager board;
    public bool player1, player2, player3, player4;
    public GameObject resetButton;
    public AdManager adM;

    public void GameStart()
    {
        System.Random rand = new System.Random();
        players = new int[currentPlayers];
        int x = 0;
        for (int i = 1; i <= 4; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Jugador" + i.ToString()).Length != 0)
            {
                players[x] = i;
                x++;
            }
        }
        currentTurnIndex = rand.Next(0, currentPlayers);
        currentTurn = players[currentTurnIndex];
        OutlineUpdate();
    }

    public void IncreaseTurn()
    {
        if (!WinCheck())
        {
            currentTurnIndex += 1;
            if (players.Length == currentTurnIndex)
            {
                currentTurnIndex = 0;
            }

            var alive = AliveCheck();

            if(player1 && !alive[0] && currentTurnIndex == 0)
            {
                currentTurnIndex += 1;
            }
            if (player2 && !alive[1] && currentTurnIndex == 1)
            {
                currentTurnIndex += 1;
            }
            if (player3 && !alive[2] && currentTurnIndex == 2)
            {
                currentTurnIndex += 1;
            }
            if (player4 && !alive[3] && currentTurnIndex == 3)
            {
                currentTurnIndex += 1;
            }

            currentTurn = players[currentTurnIndex];
            IsClickable = true;
            OutlineUpdate();
        }
        else
        {
            // To-do event when someone wins
            resetButton.SetActive(true);
            adM.StartCoroutine(adM.ShowVideoWhenReady());
        }
    }

    private bool[] AliveCheck()
    {
        bool oneAlive, twoAlive, threeAlive, fourAlive;
        oneAlive = twoAlive = threeAlive = fourAlive = false;
        foreach (GameObject token in board.tokenRegistry)
        {
            if (token != null)
            {
                int currentPlayer = token.GetComponent<TokenInteraction>().player;
                if (currentPlayer == 1)
                {
                    oneAlive = true;
                }
                else if (currentPlayer == 2)
                {
                    twoAlive = true;
                }
                else if (currentPlayer == 3)
                {
                    threeAlive = true;
                }
                else if (currentPlayer == 4)
                {
                    fourAlive = true;
                }
            }
        }
        bool[] alive = new bool[4];
        alive[0] = oneAlive;
        alive[1] = twoAlive;
        alive[2] = threeAlive;
        alive[3] = fourAlive;
        return alive;
    }

    private bool WinCheck()
    {
        var alive = AliveCheck();
        if (alive.Count(x => x) == 1)
        {
            return true;
        }
        return false; 
    }

    public void OutlineUpdate()
    {
        for (int i = 0; i < currentPlayers; i++)
        {
            GameObject[] tokensDisable = GameObject.FindGameObjectsWithTag("Jugador" + players[i].ToString());
            foreach (GameObject token in tokensDisable)
            {
                if (token != null)
                {
                    token.GetComponent<Outline>().enabled = false;
                }
            }
        }
        if (IsClickable)
        {
            GameObject[] tokensEnable = GameObject.FindGameObjectsWithTag("Jugador" + players[currentTurnIndex].ToString());
            foreach (GameObject token in tokensEnable)
            {
                if (token != null)
                {
                    token.GetComponent<Outline>().enabled = true;
                }
            }
        }
    }
}
