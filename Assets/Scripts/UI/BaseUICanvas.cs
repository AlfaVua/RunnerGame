using System.Collections;
using UnityEngine;

public class BaseUICanvas : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float hideAnimationTime = 0;

    public void Show()
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        if (!animator) return;
        animator.Play("Show");
    }

    public void Hide()
    {
        if (!gameObject.activeSelf) return;
        if (!animator)
        {
            gameObject.SetActive(false);
            return;
        }
        animator.Play("Hide");
        StartCoroutine(nameof(DisableAfterAnimationComplete));
    }

    private IEnumerator DisableAfterAnimationComplete()
    {
        yield return new WaitForSeconds(hideAnimationTime);
        gameObject.SetActive(false);
    }
}