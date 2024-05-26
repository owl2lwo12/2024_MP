using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Season
{
    public class PlayPanelManager : Singleton<PlayPanelManager>
    {
        [Header("UI")]
        public TMP_Text progressText; // ��� �� ǥ�� �ؽ�Ʈ
        public GameObject playingPanel;
        
        [Header("�� ������")]
        public List<GameObject> teamIcons = new List<GameObject>();

        [Header("��� ����")] 
        public Image leftImage;
        public Image rightImage;
        public TMP_Text leftScore;
        public TMP_Text rightScore;
        public TMP_Text winOrLose;
        
        
        private int[] _curMatch = new int[10];
        
        
        void Start()
        {
            // �ʱ� ����
            Init();
        }

        private void Update()
        {
            _curMatch = MainSystem._instance.currmatch;
            
        }

        void Init()
        {
            // ���� ��� �� �ʱ�ȭ
            SetProgressText();
            
            // ��Ī ������ ����
            Invoke("SetMatchIcons", 0.5f);
            
            // ��ġ ��� �г� ��Ȱ��ȭ
            playingPanel.SetActive(false);
        }
        
        // ���� ��� ���� ���� �Լ�
        public void SetProgressText()
        {
            int curMatch = UIManager.Instance.curMatch;
            int maxMatch = UIManager.Instance.maxMatch;
            progressText.text = $"{curMatch} ��� / {maxMatch} ���";
        }

        // ��ġ ������ ���� �Լ�
        public void SetMatchIcons()
        {
            Debug.Log("������ ����");
            
            for(int i = 0; i < _curMatch.Length; i++)
            {
                // i ��° ������ ��
                int numOfTeam = _curMatch[i];
                
                // ���� UI �� �̹��� �ҷ�����
                Image curSprite = teamIcons[i].GetComponent<Image>();
                
                // �ٲ� ��������Ʈ UIManger���� �� ���� �ҷ�����
                Sprite changeSprite = UIManager.Instance.TeamSprites[numOfTeam];
                
                // ��������Ʈ ���� �� ���� ������� ����
                curSprite.sprite = changeSprite;
                curSprite.SetNativeSize();
            }

            leftImage.sprite = teamIcons[0].GetComponent<Image>().sprite;
            leftImage.SetNativeSize();
            rightImage.sprite = teamIcons[1].GetComponent<Image>().sprite;
            rightImage.SetNativeSize();
        }
        
        // ��� ���� �Լ�
        public void PlayMatch()
        {
            // ��ġ ���� ���� �г� ǥ��
            playingPanel.SetActive(true);
            
            // UIManager�� OnStartMatch ����
            UIManager.Instance.OnStartMatch();
            
        }

        // ��ġ �гο��� '��� ����' ��ư Ŭ�� ��
        public void OnMatchOver()
        {
            // ��ġ ��� �г� ��Ȱ��ȭ
            playingPanel.SetActive(false);
            
            // ��ġ �ѹ� ����
            MainSystem._instance.IncreaseMatchNum();
            UIManager.Instance.curMatch++;
            
            // ������ �缳��
            SetMatchIcons();
            
            // �ؽ�Ʈ ����
            SetProgressText();
        }

        public void SettingMatchResultUI(int newleftScore, int newRightScore, string newWinOrLose)
        {
            leftScore.text = $"{newleftScore}";
            rightScore.text = $"{newRightScore}";
            winOrLose.text = $"{newWinOrLose}";
        }
    }
}
