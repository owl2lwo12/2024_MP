using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 *  ���� �ý��� - �� - ���� ������ ��ġ�� ��
 *  �ý���( team[] ( player[] ) ) �������� �ý����� team�� data�� �ٷ�� team���� player�� data�� �ٷ� �� �ֵ���
 *  �ý��ۿ��� �����Ͽ����� list
 *  1. �� ���� �� ���� ��� ������ §��.
 *  2. �� ���� ���� team�� ���� ������ �����Ѵ�.
 *  3. �� ���� ������ ���� ������ ���� �� �ֵ��� �Ѵ�.
 *  4. �������� ������ ��Ȳ�� ���� �� �־�� �Ѵ�.
 */


public class MainSystem : MonoBehaviour
{
    public static MainSystem _instance = null;
    
    public GameObject[] teams;
    public GameObject sched;
    public int[] currmatch = new int[10];
    [SerializeField]
    private int match_num = 0;
    private day_schedule d;
    private int playersTeam = 0;

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

    private void Start()
    {
        sched = GameObject.Find("Scheduler");
        new_season();
        d = sched.GetComponent<ScheduleMaker>().getSched(match_num);
        int j = 0;
        for(int i = 0; i< 5; i++)
        {
            currmatch[j] = d.getMatch(i,playersTeam).getHome();
            j++;
            currmatch[j] = d.getMatch(i,playersTeam).getAway();
            j++;
        }
    }
    public void new_season()
    {
        match_num = 0;
        sched.GetComponent<ScheduleMaker>().MakeSchedule();
    }
    
    private void Update()
    {
        d = sched.GetComponent<ScheduleMaker>().getSched(match_num);
        int j = 0;
        for (int i = 0; i < 5; i++)
        {
            currmatch[j] = d.getMatch(i, playersTeam).getHome();
            j++;
            currmatch[j] = d.getMatch(i, playersTeam).getAway();
            j++;
        }
    }

    public void IncreaseMatchNum()
    {
        match_num++;
    }
    
    public void Make_Match_Result()
    {
        if (match_num % 20 == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                teams[i].GetComponent<Team_Scripts>().Sorting_Players();
                teams[i].GetComponent<Team_Scripts>().LineUp();
            }
        } //20��⿡ �ѹ� ������ ������ �����ص� ���� ���� ��?
        else
        {
            for (int i = 0; i < 9; i++)
            {
                teams[i].GetComponent<Team_Scripts>().Sorting_Player_Batters();
                teams[i].GetComponent<Team_Scripts>().LineUp();
            }
        }
        d = sched.GetComponent<ScheduleMaker>().getSched(match_num);
        for(int i = 0; i < 5; i++) //�� ��⸦ ����(�� 5���)
        {
            int home = d.getMatch(i, playersTeam).getHome();
            int away = d.getMatch(i, playersTeam).getAway();

            Make_Each_Match_Result(home,away);
        }
    }
    public void Make_Each_Match_Result(int i,int j) //index 2��(�� ���� index)�� �޾ƿͼ� �� ���� ���ξ��� ���Ŀ� 
    {
        
    }

    public void Draft()
    {
        //���� �巡��Ʈ -> ���ο�� 110���� ��� �������� �� 11�� �����Ѵ�.
    }
}
