using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI._04._Setting
{
    public class SettingPanel : MonoBehaviour
    {
        [Header("슬라이더")] 
        public Slider sfxSlider;
        public Slider musicSlider;
        
        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 어플리케이션 종료
#endif
        }
        
    }
}
