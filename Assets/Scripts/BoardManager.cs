using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    int[,] board = new int[8, 8];
    private Turns turns;
    private JumpManager jump;
    private GameObject[,] tokenRegistry;
    public GameObject Player1_1, Player1_2, Player1_3, Player1_4;

    private void Start()
    {
        turns = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Turns>();
        jump = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<JumpManager>();

        tokenRegistry = new GameObject[8, 8];
    }

    public void TokenRegister(GameObject token)
    {
        tokenRegistry[(int)token.transform.position.x, (int)(4 - token.transform.position.z)] = token;
    }

    public void AddPoint(int positionX, int positionY, bool firstJump)
    {
        StartCoroutine(Point(positionX, positionY, firstJump));
    }

    private IEnumerator Point(int positionX, int positionY, bool firstJump)
    {
        if (positionX == -1 || positionX == 8 || positionY == -1 || positionY == 8)
        {
            yield break;
        }

        if (board[positionX, positionY] < 3)
        {
            board[positionX, positionY] += 1;
            
            // Animacion de aumento de ficha
        }
        else if (board[positionX, positionY] == 3)
        {
            if(firstJump)
            {
                turns.Freeze();
            }
            turns.OutlineUpdate();
            board[positionX, positionY] = 0;
            jump.Jump(positionX, positionY);
            Destroy(tokenRegistry[positionX, positionY]);
            yield return new WaitForSeconds(0.25f);
            // Convertir a los de alrededor en el color del jugador actual y en una ficha mayor
            AddPoint(positionX, positionY + 1, false);
            AddPoint(positionX, positionY - 1, false);
            AddPoint(positionX - 1, positionY, false);
            AddPoint(positionX + 1, positionY, false);
        }
        else
        {
            Debug.Log("AddPoint comparison resulted in a number higher than 3");
        }
        if(firstJump)
        {
            turns.Unfreeze();
            turns.IncreaseTurn();
        }
    }

    public void AddPlayer(int arrayPosition)
    {
        switch (arrayPosition)
        {
            case 1:
                if (board[1, 1] == 3)
                {
                    board[1, 1] = 0;
                }
                else
                {
                    board[1, 1] = 3;
                }
                break;
            case 2:
                if (board[6, 1] == 3)
                {
                    board[6, 1] = 0;
                }
                else
                {
                    board[6, 1] = 3;
                }
                break;
            case 3:
                if (board[1, 6] == 3)
                {
                    board[1, 6] = 0;
                }
                else
                {
                    board[1, 6] = 3;
                }
                break;
            case 4:
                if (board[6, 6] == 3)
                {
                    board[6, 6] = 0;
                }
                else
                {
                    board[6, 6] = 3;
                }
                break;
            default:
                Debug.Log("AddPlayer parameter must be an integer between 1 and 4.");
                break;
        }
    }
}
