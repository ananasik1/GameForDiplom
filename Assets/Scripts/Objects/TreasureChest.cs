using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item content;
    public bool isOpen;
    public SignalSender raiseItem;
    public GameObject dialogWindow;
    public Text dialogText;
    private Animator animator;
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.E) && dialogActive )
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogWindow.SetActive(true);
        dialogText.text = content.itemDiscription;
        playerInventory.AddItem(content);
        playerInventory.currentItem = content;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        animator.SetBool("Opened", true);

    }

    public void ChestAlreadyOpen()
    {
        
            dialogWindow.SetActive(false);
            raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActive = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActive = false;
        }
    }
}
