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
    public GameObject Player2_1, Player2_2, Player2_3, Player2_4;
    public GameObject Player3_1, Player3_2, Player3_3, Player3_4;
    public GameObject Player4_1, Player4_2, Player4_3, Player4_4;
    private GameObject Token1, Token2, Token3, Token4;

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
            // Borrar ficha anterior
            if(tokenRegistry[positionX, positionY] != null)
            {
                Destroy(tokenRegistry[positionX, positionY]);
            }
            board[positionX, positionY] += 1;
            // Aparecer nueva ficha
            ChooseModel();
            Vector3 originPos = new Vector3(positionX, 0.1f, -positionY + 4);
            switch (board[positionX, positionY])
            {
                case 1:
                    TokenRegister(Instantiate(Token1, originPos, Quaternion.identity));
                    break;
                case 2:
                    TokenRegister(Instantiate(Token2, originPos, Quaternion.identity));
                    break;
                case 3:
                    TokenRegister(Instantiate(Token3, originPos, Quaternion.identity));
                    break;
            }
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
            yield return new WaitForSeconds(1f);
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

    private void ChooseModel()
    {
        switch (turns.temporalTurn)
        {
            case 1:
                Token1 = Player1_1;
                Token2 = Player1_2;
                Token3 = Player1_3;
                Token4 = Player1_4;
                break;
            case 2:
                Token1 = Player2_1;
                Token2 = Player2_2;
                Token3 = Player2_3;
                Token4 = Player2_4;
                break;
            case 3:
                Token1 = Player3_1;
                Token2 = Player3_2;
                Token3 = Player3_3;
                Token4 = Player3_4;
                break;
            case 4:
                Token1 = Player4_1;
                Token2 = Player4_2;
                Token3 = Player4_3;
                Token4 = Player4_4;
                break;
        }
    }
}
