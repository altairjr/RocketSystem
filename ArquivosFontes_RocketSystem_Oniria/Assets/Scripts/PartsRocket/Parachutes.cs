using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachutes : MonoBehaviour
{
    [Header("Info")]
    public string type_;
    public string name_;
    public string desciption_;

    [Header("Layer")]
    [SerializeField] private LayerMask layerTerrain_;

    [Header("Set Raycast")]
    [SerializeField] private Transform endPos_;

    public float drag_;

    private bool groundNear_;
    private bool open_;

    private float friction_;

    private MeshRenderer renderer_;
    private Rocket rocket_;

    private void Start()
    {
        SetConfig();
    }

    private void Update()
    {
        CollisionRay();
        CuteChute();
    }

    private void SetConfig()
    {
        rocket_ = GameObject.FindGameObjectWithTag("Rocket").GetComponent<Rocket>();
        renderer_ = GetComponent<MeshRenderer>();
        gameObject.name = type_;

        drag_ = 1f;
        friction_ = 0f;
    }

    public float Drag()
    {
        if (!groundNear_ && Rocket.launch > 0)
            open_ = true;

        if(open_)
        {
            renderer_.enabled = true;
            friction_ += Time.deltaTime;
            if (friction_ >= 1f)
            {
                friction_ = 0f;
                drag_ += 1f;
            }

            if (drag_ >= 10f)
                drag_ = 10f;
        }

        return (drag_);
    }

    private void CuteChute()
    {
        if (groundNear_ && Rocket.launch > 0)
        {
            drag_ = 1f;
            rocket_.GetComponent<Rigidbody>().drag = drag_;
            renderer_.enabled = false;
            open_ = false;
            MainMenu.startGame_ = false;
        }
    }

    private void CollisionRay()
    {
       groundNear_ = Physics.Linecast(transform.position, endPos_.transform.position, layerTerrain_);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, endPos_.transform.position);
    }
}
