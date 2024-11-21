//Putri Kiara Salsabila Arief (2306250743)
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake() {
        animator.gameObject.SetActive(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName) {       
        animator.gameObject.SetActive(true);

        //animator.SetTrigger("Start");
        //animator.ResetTrigger("Start");
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) {
            yield return null;
        }

        Player.Instance.transform.position = new Vector3(0, 0, 0);

        animator.SetTrigger("EndTrasition");
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}