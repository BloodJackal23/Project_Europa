﻿using System.Collections;
using UnityEngine;

public static class SceneSystem
{
    public enum GameScene 
    { 
        MainMenu, 
        GameScene
    }

    public delegate void OnLoadStart();
    public static OnLoadStart onLoadStart;

    public delegate void WhileLoading(float _progress);
    public static WhileLoading whileLoading;

    public delegate void OnLoadEnd();
    public static OnLoadEnd onLoadEnd;

    public static IEnumerator LoadNextScene(AsyncOperation _operation)
    {
        onLoadStart?.Invoke();
        float progress = 0;
        while (!_operation.isDone)
        {
            progress = Mathf.Clamp01(_operation.progress / .9f);
            whileLoading?.Invoke(progress);
            yield return null;
        }
        onLoadEnd?.Invoke();
    }
}
