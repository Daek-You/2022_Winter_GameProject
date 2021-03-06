using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [SerializeField] private SpriteRenderer hidingSprite;
    [SerializeField] private bool isDynamicHiddenSpace;        // 상호작용 요청한 시점의 플레이어 위치에 숨을 경우
    [SerializeField] private ObjectType objectType;
    public GameObject Key;
    //public AudioClip[] audioClips;
    private SujiController suji;
    private string objectTag;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        objectTag = this.gameObject.tag;
    }


    public void Interaction()
    {
        switch ((int)objectType)
        {
            case (int)ObjectType.eSimpleEventObject:
                _audioSource.Play();
                DialogController.Instance.ShowDialog(objectTag);

                break;

            case (int)ObjectType.eCanHideObject:
                if(hidingSprite != null)
                {
                    _audioSource.Play();
                    suji.Hide(hidingSprite, isDynamicHiddenSpace);
                }

                break;

            case (int)ObjectType.eItemObject:
                if (!GameDataManager.Instance.IsKeyOn(objectTag))                 
                {
                    _audioSource.Play();
                    GameDataManager.Instance.GetKey(objectTag);                   
                    DialogController.Instance.ShowDialog(objectTag);              
                    Key.SetActive(true);
                }
                else
                {
                    _audioSource.Play();
                    DialogController.Instance.ShowDialog(objectTag + "Already");
                }
                break;


            case (int)ObjectType.eDoorObject:
                AbstractGate gate = GetComponentInChildren<AbstractGate>();

                if (gate != null)
                    gate.OnEvent(objectTag);
                break;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji = collision.gameObject.GetComponent<SujiController>();

            if (suji != null)
            {
                suji.InteractiveObject = this;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji.InteractiveObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (suji != null)
            {
                suji.InteractiveObject = null;
            }
        }
    }
}
