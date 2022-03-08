using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _player;

    public Light GlobalLight;

    public Color DayColor;
    public Color NightColor;

    [SerializeField]
    private AnimationCurve _fromDayToNightLerp;


    [SerializeField]
    private AnimationCurve _fromNightToDayLerp;


    [SerializeField]
    private float _timeOfDayLength;

    [SerializeField, Range(0f, 30f)]
    private float _transitionDayChanged;

    public TimeOfDay CurrentTimeOfDay;

    public enum TimeOfDay
    {
        Day,
        Night
    }

    public delegate void TimeOfDayDelegate(float transitionDuration, TimeOfDay timeOfDay);
    public event TimeOfDayDelegate OnTimeOfDaysChanged;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    
    private void InitTimeOfDay()
    {
        CurrentTimeOfDay = TimeOfDay.Day;

    }

    private IEnumerator UpdateTimeOfDayCoroutine()
    {
        float counter = 0f;

        while (counter < _transitionDayChanged)
        {
            //GlobalLight.color = Color.Lerp(_)
            yield return null;
        }


    }


}
