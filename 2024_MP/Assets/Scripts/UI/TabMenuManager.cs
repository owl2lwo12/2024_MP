using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class TabMenuManager : MonoBehaviour
    {
        [SerializeField] private List<Button> _btns = new List<Button>();
        public Color offColor;
        public Color onColor;
        
        
        // Start is called before the first frame update
        void Start()
        {
            SetBtn();
        }

        // 버튼 리스트에 탭 메뉴 버튼들을 추가
        void SetBtn()
        {
            // 버튼 오브젝트들을 리스트에 추가
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform btn = transform.GetChild(i);
                _btns.Add(btn.GetComponent<Button>());
                int index = i;
                btn.GetComponent<Button>().onClick.AddListener(() => OnLoadPanel(index));
            }
            
            // 처음 실행 시, 첫 번째 패널 활성화
            OnLoadPanel(transform.childCount - 1);
        }

        // 선택한 버튼에 해당하는 패널을 활성화. 나머지 패널들은 비활성화
        public void OnLoadPanel(int level)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == level) // 선택한 버튼에 해당하는 패널 활성화
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(true);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = onColor;
                }
                else // 이 외에 패널들은 비활성화
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(false);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = offColor;
                }
            }
        }
    }
}
