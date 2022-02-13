using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveObject : MonoBehaviour
{

    public bool canHideObject;
    private Collider2D _collider;
    private SujiMoveController suji;

    void Start()
    {
        _collider = GetComponent<Collider2D>();    
    }


    public void Interaction()
    {
        if (canHideObject)
        {
            suji.Hide();
            return;
        }


        /// �۾����� �߰�
        /// �ؽ�Ʈ �������� �׷� �͵�
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji = collision.gameObject.GetComponent<SujiMoveController>();

            if (suji != null)
            {
                suji.InteractiveObject = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (suji != null)
            {
                suji.InteractiveObject = null;
                suji = null;
            }
        }
    }
}
