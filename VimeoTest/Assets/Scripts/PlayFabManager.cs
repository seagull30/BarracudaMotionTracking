using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


[SerializeField]
public class Post
{
    public int videoId;
    public string videoName;
    public int videoIndex;
    public List<int> videoCategoryList = new List<int>();
    public string videoPostDate;
    public string videoUserName;
    public int videoUserId;
}


public class PlayFabManager : SingletonBehaviour<PlayFabManager>
{
    public TMP_InputField EmailInput, PasswordInput, UsernameInput;
    public string UserId;
    public Dictionary<int, string> videoCategoryName = new Dictionary<int, string>();
    public Post postData = new Post();

    //���� �ε��� ��ȣ
    private int VideoIndex = 0;
    //�� ���� �ε��� ��ȣ
    private int VideoTotalIndex = 0;


    //�α���
    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    //ȸ������
    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }




    void OnLoginSuccess(LoginResult result)
    {
        print("�α��� ����");
        UserId = result.PlayFabId;
        //UpdateVideoId();
        SceneManager.LoadScene("MenuScene");
    }

    void OnLoginFailure(PlayFabError error)
    {
        print("�α��� ����");
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("ȸ������ ����");
    }

    void OnRegisterFailure(PlayFabError error)
    {
        print("ȸ������ ����");
    }

    //���� ���ε�� DB�� ����
    public void SetVideoId(int videoId)
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { videoId.ToString(), videoId.ToString() } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }


    //���� ������ �ʱ�ȭ
    public void InitializePostData()
    {
        int rowSize = 11;

        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "InitializePostData",
            FunctionParameter = new { 
                row = rowSize,
                index = 5
            }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnInitializePostDataSuccess, OnInitializePostDataError);

    }


    void OnInitializePostDataSuccess(ExecuteCloudScriptResult result)
    {
        Debug.Log(result.FunctionResult.ToString());
    }

    void OnInitializePostDataError(PlayFabError error)
    {
        Debug.Log("�ʱ�ȭ ����");
    }




    //���� ������ ���� 
    /*    public void UpdateVideoId()
        {
            //���� �ε��� �ʱ�ȭ
            int index = 0;
            VideoIndex = 0;
            videoList = new Dictionary<int, int>();

            var request = new GetUserDataRequest() { PlayFabId = UserId };
            PlayFabClientAPI.GetUserData(request, (result) => {
                foreach (var eachData in result.Data)
                {
                    print(eachData.Key + " : " + eachData.Value.Value);

                    //���� ����Ʈ ���
                    videoList.Add(index, int.Parse(eachData.Key));

                    index++;
                }

                VideoTotalIndex = videoList.Count - 1;

            }, (error) => print("������ �ҷ����� ����"));


        }




        //���� ������ ���̵�
        public int preVideoId()
        {
            //�� ó�� �������� �ƴҶ� ����
            if (VideoIndex > 0)
            {
                VideoIndex -= 1;

                return videoList[VideoIndex];
            }
            else // �� ó�� �������϶� -1 ��ȯ
            {
                return -1;
            }
        }

        //���� ������ ���̵�
        public int nextVideoId()
        {
            //������ �������� �ƴҶ� ����
            if (VideoIndex < VideoTotalIndex)
            {
                VideoIndex += 1;
                return videoList[VideoIndex];
            }
            else //�� ������ �������϶� -1 ��ȯ
            {
                return -1;
            }
        }

        //���� �ε��� ������ ���̵�
        public int currentVideoId()
        {
            return videoList[VideoIndex];
        }*/
}
