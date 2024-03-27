using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "name_generator", menuName = "ScriptableObject/name_generator", order = 1)]
public class Name_Generator : ScriptableObject
{
    [SerializeField]
    private string[] familyname; //�� ������ ��
    [SerializeField]
    private string[] lastname; //�̸� ������ ��

    public string GenerateName()
    {
        string fname = familyname[Random.Range(0, familyname.Length)]; //familyname �� �ϳ��� �����ϰ� ������ ��
        string lname = lastname[Random.Range(0, lastname.Length)]; //lastname �� �ϳ��� ���� ������ ��
        string name = fname + lname; //�ΰ��� ��ĥ ��
        return name;
    }
}
