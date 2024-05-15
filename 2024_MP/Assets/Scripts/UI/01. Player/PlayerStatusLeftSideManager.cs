using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        [Header("Status")]
        public TMP_Dropdown Dropdown;
        public TMP_Text playerName;
        
        [Header("���� ����Ʈ")]
        public List<Human> playerList = new List<Human>();
        
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // �ʱ�ȭ �Լ�
        void Init()
        {
            // ���� ����Ʈ �ʱ�ȭ
            List<TMP_Dropdown.OptionData> playerList = new List<TMP_Dropdown.OptionData>();
            
            // TODO : ���� ����Ʈ ������ �޾ƿ���
            playerList.Add(new TMP_Dropdown.OptionData("������"));
            playerList.Add(new TMP_Dropdown.OptionData("�ɱ�ȣ"));
        
            // ������ ������ optionList�� _dropdown�� �ɼ� ���� �߰�
            Dropdown.AddOptions(playerList);
        
            // ���� _dropdown ���� ù ��° ������ ����
            Dropdown.RefreshShownValue();
            Dropdown.value = 0;
            
            // �̸� ����
            SettingPlayerName();
        }

        // ���� ��Ӵٿ��� ������ �̸� ����
        public void SettingPlayerName()
        {
            playerName.text = Dropdown.options[Dropdown.value].text;
        }
    }
}
