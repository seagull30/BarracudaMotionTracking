using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabManager2 : MonoBehaviour
{
    public static PlayFabManager2 instance = null;

    private void Awake()
    {
        SignUpPage.SetActive(false);
        SystemPage.SetActive(false);
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    //----- 싱글톤 -----

    [Header("Sign In")]
    public InputField EmailInput;
    public InputField PasswordInput;

    [Header("Sign Up")]
    public InputField EmailSignUp;
    public InputField PasswordSignUp;
    public InputField PasswordCheckSignUp;
    public InputField NameSignUp;

    [Header("System")]
    public Text SystemText;

    [Header("Page")]
    public GameObject SignUpPage;
    public GameObject SystemPage;

    public static string ID;

    public static string GetID()
    {
        return ID;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)&& Input.GetKeyDown(KeyCode.S))
        {
            Otherogin();
        }
    }

    //임시
    private void Otherogin()
    {
        EmailInput.text = "zxc@zxc.zxc";
        PasswordInput.text = "zxczxczxc";
        LoginButton();
    }

    public void LoginButton()
    {
        // 로그인에 필요한 정보 입력
        var Email_request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        // 값에 따라 로그인 실패 성공 이동 iD값을 여기서 저장
        PlayFabClientAPI.LoginWithEmailAddress(Email_request, (result) => { ID = result.PlayFabId; SceneManager.LoadScene(2); }, OnLoginFailure);   
    }
    bool IsRegister;
    public void RegisterButton()
    {
        IsRegister = true;
        // 회원가입에 필요한 정보 입력
        var Email_request = new RegisterPlayFabUserRequest { Email = EmailSignUp.text, Password = PasswordSignUp.text, Username = NameSignUp.text ,DisplayName = NameSignUp.text};

        // 가입 정보 체크
        RegisterCheck();
        
        // 회원가입
        if (IsRegister)
        {
            PlayFabClientAPI.RegisterPlayFabUser (Email_request, OnRegisterSuccess, OnRegisterFailire);
        }
    }
    public void RegisterCheck()
    {
        // 이메일 확인
        // 어렵네 이거

        // 비밀번호 자리수 체크
        if (PasswordSignUp.text.Length <= 8)
        {
            Debug.LogWarning("비밀번호 8자리 이상");
            _SystemPage("비밀번호 8자리 이상");
            IsRegister = false;
            return;
        }

        // 비밀번호 확인
        if (PasswordSignUp.text != PasswordCheckSignUp.text)
        {
            Debug.LogWarning("비밀번호 확인");
            _SystemPage("비밀번호 확인");
            IsRegister = false;
            return;
        }
    }

    public void SignTrueButton()
    {
        //회원가입 페이지 올리기
        _SignUpPage(true);
    }

    public void SignFalseButton()
    {
        //회원가입 페이지 내리기
        _SignUpPage(false);
    }
    public void SystemButton()
    {
        SystemPage.SetActive(false);
    }

    private void _SignUpPage(bool Value) { SignUpPage.SetActive(Value); }
    private void _SystemPage(string Text) { SystemPage.SetActive(true); SystemText.text = Text; }

    private void OnLoginSuccess(LoginResult result) 
    {
        Debug.Log("로그인 성공");



        //_SystemPage("로그인 성공");
        SceneManager.LoadScene(2);
    }
    private void OnLoginFailure(PlayFabError error) 
    {
        Debug.LogWarning("로그인 실패");
        _SystemPage("로그인 실패");
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
        _SignUpPage(false);
        _SystemPage("회원가입 성공");
    }
    private void OnRegisterFailire(PlayFabError error) 
    {
        Debug.LogWarning("회원가입 실패");
        _SystemPage("회원가입 실패");
    }
}
