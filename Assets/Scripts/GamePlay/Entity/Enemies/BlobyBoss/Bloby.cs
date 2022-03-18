using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bloby : MonoBehaviour
{
    public Vector2 RandomTimeToAttack;

    public GameObject[] Projectil;

    [Range(0f, 100f)]
    public float RageJauge;

    public bool IsAttack;

    public float TimeNoise = 0.5f;
    public float NoiseAmplitude = 1;

    private Animator _animator;
    private StateManager _stateManager;

    private CinemachineVirtualCamera _vcam;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateManager = GetComponent<StateManager>();
    }


    private void Update()
    {
        if (IsAttack)
        {
            Jump();
            IsAttack = false;
        }
    }

    private void Jump()
    {
        _animator.SetTrigger("Attack");
    }

    public void Noise()
    {
        StartCoroutine(NoiseEnumerator());
    }

    private IEnumerator NoiseEnumerator()
    {
        GameManager.Instance.CineMachineCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = NoiseAmplitude;
        yield return new WaitForSeconds(TimeNoise);
        GameManager.Instance.CineMachineCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
