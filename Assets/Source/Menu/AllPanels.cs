using UnityEngine;
using UnityEngine.SceneManagement;

public class AllPanels : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("GameMain");
    }
}
