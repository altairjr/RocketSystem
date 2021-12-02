using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    public float windX_ = 0f;
    public float windZ_ = 0f;

    private float time_;

    private void Start()
    {
        windX_ = Random.Range(0.1f, 0.4f);
        windZ_ = Random.Range(0.1f, 0.4f);
    }

    private void Update()
    {
        RandomWindVector();
    }

    private void RandomWindVector()
    {
        time_ += Time.deltaTime;
        if(time_ >= 300)
        {
            time_ = 0;
            windX_ = Random.Range(0.1f, 0.4f);
            windZ_ = Random.Range(0.1f, 0.4f);
        }
    }
}
