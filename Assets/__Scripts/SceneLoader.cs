using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float _transitionTime;
    
    private Animator _animator;

    private void wake()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Load(int index)
    {
        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(index);
    }

    public void LoadByIndex(int index)
    {
        StartCoroutine(Load(index));
    }

    public void Restart()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadFirst()
    {
        StartCoroutine(Load(0));
    }

    public void LoadNext()
    {
        StartCoroutine(Load(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
