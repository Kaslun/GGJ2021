using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterManager charManager;

    public Animator cardAnim;

    public Transform avatar;

    private float startTime;
    public float minSwipeTime;
    public float minSwipeDist;
    public float moveSpeed;
    public float moveTimer;
    private float swipeThreshold = .7f;
    public float distanceTreshold;
    private Vector2 startPos;
    private Vector2 swipeDir;
    public Transform origin;

    private bool isSwiping = false;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        charManager = FindObjectOfType<CharacterManager>();
    }

    private void Update()
    {
        if (cardAnim == null) cardAnim = avatar.gameObject.GetComponent<Animator>();

        if(!isSwiping)
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
                    avatar.position = Vector2.MoveTowards(avatar.position, touch.position, moveSpeed);              
                    break;

                case TouchPhase.Ended:;
                    cardAnim.SetBool("isMoving", false);
                    Vector2 swipeDir = (touch.position - startPos).normalized;
                    float swipeDist = (touch.position - startPos).magnitude;
                    float swipeTime = Time.time - startTime;

                    if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
                    {
                        //Checks right-swipe
                        if (swipeDir.x > swipeThreshold)
                        {
                            isSwiping = true;
                            StartCoroutine(Accept());
                            StartCoroutine(MoveCard(true));
                        }
                        //Checks left-swipe
                        else if (swipeDir.x < -swipeThreshold)
                        {
                            isSwiping = true;
                            StartCoroutine(Deny());
                            StartCoroutine(MoveCard(false));
                        }
                    }
                    break;
            }
        }

        else if (Input.touchCount <= 0)
        {
            avatar.position = Vector2.MoveTowards(avatar.position, origin.position, moveSpeed);
        }
#endif
#if UNITY_EDITOR_WIN
        if (Input.GetMouseButton(0))
        {
            avatar.position = Vector2.MoveTowards(avatar.position, Input.mousePosition, moveSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                cardAnim.SetBool("isMoving", true);
                startTime = Time.time;
                startPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            cardAnim.SetBool("isMoving", false);

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            swipeDir = (mousePos - startPos).normalized;
            float swipeDist = (mousePos - startPos).magnitude;
            float swipeTime = Time.time - startTime;

            if (swipeDist > minSwipeDist && swipeTime > minSwipeTime)
            {
                //Checks right-swipe
                if (swipeDir.x > swipeThreshold)
                {
                    isSwiping = true;
                    StartCoroutine(Accept());
                    StartCoroutine(MoveCard(true));
                }
                //Checks left-swipe
                else if (swipeDir.x < -swipeThreshold)
                {
                    isSwiping = true;
                    StartCoroutine(Deny());
                    StartCoroutine(MoveCard(false));
                }
            }
        }
        if(!Input.GetMouseButton(0))
        {
            avatar.position = Vector2.MoveTowards(avatar.position, origin.position, moveSpeed);
        }
#endif
    }

    public IEnumerator MoveCard(bool isAccept)
    {
        float timer = 0;


        while(timer < moveTimer)
        {
            if (isAccept)
                avatar.position = new Vector3(avatar.position.x + Vector2.right.x, avatar.position.y + Vector2.right.y);
            else
                avatar.position = new Vector3(avatar.position.x + Vector2.left.x, avatar.position.y + Vector2.left.y);
            yield return new WaitForSeconds(.1f);
            timer = timer + .1f;
        }
    }

    private IEnumerator Accept()
    {
        yield return new WaitForSeconds(moveTimer);
        if (GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        EventManager.TriggerEvent("Next");

        isSwiping = false;
    }

    private IEnumerator Deny()
    {
        yield return new WaitForSeconds(moveTimer);
        if (!GameManager.SameItem(charManager.charItem, charManager.inStockItem))
            gameManager.score++;
        else
            gameManager.score--;

        EventManager.TriggerEvent("Next");

        isSwiping = false;
    }
}
