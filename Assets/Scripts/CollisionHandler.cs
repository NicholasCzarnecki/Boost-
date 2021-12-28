using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishLevelSound;
    [SerializeField] float secondsBetweenScenes = 2f;
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is freindly");
                break;
            case "Finish":
                FinishLevel();
                break;
            default:
                CrashSequence();
                break;
        }

    }

    void FinishLevel()
    {
        GetComponent<Movement>().enabled = false;  // Disables rocket movement after crashing
        GetComponent<AudioSource>().PlayOneShot(finishLevelSound);
        Invoke("LoadNextLevel", secondsBetweenScenes);
    }
    void CrashSequence()
    {
        GetComponent<Movement>().enabled = false;  // Disables rocket movement after crashing
        GetComponent<AudioSource>().PlayOneShot(crashSound);
        Invoke("ReloadLevel", secondsBetweenScenes);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);  // Reloads the current scene that is being played
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)  //Checks to see if player is on final level.  If true, it will start the game at the first scene
        {
            nextSceneIndex = 0;
        } 
        SceneManager.LoadScene(nextSceneIndex);
    }
}
