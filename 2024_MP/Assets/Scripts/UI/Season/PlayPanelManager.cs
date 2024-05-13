using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Season
{
    public class PlayPanelManager : MonoBehaviour
    {
        public TMP_Text progressText; // ��� �� ǥ�� �ؽ�Ʈ

        public List<GameObject> TeamIcons = new List<GameObject>();

        void Start()
        {
            // �ʱ� ����
            Init();
        }

        void Init()
        {
            // ���� ��� �� �ʱ�ȭ
            SetProgressText();
            
            // ��Ī ����
            SetMatchIcons();
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
            for(int i = 0; i < TeamIcons.Count; i++)
            {
                // ���� UI �� �̹��� �ҷ�����
                Image curSprite = TeamIcons[i].GetComponent<Image>();
                
                // �ٲ� ��������Ʈ �ҷ�����
                Sprite changeSprite = UIManager.Instance.TeamSprites[i];
                
                // ��������Ʈ ���� �� ���� ������� ����
                curSprite.sprite = changeSprite;
                curSprite.SetNativeSize();
            }
        }
        
        // ��� ���� �Լ�
        public void PlayMatch()
        {
            Debug.Log("��� ����");
            
            // UIManager�� OnStartMatch ����
            UIManager.Instance.OnStartMatch();
        }
        
        
    }
}
