using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private GameObject _player;

    [SerializeField]
    private float _timeOfDayLength;

    [SerializeField, Range(0f, 30f)]
    private float _transitionDayChanged;

    public TimeOfDay CurrentTimeOfDay;

    public bool DayNightSys = true;

    public GameObject CoinPrefab;

    public GameObject CineMachineCam;


    public delegate void TimeOfDayDelegate(float transitionDuration, TimeOfDay timeOfDay);
    public event TimeOfDayDelegate OnTimeOfDaysChanged;

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

        if (DayNightSys)
            InitTimeOfDay();
    }

    private void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            ChangeTimeOfDay();
        }
    }

    public void ChangeTimeOfDay() 
    {
        DayNightSys = !DayNightSys;
        if (DayNightSys)
            InitTimeOfDay();
        else
            StopTimeofDay();
    }

    private void StopTimeofDay()
    {

        StopAllCoroutines();
        if (CurrentTimeOfDay != TimeOfDay.Day)
        {
            CurrentTimeOfDay = TimeOfDay.Day;
            OnTimeOfDaysChanged?.Invoke(_transitionDayChanged, CurrentTimeOfDay);

        }
    }

    private void InitTimeOfDay()
    {
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


    public enum TimeOfDay
    {
        Day,
        Night
    }
}
