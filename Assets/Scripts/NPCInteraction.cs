using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    private bool alreadyShown = false;
    public GameObject promptUI;
    public string textContext;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& (alreadyShown==false)) 
        {
            promptUI?.SetActive(true);
            alreadyShown = true;
        }
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E)&&(alreadyShown==true))
        {
            Debug.Log($"You have found {textContext}");
            MySceneOptions.endingOption = GameEndings.FoundCharacter;
            MySceneOptions.characterName = textContext;
            SceneManager.LoadScene(1);
        }               
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && (alreadyShown == true))
        {
            promptUI?.SetActive(false);
            alreadyShown= false;
        }
    }
    
}
