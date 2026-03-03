using System;
public static class GameManagerEndedHook
{
    public static event Action<int> OnRoundEnded;
    public static void RaiseRoundEnded(int score)
    {
        OnRoundEnded?.Invoke(score);
    }
}