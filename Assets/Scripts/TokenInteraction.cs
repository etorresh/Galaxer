using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInteraction : MonoBehaviour
{
    public int player;
    private BoardManager board;
    private Turns turns;

    private void Start()
    {
        board = GameObject.FindWithTag("MainCamera").GetComponent<BoardManager>();
        turns = GameObject.FindWithTag("MainCamera").GetComponent<Turns>();
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
