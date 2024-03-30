using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "batter_stats",menuName = "ScriptableObject/batter_stats",order = 3)]
public class Batter_Stats : ScriptableObject
{
    //stats
    //normal naming : current stats | p_ : potential of stats
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
    [SerializeField]
    private bool hand = false; // false = right , true = left

    // Constructor
    public Batter_Stats()
    {
        p_power = Random.Range(20, 101); 
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_defense = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0,p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(0, p_defense);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;
    }
    public void ReuseData()
    {
        p_power = Random.Range(20, 101);
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_defense = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0, p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(0, p_defense);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;
    }
}
