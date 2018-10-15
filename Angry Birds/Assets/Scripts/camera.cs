using UnityEngine;

public class camera : MonoBehaviour {
    public Transform farLeft;
    public Transform farRight;
    public Transform asteroid;
    public Bird bird;
	void Update () {
        if (bird.released)
        {   
            Vector3 newPosition = transform.position;
            newPosition.x = asteroid.position.x;
            newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
            transform.position = newPosition;
        }
    }
}
