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

    public void PlayFabInsert()
    {
        Post post = new Post();
        post.vimeo_id = 123123;
        post.user_id = "asdasd123";
        post.user_name = "±è¿µÈÆ";
        post.video_name = "»õ·Î¿î µ¿¿µ»ó";
        post.category_id_list.Add(10);
        post.category_id_list.Add(11);

        PlayFabManager.Instance.InsertPostData(post);
    }
}
