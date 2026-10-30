using UnityEngine;                                     

[RequireComponent(typeof(Rigidbody2D))]                
public class PlayerController : MonoBehaviour          
{
    [SerializeField] float moveSpeed = 7f;             // Velocidad horizontal del Player en unidades/segundo
    [SerializeField] float jumpForce = 8f;             // Fuerza vertical al saltar 
    [SerializeField] int maxJumps = 2;                 // Cuantos saltos antes de tocar suelo (2 = doble salto)
    [SerializeField] float fallMultiplier = 2.5f;      // Multiplicador de gravedad cuando esta cayendo, hace sentir el salto mas snappy
    [SerializeField] float lowJumpMultiplier = 2f;     // Multiplicador cuando sueltas W pronto, salto corto si no mantienes la tecla

    Rigidbody2D rb;                                    
    Animator animator;                                 
    PlayerJumpController jumpCtrl;                     
    float horizontal;                                 
    bool jumpPressed;                                 
    bool jumpHeld;                                     
    int jumpsRemaining;                                

    void Start()                                       
    {
        rb = GetComponent<Rigidbody2D>();              
        animator = GetComponent<Animator>();           
        jumpCtrl = GetComponentInChildren<PlayerJumpController>();                        
        jumpsRemaining = maxJumps;                     // Al empezar tiene todos los saltos disponibles
    }

    void Update()                                      
    {
        horizontal = Input.GetAxis("Horizontal");      // Lee el eje horizontal (-1 izq, 0 nada, 1 der). A/D o flechas por defecto.

        if (Input.GetKeyDown(KeyCode.W)) jumpPressed = true;                              
        jumpHeld = Input.GetKey(KeyCode.W);            

        if (horizontal > 0.01f)                        
        {
            Vector3 s = transform.localScale;          
            s.x = Mathf.Abs(s.x);                      
            transform.localScale = s;                 
        }
        else if (horizontal < -0.01f)                  
        {
            Vector3 s = transform.localScale;
            s.x = -Mathf.Abs(s.x);                     
            transform.localScale = s;
        }

        animator.SetBool("run", Mathf.Abs(horizontal) > 0.01f);                           
    }

    void FixedUpdate()                                 
    {
        Vector2 vel = rb.linearVelocity;               
        vel.x = horizontal * moveSpeed;                
        rb.linearVelocity = vel;                       

        if (jumpCtrl.canJump)                          
        {
            jumpsRemaining = maxJumps;                
        }

        if (jumpPressed && jumpsRemaining > 0)         
        {
            vel = rb.linearVelocity;                   
            vel.y = jumpForce;                         
            rb.linearVelocity = vel;                   
            animator.SetBool("jump", true);            
            jumpsRemaining--;                         
        }
        jumpPressed = false;                           

        if (rb.linearVelocity.y < 0)                   
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            // Anade gravedad extra, cae mas rapido = sensacion snappy en vez de flotante
        }
        else if (rb.linearVelocity.y > 0 && !jumpHeld)                                      
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
}
