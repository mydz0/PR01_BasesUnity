using UnityEngine;                                     

public class PlayerJumpController : MonoBehaviour      
{
    internal bool canJump = true;                      
    Animator animator;                                 

    void Start()                                       
    {
        animator = transform.parent.GetComponent<Animator>();                              
    }

    void OnTriggerEnter2D(Collider2D collision)        
    {
        canJump = true;                                // El Player esta tocando algo (el suelo) -> puede saltar de nuevo
        animator.SetBool("jump", false);               // Desactiva la animacion de salto -> vuelve a Idle/Run
    }

    void OnTriggerExit2D(Collider2D collision)         
    {
        canJump = false;                               
        animator.SetBool("jump", true);                // Activa la animacion de salto (esta en el aire)
    }
}
