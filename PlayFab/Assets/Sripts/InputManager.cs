using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_InputField Input1, Input2, Input3;
    public Text UserName;

    void Start()
    {
        GetUserinfo();
    }

    //사용자 데이터 세팅
    public void SetStat()
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공" ), (error) => print("데이터 저장 실패"));
    }

    public void GetStat()
    {
        var request = new GetUserDataRequest() { PlayFabId = PlayFabManager.Instance.UserId };
        PlayFabClientAPI.GetUserData(request, (result) => {
            foreach (var eachData in result.Data)
                print(eachData.Key + " : " + eachData.Value.Value);
        }, (error) => print("데이터 불러오기 실패"));
    }

    //유저 정보 가져오기
    void GetUserinfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetUserInfoSuccess, OnGetUserInfoFailure);
    }

    void OnGetUserInfoSuccess(GetAccountInfoResult result)
    {
        UserName.text = $" User : {result.AccountInfo.Username}";
        print("유저 정보 불러오기 성공");
    }

    void OnGetUserInfoFailure(PlayFabError error)
    {
        print("유저 정보 불러오기 실패");
    }



    

}
