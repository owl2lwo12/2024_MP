using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Scripts : MonoBehaviour
{
    private static int max_player = 65; // kbo������ ������ ������ �ִ� ��� �ο��� 65������ �����ȴ�.
    [SerializeField]
    private int current_player = 59; // ���� ���ܿ� �����ϴ� ������ ��

    // ���� ��� ��� �ÿ� ����� ����
    private int win = 0;
    private int lose = 0;
    private int draw = 0;
    // ���� ���, ����/  Ÿ�� --> ���� ���ӿ����� Ÿ�� �����͸� ���� Ȯ���� ����.
    [SerializeField]
    private List<GameObject> batterrplayerlist;
    [SerializeField]
    private List<GameObject> pitcherplayerlist;

    public void LineUp()
    {
        // ���� ������ ���� ����, ���� �� ������ �� ���� overall�� ���ؼ� ������ ������ ��.
        // ������ 3,4,5�� ���� power(��Ÿ,Ȩ��)��ġ�� ���� ������
        // ���� 1,2�� ���,���� ��ġ�� ���� ������
        // 6,7,8,9�� ���� 6 -> 9 -> 7 -> 8 ������ ������ ���� ������ ��ġ�Ѵ�.


        // ������ ü��(stamina)���� �̻��� ������ ���߷� �з��ؼ� ��ġ�� ���� �������� 5�� ����
        // ������ ������ stamina ���� �ɷ�ġ�� �м��ؼ� ��ġ�� ���� ������� ����.
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

}
