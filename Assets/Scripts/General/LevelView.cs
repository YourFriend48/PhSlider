using UnityEngine;
using TMPro;

namespace General
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Lean.Localization.LeanToken _token;

        private void Start()
        {
            int level = LevelLoader.Instance.Level + 1;
            _token.Value = level.ToString();
        }
    }
}
