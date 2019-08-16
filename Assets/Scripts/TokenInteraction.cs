using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInteraction : MonoBehaviour
{
    public int player;
    private BoardManager board;
    public Turns turns;

    private void Start()
    {
        GameObject mainCamera = Camera.main.gameObject;

        board = mainCamera.GetComponent<BoardManager>();
        turns = mainCamera.GetComponent<Turns>();
    }

    void OnMouseDown()
    {
        if(turns.currentTurn == player && turns.IsClickable)
        {
            board.ChooseModel();
            board.AddPoint((int)gameObject.transform.position.x, 4 - (int)gameObject.transform.position.z, true);
        }
    }
}
