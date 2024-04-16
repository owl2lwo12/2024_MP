using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "team_template", menuName = "ScriptableObject/team_template", order = 4)]
public class Team_Scripts : ScriptableObject
{
    private static int max_player = 65; // kbo������ ������ ������ �ִ� ��� �ο��� 65������ �����ȴ�.
    [SerializeField]
    private int current_player = 59; // ���� ���ܿ� �����ϴ� ������ ��

    // ���� ��� ��� �ÿ� ����� ����
    private int win = 0;
    private int lose = 0;
    private int draw = 0;
    // ���� ���, ����/  Ÿ�� --> ���� ���ӿ����� Ÿ�� �����͸� ���� Ȯ���� ����.
    private Human[] batterrplayerlist;
    private Human[] pitcherplayerlist;

    public void LineUp()
    {
        // ���� ������ ���� ����, ���� �� ������ �� ���� overall�� ���ؼ� ������ ������ ��.
        // ������ 3,4,5�� ���� power(��Ÿ,Ȩ��)��ġ�� ���� ������
        // ���� 1,2�� ���,���� ��ġ�� ���� ������
        // 6,7,8,9�� ���� 6 -> 9 -> 7 -> 8 ������ ������ ���� ������ ��ġ�Ѵ�.


        // ������ ü��(stamina)���� �̻��� ������ ���߷� �з��ؼ� ��ġ�� ���� �������� 5�� ����
        // ������ ������ stamina ���� �ɷ�ġ�� �м��ؼ� ��ġ�� ���� ������� ����.
    }
    public void Waiver() // ��ġ�� ���ٰ� �򰡵Ǵ� ���� �ذ�. ������ �� ��ܿ��� ���� ��Ȱ��ȭ �� ��.
    {

    }
}
