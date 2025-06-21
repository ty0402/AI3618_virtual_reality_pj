using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    private float _totalRotation = 0f; 
    private Quaternion _previousRotation;
    public bool isFinish;

    void Start()
    {
        _previousRotation = transform.rotation;
    }

    void Update()
    {
        if(Level1Manager.instance.TaskIndex==2)
        {
            Quaternion currentRotation = transform.rotation;

            float angleDelta = Quaternion.Angle(_previousRotation, currentRotation);

            Vector3 axis;
            float angle;
            (currentRotation * Quaternion.Inverse(_previousRotation)).ToAngleAxis(out angle, out axis);
            float directionSign = Mathf.Sign(Vector3.Dot(axis, transform.up)); 


            _totalRotation += angleDelta * directionSign;


            if (Mathf.Abs(_totalRotation) >= 720f)
            {
                _totalRotation = 0f; // ÖØÖÃ¼ÆÊýÆ÷
                isFinish = true;
            }

            _previousRotation = currentRotation;
        }

    }
}