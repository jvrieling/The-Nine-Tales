using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelTracker
{
    private static int level;
    private static char segment;

    public static string currentScene;
    public static string lastScene;

    [RuntimeInitializeOnLoadMethod]
    public static void SubscribeToNewScenes()
    {
        Debug.Log("Level Tracker Initializing...");
        SceneManager.sceneLoaded += SetNewScene;
    }

    public static void SetNewScene(Scene newScene, LoadSceneMode mode)
    {
        if (currentScene == null) { currentScene = newScene.name; }
        else
        {
            lastScene = currentScene;
            currentScene = newScene.name;
        }
    }

    public static bool CurrentLevelSegmentEquals(int lvl, char seg)
    {
        return level == lvl && segment == seg;
    }

    public static string GetCurrentLevel()
    {
        if (level == 0)
        {
            return "x-x";
        }
        return level + "-" + segment;
    }

    public static void SetLevelSegment(int lvl, char seg)
    {
        SetLevel(lvl);
        SetSegment(seg);
    }

    public static void SetLevel(int lvl)
    {
        level = lvl;
    }
    public static void SetSegment(char seg)
    {
        segment = seg;
    }
}
