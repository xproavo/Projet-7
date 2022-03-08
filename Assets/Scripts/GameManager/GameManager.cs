using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        InitTimeOfDay();
    }

    private GameObject _player;

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


    
    private void InitTimeOfDay()
    {
        CurrentTimeOfDay = TimeOfDay.Day;
        StartCoroutine(UpdateTimeOfDayCoroutine());
    }

    private IEnumerator UpdateTimeOfDayCoroutine()
    {
        while (_player != null  && !_player.GetComponent<StateManager>().Death)
        {
            yield return new WaitForSeconds(_timeOfDayLength);
            if (CurrentTimeOfDay == TimeOfDay.Day)
                CurrentTimeOfDay = TimeOfDay.Night;
            else
                CurrentTimeOfDay = TimeOfDay.Day;
            OnTimeOfDaysChanged?.Invoke(_transitionDayChanged, CurrentTimeOfDay);
        }
    }
}
