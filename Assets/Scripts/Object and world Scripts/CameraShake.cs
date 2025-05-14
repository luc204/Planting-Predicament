using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float shakeTimer;
    public float shakeAmount = 0.1f; // How strong the shake is
    public float shakeDuration = 0.2f; // How long the shake lasts

    // You can add the amplitude and frequency of the shake if you want more control
    private CinemachineBasicMultiChannelPerlin perlin;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // For shake control
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Shake the camera using perlin noise
            perlin.m_AmplitudeGain = shakeAmount;
            perlin.m_FrequencyGain = 2f; // You can adjust this value for stronger/sharper shakes

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the shake values once shake is done
            perlin.m_AmplitudeGain = 0;
            perlin.m_FrequencyGain = 0;
        }
    }

    public void TriggerShake(float duration, float amount)
    {
        // Reset the shake timer before applying the new values
        shakeTimer = duration;
        shakeAmount = amount;
    }
}

