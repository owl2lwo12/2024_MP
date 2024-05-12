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
        //���� �巡��Ʈ -> ���ο�� 110���� ��� �������� �� 11�� �����Ѵ�.
    }
}
