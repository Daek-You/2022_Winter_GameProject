using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform followingTarget;
    public float moveSpeed;
    public string VILLAGE { get; private set; } = "Village";
    public string MAINROON { get; private set; } = "Shaman_MainRoomDoor";
    public string WAREHOUSE { get; private set; } = "Shaman_WarehouseDoor";
    public string BATHROOM { get; private set; } = "Shaman_BathroomDoor";


    private Vector2 canMoveAreaCenter;       // ī�޶� �̵� ���ɿ��� �߽�
    private Vector2 canMoveAreaSize;         // ī�޶� �̵� ���ɿ��� ũ��
    private CameraManager() { }
    private float cameraHalfWidth;          // ī�޶��� ���� ���������� ���� ����
    private float cameraHalfHeight;         // ī�޶��� ���� ���������� ���� ����

    public string tagKeyName
    {
        set
        {
            
            if (!_tagKeyName.Equals(value))
            {
                _tagKeyName = value;
                OnChangeCameraVector(_tagKeyName);
            }
        }
    }
    private string _tagKeyName = "";
    private Dictionary<string, (Vector2, Vector2)> dictionary = new Dictionary<string, (Vector2, Vector2)>();


    void Awake()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Screen.width * cameraHalfHeight / Screen.height;
    }


    void Start()
    {
        dictionary.Clear();        // -106.9f
        dictionary.Add(VILLAGE, (new Vector2(-65f, 18.8f), new Vector2(400f, 48f)));
        dictionary.Add(MAINROON, (new Vector2(204.1f, -106.9f), new Vector2(123.5f, 44.1f)));
        dictionary.Add(WAREHOUSE, (new Vector2(342.4f, -284.8f), new Vector2(123.7f, 44.1f)));
        dictionary.Add(BATHROOM, (new Vector2(192.9f, -464.5f), new Vector2(101.2f, 44.1f)));
        OnChangeCameraVector(VILLAGE);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(var i in dictionary.Values)
        {
            Gizmos.DrawWireCube(i.Item1, i.Item2);
        }
    }

    private void OnChangeCameraVector(string tagKeyName)
    {
        canMoveAreaCenter = dictionary[tagKeyName].Item1;
        canMoveAreaSize = dictionary[tagKeyName].Item2;
    }

    void LateUpdate()                   /* ī�޶� �̵��� LateUpdate()���� ó�� */
    {
        FollowTarget();
    }

    private void FollowTarget()
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
