using UnityEngine;

public class PlayerSoundsOperator : MonoBehaviour
{
    [SerializeField] AudioSource stepsSource;
    [SerializeField] AudioSource jumpStart;
    [SerializeField] AudioSource jumpEnd;
    [SerializeField] AudioSource flying;
    
    bool isWalking = false;

    public void SetWalkingSound(bool isOn)
    {
        if (isOn) 
        {
            if(isWalking) return;
            //stepsSource.UnPause()
            stepsSource.Play();
            isWalking = true;
        }
        else 
        {
            if(!isWalking) return;
            stepsSource.Pause();
            isWalking = false;
        }
    }
}
