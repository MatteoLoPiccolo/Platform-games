using UnityEngine;

public class CoinBox : MonoBehaviour, ITakeShellHits
{
    [SerializeField]
    private SpriteRenderer enablesprite;
    [SerializeField]
    private SpriteRenderer disablesprite;
    [SerializeField]
    private int totalCoins = 1;

    private Animator animator;
    private int remainingCoins;

    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        if (remainingCoins > 0)
        {
            TakeCoin();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        remainingCoins = totalCoins;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (remainingCoins > 0 &&
            collision.WasHitByPlayer() &&
            collision.WasBottom())

        {
            TakeCoin();
        }
    }

    private void TakeCoin()
    {
        GameManager.Instance.AddCoin();
        remainingCoins--;
        animator.SetTrigger("FlipCoin");

        if (remainingCoins <= 0)
        {
            enablesprite.enabled = false;
            disablesprite.enabled = true;
        }
    }
}
