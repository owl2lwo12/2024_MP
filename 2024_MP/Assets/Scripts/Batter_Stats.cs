using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "batter_stats",menuName = "ScriptableObject/batter_stats",order = 3)]
public class Batter_Stats : ScriptableObject
{
    //stats
    //normal naming : current stats / p_ : potential of stats
    [SerializeField]
    private int age = 20; //���� ����
    [SerializeField]
    private int g_age = 30; // ������ �� �ִ� ����(?) �� ���� ���ķδ� ������ ���� ���ٽ��� �� ��.
    [SerializeField]
    private int c_age = 37; // ����¡ Ŀ�갡 ���� �� ����. �̶����� �ɷ�ġ�� �����ϱ� �����Ѵ�.
    [SerializeField]
    private int power = 100; // �Ŀ� : ��ŸȮ��
    [SerializeField]
    private int p_power = 100;
    [SerializeField]
    private int speed = 100; // ����Ʈ : �ַ�, ����
    [SerializeField]
    private int p_speed = 100;
    [SerializeField]
    private int defense = 100; // ���� : ��å, ȣ���� ��
    [SerializeField]
    private int p_defense = 100;
    [SerializeField]
    private int contact = 100; // ���� : ��Ÿ ��
    [SerializeField]
    private int p_contact = 100;
    [SerializeField]
    private int eye = 100; //������ : ���� ��󳻴� �ɷ�
    [SerializeField]
    private int p_eye = 100;
    [SerializeField]
    private int intelligence = 100; //���� : �Ʒ� �� ��·�, �������� ���ο�
    [SerializeField]
    private int p_intelligence = 100;

}
