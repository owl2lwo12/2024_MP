using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UI.Player
{
    public class PlayerStatusLeftSideManager : MonoBehaviour
    {
        public static PlayerStatusLeftSideManager Instance = null;
        
        [Header("Status")]
        public TMP_Dropdown dropdown;
        
        [Header("���� ����")]
        public TMP_Text playerName;
        public TMP_Text playerAge;
        public TMP_Text playerHeight;
        public TMP_Text playerWeight;
        
        
        [Header("���� ����Ʈ")]
        public List<Human> playerList = new List<Human>();
        
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // �ʱ�ȭ �Լ�
        void Init()
        {
            // ���� ����Ʈ �ʱ�ȭ
            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
            
            // TODO : ���� ����Ʈ ������ �޾ƿ���
            optionData.Add(new TMP_Dropdown.OptionData("������"));
            optionData.Add(new TMP_Dropdown.OptionData("�ɱ�ȣ"));
        
            // ������ ������ optionList�� _dropdown�� �ɼ� ���� �߰�
            dropdown.AddOptions(optionData);
        
            // ���� _dropdown ���� ù ��° ������ ����
            dropdown.RefreshShownValue();
            dropdown.value = 0;
            
            // �̸� ����
            OnChangedPlayer();
        }

        // Dropdown���� ������ �÷��̾ ������� �� �����ų �Լ�
        public void OnChangedPlayer()
        {
            // ���� ���õ� �÷��̾� �̸����� ����
            string newPlayerName = dropdown.options[dropdown.value].text;
            playerName.text = newPlayerName;
            
            // TODO : ����/Ű/������ �����ϱ�
            /*
             
             */
        }
    }
}
