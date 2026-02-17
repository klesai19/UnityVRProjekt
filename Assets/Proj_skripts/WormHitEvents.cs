using System;
using UnityEngine;
public static class WormHitEvents
{
    public static event Action<int> OnWormHit;
    public static void Raise(int points)
    {
        OnWormHit?.Invoke(points);
    }
}