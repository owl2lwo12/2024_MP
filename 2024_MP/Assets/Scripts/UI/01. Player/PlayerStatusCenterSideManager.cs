using UnityEngine;
using UnityEngine.UI;

namespace UI._01._Player
{
    public class PlayerStatusCenterSideManager : MonoBehaviour
    {
        public static PlayerStatusCenterSideManager Instance;

        [Header("Åø")] 
        public Slider strengthSlider;

        [Header("½ºÅ³")] 
        public Slider zoneSlider;
    
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

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
