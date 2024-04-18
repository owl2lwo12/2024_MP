using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TabMenuManager : MonoBehaviour
    {
        [SerializeField] private List<Button> _btns = new List<Button>();
        public Color onClickedColor;
        public Color notClickedColor;
        
        
        // Start is called before the first frame update
        void Start()
        {
            SetBtn();
        }

        void SetBtn()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform btn = transform.GetChild(i);
                _btns.Add(btn.GetComponent<Button>());
                int index = i;
                btn.GetComponent<Button>().onClick.AddListener(() => OnLoadPanel(index));
            }
        }

        public void OnLoadPanel(int level)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == level)
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(true);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = onClickedColor;
                }
                else
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(false);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = notClickedColor;
                }
            }
        }
    }
}
