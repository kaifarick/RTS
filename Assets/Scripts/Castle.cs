using UnityEngine.SceneManagement;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int health = 1000;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RestartGames()
    {
        if (health <= 0) SceneManager.LoadScene("SampleScene");
    }

}
