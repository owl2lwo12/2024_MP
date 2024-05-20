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

        [Header("툴")] 
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

        [Header("스킬")] 
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

        // 초기화
        void Init()
        {
            // 툴 스탯 초기화
            InitSlider(powerSlider);
            InitSlider(speedSlider);
            InitSlider(defSlider);
            InitSlider(contactSlider);
            InitSlider(eyeSlider);
            InitSlider(intelligenceSlider);
        }

        // 슬라이더 최대값 최소값 설정
        public void InitSlider(Slider slider)
        {
            slider.minValue = _minStatus;
            slider.maxValue = _maxStatus;
        }

        /// <summary>
        /// 플레이어의 Tool UI를 설정해주는 함수
        /// </summary>
        /// <param name="player">해당 플레이어 파라미터</param>
        public void SetPlayerToolUI(Batter_Stats player)
        {
            // 힘
            int pow = player.getpower();
            powerSlider.value = pow;
            powerText.text = $"{pow}/{_maxStatus}";
            
            // 속도
            int speed = player.getspeed();
            speedSlider.value = speed;
            speedText.text = $"{speed}/{_maxStatus}";
            
            // 방어
            int def = player.getdefense();
            defSlider.value = def;
            defText.text = $"{def}/{_maxStatus}";
            
            // 컨택
            int con = player.getcontact();
            contactSlider.value = con;
            contactText.text = $"{con}/{_maxStatus}";
            
            // 선구안
            int eye = player.geteye();
            eyeSlider.value = eye;
            eyeText.text = $"{eye}/{_maxStatus}";
            
            // 지능
            int intel = player.getintelligence();
            intelligenceSlider.value = intel;
            intelligenceText.text = $"{intel}/{_maxStatus}";
        }

        /// <summary>
        /// 플레이어의 Skill UI를 설정해주는 함수
        /// </summary>
        /// <param name="player">해당 플레이어 파라미터</param>
        public void SetPlayerSkillUI(Batter_Stats player)
        {
            
        }
    }
}
