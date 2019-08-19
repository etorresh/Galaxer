using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public int player;
    public GameObject token;

    public Camera cameraMain;
    public BoardManager board;
    public Turns turns;
    public GameObject play;

    private void Start()
    {
        Vector3 mPos = cameraMain.ViewportToWorldPoint(new Vector3(0, 0, 0));
        switch (player)
        {
            case 1:
                mPos = cameraMain.ViewportToWorldPoint(new Vector3(0, 1, 0));
                mPos.x += gameObject.GetComponent<Collider>().bounds.size.x / 2;
                mPos.z -= gameObject.GetComponent<Collider>().bounds.size.z / 2;
                break;
            case 2:
                mPos = cameraMain.ViewportToWorldPoint(new Vector3(1, 1, 0));
                mPos.x -= gameObject.GetComponent<Collider>().bounds.size.x / 2;
                mPos.z -= gameObject.GetComponent<Collider>().bounds.size.z / 2;
                break;

            case 3:
                mPos.x += gameObject.GetComponent<Collider>().bounds.size.x / 2;
                mPos.z += gameObject.GetComponent<Collider>().bounds.size.z / 2;
                break;
            case 4:
                mPos = cameraMain.ViewportToWorldPoint(new Vector3(1, 0, 0));
                mPos.x -= gameObject.GetComponent<Collider>().bounds.size.x / 2;
                mPos.z += gameObject.GetComponent<Collider>().bounds.size.z / 2;
                break;
        }
        mPos.y = 0;
        transform.position = mPos;
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
