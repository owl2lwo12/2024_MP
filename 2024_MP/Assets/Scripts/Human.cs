using System.Collections;
using System.Collections.Generic;
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
    private int age = 20; //���� ����
    [SerializeField]
    private int g_age = 30; // ������ �� �ִ� ����(?) �� ���� ���ķδ� ������ ���� ���ٽ��� �� ��.
    [SerializeField]
    private int c_age = 37; // ����¡ Ŀ�갡 ���� �� ����. �̶����� �ɷ�ġ�� �����ϱ� �����Ѵ�.
    [SerializeField]
    private int height = 170; // Ű
    [SerializeField]
    private int weight = 100; // ������
    [SerializeField]
    private int muscle = 30; // ������
    [SerializeField]
    private int injuredtime = 0; //�λ��� ���ӵǴ� �Ⱓ
    [SerializeField]
    private injury injured = injury.fine; //�λ� ����

    public Human()
    {
        g_age = Random.Range(-2, 3) + 30;
        c_age = Random.Range(-2, 3) + 37;

        if (Random.Range(0, 100) > 90) age = 23; //���� draft
        else age = 20; //���� draft
    }

    public void Aged()
    {
        age++;

        if (age <= g_age) //������ �� �ִ� ����
        {

        }
        else if (age <= c_age) //������ ���ߴ� ����
        {

        }
        else //����¡ Ŀ�갡 ���۵Ǵ� ����
        {

        }
    }
    public void Retirement() //���� ���� ---> ��Ȱ��ȭ �� ��
    {

    }
}
