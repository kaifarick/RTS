using UnityEngine.SceneManagement;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private int health = 1000;
    public int Health
    {
        get { return health; }
        set
        {
            if (value <= 0) SceneManager.LoadScene("SampleScene");
            health = value;
        }
    }
}
