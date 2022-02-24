using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public GameObject SplashObj;               //�ǳڿ�����Ʈ

    public Image image;                            //�ǳ� �̹���

    private bool checkbool = false;     //���� ���� ���� ����


    void Update()

    {

        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ���� ����

        if (checkbool)
        {
            Destroy(SplashObj);                        //�ǳ� �ı�, ����
        }

    }



    IEnumerator MainSplash()

    {

        Color color = image.color;                            //color �� �ǳ� �̹��� ����



        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����

        {

            color.a -= Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01



            image.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����



            if (image.color.a <= 0)                        //���� �ǳ� �̹��� ���� ���� 0���� ������

            {

                checkbool = true;                              //checkbool �� 

            }

        }


        yield return null;                                        //�ڷ�ƾ ����

    }
}
