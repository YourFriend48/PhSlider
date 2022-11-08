using UnityEngine;
using TMPro;

namespace General
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelView : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            int level = LevelLoader.Instance.Level + 1;
            _text.text = "LEVEL " + level.ToString();
        }
    }
}
