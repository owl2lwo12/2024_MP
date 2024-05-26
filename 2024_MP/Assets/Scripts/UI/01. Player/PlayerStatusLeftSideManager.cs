using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI._01._Player;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        public Slider playerInjurySlider;
        
        [Header("���� ����Ʈ")]
        public List<GameObject> playerList = new List<GameObject>(); // ���� ����Ʈ
        private List<Batter_Stats> _batterStatsList = new List<Batter_Stats>();
        public List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>(); // ��Ӵٿ� ����Ʈ
        public Team_Scripts myTeam;
        
        void Start()
        {
            Init();
        }

        private void Awake()
        {
            // Singleton
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
            InitPlayerList();
            
            // �̸� ����
            OnChangedPlayer();
        }

        // ���� ����Ʈ �ʱ�ȭ
        public void InitPlayerList()
        {
            // TODO : ���� ����Ʈ �޾ƿ���
            playerList = myTeam.Batterrplayerlist;
            
            foreach (var gameObject in playerList)
            {
                _batterStatsList.Add(gameObject.GetComponent<Batter_Stats>());
            }
            
            if (playerList.Count > 0)
            {
                foreach (var player in _batterStatsList)
                {
                    optionData.Add(new TMP_Dropdown.OptionData(player.getname()));
                }
            }
            else if(playerList == null || playerList.Count <= 0)
            {
                Debug.Log("���� ����Ʈ�� �������� �ʽ��ϴ�.");
            }
            
            // ������ ������ optionList�� _dropdown�� �ɼ� ���� �߰�
            dropdown.AddOptions(optionData);
        
            // ���� _dropdown ���� ù ��° ������ ����
            dropdown.RefreshShownValue();
            dropdown.value = 0;
        }

        // Dropdown���� ������ �÷��̾ ������� �� �����ų �Լ�
        public void OnChangedPlayer()
        {
            GameObject go = playerList[dropdown.value];
            Batter_Stats player = go.GetComponent<Batter_Stats>();
            
            // ���� ���õ� �÷��̾� �̸����� ����
            string newPlayerName = dropdown.options[dropdown.value].text;
            playerName.text = newPlayerName;

            // �⺻ ���� UI ����
            SetPlayerBasicStatsUI(player);
            
            // ���� ����
            // �� ����
            PlayerStatusCenterSideManager.Instance.SetPlayerToolUI(player);
            // ��ų ����
            PlayerStatusCenterSideManager.Instance.SetPlayerSkillUI(player);
        }

        // �⺻ ���� UI ����
        void SetPlayerBasicStatsUI(Batter_Stats player)
        {
            // ����/Ű/������ ����
            playerAge.text = player.getage().ToString() + "��";
            playerHeight.text = player.getheight().ToString() + "cm";
            playerWeight.text = player.getweight().ToString() +"kg";
            
            // �λ� ���� ����
            injury injury = player.getinjurytype();
            switch (injury)
            {
                case injury.fine:
                    playerInjurySlider.value = 0f;
                    break;
                case injury.fatigue:
                    playerInjurySlider.value = 1.1f;
                    break;
                case injury.hurt:
                    playerInjurySlider.value = 2.1f;
                    break;
                case injury.serious:
                    playerInjurySlider.value = 3.1f;
                    break;
                case injury.critical:
                    playerInjurySlider.value = 4.1f;
                    break;
                case injury.dead:
                    playerInjurySlider.value = 5.0f;
                    break;
            }
        }
    }
}
