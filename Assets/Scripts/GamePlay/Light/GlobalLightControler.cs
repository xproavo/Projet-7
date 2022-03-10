using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class GlobalLightControler : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Light2D _globalLight;

    [SerializeField]
    private Color _dayColor = Color.white;

    [SerializeField]
    private Color _nightColor = Color.black;

    [SerializeField]
    private AnimationCurve _fromDayToNightLerp;

    [SerializeField]
    private AnimationCurve _fromNightToDayLerp;

    private void Start()
    {
        _gameManager.OnTimeOfDaysChanged += UpdateLighting;
    }

    private void UpdateLighting(float transitionDuration, GameManager.TimeOfDay timeOfDay)
    {
        StartCoroutine(UpdateLightingCoroutine(transitionDuration, timeOfDay));

    } 

    private IEnumerator UpdateLightingCoroutine(float transitionDuration, GameManager.TimeOfDay timeOfDay)
    {
        AnimationCurve curve;
        if (timeOfDay == GameManager.TimeOfDay.Day)
            curve = _fromNightToDayLerp;
        else
            curve = _fromDayToNightLerp;
        float counter = 0f;
        while (counter < transitionDuration)
        {
            float transition = curve.Evaluate(counter / transitionDuration); 
            _globalLight.color = Color.Lerp(_dayColor, _nightColor, transition);

            counter += Time.deltaTime;


            yield return null;
        }


    }

}
