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
            currentTurn = players[currentTurnIndex];
            IsClickable = true;
            OutlineUpdate();
        }
    }

    private bool WinCheck()
    {
        // returns wiiner, if no one has won returns 0
        bool oneAlive, twoAlive, threeAlive, fourAlive;
        oneAlive = twoAlive = threeAlive = fourAlive = false;

        foreach (GameObject token in board.tokenRegistry)
        {
            if (new[] { oneAlive, twoAlive, threeAlive, fourAlive }.Count(x => x) > 1)
            {
                break;
            }
            if (token != null)
            {
                if (token.GetComponent<TokenInteraction>().player == 1)
                {
                    oneAlive = true;
                }
                if (token.GetComponent<TokenInteraction>().player == 2)
                {
                    twoAlive = true;
                }
                if (token.GetComponent<TokenInteraction>().player == 3)
                {
                    threeAlive = true;
                }
                if (token.GetComponent<TokenInteraction>().player == 4)
                {
                    fourAlive = true;
                }
            }
        }
        if (new[] { oneAlive, twoAlive, threeAlive, fourAlive }.Count(x => x) == 1)
        {
            print("someone won");
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
