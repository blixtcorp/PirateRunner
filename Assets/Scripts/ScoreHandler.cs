using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler
{

    private static int score = 0;

    public static void increaseScore(int increment)
    {
        score += increment;
    }

    public static int getScore()
    {
        return score;
    }
}
