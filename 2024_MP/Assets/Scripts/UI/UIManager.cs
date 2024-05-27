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
    public List<string> TeamNames = new List<string>();
    
    [Header("Season/Play")] 
    public string descSeasonPlay = "Season Play ���� ����";
    public int curMatch; // ���� �÷����� ��� ��
    public int maxMatch = 144; // ���� ���� �÷����� ��� ��
    public UnityEvent onStartMatch;

    [Header("Season/Rank")] 
    public string descSeasonRank = "Season Rank ���� ����";
    public int myCurRank; // �÷��̾� ���� ����
    public List<Team_Scripts> rankList = new List<Team_Scripts>(10);// ���� ���� ����Ʈ
    public GameObject noticePanel;
    
    [Header("Player")] 
    public string descPlayer = "Player Tab ���� ����";

    [Header("Player/Tool")] public string descPlayerTool = "Player Tool ���� ����";
    
    [Header("Player/Skill")] public string descPlayerSkill = "Player Skill ���� ����";
    
    [Header("Team ����")] 
    public string descTeam = "Team Tab ���� ����";
    public Team_Scripts myTeam;
    
    [Header("Season ����")] 
    public string descTraining = "Training Tab ���� ����";
    [Header("Setting ����")] 
    public string descSetting = "Setting Tab ���� ����";

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

    // ------------------------- Season ��� ---------------------------
    
    // ��� ���� �� ����
    public void OnStartMatch()
    {
        if (curMatch < maxMatch + 1) // ������ ������� �ʾ��� ��
        {
            onStartMatch.Invoke();
        }
        else
        {
            Debug.Log("�̹� ���� �����Դϴ�."); // ������ ������� ��
        }
    }
    
    // ------------------------- Player ��� ---------------------------
    
    
    // ----��Ÿ----
    public void CheckProgressMatchOrNot()
    {
        if (curMatch == 1)
        {
            noticePanel.SetActive(true);
        }
        else
        {
            noticePanel.SetActive(false);
        }
    }
}
