using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance {get; private set;}

    public CinemachineFreeLook cinemachineFreeLook;
    private float time;
    private float total_time;
    private float og_intensity;

    private void Awake()
    {
        Instance = this;
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float shake_intensity, float shake_time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineFreeLook.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shake_intensity;
        og_intensity = shake_intensity;
        time = shake_time;
        total_time = shake_time;
        Debug.Log("start shaking");

    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;


            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                cinemachineFreeLook.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                Mathf.Lerp(og_intensity, 0f, (1 - (time / total_time)));
            //Debug.Log("stop shaking");


        }
    }



}