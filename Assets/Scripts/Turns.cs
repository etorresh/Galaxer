using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public int currentPlayers;
    private int[] players;
    public int currentTurn;
    public bool IsClickable = true;
    int currentTurnIndex;
    private BoardManager board;

    private void Start()
    {
        board = GameObject.FindWithTag("MainCamera").GetComponent<BoardManager>();
    }

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
        board.waitTime = 0;
        currentTurnIndex += 1;
        if(players.Length == currentTurnIndex)
        {
            currentTurnIndex = 0;
        }
        currentTurn = players[currentTurnIndex];
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
        if(IsClickable)
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
