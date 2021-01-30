using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Animator anim;
    private GameManager gameManager;
    private CharacterManager charManager;

    [SerializeField]
    private Transform avatar;

    private float startTime;
    public float minSwipeTime;
    public float minSwipeDist;
    public float moveSensitivity;
    private float swipeThreshold = .7f;
    private Vector2 startPos;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charManager = FindObjectOfType<CharacterManager>();
    }

    private void Update()
    {
        TouchInput();
    }

    private void TouchInput()
    {
#if UNITY_ANDROID
        print("android");
        if (Input.touchCount > 0)
        {
            print("Started swipey");
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTime = Time.time;
                    startPos = touch.position;
                    print("Began timer");
                    break;

                case TouchPhase.Moved:
                    Vector3.Lerp(avatar.position, touch.position, Time.deltaTime * moveSensitivity);
                    break;

                case TouchPhase.Ended:
                    Vector2 swipeDir = (touch.position - startPos).normalized;
                    float swipeDist = (touch.position - startPos).magnitude;
                    float swipeTime = Time.time - startTime;

                    if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
                    {
                        //Checks right-swipe
                        if (swipeDir.x > swipeThreshold) Accept();
                        //Checks left-swipe
                        else if (swipeDir.x < -swipeThreshold) Deny();
                        //Checks up-swipe
                        else if (swipeDir.y > swipeThreshold) ShowItem();
                        //Checks down-swipe
                        else if (swipeDir.y < -swipeThreshold) HideItem();
                    }
                    break;
            }
        }
#endif
#if UNITY_EDITOR_WIN
        print("editor");
        if (Input.GetMouseButton(0))
        {
            print("moving towards mouse");
            Vector3.Lerp(avatar.position, Input.mousePosition, Time.deltaTime * moveSensitivity);

            if (Input.GetMouseButtonDown(0))
            {
                startTime = Time.time;
                startPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            print("released mouse");
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 swipeDir = (mousePos - startPos).normalized;
            float swipeDist = (mousePos - startPos).magnitude;
            float swipeTime = Time.time - startTime;

            if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
            {
                print("swiiiper");
                //Checks right-swipe
                if (swipeDir.x > swipeThreshold) Accept();
                //Checks left-swipe
                else if (swipeDir.x < -swipeThreshold) Deny();
                //Checks up-swipe
                else if (swipeDir.y > swipeThreshold) ShowItem();
                //Checks down-swipe
                else if (swipeDir.y < -swipeThreshold) HideItem();
            }
            print("no swipe");
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

    public void ShowItem()
    {
        if(!anim.GetBool("isShowing"))
            anim.SetBool("isShowing", true);
    }
    public void HideItem()
    {
        if (anim.GetBool("isShowing"))
            anim.SetBool("isShowing", false);
    }
}
