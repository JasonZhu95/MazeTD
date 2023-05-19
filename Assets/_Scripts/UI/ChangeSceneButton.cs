using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        LevelManager.Instance.LoadScene(sceneName);
    }
}
