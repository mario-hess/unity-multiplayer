using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public static class Loader
{
    public enum Scene : int
    {
        Network,
        Menu,
        LoadingSpinner,
        CharacterCreation,
        CharacterSelection,
        Main,
    }

    public static void LoadLevel(Scene levelToLoad, Scene levelToUnload)
    {
        SceneManager.LoadScene(levelToLoad.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(levelToUnload.ToString());
    }
}
