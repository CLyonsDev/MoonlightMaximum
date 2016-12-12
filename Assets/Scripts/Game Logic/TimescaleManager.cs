using UnityEngine;
using System.Collections;

public static class TimescaleManager {

    public static void SetPaused(bool isPaused)
    {
        if (isPaused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }
}
