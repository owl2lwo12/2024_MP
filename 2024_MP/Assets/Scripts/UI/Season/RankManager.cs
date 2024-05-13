using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public TMP_Text rank;
    public Image icon;
    public TMP_Text name;
    public TMP_Text numOfMatch; // 경기 수
    public TMP_Text numOfWin; // 승리 수
    public TMP_Text numOfLose; // 패배 수
    public TMP_Text numOfDraw; // 무승부 수
    public TMP_Text oddsOfWin; // 승률 (numOfWin / (numOfWin + numOfLose)) 
    private void Start()
    {
        Init();
    }

    void Init()
    {
    }
}
