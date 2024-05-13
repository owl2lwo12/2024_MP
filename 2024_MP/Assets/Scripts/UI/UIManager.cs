using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    [Header("Status")] 
    public List<Sprite> TeamSprites = new List<Sprite>();
    
    [Header("Season/Play")] 
    public string descSeasonPlay = "Season Play 변수 모음";
    public int curMatch; // 현재 플레이한 경기 수
    public int maxMatch = 144; // 시즌 동안 플레이할 경기 수
    public UnityEvent onStartMatch;

    [Header("Season/Rank")] 
    public string descSeasonRank = "Season Rank 변수 모음";
    public int myCurRank; // 플레이어 시즌 순위
    public List<Team_Scripts> rankList = new List<Team_Scripts>(10);// 시즌 순위 리스트
    
    [Header("Player")] 
    public string descPlayer = "Player Tab 변수 모음";

    [Header("Player/Tool")] public string descPlayerTool = "Player Tool 변수 모음";
    
    [Header("Player/Skill")] public string descPlayerSkill = "Player Skill 변수 모음";
    
    [Header("Team 변수")] 
    public string descTeam = "Team Tab 변수 모음";
    public Team_Scripts myTeam;
    
    [Header("Season 변수")] 
    public string descTraining = "Training Tab 변수 모음";
    [Header("Setting 변수")] 
    public string descSetting = "Setting Tab 변수 모음";

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }

    // ------------------------- Season 기능 ---------------------------
    
    // 경기 시작 시 실행
    public void OnStartMatch()
    {
        if (curMatch < maxMatch) // 시즌이 종료되지 않았을 시
        {
            onStartMatch.Invoke();
        }
        else
        {
            Debug.Log("이미 시즌 종료입니다."); // 시즌이 종료됐을 때
        }
    }
    
    // ------------------------- Player 기능 ---------------------------
    
}
