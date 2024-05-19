using System;
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

        // �� UI ����
        public void SetPlayerToolUI(Batter_Stats player)
        {
            // ��
            int pow = player.getpower();
            powerSlider.value = pow;
            
            // �ӵ�
            int speed = player.getspeed();
            speedSlider.value = speed;
            
            // ���
            int def = player.getdefense();
            speedSlider.value = def;
            
            // ����
            int con = player.getcontact();
            speedSlider.value = con;
            
            // ������
            int eye = player.geteye();
            speedSlider.value = eye;
            
            // ����
            int intel = player.getintelligence();
            speedSlider.value = intel;
        }

        // 
        public void SetPlayerSkillUI(Batter_Stats player)
        {
            
        }
    }
}
