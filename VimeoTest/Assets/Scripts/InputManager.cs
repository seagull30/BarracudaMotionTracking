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
    public TextMeshProUGUI UserName;

    void Start()
    {
        GetUserinfo();
    }

    //����� ������ ����
    public void SetStat()
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("������ ���� ����"), (error) => print("������ ���� ����"));
    }

    public void GetStat()
    {
        var request = new GetUserDataRequest() { PlayFabId = PlayFabManager.Instance.UserId };
        PlayFabClientAPI.GetUserData(request, (result) => {
            foreach (var eachData in result.Data)
                print(eachData.Key + " : " + eachData.Value.Value);
        }, (error) => print("������ �ҷ����� ����"));
    }

    //���� ���� ��������
    void GetUserinfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetUserInfoSuccess, OnGetUserInfoFailure);
    }

    void OnGetUserInfoSuccess(GetAccountInfoResult result)
    {
        UserName.text = $" User : {result.AccountInfo.Username}";
        print("���� ���� �ҷ����� ����");
    }

    void OnGetUserInfoFailure(PlayFabError error)
    {
        print("���� ���� �ҷ����� ����");
    }





}
