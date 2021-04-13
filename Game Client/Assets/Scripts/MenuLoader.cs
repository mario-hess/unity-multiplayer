using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
