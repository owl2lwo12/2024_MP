using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI._01._Player
{
    public class PlayerStatusCenterSideManager : MonoBehaviour
    {
        public static PlayerStatusCenterSideManager Instance;

        [Header("��")] 
        public Slider powerSlider;
        public Slider speedSlider;
        public Slider defSlider;
        public Slider contactSlider;
        public Slider eyeSlider;
        public Slider intelligenceSlider;
        public TMP_Text powerText;
        public TMP_Text speedText;
        public TMP_Text defText;
        public TMP_Text contactText;
        public TMP_Text eyeText;
        public TMP_Text intelligenceText;

        [Header("��ų")] 
        public Slider zoneSlider;

        private int _minStatus = 20;
        private int _maxStatus = 100;
    
        
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

        private void Start()
        {
            Init();
        }

        // �ʱ�ȭ
        void Init()
        {
            // �� ���� �ʱ�ȭ
            InitSlider(powerSlider);
            InitSlider(speedSlider);
            InitSlider(defSlider);
            InitSlider(contactSlider);
            InitSlider(eyeSlider);
            InitSlider(intelligenceSlider);
        }

        // �����̴� �ִ밪 �ּҰ� ����
        public void InitSlider(Slider slider)
        {
            slider.minValue = _minStatus;
            slider.maxValue = _maxStatus;
        }

        /// <summary>
        /// �÷��̾��� Tool UI�� �������ִ� �Լ�
        /// </summary>
        /// <param name="player">�ش� �÷��̾� �Ķ����</param>
        public void SetPlayerToolUI(Batter_Stats player)
        {
            // ��
            int pow = player.getpower();
            powerSlider.value = pow;
            powerText.text = $"{pow}/{_maxStatus}";
            
            // �ӵ�
            int speed = player.getspeed();
            speedSlider.value = speed;
            speedText.text = $"{speed}/{_maxStatus}";
            
            // ���
            int def = player.getdefense();
            defSlider.value = def;
            defText.text = $"{def}/{_maxStatus}";
            
            // ����
            int con = player.getcontact();
            contactSlider.value = con;
            contactText.text = $"{con}/{_maxStatus}";
            
            // ������
            int eye = player.geteye();
            eyeSlider.value = eye;
            eyeText.text = $"{eye}/{_maxStatus}";
            
            // ����
            int intel = player.getintelligence();
            intelligenceSlider.value = intel;
            intelligenceText.text = $"{intel}/{_maxStatus}";
        }

        /// <summary>
        /// �÷��̾��� Skill UI�� �������ִ� �Լ�
        /// </summary>
        /// <param name="player">�ش� �÷��̾� �Ķ����</param>
        public void SetPlayerSkillUI(Batter_Stats player)
        {
            
        }
    }
}
