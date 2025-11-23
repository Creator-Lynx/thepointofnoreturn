using System.Collections;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerSoundsOperator : MonoBehaviour
{
    [SerializeField] AudioSource walkSource;
    [SerializeField] AudioSource jumpStart;
    [SerializeField] AudioSource jumpEnd;
    [SerializeField] AudioSource flying;
    
    [Space(30)]
    [Header("Walk SFX settings")]
    [SerializeField] float MaxWalkVolume = 1f;
    [SerializeField] float curvesTimeStep = 0.02f;
    [SerializeField] float TimeoutToStopWalking = 0.06f;
    [Space(10)]
    [SerializeField] AnimationCurve StartWalkVolumeCurve;
    [SerializeField] float StartWalkVolumeTime = 0.1f;
    [Space(10)]
    [SerializeField] AnimationCurve EndWalkVolumeCurve;
    [SerializeField] float EndWalkVolumeTime = 0.1f;

    [Space(30)]



    bool isWalking = false;

    bool onStopWalkingCorutine = false;
    public void SetWalkingSound(bool isOn)
    {
        if (isOn) 
        {
            if(isWalking) 
            {
                onStopWalkingCorutine = false;
                StopCoroutine(WalkSoundStopDelay());
                return;
            }
            walkSource.Play();
            StopCoroutine(EndOfWalkSound());
           
            walkSource.volume = MaxWalkVolume;
            isWalking = true;
        }
        else 
        {
            if(!isWalking) return;
            if(!onStopWalkingCorutine)
            {
                onStopWalkingCorutine = true;
                StartCoroutine(WalkSoundStopDelay());
            }
            else
            {
                return;
            }
        }
    }

    IEnumerator EndOfWalkSound()
    {
        float time = 0;
        while (time < EndWalkVolumeTime)
        {
            walkSource.volume = EndWalkVolumeCurve.Evaluate(time / EndWalkVolumeTime) * MaxWalkVolume;
            time += curvesTimeStep;
            yield return new WaitForSeconds(curvesTimeStep);
        }
        yield return new WaitForEndOfFrame();
        walkSource.Pause();
    }
    IEnumerator WalkSoundStopDelay()
    {
        yield return new WaitForSeconds(TimeoutToStopWalking);

        isWalking = false;
        StartCoroutine(EndOfWalkSound());
    }
}
