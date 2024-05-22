using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Pitcher_Stats : Human
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
    private int stamina = 30; //���¹̳�(ü��) --> ������ �������� ���� �ɷ�ġ ����.
    [SerializeField]
    private int cStamina = 30; //���� �ܿ� ���¹̳�
    [SerializeField]
    private bool hand = false; // false = right , true = left

    //season records
    private int era = 0;//����
    private float inning = 0;//������ �̴�

    // Constructor
    public Pitcher_Stats()
    {
        pSpeed = Random.Range(20, 101);
        stamina = Random.Range(20, 121);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0,pControl + 1);
        cStamina = stamina;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
    public void ReuseData()
    {
        pSpeed = Random.Range(20, 101);
        stamina = Random.Range(20, 121);
        pControl = Random.Range(20, 101);

        speed = Random.Range(0, pSpeed + 1);
        control = Random.Range(0, pControl + 1);
        cStamina= stamina;

        if (Random.Range(0, 100) > 75) hand = true;
        else hand = false;
    }
    // Getter
    public int getcontrol() { return control; }
    public int getstamina() { return stamina; }
    public int getspeed() { return speed; }
    public bool gethand() { return hand; }

    public int getTotal()
    {
        int tot = 0;
        tot += control + speed;
        if (tot > 170) tot *= 3; //���������� �������� ���� ����
        if(stamina > 80) //�������� ����
        {
            tot += stamina * 2;
        }
        else
        {
            tot += stamina;
        }
        tot -= getage(); //����ϸ� ���� ���� ����� ©������

        if (getinjurytype() != injury.fine) tot = -tot;

        return tot;
    } //�� stat Ȯ�ο�

    public void After_Match() //���� ��ġ���� ������� �� �����Լ�
    {
        float p;
        if (getage() <= getgage())
        {
            p = Random.Range(0, 1);
            if (control < pControl)
            {
                if (p > 0.9f) control++;
            }
            else
            {
                if (p > 0.995f) control++;
            }
            p = Random.Range(0, 1);
            if (speed < pSpeed)
            {
                if (p > 0.9f) speed++;
            }
            else
            {
                if (p > 0.995f) speed++;
            }
        }
        else if(getage() <= getcage())
        {
            p = Random.Range(0, 6);
            if (control > pControl)
                control -= (int)p/5;
            if(speed > pSpeed)
                speed -= (int)p/5;
        }
        else
        {
            p = Random.Range(-3, 1);
            control += (int)p;
            p = Random.Range(-3, 1);
            speed += (int)p;
        }
    }
    public void One_Match()
    {
        if (stamina - cStamina > 20)
        {
            cStamina += 20;
        }
        else if (stamina - cStamina <= 20)
        {
            cStamina = stamina;
        }
    }
}
