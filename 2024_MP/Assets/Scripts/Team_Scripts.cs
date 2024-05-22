using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� �ɷ�ġ ����
public class Compare_Stats_B : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        
            return b.GetComponent<Batter_Stats>().getTotal().CompareTo(a.GetComponent<Batter_Stats>().getTotal());
    }
} // Ÿ�� ������������ ����
public class Compare_Stat_P : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {

        return b.GetComponent<Pitcher_Stats>().getTotal().CompareTo(a.GetComponent<Pitcher_Stats>().getTotal());
    }
} // ���� ������������ ����

public class Compare_B_Speed : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        return b.GetComponent<Batter_Stats>().getspeed().CompareTo(a.GetComponent<Batter_Stats>().getspeed());
    }
} //���ξ� ���ǵ� ������� ������ �� ���
public class Compare_B_Power : IComparer<GameObject>
{
    public int Compare(GameObject a, GameObject b)
    {
        return b.GetComponent<Batter_Stats>().getpower().CompareTo(a.GetComponent<Batter_Stats>().getpower());
    }
} //���ξ� �Ŀ� ������� ������ �� ���

public class Team_Scripts : MonoBehaviour
{
    private static int max_player = 65; // kbo������ ������ ������ �ִ� ��� �ο��� 65������ �����ȴ�.
    [SerializeField]
    private int current_player = 59; // ���� ���ܿ� �����ϴ� ������ ��
    private int start_pitcher_idx = 0; // � ���������� ���� ��⿡ ������ �� ������ �ε���
    // ���� ��� ��� �ÿ� ����� ����
    private int win = 0;
    private int lose = 0;
    private int draw = 0;
    // ���� ���, ����/  Ÿ�� --> ���� ���ӿ����� Ÿ�� �����͸� ���� Ȯ���� ����.
    [SerializeField]
    private List<GameObject> batterrplayerlist;
    [SerializeField]
    private List<GameObject> pitcherplayerlist;

    private List<GameObject> line_up_batters_start; //��⿡ �� Ÿ�� ���ξ� 9
    private List<GameObject> line_up_batters_sub; //��⿡ �� �ĺ� Ÿ�� ���ξ�
    private List<GameObject> line_up_pitchers_start; //��⿡ �� ���� ���ξ�
    private List<GameObject> line_up_pitchers_sub; //��⿡ �� �߰� ���� ���ξ�

    public void Sorting_Players() //�� ��� sort�ϸ� ���� �ε��� �ʹ� ����� �� ���Ƽ� �ϴ� �и��س���
    {
        batterrplayerlist.Sort(new Compare_Stats_B());
        pitcherplayerlist.Sort(new Compare_Stat_P());
    }
    public void Sorting_Player_Batters() // �� ������ �ʹ� ���� �ٲٸ� �̻��� �� �� �����Ƿ� Ÿ�ڸ� �ٲ� �� �ֵ���
    {
        batterrplayerlist.Sort(new Compare_Stats_B());
    }
    public void LineUp()
    {
        // ���� ������ ���� ����, ���� �� ������ �� ���� overall�� ���ؼ� ������ ������ ��.
        // ������ 3,4,5�� ���� power(��Ÿ,Ȩ��)��ġ�� ���� ������
        // ���� 1,2�� ���,���� ��ġ�� ���� ������
        // 6,7,8,9�� ���� 6 -> 9 -> 7 -> 8 ������ ������ ���� ������ ��ġ�Ѵ�. ---- �̰� �ƴ�
        // ���� ������ ������� ������ ������� ä���,
        bool[] cur_pos = new bool[8];
        foreach(var player in batterrplayerlist)
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
                            line_up_batters_start.Add(player) ;
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
                    if(line_up_batters_sub.Count < 6) //6���߿� ���� �տ� �ִ� �� ���� ����Ÿ�ڷ� �� ����
                        line_up_batters_sub.Add(player);
                    break;
            }
        }
        line_up_batters_start.Add(line_up_batters_sub[0]); //����Ÿ�ڷ� �Ѹ� ����
        line_up_batters_sub.RemoveAt(0); //�ĺ������� ���ְ�

        //���� Ÿ�� ������ ¥���Ѵ�.
        // 1,2,9���� ���� �ӵ��� ���� �������� ��ġ�ϴ� ���� ������ ���̹Ƿ� �� �� ��
        //���� 3~8�� Ÿ���� �Ŀ� ������� ������������ 3 - 4�� Ÿ���� ��ȯ���ָ� ���� ���� ��?
        line_up_batters_start.Sort(new Compare_B_Speed());
        GameObject temp = line_up_batters_start[2]; 
        line_up_batters_start[2] = line_up_batters_start[8];
        line_up_batters_start[8] = temp; //������� ����Ǹ� 1,2,9�� Ÿ���� ä������.

        List<GameObject> subList = line_up_batters_start.GetRange(2, 6);
        subList.Sort(new Compare_B_Power());
        for(int i = 0; i < subList.Count; i++)
        {
            line_up_batters_start[i+2] = subList[i];
        }
        temp = line_up_batters_start[2];
        line_up_batters_start[2] = line_up_batters_start[3];
        line_up_batters_start[3] = temp;

        // ������ ü��(stamina)���� �̻��� ������ ���߷� �з��ؼ� ��ġ�� ���� �������� 5�� ����
        // ������ ������ stamina ���� �ɷ�ġ�� �м��ؼ� ��ġ�� ���� ������� ����.
        foreach(var player in pitcherplayerlist)
        {
            if(player.GetComponent<Pitcher_Stats>().getstamina() >= 80 && line_up_pitchers_start.Count < 5)
            {//5���� + Stamina ����ؼ� �̷���
                line_up_pitchers_start.Add(player);
            }
            else if (line_up_pitchers_sub.Count < 7)
            {//������ �ִ� 7���� �ǵ���
                line_up_pitchers_sub.Add(player);
            }
        }
    }
    public void Waiver() // ��ġ�� ���ٰ� �򰡵Ǵ� ���� �ذ�. ������ ����
    {
        int t = 0;
        if (batterrplayerlist.Count >= pitcherplayerlist.Count) { //�Ϲ������� ������ ��ġ�� �����Ƿ� ���� ���� Ÿ�ڸ� ���� ¥����.
            for(int i = 1; i < batterrplayerlist.Count; i++)
            {
                if (batterrplayerlist[t].GetComponent<Batter_Stats>().getTotal() > batterrplayerlist[i].GetComponent<Batter_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        else
        {
            for(int i =1;i<pitcherplayerlist.Count;i++) {
                if (pitcherplayerlist[t].GetComponent<Pitcher_Stats>().getTotal() > pitcherplayerlist[i].GetComponent<Pitcher_Stats>().getTotal())
                {
                    t = i;
                }
            }
        }
        //pitcherplayerlist.RemoveAt(t); //���� gameobject�� ����ٸ�, ��Ȱ��ȭ�ϴ� ������ ������ ��
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

    public int get_start_pitcher() { return start_pitcher_idx; }
    public void after_game()
    {
        start_pitcher_idx = (start_pitcher_idx + 1) % 5;
    }
}
