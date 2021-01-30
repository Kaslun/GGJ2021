using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterManager charManager;

    public Animator cardAnim;

    [SerializeField]
    private Transform avatar;

    private float startTime;
    public float minSwipeTime;
    public float minSwipeDist;
    public float moveSpeed;
    private float swipeThreshold = .7f;
    private Vector2 startPos;
    public Transform origin;

    private bool isSwiping;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charManager = FindObjectOfType<CharacterManager>();
    }

    private void Update()
    {
        TouchInput();

#if UNITY_ANDROID
        if(Input.GetKey(KeyCode.Escape)){
            EventManager.TriggerEvent("GoBack");
        }
#endif
    }

    private void TouchInput()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    cardAnim.SetBool("isMoving", true);
                    startTime = Time.time;
                    startPos = touch.position;

                    break;

                case TouchPhase.Moved:
                    print("moving towards mouse");
                    Vector3.MoveTowards(avatar.position, touch.position, Time.deltaTime * moveSpeed);              
                    break;

                case TouchPhase.Ended:;
                    cardAnim.SetBool("isMoving", false);
                    Vector2 swipeDir = (touch.position - startPos).normalized;
                    float swipeDist = (touch.position - startPos).magnitude;
                    float swipeTime = Time.time - startTime;

                    if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
                    {
                        //Checks right-swipe
                        if (swipeDir.x > swipeThreshold) Accept();
                        //Checks left-swipe
                        else if (swipeDir.x < -swipeThreshold) Deny();
                    }
                    break;
            }
        }
#endif
#if UNITY_EDITOR_WIN
        if (Input.GetMouseButton(0))
        {
            print("moving towards mouse");
            avatar.position = Vector2.MoveTowards(avatar.position, Input.mousePosition, moveSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = false;
                cardAnim.SetBool("isMoving", true);
                startTime = Time.time;
                startPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            cardAnim.SetBool("isMoving", false);

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 swipeDir = (mousePos - startPos).normalized;
            float swipeDist = (mousePos - startPos).magnitude;
            float swipeTime = Time.time - startTime;

            if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
            {
                isSwiping = true;
                //Checks right-swipe
                if (swipeDir.x > swipeThreshold) Accept();
                //Checks left-swipe
                else if (swipeDir.x < -swipeThreshold) Deny();
            }
        }
        if(!Input.GetMouseButton(0) && !isSwiping)
        {
            print("moving back");
            avatar.position = Vector2.MoveTowards(avatar.position, origin.position, moveSpeed);
        }
#endif
    }

    public void Accept()
    {
        if (GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        EventManager.TriggerEvent("Next");
    }

    public void Deny()
    {
        if (!GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        EventManager.TriggerEvent("Next");
    }
}
