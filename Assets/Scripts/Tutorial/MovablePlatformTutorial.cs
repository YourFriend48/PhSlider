using UnityEngine;

public class MovablePlatformTutorial : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] private Trigger _deadendTrigger;
    [SerializeField] private Trigger _chooseTrigger;
    [SerializeField] private Trigger _passClearTrigger;

    [Header("Arrows")]
    [SerializeField] private TutorialArrow _deadendArrow;
    [SerializeField] private TutorialArrow _arrowOfClearPass;

    [SerializeField] private TutorialCross _tutorialCross;

    private void OnEnable()
    {
        _deadendTrigger.Enter += OnDeadendEnter;
        _chooseTrigger.Enter += OnChooseZoneEnter;
        _passClearTrigger.Enter += OnPassClearEnter;
    }

    private void OnDisable()
    {
        _deadendTrigger.Enter -= OnDeadendEnter;
        _chooseTrigger.Enter -= OnChooseZoneEnter;
        _passClearTrigger.Enter -= OnPassClearEnter;
    }

    private void OnDeadendEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _deadendTrigger.Enter -= OnDeadendEnter;
            _tutorialCross.gameObject.SetActive(false);
            _deadendArrow.gameObject.SetActive(false);
            _arrowOfClearPass.gameObject.SetActive(true);
        }
    }

    private void OnChooseZoneEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _chooseTrigger.Enter -= OnChooseZoneEnter;
            _tutorialCross.gameObject.SetActive(true);
            _deadendArrow.gameObject.SetActive(true);
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
