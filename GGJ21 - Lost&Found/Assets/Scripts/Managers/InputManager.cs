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
    [SerializeField]
    private Transform rightTarget;
    [SerializeField]
    private Transform leftTarget;

    private Transform target;
    private bool moveToTarget;

    private float startTime;
    public float minSwipeTime;
    public float minSwipeDist;
    private float swipeThreshold = .7f;
    private Vector2 startPos;
    private Vector3 avatarOrigin;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charManager = FindObjectOfType<CharacterManager>();
        avatarOrigin = avatar.position;
    }

    private void Update()
    {
        TouchInput();

        if (moveToTarget)
        {
            float distToTarget = Vector3.Distance(avatar.position, target.position);
            if (distToTarget > 0.1)
                Vector3.Lerp(avatar.position, target.position, Time.deltaTime);
            else
            {
                moveToTarget = false;
                avatar.position = avatarOrigin;
            }
        }
    }

    private void TouchInput()
    {
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
                    avatar.position = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 swipeDir = (touch.position - startPos).normalized;
                    float swipeDist = (touch.position - startPos).magnitude;
                    float swipeTime = Time.time - startTime;

                    if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
                    {
                        //Checks right-swipe
                        if (swipeDir.x > swipeThreshold)
                            Accept();
                        //Checks left-swipe
                        else if (swipeDir.x < -swipeThreshold)
                            Deny();
                        //Checks up-swipe
                        else if (swipeDir.y > swipeThreshold)
                            ShowItem();
                        //Checks down-swipe
                        else if (swipeDir.y < -swipeThreshold)
                            break;

                    }
                    else
                        print("no swipe 5 u");
                    break;

            }
        }
    }

    public void Accept()
    {
        target = rightTarget;
        moveToTarget = true;

        if (GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        charManager.ChangeCharacter();
    }

    public void Deny()
    {
        target = leftTarget;
        moveToTarget = true;

        if (!GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        charManager.ChangeCharacter();
    }

    private void ShowItem()
    {
        print("Showing " + charManager.inStockItem.name);
    }
}
