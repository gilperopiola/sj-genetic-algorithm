using UnityEngine;

public class Ball : MonoBehaviour
{
    //variables
    private float speed;
    private float acceleration = 0.004f;

    //constants
    private float startingSpeed = 6.0f;

    void Awake()
    {
        speed = startingSpeed;
    }

    void Update()
    {
        if (Globals.isPaused)
        {
            return;
        }

        transform.Rotate(Vector3.forward * speed);
        speed += acceleration;
        speed = Mathf.Round(speed * 100f) / 100f; //2 decimals
    }

    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        speed = startingSpeed;
    }
}
