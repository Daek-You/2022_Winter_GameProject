using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform followingTarget;
    public float moveSpeed;
    public Vector2 canMoveAreaCenter;       // ī�޶� �̵� ���ɿ��� �߽�
    public Vector2 canMoveAreaSize;         // ī�޶� �̵� ���ɿ��� ũ��

    private float cameraHalfWidth;          // ī�޶��� ���� ���������� ���� ����
    private float cameraHalfHeight;         // ī�޶��� ���� ���������� ���� ����
    
    public Vector2 canMoveAreaCenter2;
    public Vector2 canMoveAreaSize2; 
    public Vector2 canMoveAreaCenter3;
    public Vector2 canMoveAreaSize3; 
    public Vector2 canMoveAreaCenter4;
    public Vector2 canMoveAreaSize4;

    void Awake()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Screen.width * cameraHalfHeight / Screen.height;

    }

    void LateUpdate()                   /* ī�޶� �̵��� LateUpdate()���� ó�� */
    {
        if (followingTarget.position.y > canMoveAreaCenter.y - canMoveAreaSize.y * 0.5 && followingTarget.position.y < canMoveAreaCenter.y + canMoveAreaSize.y * 0.5)
            FollowTarget(canMoveAreaCenter);
        else if (followingTarget.position.y > canMoveAreaCenter2.y - canMoveAreaSize2.y * 0.5 && followingTarget.position.y < canMoveAreaCenter2.y + canMoveAreaSize2.y * 0.5)
            FollowTarget(canMoveAreaCenter2);
        else if (followingTarget.position.y > canMoveAreaCenter3.y - canMoveAreaSize3.y * 0.5 && followingTarget.position.y < canMoveAreaCenter3.y + canMoveAreaSize3.y * 0.5)
            FollowTarget(canMoveAreaCenter3);
        else if (followingTarget.position.y > canMoveAreaCenter4.y - canMoveAreaSize4.y * 0.5 && followingTarget.position.y < canMoveAreaCenter4.y + canMoveAreaSize4.y * 0.5)
            FollowTarget(canMoveAreaCenter4);
    }

    private void OnDrawGizmos()         /* ī�޶� Ÿ�ٿ� ���� �̵��� ������ ������ ǥ�� */
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(canMoveAreaCenter, canMoveAreaSize);
        Gizmos.DrawWireCube(canMoveAreaCenter2, canMoveAreaSize2);
        Gizmos.DrawWireCube(canMoveAreaCenter3, canMoveAreaSize3);
        Gizmos.DrawWireCube(canMoveAreaCenter4, canMoveAreaSize4);
    }

    private void FollowTarget(Vector2 canMoveAreaCenter)
    {
        Vector3 targetPosition = new Vector3(followingTarget.position.x,
                                             this.transform.position.y,
                                             this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position,
                                               targetPosition,
                                               moveSpeed * Time.deltaTime);

        float restrictionAreaX = canMoveAreaSize.x * 0.5f - cameraHalfWidth;
        float clampX = Mathf.Clamp(this.transform.position.x, -restrictionAreaX + canMoveAreaCenter.x, restrictionAreaX + canMoveAreaCenter.x);

        float restrictionAreaY = canMoveAreaSize.y * 0.5f - cameraHalfHeight;
        float clampY = Mathf.Clamp(this.transform.position.y, -restrictionAreaY + canMoveAreaCenter.y, restrictionAreaY + canMoveAreaCenter.y);


        this.transform.position = new Vector3(clampX, clampY, this.transform.position.z);
    }
}
