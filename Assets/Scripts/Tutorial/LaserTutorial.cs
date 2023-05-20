using UnityEngine;

public class LaserTutorial : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] private Trigger _deadendTrigger;
    [SerializeField] private Trigger _passClearTrigger;

    [Header("Arrows")]
    [SerializeField] private TutorialArrow _deadendArrow;
    [SerializeField] private TutorialArrow _arrowOfClearPass;

    [SerializeField] private TutorialCross _tutorialCross;

    private void OnEnable()
    {
        _deadendTrigger.Enter += OnDeadEndEnter;
        _passClearTrigger.Enter += OnPassClearEnter;
        _deadendArrow.Enable();
        _tutorialCross.Enable();
    }

    private void OnDisable()
    {
        _deadendTrigger.Enter -= OnDeadEndEnter;
        _passClearTrigger.Enter -= OnPassClearEnter;
    }

    private void OnDeadEndEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _deadendTrigger.Enter -= OnDeadEndEnter;
            _deadendArrow.gameObject.SetActive(false);
            _tutorialCross.gameObject.SetActive(false);
            _arrowOfClearPass.gameObject.SetActive(true);
            _arrowOfClearPass.Enable();
        }
    }

    private void OnPassClearEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _passClearTrigger.Enter -= OnPassClearEnter;
            _arrowOfClearPass.gameObject.SetActive(false);
        }
    }
}
