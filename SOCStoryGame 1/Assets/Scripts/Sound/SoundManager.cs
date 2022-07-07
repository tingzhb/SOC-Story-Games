using FMOD.Studio;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private FMODUnity.EventReference[] levelMusic;

    private EventInstance levelMuiscInstance;

    public void PlayMusic(int levelNumber){
        levelMuiscInstance = FMODUnity.RuntimeManager.CreateInstance(levelMusic[levelNumber]);
        levelMuiscInstance.start();
    }

    public void StopMusic(){
        levelMuiscInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayErrorSound(){
        Debug.Log("Error Sound");
    }
}