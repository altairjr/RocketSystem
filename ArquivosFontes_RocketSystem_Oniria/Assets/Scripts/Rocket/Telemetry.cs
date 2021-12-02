using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Telemetry : MonoBehaviour
{
    [Header("Telemetry")]
    public Vector3 distance_;

    [Header("UI Alt")]
    [SerializeField] private TMP_Text altText_;

    private float alt_;

    private Rigidbody rigidbody_;

    private void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Distance();
        AltUI();
    }

    private void AltUI()
    {
        altText_.text = alt_.ToString("0000");
    }

    private void Distance()
    {
        distance_ = rigidbody_.velocity;
        if(distance_.y > 0)
        {
            alt_ += Time.deltaTime;
        }
    }
}
