using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


[SerializeField]
public class Post
{
    public int vimeo_id;
    public int user_id;
    public string user_name;
    public int index;
    public string video_name;
    public string video_upload_date;
    public List<int> category_id_list = new List<int>();
}


public class PlayFabManager : SingletonBehaviour<PlayFabManager>
{
    public TMP_InputField EmailInput, PasswordInput, UsernameInput;
    public string UserId;
    public Dictionary<int, string> videoCategoryName = new Dictionary<int, string>();
    public List<Post> postData = new List<Post>();

    //비디오 인덱스 번호
    private int VideoIndex = 0;
    //총 비디오 인덱스 번호
    private int VideoTotalIndex = 0;


    //로그인
    public void LoginBtn()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    //회원가입
    public void RegisterBtn()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }




    void OnLoginSuccess(LoginResult result)
    {
        print("로그인 성공");
        UserId = result.PlayFabId;
        //UpdateVideoId();
        SceneManager.LoadScene("MenuScene");
    }

    void OnLoginFailure(PlayFabError error)
    {
        print("로그인 실패");
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
    }

    void OnRegisterFailure(PlayFabError error)
    {
        print("회원가입 실패");
    }

    //비디오 업로드시 DB에 저장
    public void SetVideoId(int videoId)
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { videoId.ToString(), videoId.ToString() } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }


    //비디오 데이터 초기화
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

        /*       List<string> test = new List<string>();
               test = result.FunctionResult;

               JsonObject jsonResult = (JsonObject)result.FunctionResult;

               string json = JsonUtility.ToJson(jsonResult);
               //postData = JsonUtility.FromJson<List<Post>>(result.FunctionResult.ToString());

               Debug.Log(json);*/

        //JsonObject jsonResult = (JsonObject)result.FunctionResult.ToString();

        //JsonObject jsonResult = (JsonObject)result.FunctionResult;
        //string json = JsonUtility.ToJson(result.FunctionResult.ToString());
        //jsonResult["testData"]

        //JsonArray jsonArray = new JsonArray();
        //jsonArray = (JsonArray)jsonResult["testData"];

        JsonArray jsonArray = new JsonArray();
        jsonArray = (JsonArray)result.FunctionResult;

        JsonObject jsonObject = (JsonObject)jsonArray[0];

        Debug.Log(jsonObject);

        //saveBackGround = JsonUtility.FromJson<SaveDataBackGround>(url.ToString());

        /*        int list_cnt = jsonArray.Count;
                for(int i = 0; i < list_cnt; i++)
                {
                    JsonObject jsonObject = jsonArray.get
                }*/

        //Debug.Log(result.FunctionResult);




    }

    void OnInitializePostDataError(PlayFabError error)
    {
        Debug.Log("초기화 실패");
    }




    //비디오 데이터 갱신 
    /*    public void UpdateVideoId()
        {
            //비디오 인덱스 초기화
            int index = 0;
            VideoIndex = 0;
            videoList = new Dictionary<int, int>();

            var request = new GetUserDataRequest() { PlayFabId = UserId };
            PlayFabClientAPI.GetUserData(request, (result) => {
                foreach (var eachData in result.Data)
                {
                    print(eachData.Key + " : " + eachData.Value.Value);

                    //비디오 리스트 담기
                    videoList.Add(index, int.Parse(eachData.Key));

                    index++;
                }

                VideoTotalIndex = videoList.Count - 1;

            }, (error) => print("데이터 불러오기 실패"));


        }




        //이전 동영상 아이디
        public int preVideoId()
        {
            //맨 처음 페이지가 아닐때 실행
            if (VideoIndex > 0)
            {
                VideoIndex -= 1;

                return videoList[VideoIndex];
            }
            else // 맨 처음 동영상일때 -1 반환
            {
                return -1;
            }
        }

        //다음 동영상 아이디
        public int nextVideoId()
        {
            //마지막 페이지가 아닐때 실행
            if (VideoIndex < VideoTotalIndex)
            {
                VideoIndex += 1;
                return videoList[VideoIndex];
            }
            else //맨 마지막 동영상일때 -1 반환
            {
                return -1;
            }
        }

        //현재 인덱스 동영상 아이디
        public int currentVideoId()
        {
            return videoList[VideoIndex];
        }*/
}
