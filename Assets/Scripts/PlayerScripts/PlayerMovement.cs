using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    walk,
    attack,
    interact,
    stagger,
    idle
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerStates currentState;
    public float speed;
    //delete new
    private new Rigidbody2D rigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public SignalSender PlayerHealthSignal;
    public VectorValue startigPosition;
    public Inventory playerInventory;
    public SpriteRenderer reciveSpriteItem;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerStates.walk;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = startigPosition.initialValue;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerStates.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
     

        if (Input.GetButtonDown("attack") && currentState != PlayerStates.attack && currentState!=PlayerStates.stagger) 
        {
            StartCoroutine(attackCO());
        }
        else if (currentState == PlayerStates.walk || currentState == PlayerStates.idle)
        {
            UpdateAnimation();
        }
        
    }

    private IEnumerator attackCO()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerStates.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerStates.interact)
        {
            currentState = PlayerStates.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerStates.interact)
            {
                animator.SetBool("ReceiveItem", true);
                currentState = PlayerStates.interact;
                reciveSpriteItem.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("ReceiveItem", false);
                currentState = PlayerStates.idle;
                reciveSpriteItem.sprite = null;
                playerInventory.currentItem = null;

            }
        }
       
      
    }

    void UpdateAnimation()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        rigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }

    public void Knock(float KnockTime,float damage)
    {
        currentHealth.RunTimeValue -= damage;
        PlayerHealthSignal.Raise();

        if (currentHealth.RunTimeValue>0)
        {
            StartCoroutine(KnockCo(KnockTime));

        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        if (rigidbody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            rigidbody.velocity = Vector2.zero;
            rigidbody.GetComponent<PlayerMovement>().currentState = PlayerStates.idle;
            rigidbody.velocity = Vector2.zero;


        }
    }
}
