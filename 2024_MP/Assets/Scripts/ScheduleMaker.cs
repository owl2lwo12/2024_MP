using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public int getHome()
    {
        return home;
    }
    public int getAway()
    {
        return away;
    }
}
public struct day_schedule
{
    match[] matches;
    public match getMatch(int idx, int p_idx)//idx: ���° ��� ����Ʈ�� ������ ��, p_idx: player�� ���� �����´�
    {
        find_players_team(p_idx);
        return matches[idx];
    }

    public void find_players_team(int idx) //�� ��(player �Ҽ��� ��)�� match�� ���� ���� �ø���. ���� idx�� ������ ��
    {
        match tmp;
        for(int i = 0; i < 5; i++)
        {
            if (matches[i].getHome() == idx || matches[i].getAway() == idx)
            {
                tmp = matches[i];
                matches[i] = matches[0];
                matches[0] = tmp;
                break;
            }
        }
    }
    public day_schedule(match a,match b,match c, match d, match e)
    {
        matches = new match[5];
        matches[0] = a;
        matches[1] = b;
        matches[2] = c;
        matches[3] = d;
        matches[4] = e;
    }
    public int find_match(int a) //��ġ�鿡�� ��� ã��
    {
        int result = 0;
        for(int i = 0; i < 5; i++)
        {
            if(matches[i].getHome() == a)
            {
                result = matches[i].getAway();
                break;
            }
            if (matches[i].getAway() == a)
            {
                result = matches[i].getHome();
                break;
            }
        }
        return result;
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
/// 3 ~ 110 : 4 sets of triple match with one other teams. = triple match -> �� ���� 4��(home 2, away 2)
/// 111 ~ 144 : 2 sets of double match with one other teams. = double match -> �� ���� 2��(home 1, away 1)
/// </summary>
public class ScheduleMaker : MonoBehaviour
{
    private day_schedule[] schedule = new day_schedule[144];

    public day_schedule getSched(int idx)
    {
        return schedule[idx];
    }
    public void MakeSchedule()
    {
        match m1 = new match(0, 5);
        match m2 = new match(1, 6);
        match m3 = new match(2, 7);
        match m4 = new match(3, 8);
        match m5 = new match(4, 9);
        // ù �� ���� ������ ����ȴ�.
        schedule[0] = new day_schedule(m1, m2, m3, m4, m5);
        schedule[1] = new day_schedule(m1, m2, m3, m4, m5);
        int idx_fys = 2;
        List<int> fys;
        for (int j = 0; j < 4; j++) //���⼭ 3������ ����� ��
        {
            fys = Shuffle_Fisher_Yates(10);
            for(int k =0; k < 9; k++)
            {
                m1 = new match(fys[0]-1, fys[9]-1);
                m2 = new match(fys[1]-1, fys[8]-1);
                m3 = new match(fys[2]-1, fys[7]-1);
                m4 = new match(fys[3]-1, fys[6]-1);
                m5 = new match(fys[4]-1, fys[5]-1);
                schedule[idx_fys] = new day_schedule(m1,m2,m3,m4,m5);
                idx_fys++;
                schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
                idx_fys++;
                schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
                idx_fys++;

                for (int i = 0; i < 10; i++)
                {
                    fys[i] = (fys[i] + 1) % 10;
                }
            }
        }

        fys = Shuffle_Fisher_Yates(10); //2������ 1ȸ �����ǰ�,
        for (int k = 0; k < 9; k++)
        {
            m1 = new match(fys[0], fys[9]);
            m2 = new match(fys[1], fys[8]);
            m3 = new match(fys[2], fys[7]);
            m4 = new match(fys[3], fys[6]);
            m5 = new match(fys[4], fys[5]);
            schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
            idx_fys++;
            schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
            idx_fys++;

            for (int i = 0; i < 10; i++)
            {
                fys[i] = (fys[i] + 1) % 10;
            }
        }

        fys = Shuffle_Fisher_Yates(5); //���� 2������ ���� ��ġ�� �����ϰ� �������ϹǷ�
        for(int i = 4; i >= 0; i--)
        {
            fys.Add(fys[i] + 5);
        }
        for(int i = 0; i< 10; i++) //�̹� �� ��ġ���̴ϱ� �� ƽ ������
        {
            fys[i] = (fys[i] + 1) % 10;
        }
        for (int k = 0; k < 8; k++)
        {
            m1 = new match(fys[0], fys[9]);
            m2 = new match(fys[1], fys[8]);
            m3 = new match(fys[2], fys[7]);
            m4 = new match(fys[3], fys[6]);
            m5 = new match(fys[4], fys[5]);
            schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
            idx_fys++;
            schedule[idx_fys] = new day_schedule(m1, m2, m3, m4, m5);
            idx_fys++;

            for (int i = 0; i < 10; i++)
            {
                fys[i] = (fys[i] + 1) % 10;
            }
        }
    }
    
    public List<int> Shuffle_Fisher_Yates(int num)
    {
        List<int> result = new List<int>();
        for(int i =1; i <= num; i++)
        {
            result.Add(i);
        }

        int n = result.Count;
        while(n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n);
            int val = result[k];
            result[k] = result[n];
            result[n] = val;
        }
        return result;
    }
}