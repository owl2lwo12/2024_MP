using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI._01._Player;
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
        public List<Batter_Stats> playerList = new List<Batter_Stats>();
        
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
            Batter_Stats player = new Batter_Stats();
            
            // ���� ���õ� �÷��̾� �̸����� ����
            string newPlayerName = dropdown.options[dropdown.value].text;
            playerName.text = newPlayerName;
            
            // ����/Ű/������ �����ϱ�
            playerAge.text = player.getage().ToString();
            playerHeight.text = player.getheight().ToString();
            playerWeight.text = player.getweight().ToString();
             
            
            // ���� ����
            // �� ����
            PlayerStatusCenterSideManager.Instance.SetPlayerToolUI(player);
            // ��ų ����
            PlayerStatusCenterSideManager.Instance.SetPlayerSkillUI(player);
        }
    }
}
