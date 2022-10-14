using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class LoadSceneManager : MonoBehaviour
{


    public void PlaySceneLoad()
    {
        SceneManager.LoadScene("PlayerScene");
    }

    public void RecordSceneLoad()
    {
        SceneManager.LoadScene("CameraScene");
    }

}
