using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder: MonoBehaviour
{
    private int[,] Paths;
    private int[,] boardClone;
    private int jumpEnumerator;

    private List<int> blacklist, pathsTime;
    private int timer;

    private int originX, originY;

    public int Run(int[,] boardState, int posX, int posY)
    {
        // Variables are declared and reset.
        Paths = new int[8, 8];
        boardClone = (int[,])boardState.Clone();
        jumpEnumerator = 1;
        blacklist = new List<int>();
        pathsTime = new List<int>();
        timer = 0;

        CreatePaths(posX, posY);

        // Find starting position
        for (int i = 0; i < Paths.GetLength(0); i++)
        {
            for (int j = 0; j < Paths.GetLength(1); j++)
            {
                if (Paths[j, i] == 1)
                {
                    originX = j;
                    originY = i;
                    FindPath(j, i);
                }
            }
        }
        return Mathf.Max(pathsTime.ToArray());
    }

    private void CreatePaths(int posX, int posY)
    {
        // Simulates jumps and creates possible paths.
        if (boardClone[posX, posY] == 3)
        {
            boardClone[posX, posY] = 0;
            Paths[posX, posY] = jumpEnumerator;
            jumpEnumerator += 1;
            if(posX != 7)
            {
                CreatePaths(posX + 1, posY);
            }
            if (posX != 0)
            {
                CreatePaths(posX - 1, posY);
            }
            if (posY != 7)
            {
                CreatePaths(posX, posY + 1);
            }
            if (posY != 0)
            {
                CreatePaths(posX, posY - 1);
            }
        }
        else
        {
            boardClone[posX, posY] += 1;
        }
    }

    private void FindPath(int posX, int posY)
    {
        // Runs throughout all possible path deadends and returns the size of the longest one.
        // if a deadend is found: send positon value to blacklist and timer to pathsTime, then reset timer.
        bool move = false;
        if (posX != 7)
        {
            if(Paths[posX + 1, posY] > Paths[posX, posY] && !blacklist.Contains(Paths[posX + 1, posY]))
            {
                timer++;
                FindPath(posX + 1, posY);
                move = true;
            }
        }
        if (posX != 0)
        {
            if (Paths[posX - 1, posY] > Paths[posX, posY] && !blacklist.Contains(Paths[posX - 1, posY]) && !move)
            {
                timer++;
                FindPath(posX - 1, posY);
                move = true;
            }
        }
        if (posY != 7)
        {
            if (Paths[posX, posY + 1] > Paths[posX, posY] && !blacklist.Contains(Paths[posX, posY + 1]) && !move)
            {
                timer++;
                FindPath(posX, posY + 1);
                move = true;
            }
        }
        if (posY != 0)
        {
            if (Paths[posX, posY- 1] > Paths[posX, posY] && !blacklist.Contains(Paths[posX, posY - 1]) && !move)
            {
                timer++;
                FindPath(posX, posY - 1);
                move = true;
            }
        }
        if(!move)
        {
            if(Paths[posX, posY] == 1)
            {
                return;
            }
            blacklist.Add(Paths[posX, posY]);
            pathsTime.Add(timer);
            timer = 0;
            FindPath(originX, originY);

        }
        }
    }
