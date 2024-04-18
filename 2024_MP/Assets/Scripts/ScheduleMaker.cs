using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct match
{
    int home;
    int away;
    public match(int h, int a)
    {
        home = h;
        away = a;
    }
}
public struct day_schedule
{
    match[] matches;
    public day_schedule(match a,match b,match c, match d, match e)
    {
        matches = new match[5];
        matches[0] = a;
        matches[1] = b;
        matches[2] = c;
        matches[3] = d;
        matches[4] = e;
    }
}
/// <summary>
/// condition:
/// --------------------------------
/// first double match in seasons
/// 1 - 6
/// 2 - 7
/// 3 - 8
/// 4 - 9
/// 5 - 10
/// -------------------------------
/// 3 ~ 110 : 4 sets of triple match with one other teams. = triple match -> 각 팀당 4번(home 2, away 2)
/// 111 ~ 144 : 2 sets of double match with one other teams. = double match -> 각 팀당 2번(home 1, away 1)
/// </summary>
public class ScheduleMaker : MonoBehaviour
{
    private day_schedule[] schedule = new day_schedule[144];

    public void MakeSchedule()
    {
        match m1 = new match(1, 6);
        match m2 = new match(2, 7);
        match m3 = new match(3, 8);
        match m4 = new match(4, 9);
        match m5 = new match(5, 10);
        // 첫 두 경기는 등수대로 진행된다.
        schedule[0] = new day_schedule(m1, m2, m3, m4, m5);
        schedule[1] = new day_schedule(m1, m2, m3, m4, m5);

        int[,] home_match = new int[10,10];// int[i,j] 에서 
        int[,] away_match = new int[10,10];// i-1 : 각 팀의 등수, j : 타 팀과 남은 연전 수
        // count를 초기화 먼저 진행할 것이다. 
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if (i != j)
                {
                    home_match[i, j] = 2;
                    away_match[i, j] = 2;
                }
                else
                {
                    home_match[i, j] = 0;
                    away_match[i, j] = 0;
                }
            }
        }

        for(int i = 0; i < 54; i++)
        {
            int[,] set_of_schedule = new int[10,9];// i : 각 팀의 등수, j : 각 팀의 스케쥴
            int[] sequence_make_schedule = new int[10]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}; //스케쥴을 결정 할 순서를 정한다.

        }
    }
}