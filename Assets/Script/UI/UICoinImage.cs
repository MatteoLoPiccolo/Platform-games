using UnityEngine;

public class UICoinImage : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.OnCoinsChange += Pulse;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChange -= Pulse;
    }

    private void Pulse(int coins)
    {
        animator.SetTrigger("Pulse");
    }
}
