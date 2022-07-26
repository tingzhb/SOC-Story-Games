using FMOD.Studio;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private FMODUnity.EventReference[] levelMusic;
    [SerializeField] private FMODUnity.EventReference ambientMusic;
    private EventInstance levelMuiscInstance, ambientMusicInstance;
    
    public void PlayAmbientMusic(){
        ambientMusicInstance = FMODUnity.RuntimeManager.CreateInstance(ambientMusic);
        ambientMusicInstance.start();
    }

    public void PlayMusic(int levelNumber){
        levelMuiscInstance = FMODUnity.RuntimeManager.CreateInstance(levelMusic[levelNumber]);
        levelMuiscInstance.start();
    }

    public void StopMusic(){
        levelMuiscInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayTapSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/General/Click");
    }

    public void PlayDragSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Candle");
    }

    public void PlayErrorSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/General/Defeat");
    }
    public void PlaySuccessSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/General/Victory");
    }
    public void PlayBubblePopSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/BubbleBust");
    }
    public void PlayPlopSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Plopp");
    }
    public void PlayStirSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/FryingEgg/Stir");
    }
    public void PlayEatingSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Eating");
    }
}