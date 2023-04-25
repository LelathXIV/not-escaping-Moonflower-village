using UnityEngine;

public class TotemBehaviour : MonoBehaviour
{
    public int nextSceneNumber;
    public GameObject totemGenerator;

    private void Awake()
    {
        nextSceneNumber = SaveGameManager.CurrentSaveData.currentlyPlayableScene;
        totemGenerator = GameObject.Find("Spawners"); //spawnr OBJ
    }

    private void Start()
    {
        nextSceneNumber = SaveGameManager.CurrentSaveData.currentlyPlayableScene;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            totemGenerator.GetComponent<FadeInFadeOut>().StartFadeIn();
            GetComponent<SceneSwitcher>().nextSceneNumber = nextSceneNumber;
            SaveGameManager.CurrentSaveData.isInAstral = false;
            SaveGameManager.CurrentSaveData.currentPlayerHP = SaveGameManager.CurrentSaveData.MAXplayerHP;
            SaveGameManager.SaveGame();
            GetComponent<SceneSwitcher>().LoadNextScene();
        }
    }
}
