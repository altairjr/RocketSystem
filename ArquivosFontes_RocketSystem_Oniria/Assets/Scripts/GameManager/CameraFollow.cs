using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Set objetc follow")]
    [SerializeField] private GameObject pivot_;

    [Header("Set position")]
    [SerializeField] private Vector3 offSet_;
    [SerializeField] private Quaternion angle_;

    private void LateUpdate()
    {
        Config(offSet_, angle_);
    }

    private void Config(Vector3 offSet, Quaternion angle)
    {
        Vector3 _pos = pivot_.transform.position;
        transform.position = pivot_.transform.position + offSet;
        transform.rotation = angle;
    }
}
