using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    int[,] board = new int[8, 8];
    public Turns turns;
    public JumpManager jump;
    public PathFinder pf;

    public GameObject[,] tokenRegistry;
    public GameObject Player1_1, Player1_2, Player1_3;
    public GameObject Player2_1, Player2_2, Player2_3;
    public GameObject Player3_1, Player3_2, Player3_3;
    public GameObject Player4_1, Player4_2, Player4_3;
    private GameObject Token1, Token2, Token3;

    // The reason why PathFinder exists
    public int waitTime;

    private void Start()
    {
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
        if (positionX != -1 && positionX != 8 && positionY != -1 && positionY != 8)
        {
            if (board[positionX, positionY] < 3)
            {
                // Delete old token gameobject
                if (tokenRegistry[positionX, positionY] != null)
                {
                    Destroy(tokenRegistry[positionX, positionY]);
                }
                board[positionX, positionY] += 1;
                // Spawn new token
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
                if (firstJump)
                {
                    // Predict jump time here.
                    waitTime = pf.Run(board, positionX, positionY);
                    Debug.Log("waitTime: " + waitTime);
                    turns.IsClickable = false;
                    turns.OutlineUpdate();
                }
                board[positionX, positionY] = 0;
                jump.Jump(positionX, positionY);
                Destroy(tokenRegistry[positionX, positionY]);
                yield return new WaitForSeconds(1f);
                // Wait a second for jump animation to end
                // Then check surrounding tokens

                AddPoint(positionX, positionY + 1, false);
                AddPoint(positionX, positionY - 1, false);
                AddPoint(positionX - 1, positionY, false);
                AddPoint(positionX + 1, positionY, false);
            }
            if (firstJump)
            {
                // Use jump prediction time to wait here.
                //yield return new WaitForSeconds(waitTime + 0.1f);
                yield return new WaitForSeconds(waitTime);
                waitTime = 0;
                turns.IncreaseTurn();
            }
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

    public void ChooseModel()
    {
        switch (turns.currentTurn)
        {
            case 1:
                Token1 = Player1_1;
                Token2 = Player1_2;
                Token3 = Player1_3;
                break;
            case 2:
                Token1 = Player2_1;
                Token2 = Player2_2;
                Token3 = Player2_3;
                break;
            case 3:
                Token1 = Player3_1;
                Token2 = Player3_2;
                Token3 = Player3_3;
                break;
            case 4:
                Token1 = Player4_1;
                Token2 = Player4_2;
                Token3 = Player4_3;
                break;
        }
    }
}