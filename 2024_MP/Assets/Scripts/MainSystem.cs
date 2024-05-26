using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor;


/*
 *  메인 시스템 - 팀 - 선수 구조로 배치할 것
 *  시스템( team[] ( player[] ) ) 느낌으로 시스템이 team의 data를 다루고 team들이 player의 data를 다룰 수 있도록
 *  시스템에서 수행하여야할 list
 *  1. 매 시즌 각 팀의 경기 일정을 짠다.
 *  2. 매 시즌 이후 team에 신인 선수를 배정한다.
 *  3. 각 시즌별 등수기록 등을 영구히 가질 수 있도록 한다.
 *  4. 진행중인 시즌의 현황을 가질 수 있어야 한다.
 */

public class Compare_Rank: IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        return b.GetComponent<Team_Scripts>().WinRate.CompareTo(a.GetComponent<Team_Scripts>().WinRate);
    }
}

public class MainSystem : MonoBehaviour
{
    public static MainSystem _instance = null;
    
    public GameObject[] teams;
    private List<GameObject> team_Rank;
    public GameObject sched;
    public int[] currmatch = new int[10];
    [SerializeField]
    private int match_num = 0;
    private day_schedule d;
    [SerializeField]
    private int playersTeam = 0;
    [SerializeField]
    private int stats_Change = 12;

    private int players_Match_Index = 1;
    private int score_result_home = 0;
    private int score_result_away = 0;
    private String matchResult = "??";

    public int Players_Match_Index { get => players_Match_Index; set => players_Match_Index = value; }
    public int Score_result_home { get => score_result_home; set => score_result_home = value; }
    public int Score_result_away { get => score_result_away; set => score_result_away = value; }
    public string MatchResult { get => matchResult; set => matchResult = value; }
    public int Match_num { get => match_num; set => match_num = value; }

    public void MakeRank()
    {
        team_Rank.Sort(new Compare_Rank());
        for(int i = 0; i< 10; i++)
        {
            team_Rank[i].GetComponent<Team_Scripts>().Rank = i + 1;
        }
    }
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
        /*foreach(GameObject t in teams)
        {
            team_Rank.Add(t);
        }*/
        sched = GameObject.Find("Scheduler");
        new_season();
        d = sched.GetComponent<ScheduleMaker>().getSched(Match_num);
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
        Match_num = 0;
        sched.GetComponent<ScheduleMaker>().MakeSchedule();
    }
    
    private void Update()
    {
        d = sched.GetComponent<ScheduleMaker>().getSched(Match_num);
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
        Match_num++;
    }
    public void SetResults()
    {
        d = sched.GetComponent<ScheduleMaker>().getSched(Match_num - 1);
        int home = d.getMatch(players_Match_Index, playersTeam).getHome();
        int away = d.getMatch(players_Match_Index, playersTeam).getAway();

        if (home == 0)
        {
            if (score_result_home > score_result_away) matchResult = "Win";
            else if (score_result_away == score_result_home) matchResult = "Draw";
            else matchResult = "Lose";
        }
        else
        {
            if (score_result_home < score_result_away) matchResult = "Win";
            else if (score_result_away == score_result_home) matchResult = "Draw";
            else matchResult = "Lose";
        }
    }
    public void Make_Match_Result()
    {
        if (Match_num % 20 == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                teams[i].GetComponent<Team_Scripts>().Sorting_Players();
                teams[i].GetComponent<Team_Scripts>().LineUp();
            }
        } //20경기에 한번 정도는 투수진 정비해도 되지 않을 까?
        else
        {
            for (int i = 0; i < 9; i++)
            {
                teams[i].GetComponent<Team_Scripts>().Sorting_Player_Batters();
                teams[i].GetComponent<Team_Scripts>().LineUp();
            }
        }
        d = sched.GetComponent<ScheduleMaker>().getSched(Match_num);
        for(int i = 0; i < 5; i++) //각 경기를 수행(총 5경기)
        {
            int home = d.getMatch(i, playersTeam).getHome();
            int away = d.getMatch(i, playersTeam).getAway();

            Make_Each_Match_Result(home,away);
            if(home == 0 || away == 0)
            {
                players_Match_Index = i;
            }
        }
        foreach(GameObject t in teams)
        {
            foreach(GameObject p in t.GetComponent<Team_Scripts>().Pitcherplayerlist)
            {
                p.GetComponent<Pitcher_Stats>().One_Match(); //경기가 끝났으니까 
            }
        }
        Match_num++;
        if(Match_num%stats_Change == 0)
        {
            StatChange();
        }
        foreach(GameObject t in teams)
        {
            t.GetComponent<Team_Scripts>().SetWinRate();
        }
        SetResults();
    }
    public void Make_Each_Match_Result(int i,int j) //index 2개(각 팀의 index)를 받아와서 두 팀의 라인업을 비교후에 
    {
        int teamScoreAway = 0;
        int teamScoreHome = 0;

        bool turn = true; // true는 회 초 false는 회 말
        bool f_base = false;
        bool s_base = false;
        bool t_base = false; // 1,2,3루 차있으면 true.
        int inning = 1;
        int outCount = 0;

        int home_turned_batter = 0;
        int away_turned_batter = 0;

        GameObject home_Pitcher = teams[i].GetComponent<Team_Scripts>().get_start_pitcher(Match_num%5);
        GameObject away_Pitcher = teams[j].GetComponent<Team_Scripts>().get_start_pitcher(Match_num%5);

        List<GameObject> home_Batters = teams[i].GetComponent<Team_Scripts>().get_batters();
        List<GameObject> away_Batters = teams[j].GetComponent<Team_Scripts>().get_batters();

        while (true)
        {
            int c = 130;
            int h = 10;
            int bb = 40;

            if (turn == true)
            {
                h += (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getpower() - home_Pitcher.GetComponent<Pitcher_Stats>().getspeed()) / 2;
                c += (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getcontact() - home_Pitcher.GetComponent<Pitcher_Stats>().getcontrol());
                bb += (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().geteye() - home_Pitcher.GetComponent<Pitcher_Stats>().getcontrol()) / 4;

                int rslt = UnityEngine.Random.Range(1, 501);
                if (rslt < h) //홈런일 때
                {
                    int scored = 1;
                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().H_run++;
                    if (f_base == true) scored++;
                    if (s_base == true) scored++;
                    if (t_base == true) scored++;
                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += scored;
                    home_Pitcher.GetComponent<Pitcher_Stats>().Era += scored;
                    teamScoreAway += scored;
                }
                else if (rslt < c) // 안타일 때
                {
                    int hardhit = UnityEngine.Random.Range(0, 100); //장타 확률
                    int speedster = UnityEngine.Random.Range(0, 100); //발이 빨라서 추가 진루 확률

                    if (hardhit < away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getpower() / 2) //장타
                    {
                        if (speedster < away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getspeed() / 4) //장타인테 타구 위치가 좋음 + 발 빨라서 3루타까지도
                        {
                            if (f_base == true)
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //만루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                        teamScoreAway += 3;
                                    }
                                    else//1,2루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreAway += 2;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//1,3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreAway += 2;
                                    }
                                    else//1루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreAway += 2;
                                    }
                                    else//2루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                    }
                                    else//비어있음
                                    {
                                        // 아무것도 없다.
                                    }
                                }
                            }
                            t_base = true;
                            s_base = false;
                            f_base = false;
                        }
                        else if (speedster < away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getspeed() * 2) //장타 타구 위치가 애매 + 타구처리가 적절해서 2루타
                        {
                            if (f_base == true)
                            {
                                if (outCount == 2) //2아웃일때
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                            teamScoreAway += 3;
                                        }
                                        else//1,2루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;

                                        }
                                        else//1루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                        }
                                    }
                                    f_base = false;
                                    s_base = true;
                                    t_base = false;
                                }
                                else
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;
                                            t_base = false;
                                        }
                                        else//1,2루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                            t_base = true;
                                            s_base = false;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                        }
                                        else//1루
                                        {
                                            t_base = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreAway += 2;
                                        t_base = false;
                                    }
                                    else//2루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                        t_base = false;
                                        s_base = true;
                                    }
                                    else//비어있음
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        t_base = true;
                                    }
                                }
                            }
                        }
                        else //장타긴 한데 위치도 애매하고 타자가 느림 1루타
                        {
                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Hit++;
                            if (f_base == true)
                            {
                                if (outCount == 2)
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                            teamScoreAway += 3;
                                        }
                                        else//1,2루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;
                                        }
                                        else//1루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                        }
                                    }
                                    f_base = true;
                                    s_base = false;
                                    t_base = false;
                                }
                                else
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreAway += 2;
                                            s_base = false;
                                        }
                                        else//1,2루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                            s_base = false;
                                            t_base = true;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreAway++;
                                        }
                                        else//1루
                                        {
                                            t_base = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreAway += 2;
                                        s_base = false;
                                        t_base = false;
                                        f_base = true;
                                    }
                                    else//2루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                        f_base = true;
                                        s_base = false;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreAway++;
                                        t_base = false;
                                        f_base = true;
                                    }
                                    else//비어있음
                                    {
                                        f_base = true;
                                    }
                                }
                            }
                        }
                    }
                    else //단타
                    {
                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Hit++;
                        if (f_base == true)
                        {
                            if (s_base == true)
                            {
                                if (t_base == true) //만루
                                {
                                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreAway++;
                                }
                                else //1,2루
                                {
                                    t_base = true; //만루로
                                }
                            }
                            else
                            {
                                if (t_base == true) //1,3루
                                {
                                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreAway++;
                                    t_base = false;
                                    s_base = true; // 1,2루로
                                }
                                else //1루
                                {
                                    s_base = true;//1,2루로
                                }
                            }
                        }
                        else
                        {
                            if (s_base == true)
                            {
                                if (t_base == true) //2,3루
                                {
                                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreAway++;
                                    s_base = false;
                                    f_base = true; //1,3루로
                                }
                                else//2루
                                {
                                    s_base = false;
                                    t_base = true;
                                    f_base = true; //1,3루로
                                }
                            }
                            else
                            {
                                if (t_base == true) //3루
                                {
                                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreAway++;
                                    t_base = false;
                                    f_base = true; //1루로
                                }
                                else //비었다
                                {
                                    f_base = true;
                                }
                            }
                        }
                    }
                }
                else if (rslt < c + bb) //볼넷일 때
                {
                    away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Ball4++;
                    if (f_base == true)
                    {
                        if (s_base == true)
                        {
                            if (t_base == true) //만루
                            {
                                away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                teamScoreAway++;
                            }
                            else //1,2루
                            {
                                t_base = true;
                            }
                        }
                        else
                        {
                            if (t_base == true) //1,3루
                            {
                                s_base = true;
                            }
                            else //1루
                            {
                                s_base = true;
                            }
                        }
                    }
                    else
                    {
                        f_base = true;
                    }
                }
                else //아웃
                {
                    outCount++;
                    home_Pitcher.GetComponent<Pitcher_Stats>().Inning++;
                    int out_result = UnityEngine.Random.Range(0, 101);
                    if (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getintelligence() < out_result)//지능이 낮을 수록 삼진률 상승
                    {
                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().S_out++;
                    }
                    out_result = UnityEngine.Random.Range(0, 101);
                    if (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getpower() > out_result) //힘이 좋을수로 뜬공 비율 높음
                    {
                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().F_out++;
                        if (t_base == true)
                        {
                            out_result = UnityEngine.Random.Range(0, 101);
                            if (away_Batters[away_turned_batter].GetComponent<Batter_Stats>().getpower() > out_result)
                            {
                                t_base = false;
                                away_Batters[away_turned_batter].GetComponent<Batter_Stats>().Score++;
                                home_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                teamScoreAway++;
                            }
                        }
                    }
                    else
                    {
                        away_Batters[away_turned_batter].GetComponent<Batter_Stats>().G_out++;
                    }
                }
                away_turned_batter = (away_turned_batter + 1) % 9;
            }
            else //회 말일 경우
            {
                h += (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getpower() - away_Pitcher.GetComponent<Pitcher_Stats>().getspeed()) / 2;
                c += (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getcontact() - away_Pitcher.GetComponent<Pitcher_Stats>().getcontrol());
                bb += (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().geteye() - away_Pitcher.GetComponent<Pitcher_Stats>().getcontrol()) / 4;

                int rslt = UnityEngine.Random.Range(1, 501);
                if (rslt < h) //홈런일 때
                {
                    int scored = 1;
                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().H_run++;
                    if (f_base == true) scored++;
                    if (s_base == true) scored++;
                    if (t_base == true) scored++;
                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += scored;
                    away_Pitcher.GetComponent<Pitcher_Stats>().Era += scored;
                    teamScoreHome += scored;
                }
                else if (rslt < c) // 안타일 때
                {
                    int hardhit = UnityEngine.Random.Range(0, 100); //장타 확률
                    int speedster = UnityEngine.Random.Range(0, 100); //발이 빨라서 추가 진루 확률

                    if (hardhit < home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getpower() / 2) //장타
                    {
                        if (speedster < home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getspeed() / 4) //장타인테 타구 위치가 좋음 + 발 빨라서 3루타까지도
                        {
                            if (f_base == true)
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //만루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                        teamScoreHome += 3;
                                    }
                                    else//1,2루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreHome += 2;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//1,3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreHome += 2;
                                    }
                                    else//1루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreHome += 2;
                                    }
                                    else//2루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().T_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                    }
                                    else//비어있음
                                    {
                                        // 아무것도 없다.
                                    }
                                }
                            }
                            t_base = true;
                            s_base = false;
                            f_base = false;
                        }
                        else if (speedster < home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getspeed() * 2) //장타 타구 위치가 애매 + 타구처리가 적절해서 2루타
                        {
                            if (f_base == true)
                            {
                                if (outCount == 2) //2아웃일때
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                            teamScoreHome += 3;
                                        }
                                        else//1,2루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;

                                        }
                                        else//1루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                        }
                                    }
                                    f_base = false;
                                    s_base = true;
                                    t_base = false;
                                }
                                else
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;
                                            t_base = false;
                                        }
                                        else//1,2루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                            t_base = true;
                                            s_base = false;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                        }
                                        else//1루
                                        {
                                            t_base = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreHome += 2;
                                        t_base = false;
                                    }
                                    else//2루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                        t_base = false;
                                        s_base = true;
                                    }
                                    else//비어있음
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().D_hit++;
                                        t_base = true;
                                    }
                                }
                            }
                        }
                        else //장타긴 한데 위치도 애매하고 타자가 느림 1루타
                        {
                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Hit++;
                            if (f_base == true)
                            {
                                if (outCount == 2)
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 3;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 3;
                                            teamScoreHome += 3;
                                        }
                                        else//1,2루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;
                                        }
                                        else//1루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                        }
                                    }
                                    f_base = true;
                                    s_base = false;
                                    t_base = false;
                                }
                                else
                                {
                                    if (s_base == true)
                                    {
                                        if (t_base == true) //만루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                            teamScoreHome += 2;
                                            s_base = false;
                                        }
                                        else//1,2루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                            s_base = false;
                                            t_base = true;
                                        }
                                    }
                                    else
                                    {
                                        if (t_base == true)//1,3루
                                        {
                                            home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                            away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                            teamScoreHome++;
                                        }
                                        else//1루
                                        {
                                            t_base = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (s_base == true)
                                {
                                    if (t_base == true) //2,3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score += 2;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era += 2;
                                        teamScoreHome += 2;
                                        s_base = false;
                                        t_base = false;
                                        f_base = true;
                                    }
                                    else//2루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                        f_base = true;
                                        s_base = false;
                                    }
                                }
                                else
                                {
                                    if (t_base == true)//3루
                                    {
                                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                        away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                        teamScoreHome++;
                                        t_base = false;
                                        f_base = true;
                                    }
                                    else//비어있음
                                    {
                                        f_base = true;
                                    }
                                }
                            }
                        }
                    }
                    else //단타
                    {
                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Hit++;
                        if (f_base == true)
                        {
                            if (s_base == true)
                            {
                                if (t_base == true) //만루
                                {
                                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreHome++;
                                }
                                else //1,2루
                                {
                                    t_base = true; //만루로
                                }
                            }
                            else
                            {
                                if (t_base == true) //1,3루
                                {
                                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreHome++;
                                    t_base = false;
                                    s_base = true; // 1,2루로
                                }
                                else //1루
                                {
                                    s_base = true;//1,2루로
                                }
                            }
                        }
                        else
                        {
                            if (s_base == true)
                            {
                                if (t_base == true) //2,3루
                                {
                                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreHome++;
                                    s_base = false;
                                    f_base = true; //1,3루로
                                }
                                else//2루
                                {
                                    s_base = false;
                                    t_base = true;
                                    f_base = true; //1,3루로
                                }
                            }
                            else
                            {
                                if (t_base == true) //3루
                                {
                                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                    away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                    teamScoreHome++;
                                    t_base = false;
                                    f_base = true; //1루로
                                }
                                else //비었다
                                {
                                    f_base = true;
                                }
                            }
                        }
                    }
                }
                else if (rslt < c + bb) //볼넷일 때
                {
                    home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Ball4++;
                    if (f_base == true)
                    {
                        if (s_base == true)
                        {
                            if (t_base == true) //만루
                            {
                                home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                teamScoreHome++;
                            }
                            else //1,2루
                            {
                                t_base = true;
                            }
                        }
                        else
                        {
                            if (t_base == true) //1,3루
                            {
                                s_base = true;
                            }
                            else //1루
                            {
                                s_base = true;
                            }
                        }
                    }
                    else
                    {
                        f_base = true;
                    }
                }
                else //아웃
                {
                    outCount++;
                    away_Pitcher.GetComponent<Pitcher_Stats>().Inning++;
                    int out_result = UnityEngine.Random.Range(0, 101);
                    if (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getintelligence() < out_result)//지능이 낮을 수록 삼진률 상승
                    {
                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().S_out++;
                    }
                    out_result = UnityEngine.Random.Range(0, 101);
                    if (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getpower() > out_result) //힘이 좋을수로 뜬공 비율 높음
                    {
                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().F_out++;
                        if (t_base == true)
                        {
                            out_result = UnityEngine.Random.Range(0, 101);
                            if (home_Batters[home_turned_batter].GetComponent<Batter_Stats>().getpower() > out_result)
                            {
                                t_base = false;
                                home_Batters[home_turned_batter].GetComponent<Batter_Stats>().Score++;
                                away_Pitcher.GetComponent<Pitcher_Stats>().Era++;
                                teamScoreHome++;
                            }
                        }
                    }
                    else
                    {
                        home_Batters[home_turned_batter].GetComponent<Batter_Stats>().G_out++;
                    }
                }
                home_turned_batter = (home_turned_batter + 1) % 9;
            }
            
            if(outCount == 3)
            {
                outCount = 0;
                if (turn == true)
                {
                    f_base = false;
                    t_base = false;
                    s_base = false;
                    turn = false;
                }
                else
                {
                    f_base = false;
                    t_base = false;
                    s_base = false;
                    turn = true;
                    if (inning < 9) inning++;
                    else break;
                }
            }

            if(home_Pitcher.GetComponent<Pitcher_Stats>().CStamina < 10 || 
                (home_Pitcher.GetComponent<Pitcher_Stats>().getstamina()>=80 && home_Pitcher.GetComponent<Pitcher_Stats>().TodayEra > 4)||
                (home_Pitcher.GetComponent<Pitcher_Stats>().getstamina()< 80 && home_Pitcher.GetComponent<Pitcher_Stats>().TodayEra > 2))
            {
                teams[i].GetComponent<Team_Scripts>().Change_Pitcher(teamScoreHome - teamScoreAway);
            }
            if (away_Pitcher.GetComponent<Pitcher_Stats>().CStamina < 10 ||
                (away_Pitcher.GetComponent<Pitcher_Stats>().getstamina() >= 80 && away_Pitcher.GetComponent<Pitcher_Stats>().TodayEra > 4) ||
                (away_Pitcher.GetComponent<Pitcher_Stats>().getstamina() < 80 && away_Pitcher.GetComponent<Pitcher_Stats>().TodayEra > 2))
            {
                teams[j].GetComponent<Team_Scripts>().Change_Pitcher(teamScoreAway - teamScoreHome);
            }
        }

        if(teamScoreAway > teamScoreHome)
        {
            teams[i].GetComponent<Team_Scripts>().Match_Lose();
            teams[j].GetComponent<Team_Scripts>().Match_Win();
        }
        else if(teamScoreHome == teamScoreAway)
        {
            teams[i].GetComponent<Team_Scripts>().Match_Draw();
            teams[j].GetComponent<Team_Scripts>().Match_Draw();
        }
        else
        {
            teams[j].GetComponent<Team_Scripts>().Match_Lose();
            teams[i].GetComponent<Team_Scripts>().Match_Win();
        }
        if (i == 0 || j == 0)
        {
            score_result_home = teamScoreHome;
            score_result_away = teamScoreAway;
        }
    }

    public void Draft()
    {
        //신인 드래프트 -> 새로운선수 110명을 등수 역순으로 각 11명씩 선정한다.
    }

    public void StatChange()
    {
        foreach(GameObject t in teams)
        {
            t.GetComponent<Team_Scripts>().StatChange();
        }
    }
}
