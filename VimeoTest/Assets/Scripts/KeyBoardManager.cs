using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardManager : MonoBehaviour
{
    public void OnKeyBoard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        Debug.Log("Ű���� ȣ��");
    }
}
