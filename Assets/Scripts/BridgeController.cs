using UnityEngine;
using System.Collections;

public class BridgeController : Triggerable
{

    public float RotateAngle = 45f;
    public float RotateRate = .1f;
    public Utility.Directions RotateDirection;

    private bool _retracting = false;
    private float _distanceRetracted = 0;

    void Update()
    {
        if (_retracting && _distanceRetracted < RotateAngle)
        {
            _distanceRetracted += RotateRate;
            Quaternion rot = transform.rotation;
            switch (RotateDirection)
            {
                case Utility.Directions.left:
                    rot.z += RotateRate;
                    break;
                case Utility.Directions.right:
                    rot.z -= RotateRate;
                    break;
            }
            transform.rotation = rot;
        }
    }

    public override void Trigger()
    {
        _retracting = true;
    }

}
