using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
