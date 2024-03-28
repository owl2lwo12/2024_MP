using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pitcher_stats", menuName = "ScriptableObject/pitcher_stats", order = 2)]
public class Pitcher_Stats : ScriptableObject
{
    //stats
    //���� ������ stat�� ���������� �����ϰ� �ٷ�⿣ �ð��� ���� ������ �� ����.
    //����, ������ stats�� ������ ����� ���� ������ ���� ��.
    // stat : current stats | p_stats : potential stats
    [SerializeField]
    private int control = 100; // ���� --> ���� ������ ���� ����.(������ ��, ��ġ�� ��, ���� ��)
    [SerializeField]
    private int pControl = 100;
    [SerializeField]
    private int speed = 100; // ���� --> ��ŸȮ���� ���� + 
    [SerializeField]
    private int pSpeed = 100;
    [SerializeField]
    private int stamina = 100; //���¹̳�(ü��) --> ������ �������� ���� �ɷ�ġ ����.
    [SerializeField]
    private int pStamina = 100;
    [SerializeField]
    private bool hand = false; // false = right , true = left
    [SerializeField]
    private int age = 20; //���� ����
    [SerializeField]
    private int g_age = 30; // ������ �� �ִ� ����(?) �� ���� ���ķδ� ������ ���� ���ٽ��� �� ��.
    [SerializeField]
    private int c_age = 37; // ����¡ Ŀ�갡 ���� �� ����. �̶����� �ɷ�ġ�� �����ϱ� �����Ѵ�.


    // Constructor
    public Pitcher_Stats()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0,pControl + 1);
        stamina = Random.Range (0,pStamina + 1);

        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;

        if (Random.Range(0, 100) > 90) age = 23; //���� draft
        else age = 20; //���� draft
    }
    public void ReuseData()
    {
        pSpeed = Random.Range(20, 101);
        pStamina = Random.Range(20, 101);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0, pControl + 1);
        stamina = Random.Range(0, pStamina + 1);

        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;

        if (Random.Range(0, 100) > 90) age = 23;
        else age = 20;
    }

    public void Retirement() //���� ���� ---> ��Ȱ��ȭ �� ��
    {

    }

    public void Aged()
    {
        age++;

        if(age <= g_age) //������ �� �ִ� ����
        {

        }
        else if(age <= c_age) //������ ���ߴ� ����
        {

        }
        else //����¡ Ŀ�갡 ���۵Ǵ� ����
        {

        }
    }

}
