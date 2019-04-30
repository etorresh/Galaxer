using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public int player;
    private GameObject token;
    private BoardManager board;
    private Turns turns;
    private GameObject play;

    private void Awake()
    {
        play = GameObject.Find("Play");
    }

    private void Start()
    {
        token = GameObject.FindWithTag("Jugador" + player.ToString());
        token.SetActive(false);
        board = GameObject.FindWithTag("MainCamera").GetComponent<BoardManager>();
        turns = GameObject.FindWithTag("MainCamera").GetComponent<Turns>();
        if (play.activeSelf)
        {
            play.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        token.SetActive(!token.activeSelf);
        board.AddPlayer(player);

        // Activates play button if there are 2 or more players.
        if (token.activeSelf)
        {
            turns.currentPlayers += 1;
            if (turns.currentPlayers >= 2)
            {
                play.SetActive(true);
            }
        }
        else
        {
            turns.currentPlayers -= 1;
            if (turns.currentPlayers == 1)
            {
                play.SetActive(false);
            }
        }
    }

    // Destroys disabled tokens and all player spawns. If active adds token to token registry.
    public void StartGame()
    {
        if(!token.activeSelf)
        {
            Destroy(token);
        }
        else
        {
            board.TokenRegister(token);
        }
        Destroy(gameObject);
    }
}
