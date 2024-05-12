using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 *  메인 시스템 - 팀 - 선수 구조로 배치할 것
 *  시스템( team[] ( player[] ) ) 느낌으로 시스템이 team의 data를 다루고 team들이 player의 data를 다룰 수 있도록
 *  시스템에서 수행하여야할 list
 *  1. 매 시즌 각 팀의 경기 일정을 짠다.
 *  2. 매 시즌 이후 team에 신인 선수를 배정한다.
 *  3. 각 시즌별 등수기록 등을 영구히 가질 수 있도록 한다.
 *  4. 진행중인 시즌의 현황을 가질 수 있어야 한다.
 */


public class MainSystem : MonoBehaviour
{
    public GameObject[] teams;
    public GameObject sched;
    public int[] currmatch = new int[10];
    [SerializeField]
    private int match_num = 0;
    private day_schedule d;
    private void Start()
    {
        sched = GameObject.Find("Scheduler");
        new_season();
        d = sched.GetComponent<ScheduleMaker>().getSched(match_num);
        int j = 0;
        for(int i = 0; i< 5; i++)
        {
            currmatch[j] = d.getMatch(i).getHome();
            j++;
            currmatch[j] = d.getMatch(i).getAway();
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
            currmatch[j] = d.getMatch(i).getHome();
            j++;
            currmatch[j] = d.getMatch(i).getAway();
            j++;
        }
    }
    public void Draft()
    {
        //신인 드래프트 -> 새로운선수 110명을 등수 역순으로 각 11명씩 선정한다.
    }
}
