using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선수 데이터가 가져야할 것
/// 1. 선수들의 stats
/// 2. 선수들의 records
/// </summary>

// 수비 포지션 알아보기 쉽게, 각 1루 2루 유격 3루 포수 좌익 중견 우익 순
public enum def_position { B1, B2, SS, B3, C, LF, CF, RF};

public class Batter_Stats : Human
{
    //stats
    //normal naming : current stats | p_ : potential of stats
    [SerializeField]
    private int power = 100; // 파워 : 장타확률
    [SerializeField]
    private int p_power = 100;
    [SerializeField]
    private int speed = 100; // 스피트 : 주루, 도루
    [SerializeField]
    private int p_speed = 100;
    [SerializeField]
    private int defense = 100; // 수비 : 실책, 호수비 등
    [SerializeField]
    private int contact = 100; // 컨택 : 안타 등
    [SerializeField]
    private int p_contact = 100;
    [SerializeField]
    private int eye = 100; //선구안 : 공을 골라내는 능력
    [SerializeField]
    private int p_eye = 100;
    [SerializeField]
    private int intelligence = 100; //지능 : 훈련 시 상승량, 투수와의 수싸움
    [SerializeField]
    private int p_intelligence = 100;
    [SerializeField]
    private bool hand = false; // false = right , true = left

    // 포지션 stats는 각 포지션 별 능력치, 같이 묶는게 좋을 수도 있긴 한데, 일단은 분리
    // 일단 유격 - 2루(연관성 크다)
    //      3루 - 1루
    //      중견 - 좌/우익
    // 1루는 왠만하면 다들 적당히 한다.
    // 유격수로 뛰어나면, 3루, 외야에서도 왠만큼 한다.
    // 2루수 또한 외야 컨버젼이 가능하다.
    // 수비능력치(defense 0 ~ 100) 기준 사람같이 한다는 1루수 20정도? 좌익/우익은 50정도?
    // 3루 55, 포수 - 포수출신 아니면 거의 가능한 사람이 없는 걸로. 선천적으로 해본 적 없으면
    // 2루/ 중견 70? 유격 80 --> 이후 실 능력치에 따라 포지션 수비 훈련 효율이 달라지도록/
    [SerializeField]
    private def_position main_pos;
    private List<def_position> sub_pos;
    private int pos_stats_main = 0;
    private List<int> pos_stats_sub;

    //season records;
    private int hit = 0;//안타
    private int d_hit = 0;//2루타
    private int t_hit = 0;//3루타
    private int h_run = 0;//홈런
    private int steal = 0;//도루
    private int ball4 = 0;//4볼
    private int score = 0;//타점
    private int s_out = 0;//삼진
    private int f_out = 0;//뜬공
    private int g_out = 0;//땅볼

    //detail stats
    private int inner = 0;//내야타구
    private int outer = 0;//외야타구
    private int left_dir = 0;//좌측 타구
    private int center_dir = 0;//중앙 타구
    private int right_dir = 0;//우측 타구

    //details
    [SerializeField]
    private int training_style = 0; // 100일수록 웨이트 트레이닝 위주(0~10)
    [SerializeField]
    private int batting_theory = 0; // 100일수록 뜬공 지향(0~10)
    [SerializeField]
    private int batting_position = 0; //100일수록 로테이셔널 히팅에 가깝게(0~10)

    [SerializeField]
    private int batting_positive = 0; //100에 가까울수록 적극적인 스윙(0~10)
    [SerializeField]
    private int zone_size = 0;//100에 가까울수록 존의 크기를 크게 잡는다.(0~10)

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

    public int getTotal() //라인업이나 그렇게 선수 가치를 판단하기 위해 사용할 것
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

    public void After_Match() //일정 경기가 끝날 때 마다 랜덤한 수치적 상승, 훈련 조건도 추후 추가할 것
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
    public void One_Match() //main position만 성장 + 특정 포지션으로 출장 시 추가적 성장 있을 것(다른 함수에서)
    {
        if(defense > pos_stats_main)
        pos_stats_main += Random.Range(0, 1);
    }
}
