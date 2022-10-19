using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _rotationAxis;

    private Vector3 _defaultAngles;

    private void Start()
    {
        _defaultAngles = transform.localRotation.eulerAngles;
        Rotate();
    }

    private void Rotate()
    {
        StartCoroutine(Rotating());
    }

    private IEnumerator Rotating()
    {
        float angle = 0;
        float angleCurve = 360;

        while (true)
        {
            angle += _speed * Time.deltaTime;

            if (angle > angleCurve)
            {
                angle -= angleCurve;
            }

            var newRotation = (Quaternion.AngleAxis(angle, _rotationAxis)).eulerAngles;
            newRotation.x += _defaultAngles.x;
            newRotation.y += _defaultAngles.y;
            newRotation.z += _defaultAngles.z;

            transform.localRotation = Quaternion.Euler(newRotation);

            yield return null;
        }
    }
}
