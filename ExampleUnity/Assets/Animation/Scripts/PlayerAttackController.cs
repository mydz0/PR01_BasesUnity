using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] KeyCode attackKey = KeyCode.L;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float hitDelay = 0.15f;
    [SerializeField] Animator playerAnimator;

    Transform attackOrigin;

    void Start()
    {
        if (playerAnimator == null && transform.parent != null)
        {
            playerAnimator = transform.parent.GetComponent<Animator>();
        }
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

        // Usa la posicion del padre (Player) como origen del ataque,
        // en vez de la posicion del propio PlayerAttack (que esta offset).
        attackOrigin = transform.parent != null ? transform.parent : transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (playerAnimator != null) playerAnimator.SetTrigger("attack");
        StartCoroutine(DestroyCoinsDelayed());
    }

    IEnumerator DestroyCoinsDelayed()
    {
        yield return new WaitForSeconds(hitDelay);
        int destroyed = CoinManager.Instance.DestroyCoinsInRange(attackOrigin.position, attackRange);
        if (destroyed > 0)
        {
            GameManager.Instance.HitCoin(destroyed);
        }
    }
}
