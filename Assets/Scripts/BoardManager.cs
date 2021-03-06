﻿using System.Collections;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    int[,] board = new int[8, 8];
    public Turns turns;
    public JumpManager jump;

    public GameObject[,] tokenRegistry;
    public GameObject Player1_1, Player1_2, Player1_3, Player1_4;
    public GameObject Player2_1, Player2_2, Player2_3, Player2_4;
    public GameObject Player3_1, Player3_2, Player3_3, Player3_4;
    public GameObject Player4_1, Player4_2, Player4_3, Player4_4;
    private GameObject Token1, Token2, Token3, Token4;

    public AudioSource source;
    public AudioClip pointSound, explosionSound;

    private void Start()
    {
        Physics.autoSimulation = false;

        tokenRegistry = new GameObject[8, 8];
    }

    public void TokenRegister(GameObject token)
    {
        tokenRegistry[(int)token.transform.position.x, (int)(4 - token.transform.position.z)] = token;
    }

    public void AddPoint(int positionX, int positionY, bool firstJump)
    {
        source.clip = pointSound;
        source.Play();
        StartCoroutine(Point(positionX, positionY, firstJump));
    }

    private IEnumerator Point(int positionX, int positionY, bool firstJump)
    {
        if (positionX != -1 && positionX != 8 && positionY != -1 && positionY != 8)
        {
            if (board[positionX, positionY] == 3)
            {
                
                if (firstJump)
                {
                    turns.OutlineUpdate();
                }

                board[positionX, positionY] = 0;

                
                Destroy(tokenRegistry[positionX, positionY]);
                GameObject token4 = Instantiate(Token4, new Vector3(positionX, 0.1f, -positionY + 4), Quaternion.identity);
                float waitTime = 0.20f;
                if(firstJump)
                {
                    waitTime = 0.35f;
                }
                yield return new WaitForSeconds(waitTime);
                Destroy(token4);

                source.clip = explosionSound;
                source.Play();
                jump.Jump(positionX, positionY);


                // Wait for jump animation to end
                yield return new WaitForSeconds(0.9f);

                bool right, left, down, up;
                right = left = down = up = false;
                // Check surrounding tokens
                if (positionX != 7)
                {
                    right = CheckPoints(positionX + 1, positionY);
                }
                if (positionX != 0)
                {
                    left = CheckPoints(positionX - 1, positionY);

                }
                if (positionY != 7)
                {
                    down = CheckPoints(positionX, positionY + 1);
                }
                if (positionY != 0)
                {
                    up = CheckPoints(positionX, positionY - 1);
                }
                if(right)
                {
                    yield return StartCoroutine(Point(positionX + 1, positionY, false));
                }
                if(left)
                {
                    yield return StartCoroutine(Point(positionX - 1, positionY, false));
                }
                if(up)
                {
                    yield return StartCoroutine(Point(positionX, positionY - 1, false));
                }
                if (down)
                {
                    yield return StartCoroutine(Point(positionX, positionY + 1, false));
                }
            }
            else
            {
                CheckPoints(positionX, positionY);
            }
            if (firstJump)
            {
                turns.IncreaseTurn();
            }
        }
    }

    bool CheckPoints(int posX, int posY)
    {
        Vector3 originPos = new Vector3(posX, 0.1f, -posY + 4);
        bool jump = false;

        if (board[posX, posY] == 3)
        {
            jump = true;
            // Spawn 4 points token
            Destroy(tokenRegistry[posX, posY]);
            TokenRegister(Instantiate(Token4, originPos, Quaternion.identity));

        }
        else
        {
            if (tokenRegistry[posX, posY] != null)
            {
                Destroy(tokenRegistry[posX, posY]);
            }
            board[posX, posY] += 1;
            // Spawn new token
            switch (board[posX, posY])
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
        return jump;
    }

    public void AddPlayer(int arrayPosition)
    {
        switch (arrayPosition)
        {
            case 1:
                if (board[1, 1] == 3)
                {
                    turns.player1 = false;
                    board[1, 1] = 0;
                }
                else
                {
                    turns.player1 = true;
                    board[1, 1] = 3;
                }
                break;
            case 2:
                if (board[6, 1] == 3)
                {
                    turns.player2 = false;
                    board[6, 1] = 0;
                }
                else
                {
                    turns.player2 = true;
                    board[6, 1] = 3;
                }
                break;
            case 3:
                if (board[1, 6] == 3)
                {
                    turns.player3 = false;
                    board[1, 6] = 0;
                }
                else
                {
                    turns.player3 = true;
                    board[1, 6] = 3;
                }
                break;
            case 4:
                if (board[6, 6] == 3)
                {
                    turns.player4 = false;
                    board[6, 6] = 0;
                }
                else
                {
                    turns.player4 = true;
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

    private string Nasty2D(int[,] nastyArray)
    {
        string boardS = "";
        for (int i = 0; i < nastyArray.GetLength(0); i++)
        {
            for (int j = 0; j < nastyArray.GetLength(1); j++)
            {
                boardS += nastyArray[j, i] + " - ";
                if (j == (nastyArray.GetLength(1) - 1))
                {
                    boardS += System.Environment.NewLine;
                }
            }
        }
        return boardS;
    }
}