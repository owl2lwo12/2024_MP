using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총 능력치 기준
public class Compare_Stats_B : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        
            return b.GetComponent<Batter_Stats>().getTotal().CompareTo(a.GetComponent<Batter_Stats>().getTotal());
    }
} // 타자 내림차순으로 정렬
public class Compare_Stat_P : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {

        return b.GetComponent<Pitcher_Stats>().getTotal().CompareTo(a.GetComponent<Pitcher_Stats>().getTotal());
    }
} // 투수 내림차순으로 정렬

public class Compare_B_Speed : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        return b.GetComponent<Batter_Stats>().getspeed().CompareTo(a.GetComponent<Batter_Stats>().getspeed());
    }
} //라인업 스피드 순서대로 정렬할 때 사용
public class Compare_B_Power : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        return b.GetComponent<Batter_Stats>().getpower().CompareTo(a.GetComponent<Batter_Stats>().getpower());
    }
} //라인업 파워 순서대로 정렬할 때 사용

public class Team_Scripts : MonoBehaviour
{
    private static int max_player = 65; // kbo규정에 따르면 구단의 최대 등록 인원은 65명으로 구성된다.
    [SerializeField]
    private int current_player = 59; // 현재 구단에 존재하는 선수의 수
    // 시즌 등수 계산 시에 사용할 스탯
    private int win = 0;
    private int lose = 0;
    private int draw = 0;
    // 선수 명단, 투수/  타자 --> 실제 게임에서는 타자 데이터만 사용될 확률이 높다.
    [SerializeField]
    private List<GameObject> batterrplayerlist;
    [SerializeField]
    private List<GameObject> pitcherplayerlist;

    private List<GameObject> line_up_batters_start; //경기에 뛸 타자 라인업 9
    private List<GameObject> line_up_batters_sub; //경기에 뛸 후보 타자 라인업
    private List<GameObject> line_up_pitchers_start; //경기에 뛸 투수 라인업
    private List<GameObject> line_up_pitchers_sub; //경기에 뛸 중계 투수 라인업

    public List<GameObject> Batterrplayerlist { get => batterrplayerlist; set => batterrplayerlist = value; }
    public List<GameObject> Pitcherplayerlist { get => pitcherplayerlist; set => pitcherplayerlist = value; }

    public GameObject Change_Pitcher(int score_gap)
    {
        if(Mathf.Abs(score_gap) <= 3)
        {
            return line_up_batters_sub[Random.Range(0, 3)];
        }
        else
        {
            return line_up_batters_sub[Random.Range(3,7)];
        }
    }
    public void Sorting_Players() //매 경기 sort하면 게임 로딩이 너무 길어질 것 같아서 일단 분리해놓음
    {
        Batterrplayerlist.Sort(new Compare_Stats_B());
        Pitcherplayerlist.Sort(new Compare_Stat_P());
    }
    public void Sorting_Player_Batters() // 또 투수는 너무 자주 바꾸면 이상해 질 수 있으므로 타자만 바꿀 수 있도록
    {
        Batterrplayerlist.Sort(new Compare_Stats_B());
    }
    public void LineUp()
    {
        // 먼저 포지션 별로 정렬, 이후 각 포지션 별 종합 overall를 비교해서 선수를 배정할 것.
        // 포지션 3,4,5에 먼저 power(장타,홈런)가치가 높은 선수를
        // 이후 1,2에 출루,컨택 가치가 높은 선수를
        // 6,7,8,9는 각각 6 -> 9 -> 7 -> 8 순서로 오버롤 높은 선수를 배치한다. ---- 이거 아님
        // 먼저 오버롤 순서대로 포지션 비었으면 채우고,
        bool[] cur_pos = new bool[8];
        foreach (var player in Batterrplayerlist)
        {
            switch (player.GetComponent<Batter_Stats>().getmaindefposition())
            {
                case def_position.B1:
                    if (!cur_pos[0])
                    {
                        cur_pos[0] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.B2:
                    if (!cur_pos[1])
                    {
                        cur_pos[1] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (!cur_pos[6])
                        {
                            cur_pos[6] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.B3:
                    if (!cur_pos[2])
                    {
                        cur_pos[2] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (!cur_pos[0])
                        {
                            cur_pos[0] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.SS:
                    if (!cur_pos[3])
                    {
                        cur_pos[3] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (!cur_pos[1])
                        {
                            cur_pos[1] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (!cur_pos[6])
                        {
                            cur_pos[6] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.C:
                    if (!cur_pos[4])
                    {
                        cur_pos[4] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.LF:
                    if (!cur_pos[5])
                    {
                        cur_pos[5] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (!cur_pos[7])
                        {
                            cur_pos[7] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (!cur_pos[0])
                        {
                            cur_pos[0] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.CF:
                    if (!cur_pos[6])
                    {
                        cur_pos[6] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (!cur_pos[5])
                        {
                            cur_pos[5] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (!cur_pos[7])
                        {
                            cur_pos[7] = true;
                            line_up_batters_start.Add(player);
                        }
                        else if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                case def_position.RF:
                    if (!cur_pos[7])
                    {
                        cur_pos[7] = true;
                        line_up_batters_start.Add(player);
                    }
                    else
                    {
                        if (line_up_batters_sub.Count < 6)
                            line_up_batters_sub.Add(player);
                    }
                    break;
                default:
                    if (line_up_batters_sub.Count < 6) //6명중에 가장 앞에 있는 한 명은 지명타자로 뺄 것임
                        line_up_batters_sub.Add(player);
                    break;
            }
        }
        line_up_batters_start.Add(line_up_batters_sub[0]); //지명타자로 한명 들어가고
        line_up_batters_sub.RemoveAt(0); //후보에서는 빼주고

        //이제 타선 순서를 짜야한다.
        // 1,2,9번은 먼저 속도가 빠른 선수들을 배치하는 것이 정석일 것이므로 그 대 로
        //이후 3~8번 타순은 파워 순서대로 내림차순으로 3 - 4번 타순을 교환해주면 되지 않을 까?
        line_up_batters_start.Sort(new Compare_B_Speed());
        GameObject temp = line_up_batters_start[2];
        line_up_batters_start[2] = line_up_batters_start[8];
        line_up_batters_start[8] = temp; //여기까지 진행되면 1,2,9번 타순이 채워진다.

        List<GameObject> subList = line_up_batters_start.GetRange(2, 6);
        subList.Sort(new Compare_B_Power());
        for (int i = 0; i < subList.Count; i++)
        {
            line_up_batters_start[i + 2] = subList[i];
        }
        temp = line_up_batters_start[2];
        line_up_batters_start[2] = line_up_batters_start[3];
        line_up_batters_start[3] = temp;

        // 투수는 체력(stamina)일정 이상의 선수를 선발로 분류해서 가치가 높은 선수부터 5명 선정
        // 나머지 투수는 stamina 제외 능력치를 분석해서 가치가 높은 순서대로 배정.
        foreach (var player in Pitcherplayerlist)
        {
            if (player.GetComponent<Pitcher_Stats>().getstamina() >= 80 && line_up_pitchers_start.Count < 5)
            {//5선발 + Stamina 고려해서 이렇게
                line_up_pitchers_start.Add(player);
            }
            else if (line_up_pitchers_sub.Count < 7)
            {//불펜은 최대 7명이 되도록
                line_up_pitchers_sub.Add(player);
            }
        }
    }
    public void Waiver() // 가치가 없다고 평가되는 선수 해고. 선수는 삭제
    {
        int t = 0;
        if (Batterrplayerlist.Count >= Pitcherplayerlist.Count)
        { //일반적으로 투수의 가치가 높으므로 같은 수면 타자를 먼저 짜른다.
            for (int i = 1; i < Batterrplayerlist.Count; i++)
            {
                if (Batterrplayerlist[t].GetComponent<Batter_Stats>().getTotal() > Batterrplayerlist[i].GetComponent<Batter_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        else
        {
            for (int i = 1; i < Pitcherplayerlist.Count; i++)
            {
                if (Pitcherplayerlist[t].GetComponent<Pitcher_Stats>().getTotal() > Pitcherplayerlist[i].GetComponent<Pitcher_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        //pitcherplayerlist.RemoveAt(t); //만약 gameobject로 만든다면, 비활성화하는 것으로 변경할 것
    }
    public void Match_Lose()
    {
        lose++;
    }
    public void Match_Win()
    {
        win++;
    }
    public void Match_Draw()
    {
        draw++;
    }

    public GameObject get_start_pitcher(int i) { return line_up_pitchers_start[i]; }
    public List<GameObject> get_sub_pitcher() { return line_up_pitchers_sub; }
    public List<GameObject> get_batters() { return line_up_batters_start; }
    public List<GameObject> get_sub_batters() { return line_up_batters_sub; }

    public void StatChange()
    {
        foreach(GameObject b in batterrplayerlist)
        {
            b.GetComponent<Batter_Stats>().After_Match();
        }
        foreach(GameObject p in pitcherplayerlist)
        {
            p.GetComponent<Pitcher_Stats>().After_Match();
        }
    }
}
