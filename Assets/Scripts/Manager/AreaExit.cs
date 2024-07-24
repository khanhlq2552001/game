using UnityEngine;
using static UnityEngine.Rendering.DebugManager;
using UnityEngine.SceneManagement;
using System.Collections;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            StartCoroutine(SceneManagement.Instance.FadeToLoadScene(sceneToLoad));
        }
    }


}
