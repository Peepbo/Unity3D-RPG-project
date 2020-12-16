using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystic : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField] private RectTransform _Background;
    [SerializeField] private RectTransform _Joystick;
  
    private float radius;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;

    private bool isTouch = false;
    private Vector3 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        radius = _Background.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouch)
        {
            player.transform.position += movePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)_Background.position;

        value = Vector2.ClampMagnitude(value, radius);// 조이스틱 못나가게 가두기

        _Joystick.localPosition = value;

        value = value.normalized;
        movePosition = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, value.y * moveSpeed * Time.deltaTime);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        _Joystick.localPosition = Vector3.zero;   
    }
}
