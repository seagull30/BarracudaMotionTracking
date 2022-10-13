using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordBtn : MonoBehaviour
{
    private Button[] Btns;
    private int BtnCount;

    enum BtnNum
    {
        Start,
        Pause,
        Resume,
        Cancel,
        Upload,
        Menu,

    }

    private void Awake()
    {
        BtnCount = transform.childCount;
        Btns = new Button[BtnCount];

        for (int i = 0; i < BtnCount; ++i)
        {
            Btns[i] = transform.GetChild(i).GetComponent<Button>();
            Btns[i].gameObject.SetActive(false);
        }
        Btns[(int)BtnNum.Start].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(true);
    }
   
    public void StartRecording()
    {
        Btns[(int)BtnNum.Pause].gameObject.SetActive(true);
        Btns[(int)BtnNum.Upload].gameObject.SetActive(true);
        Btns[(int)BtnNum.Cancel].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(false);
    }

    public void PauseRecording()
    {
        Btns[(int)BtnNum.Pause].gameObject.SetActive(false);
        Btns[(int)BtnNum.Resume].gameObject.SetActive(true);

    }

    public void ResumeRecording()
    {
        Btns[(int)BtnNum.Resume].gameObject.SetActive(false);
        Btns[(int)BtnNum.Pause].gameObject.SetActive(true);

    }

    public void CancelRecording()
    {
        Btns[(int)BtnNum.Resume].gameObject.SetActive(false);
        Btns[(int)BtnNum.Pause].gameObject.SetActive(false);
        Btns[(int)BtnNum.Start].gameObject.SetActive(true);
        Btns[(int)BtnNum.Menu].gameObject.SetActive(true);
    }

    public void Upload()
    {

    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
