using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����Ͱ� �������� ��
/// 1. �������� stats
/// 2. �������� records
/// </summary>

// ���� ������ �˾ƺ��� ����, �� 1�� 2�� ���� 3�� ���� ���� �߰� ���� ��
public enum def_position { B1, B2, SS, B3, C, LF, CF, RF};

public class Batter_Stats : Human
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

    // ������ stats�� �� ������ �� �ɷ�ġ, ���� ���°� ���� ���� �ֱ� �ѵ�, �ϴ��� �и�
    // �ϴ� ���� - 2��(������ ũ��)
    //      3�� - 1��
    //      �߰� - ��/����
    // 1��� �ظ��ϸ� �ٵ� ������ �Ѵ�.
    // ���ݼ��� �پ��, 3��, �ܾ߿����� �ظ�ŭ �Ѵ�.
    // 2��� ���� �ܾ� �������� �����ϴ�.
    // ����ɷ�ġ(defense 0 ~ 100) ���� ������� �Ѵٴ� 1��� 20����? ����/������ 50����?
    // 3�� 55, ���� - ������� �ƴϸ� ���� ������ ����� ���� �ɷ�. ��õ������ �غ� �� ������
    // 2��/ �߰� 70? ���� 80 --> ���� �� �ɷ�ġ�� ���� ������ ���� �Ʒ� ȿ���� �޶�������/
    [SerializeField]
    private def_position main_pos;
    private List<def_position> sub_pos;
    private int pos_stats_main = 0;
    private List<int> pos_stats_sub;

    //season records;
    private int hit = 0;//��Ÿ
    private int d_hit = 0;//2��Ÿ
    private int t_hit = 0;//3��Ÿ
    private int h_run = 0;//Ȩ��
    private int steal = 0;//����
    private int ball4 = 0;//4��
    private int score = 0;//Ÿ��
    private int s_out = 0;//����
    private int f_out = 0;//���
    private int g_out = 0;//����

    //detail stats
    private int inner = 0;//����Ÿ��
    private int outer = 0;//�ܾ�Ÿ��
    private int left_dir = 0;//���� Ÿ��
    private int center_dir = 0;//�߾� Ÿ��
    private int right_dir = 0;//���� Ÿ��

    //details
    [SerializeField]
    private int training_style = 0; // 100�ϼ��� ����Ʈ Ʈ���̴� ����(0~10)
    [SerializeField]
    private int batting_theory = 0; // 100�ϼ��� ��� ����(0~10)
    [SerializeField]
    private int batting_position = 0; //100�ϼ��� �����̼ų� ���ÿ� ������(0~10)

    [SerializeField]
    private int batting_positive = 0; //100�� �������� �������� ����(0~10)
    [SerializeField]
    private int zone_size = 0;//100�� �������� ���� ũ�⸦ ũ�� ��´�.(0~10)

    //getset records
    public int Hit { get => hit; set => hit = value; }
    public int D_hit { get => d_hit; set => d_hit = value; }
    public int T_hit { get => t_hit; set => t_hit = value; }
    public int H_run { get => h_run; set => h_run = value; }
    public int Steal { get => steal; set => steal = value; }
    public int Ball4 { get => ball4; set => ball4 = value; }
    public int Score { get => score; set => score = value; }
    public int S_out { get => s_out; set => s_out = value; }
    public int F_out { get => f_out; set => f_out = value; }
    public int G_out { get => g_out; set => g_out = value; }
    // getset serial records
    public int Inner { get => inner; set => inner = value; }
    public int Outer { get => outer; set => outer = value; }
    public int Left_dir { get => left_dir; set => left_dir = value; }
    public int Center_dir { get => center_dir; set => center_dir = value; }
    public int Right_dir { get => right_dir; set => right_dir = value; }
    public int Training_style { get => training_style; set => training_style = value; }
    public int Batting_theory { get => batting_theory; set => batting_theory = value; }
    public int Batting_position { get => batting_position; set => batting_position = value; }
    public int Batting_positive { get => batting_positive; set => batting_positive = value; }
    public int Zone_size { get => zone_size; set => zone_size = value; }

    // Constructor
    /*public Batter_Stats()
    {
        p_power = Random.Range(20, 101); 
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        if(p_power > 65)
        {
            training_style = Random.Range(50, 101);
            batting_theory = Random.Range(70, 101);
        }
        else
        {
            training_style = Random.Range(0, 51);
            batting_theory = Random.Range(0, 80);
        }

        power = Random.Range(0, p_power);
        speed = Random.Range(0,p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(20, 101);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;

        if(getheight() > 190)
        {
            int t = Random.Range(1, 5);
            switch(t)
            {
                case 1:
                    main_pos = def_position.B1;
                    break;
                case 2:
                    main_pos = def_position.B3;
                    break;
                case 3:
                    main_pos = def_position.LF;
                    break;
                case 4:
                    main_pos = def_position.RF;
                    break;
            }
        }else if(getheight() > 180)
        {
            int t = Random.Range(1, 9);
            switch(t)
            {
                case 1:
                    main_pos = def_position.B1;
                    break; 
                case 2:
                    main_pos = def_position.B3;
                    break;
                case 3:
                    main_pos = def_position.LF;
                    break;
                case 4:
                    main_pos = def_position.RF;
                    break;
                case 5:
                    main_pos = def_position.CF;
                    break;
                case 6:
                    main_pos = def_position.SS;
                    break;
                case 7:
                    main_pos = def_position.B2;
                    break;
                case 8:
                    main_pos = def_position.C;
                    break;
            }
        }
        else
        {
            int t = Random.Range(1, 5);
            switch(t)
            {
                case 1:
                    main_pos = def_position.B2;
                    break; 
                case 2:
                    main_pos = def_position.SS;
                    break;
                case 3:
                    main_pos = def_position.CF;
                    break;
                case 4:
                    main_pos = def_position.C;
                    break;
            }
        }

        pos_stats_main = Random.Range(0, defense);
        if(main_pos == def_position.SS)
        {
            sub_pos =  new List<def_position> { def_position.B2, def_position.CF };
            pos_stats_sub = new List<int> { pos_stats_main * 8 / 10, pos_stats_main * 6 / 10 };
        }
        else if(main_pos == def_position.B2)
        {
            sub_pos = new List<def_position> { def_position.CF };
            pos_stats_sub = new List<int> { pos_stats_main * 6 / 10 };
        }
        else if(main_pos == def_position.CF)
        {
            sub_pos = new List<def_position> { def_position.LF, def_position.RF };
            pos_stats_sub = new List<int> { pos_stats_main, pos_stats_main };
        }
        else if(main_pos == def_position.RF)
        {
            sub_pos = new List<def_position> { def_position.LF, def_position.B1 };
            pos_stats_sub = new List<int> { pos_stats_main * 9 / 10, pos_stats_main * 7 / 10 };
        }
        else if(main_pos == def_position.LF)
        {
            sub_pos = new List<def_position> { def_position.RF, def_position.B1 };
            pos_stats_sub = new List<int> { pos_stats_main * 9 / 10, pos_stats_main * 7 / 10 };
        }
        else if(main_pos == def_position.B3)
        {
            sub_pos = new List<def_position> { def_position.B1 };
            pos_stats_sub = new List<int> { pos_stats_main * 8 / 10 };
        }

        batting_position = Random.Range(0, 101);
        batting_positive = Random.Range(0, 101);
        zone_size = Random.Range(0, 101);
    }*/
    public void ReuseData()
    {
        p_power = Random.Range(20, 101);
        p_speed = Random.Range(20, 101);
        p_intelligence = Random.Range(20, 101);
        p_eye = Random.Range(20, 101);
        p_contact = Random.Range(20, 101);

        power = Random.Range(0, p_power);
        speed = Random.Range(0, p_speed);
        intelligence = Random.Range(0, p_intelligence);
        eye = Random.Range(0, p_eye);
        defense = Random.Range(20, 101);
        contact = Random.Range(0, p_contact);

        if (Random.Range(0, 100) > 40) hand = true;
        else hand = false;
    }

    //getter
    public int getpower() { return power; }
    public int getspeed() { return speed; }
    public int getcontact() { return contact; }
    public int getdefense() { return defense; }
    public int geteye() { return eye; }
    public int getintelligence() { return intelligence; }
    public def_position getmaindefposition() { return main_pos; }
    public List<def_position> getsubdefposition() { return sub_pos; }
    public int getmaindefstats() { return pos_stats_main; }
    public List<int> getsubdefstats() { return pos_stats_sub; }

    public int getTotal() //���ξ��̳� �׷��� ���� ��ġ�� �Ǵ��ϱ� ���� ����� ��
    {
        int tot = 0;

        tot += power + speed + contact + eye + intelligence;

        if (tot > 400) tot *= 2;

        if (main_pos == def_position.CF || main_pos == def_position.B2) tot += 30;

        if (main_pos == def_position.SS) tot += 50;

        tot -= getage();
        if (getage() > 35) tot += getage() * 3;

        if (getinjurytype() != injury.fine) tot = -tot;

        return tot;
    }

    public void After_Match() //���� ��Ⱑ ���� �� ���� ������ ��ġ�� ���, �Ʒ� ���ǵ� ���� �߰��� ��
    {
        //Debug.Log("stats change");
        float p;
        if (getage() <= getgage())
        {
            p = Random.Range(0f, 1f);
            int _r = Random.Range(0, 10);
            if (power < p_power)
            {
                if (p > 0.9f) power++;                
                if (_r < training_style) power++;
            }
            else
            {
                if (p > 0.995f) power++;
                if (p > 0.999f)
                {
                    if (_r < training_style) p_power++;
                }
            }
            if (speed < p_speed)
            {
                if (p > 0.9f) speed++;
                if (_r < training_style) speed++;
            }
            else
            {
                if (p > 0.995f) speed++;
                if(p > 0.999f)
                {
                    if (_r < training_style) p_speed++;
                }
            }
            if (contact < p_contact)
            {
                if (p > 0.9f) contact++;
            }
            else
            {
                if (p > 0.995f) contact++;
            }
            if (eye < p_eye)
            {
                if (p > 0.9f) eye++;
            }
            else
            {
                if (p > 0.995f) eye++;
            }
            if (intelligence < p_intelligence)
            {
                if (p > 0.9f) intelligence++;
            }
            else
            {
                if (p > 0.995f) intelligence++;
            }
        }
        else if(getgage()<= getcage())
        {
            p = Random.Range(0, 6);
            if(power > p_power)
                power -= (int)p / 5;
            if(speed > p_speed)
                speed -= (int)p / 5;
            if(contact > p_contact)
                contact -= (int)p / 5;
            if(eye > p_eye)
                eye -= (int)p / 5;
            if(intelligence > p_intelligence)
                intelligence -= (int)p / 5;
        }
        else
        {
            p = Random.Range(-3, 1);
            power += (int)p;
            p = Random.Range(-3, 1);
            contact += (int)p;
            p = Random.Range(-3, 1);
            eye += (int)p;
            p = Random.Range(-3, 1);
            intelligence += (int)p;
            p = Random.Range(-3, 1);
            speed += (int)p;
        }
    }
    public void One_Match() //main position�� ���� + Ư�� ���������� ���� �� �߰��� ���� ���� ��(�ٸ� �Լ�����)
    {
        if(defense > pos_stats_main)
        pos_stats_main += Random.Range(0, 1);
    }
}
