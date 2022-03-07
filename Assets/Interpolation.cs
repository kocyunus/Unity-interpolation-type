using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public interpType it = interpType.Lineer;
    public enum interpType 
    {
    EaseIn,
    EaseOut,
    SmoothStep,
    SmoothStep2,
    easeOutQuit,
    Lineer

    }
    [SerializeField] float moveTime = 0.5f;
    bool isMoving = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(new Vector3(5, 0, 0), 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(new Vector3(-5, 0, 0), 0.5f);

        }
    }
    void Move(Vector3 destination, float moveTime)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRoutine(destination, moveTime));
        }
    }
    IEnumerator MoveRoutine(Vector3 destination, float moveTime)
    {
        Vector3 startPosition = transform.position;
        bool reachedDestination = false;

        float elapsedTime = 0f;
        isMoving = true;
        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;
                transform.position = destination;
                break;
            }
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / moveTime, 0f, 1f);
   
            switch (it)
            {
                case interpType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI / 2);
                    break;
                case interpType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI / 2);
                    break;
                case interpType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    break;
                case interpType.SmoothStep2:
                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;
                case interpType.easeOutQuit:
                    t = 1 - Mathf.Pow(1 - t, 0.5f);
                    break;
                case interpType.Lineer:
                    break;
                default:
                    break;
            }

            transform.position = Vector3.Lerp(startPosition, destination, t);
            yield return null;
        }
        isMoving = false;
    }
}
