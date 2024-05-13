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
    public string descSeasonPlay = "Season Play ���� ����";
    public int curMatch; // ���� �÷����� ��� ��
    public int maxMatch = 144; // ���� ���� �÷����� ��� ��
    public UnityEvent onStartMatch;

    [Header("Season/Rank")] 
    public string descSeasonRank = "Season Rank ���� ����";
    public int myCurRank; // �÷��̾� ���� ����
    public List<Team_Scripts> rankList = new List<Team_Scripts>(10);// ���� ���� ����Ʈ
    
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
        if (curMatch < maxMatch) // ������ ������� �ʾ��� ��
        {
            onStartMatch.Invoke();
        }
        else
        {
            Debug.Log("�̹� ���� �����Դϴ�."); // ������ ������� ��
        }
    }
    
    // ------------------------- Player ��� ---------------------------
    
}
