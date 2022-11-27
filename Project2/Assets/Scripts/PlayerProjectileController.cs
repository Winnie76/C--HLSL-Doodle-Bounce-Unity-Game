using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    public Vector3 velocity;
    private float timer;
    public float secondsUntilDestroy = 2f;
    public int projectileForce = 1000;

    private void Start()
    {
        Vector2 mouseScreenPos = Input.mousePosition;

        // To fire a projectile towards the mouse position, we need to be able
        // to convert a 2D screen position into a world space position. In
        // order to do this we first have to figure out how far from the camera
        // the game world plane is. Since it's played in the X-Y plane (Z = 0)
        // we can simply take the camera's z-position to be this distance.
        float distanceFromCameraToXYPlane = this.transform.position.z - Camera.main.transform.position.z;

        // Next we can use the camera method ScreenToWorldPoint(). Note that this
        // method expects a Vector3 (not a Vector2), where x and y are the
        // screen pixel coordinates, and z is the world distance from the camera 
        // to project to.
        Vector3 screenPosWithZDistance = (Vector3)mouseScreenPos + (Vector3.forward * distanceFromCameraToXYPlane);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPosWithZDistance);
        
        // Take the opposite and adjacent side
        float O = mouseWorldPosition.x - this.transform.position.x;
        float A = mouseWorldPosition.y - this.transform.position.y;

        // Imaginary cone above player where the projectiles can be fired
        Vector3 cone;

        // Depends on which quandrant the mouse is in
        if (O >= 0 && A >= 0) {
            if (Mathf.Asin(O/A) <= Mathf.PI/3.0f) {
                cone = new Vector3(O, A, 0.0f);
            }
            else {
                cone = new Vector3(Mathf.Sqrt(3), 2.0f, 0.0f);
            }
        }
        else if (O < 0 && A >= 0) {
                if (Mathf.Asin(O/A) <= Mathf.PI/3.0f) {
                cone = new Vector3(O, A, 0.0f);
            }
            else {
                cone = new Vector3(-Mathf.Sqrt(3), 2.0f, 0.0f);
            }
        }
        else if (O >= 0 && A < 0) {
            cone = new Vector3(Mathf.Sqrt(3), 2.0f, 0.0f);
        }
        else if (O < 0 && A < 0) {
            cone = new Vector3(-Mathf.Sqrt(3), 2.0f, 0.0f);
        }
        else {
            cone = new Vector3(0f, 1f, 0f);
        }
        this.transform.position = this.transform.position + cone.normalized * 1.5f;
        this.GetComponent<Rigidbody>().AddForce(cone.normalized * projectileForce);

        // Destroy this projectile after a set amount of time
        Destroy(this.gameObject, secondsUntilDestroy);
    }

    private void OnTriggerEnter(Collider other){
        Destroy(this.gameObject);
        Destroy(other.gameObject);
    }
}
