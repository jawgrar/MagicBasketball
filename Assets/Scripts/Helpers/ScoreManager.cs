public static class ScoreManager
{
    private static int score;

    public static int GetScore()
    {
        return score;
    }

    public static void Reset()
    {
        score = 0;
    }

    public static void Update()
    {
        score++;
    }
}
