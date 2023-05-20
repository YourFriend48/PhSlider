using System.Collections;
using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    [SerializeField] private float _intervalOfUpdate;

    private Player _player;
    private Coroutine _rotating;
    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_intervalOfUpdate);
    }

    public void Init(Player player)
    {
        _player = player;
    }

    public void StartRotating()
    {
        _rotating = StartCoroutine(Rotating());
    }

    public void StopRotating()
    {
        if (_rotating != null)
        {
            StopCoroutine(_rotating);
        }
    }

    private IEnumerator Rotating()
    {
        while (true)
        {
            float offsetX = (_player.transform.position.x - transform.position.x);
            float offsetZ = (_player.transform.position.z - transform.position.z);


            Vector3 direction;

            if (Mathf.Abs(offsetX) > Mathf.Abs(offsetZ))
            {
                direction = new Vector3((int)offsetX, 0, 0);
            }
            else
            {
                direction = new Vector3(0, 0, (int)offsetZ);
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            yield return _waiting;
        }
    }
}
