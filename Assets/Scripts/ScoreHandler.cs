using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler
{

    private static int score = 0;

    public static void setScore(int newScore)
    {
        score = newScore;
    }

    public static int getScore()
    {
        return score;
    }

    public static void resetScore()
    {
        score = 0;
    }
}
