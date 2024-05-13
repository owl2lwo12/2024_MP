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

        // ��ư ����Ʈ�� �� �޴� ��ư���� �߰�
        void SetBtn()
        {
            // ��ư ������Ʈ���� ����Ʈ�� �߰�
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform btn = transform.GetChild(i);
                _btns.Add(btn.GetComponent<Button>());
                int index = i;
                btn.GetComponent<Button>().onClick.AddListener(() => OnLoadPanel(index));
            }
            
            // ó�� ���� ��, ù ��° �г� Ȱ��ȭ
            OnLoadPanel(transform.childCount - 1);
        }

        // ������ ��ư�� �ش��ϴ� �г��� Ȱ��ȭ. ������ �гε��� ��Ȱ��ȭ
        public void OnLoadPanel(int level)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == level) // ������ ��ư�� �ش��ϴ� �г� Ȱ��ȭ
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(true);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = onColor;
                }
                else // �� �ܿ� �гε��� ��Ȱ��ȭ
                {
                    _btns[i].transform.GetChild(0).gameObject.SetActive(false);
                    TextMeshProUGUI btnText = _btns[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = offColor;
                }
            }
        }
    }
}
