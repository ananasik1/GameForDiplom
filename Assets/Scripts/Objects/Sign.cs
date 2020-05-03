using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{

    public GameObject dialogBoox;
    public Text dialogText;
    public string dialog;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogActive)
        {
            if (dialogBoox.activeInHierarchy)
            {
                dialogBoox.SetActive(false);
            }
            else
            {
                dialogBoox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private  void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            dialogActive = false;
            dialogBoox.SetActive(false);
        }
    }
}
