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
        public GameObject matchBtn;
        
        [Header("�� ������")]
        public List<GameObject> teamIcons = new List<GameObject>();

        [Header("��� ����")] 
        public Image leftImage;
        public Image rightImage;
        public TMP_Text leftScore;
        public TMP_Text rightScore;
        public TMP_Text winOrLose;

        [Header("������Ʈ ����")] 
        public GameObject loadingPanel;
        public GameObject overBtn;
        public GameObject teamGroup;
        public GameObject scoreGroup;
        
        [SerializeField]
        private int[] _curMatch = new int[10];
        
        
        void Start()
        {
            // �ʱ� ����
            Init();
        }

        private void Update()
        {
            _curMatch = MainSystem._instance.currmatch;
            
            
            // ������ �缳��
            SetMatchIcons();
            
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
            loadingPanel.SetActive(true);
            overBtn.SetActive(false);
            teamGroup.SetActive(false);
            scoreGroup.SetActive(false);
            
            Invoke("OnLoadMatchResult", 2f);
            
            // UIManager�� OnStartMatch ����
            UIManager.Instance.OnStartMatch();
            
        }

        public void OnLoadMatchResult()
        {
            loadingPanel.SetActive(false);
            overBtn.SetActive(true);
            teamGroup.SetActive(true);
            scoreGroup.SetActive(true);
        }

        // ��ġ �гο��� '��� ����' ��ư Ŭ�� ��
        public void OnMatchOver()
        {
            // ��ġ ��� �г� ��Ȱ��ȭ
            playingPanel.SetActive(false);
            
            // ��ġ �ѹ� ����
            MainSystem._instance.IncreaseMatchNum();

            if (UIManager.Instance.curMatch == UIManager.Instance.maxMatch) // ���� ��ġ�� 144��
            {
                matchBtn.SetActive(false);
                Debug.Log("���� ����");
            }
            else
            {
                UIManager.Instance.curMatch++;
            }
            
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
