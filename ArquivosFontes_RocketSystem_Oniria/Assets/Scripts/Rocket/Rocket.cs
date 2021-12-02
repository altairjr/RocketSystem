using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Stages Rockets")]
    public List<GameObject> stages_ = new List<GameObject>();

    [Header("Thrust and Fuel")]
    [SerializeField] private float thrust_;

    [Header("Components")]
    [SerializeField] private Rigidbody rigidbody_;

    [SerializeField] private CountTime countTime_;

    [SerializeField] private Telemetry telemetry_;

    [SerializeField] private WindForce windForce_;

    // Launch
    public static float launch = 0f;

    private bool burn_;

    // FUEL
    private float fuel_;

    // Momentum
    private float momentum_ = 0f;
    private float friction_ = 0f;
    private float frictionTime_ = 0f;

    private Vector3 velocity_;

    // Parachutes
    private float drag_ = 1f;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject _stages = transform.GetChild(i).gameObject;
            stages_.Add(_stages);
        }
    }

    private void FixedUpdate()
    {
        Burn();
        Momentum();
    }

    private void Update()
    {
        Stages();
        SetStatusRocket();
    }

    private void Burn()
    {
        if (fuel_ > 0 && !countTime_.scrub_ && countTime_.launch_)
            burn_ = true;
        else
            burn_ = false;
    }

    private void Momentum()
    {
        if (burn_)
        {
            momentum_ = thrust_;
            launch = 1;
            ForceRocket(velocity_);
            AngleRocket(-45);
        }
        else
        {
            if(launch > 0)
            {
                frictionTime_ += Time.deltaTime;
                if (frictionTime_ >= 1f)
                {
                    frictionTime_ = 0;
                    friction_ = 0.2f;
                    momentum_ -= friction_;
                    if (telemetry_.distance_.y <= -1)
                    {
                        friction_ = 0.8f;
                        momentum_ -= friction_;
                    }
                }
            }

            if (momentum_ <= 0)
                momentum_ = 0;

            velocity_ = new Vector3(((momentum_ / 8) - windForce_.windX_), momentum_, windForce_.windZ_);
            ForceRocket(velocity_);
            AngleRocket(0);
        }
    }

    private GameObject Stages()
    {
        var currentStage = stages_[0];
        return (currentStage);
    }

    private void SetStatusRocket()
    {
        switch (Stages().gameObject.name)
        {
            case "boo":
                Refuel();
                if (burn_)
                    Booster();
                if (fuel_ <= 0)
                    Decoupler();
                break;

            case "chute":
                if (!burn_ && telemetry_.distance_.y <= -1f)
                    Parachute();
                break;
        }   
    }

    private void Decoupler()
    {
        var _booster = Stages().GetComponent<Boosters>();
        _booster.disabled_ = true;
        stages_.Remove(Stages());
    }

    private void Refuel()
    {
        var _booster = Stages().GetComponent<Boosters>();
        fuel_ = _booster.fuel_;
        thrust_ = _booster.thrust_;
    }

    private void Booster()
    {
        var _booster = Stages().GetComponent<Boosters>();
        fuel_ = _booster.Burn();
        velocity_ = new Vector3(((momentum_ / 8) - windForce_.windX_), momentum_, windForce_.windZ_);
    }

    private void Parachute()
    {
        var _parachute = Stages().GetComponent<Parachutes>();
        drag_ = _parachute.Drag();
        rigidbody_.drag = drag_;
    }

    private void ForceRocket(Vector3 velocity)
    {
        rigidbody_.AddForce(velocity * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void AngleRocket(float angleZ)
    {
        rigidbody_.MoveRotation(Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0, 0, angleZ), 0.15f * Time.deltaTime));
    }
}
