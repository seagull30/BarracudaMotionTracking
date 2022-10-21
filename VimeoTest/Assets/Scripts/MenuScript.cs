using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
  
    public void CameraButtonClick()
    {
        SceneManager.LoadScene("CameraScene");
    }
    public void PlayerButtonClick()
    {
        SceneManager.LoadScene("PlayerScene");
    }

    public void PlayFabTest()
    {
        PlayFabManager.Instance.InitializePostData();
    }
}
