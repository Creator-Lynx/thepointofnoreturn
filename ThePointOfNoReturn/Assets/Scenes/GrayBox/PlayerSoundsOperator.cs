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
    [Space(10)]
    [SerializeField] AnimationCurve WalkVolumeCurve;
    [SerializeField] float SecondToExtremeVolume = 1f;


    [Space(30)]



    bool isWalking = false;

    public void SetWalkingSound(bool isOn)
    {
        isWalking = isOn;
    }

    float currentCurveTime = 0f;
    void CurretCurveMoving()
    {
        walkSource.volume = WalkVolumeCurve.Evaluate(currentCurveTime) * MaxWalkVolume;

        if(isWalking)
        {
            if(currentCurveTime < 1f)
            {
                currentCurveTime += Time.fixedDeltaTime / SecondToExtremeVolume;
            }
            else currentCurveTime = 1f;
        }
        else
        {
            if(currentCurveTime > 0f)
            {
                currentCurveTime -= Time.fixedDeltaTime / SecondToExtremeVolume;
            }
            else currentCurveTime = 0f;
        }
    }

    void FixedUpdate()
    {
        CurretCurveMoving();
    }

}
