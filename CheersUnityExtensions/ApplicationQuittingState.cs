using UnityEngine;

public static class ApplicationQuittingState
{
    [OnEnterPlay_Set(false)]
    static bool isQuitting;
    public static bool IsQuitting => isQuitting;

    [UniversalInitializeOnLoadMethod]
    static void Init()
    {
        Application.quitting += OnQuit;
    }

    static void OnQuit()
    {
        isQuitting = true;
    }
}