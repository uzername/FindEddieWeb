using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached Town Exit");
            MySceneOptions.endingOption = GameEndings.LeftGate;
            SceneManager.LoadScene(1);
        }
    }
}
