using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//fine�� default
//fatigue�� �Ƿ� ���·� (���� �Ⱓ���� �ɷ�ġ ����) �ð��� ������ �ڵ������� �ؼ�
//fine�� fatigue���¿����� ��� ������ �����ϴ�.
//hurt�� ��� ��, �߸� ��, �鵵�ϴٰ� ���� ���� ���� �ܱⰣ���� ������ �Ұ����� �λ�
//fine�� fatigue, hurt ���¿����� �������� �ɷ�ġ �ջ��� ����.
//serious�� ��� �ܽ�Ʈ��, ����� ���� �λ����� �ɷ�ġ�� �������� ���Ұ� ���� �߻��Ѵ�.
//critical�� ��� �����ͼ�, ������ ���� �λ����� �ɷ�ġ�� �������� ���Ұ� ���� �߻��Ѵ�.
//serious�� critical�� ��쿡 �߻��ϴ� �������� �ɷ�ġ ���Ҹ� ���� �Ұ� �Ⱓ�� ���� ��, ȸ���� �� �ִ� recovery �ý����� �־ ���� �� ����.
//dead�� ��� ���������� ���� ���� �ǹ��ϰ�, ���� Ȱ���� �Ұ����� �λ�, ��ȭ, ��� ������ �������� ���Ѵ�.
public enum injury { fine, fatigue, hurt, serious, critical, dead };
public class Human : MonoBehaviour 
{
    [SerializeField]
    private string name = "default";
    [SerializeField]
    private int age = 20; //���� ����
    [SerializeField]
    private int g_age = 30; // ������ �� �ִ� ����(?) �� ���� ���ķδ� ������ ���� ���ٽ��� �� ��.
    [SerializeField]
    private int c_age = 37; // ����¡ Ŀ�갡 ���� �� ����. �̶����� �ɷ�ġ�� �����ϱ� �����Ѵ�.
    [SerializeField]
    private float height = 170; // Ű
    [SerializeField]
    private float weight = 100; // ������
    [SerializeField]
    private float muscle = 30; // ������
    [SerializeField]
    private int injuredtime = 0; //�λ��� ���ӵǴ� �Ⱓ
    [SerializeField]
    private injury injured = injury.fine; //�λ� ����

    //constructor
    public Human()
    {
        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 90) age = 23; //���� draft
        else age = 20; //���� draft
    }

    public bool Retirement() //���� ���� ---> ������ ���̴ϱ� ����? �� ��
    {
        bool iretire = false;
        if(injured == injury.dead) {
            iretire = true;
            return iretire; 
        }else if (age > c_age)
        {
            int t = age - c_age;
            int r = Random.Range(0, 13);
            if (r <= t) iretire = true;
            return iretire;
        }
        return iretire;
    }
    //getter
    public string getname() { return name; }
    public int getage() { return age; }
    public int getcage() { return c_age; }
    public int getgage() { return g_age; }
    public float getheight() { return height; }
    public float getweight() { return weight; }
    public float getmuscle() { return muscle; }
    public int getinjuredtime() { return injuredtime; }
    public injury getinjurytype() { return injured; }
    //setter
    public void Aged()
    {
        age++;
    }
    public void weightcontrol(float w)
    {
        weight += w;
    }
    public void grow(float g)
    {
        height += g;
    }
    public void weighttraining(float m)
    {
        muscle += m;
    }
    public void badHappened(injury i)
    {
        injured = i;
        if(injured == injury.serious)
        {
            injuredtime = Random.Range(30, 90);
        }else if(injured == injury.critical)
        {
            injuredtime = Random.Range(144, 200);
        }else if(injured == injury.dead)
        {
            injuredtime = Random.Range(500, 1000);
        }
        else if(injured == injury.fatigue)
        {
            injuredtime = Random.Range(3, 8);
        }else if(injured == injury.hurt)
        {
            injuredtime = Random.Range(7, 15);
        }
    }

}
