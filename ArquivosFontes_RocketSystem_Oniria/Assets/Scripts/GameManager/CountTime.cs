using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountTime : MonoBehaviour
{
    [Header("Components the UI")]
    [SerializeField] private TMP_Text secondsText_;
    [SerializeField] private TMP_Text minutesText_;
    [SerializeField] private TMP_Text hoursText_;

    private float seconds_ = 0f;
    private float minutes_ = 0f;
    private float hours_ = 0f;

    public bool launch_ = false;
    public bool scrub_ = false;

    private void Start()
    {
        seconds_ = -3;
    }

    private void Update()
    {
        UItime();
        if (MainMenu.startGame_)
        {
            curTime();
            CheckTimeLiftoff();
        }
    }

    private void UItime()
    {
        secondsText_.text = seconds_.ToString("00");
        minutesText_.text = minutes_.ToString("00");
        hoursText_.text = hours_.ToString("00");
    }

    private void curTime()
    {
        seconds_ += Time.deltaTime;
        if (seconds_ >= 59)
        {
            seconds_ = 0;
            minutes_++;
        }

        if (minutes_ >= 59)
        {
            minutes_ = 0;
            hours_++;
        }

        if (hours_ >= 24)
        {
            hours_ = 0;
        }
    }

    private void CheckTimeLiftoff()
    {
        if (seconds_ >= 0 && !scrub_)
            launch_ = true;
        else
            launch_ = false;
    }
}
