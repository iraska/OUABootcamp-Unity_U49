using UnityEngine;
using GameAnalyticsSDK;
using System.Collections.Generic;

public class GameAnalyticsManager : MonoBehaviour
{
    public static GameAnalyticsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        GameAnalytics.Initialize();
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            var errorData = new Dictionary<string, object>
            {
                { "Log", logString },
                { "StackTrace", stackTrace }
            };
            GameAnalytics.NewErrorEvent(GAErrorSeverity.Error, logString ,errorData);
        }
    }
    public void LevelStarted(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", level.ToString());
    }

    public void LevelCompleted(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level", level.ToString());
    }

    public void LevelFailed(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", level.ToString());
    }
}
