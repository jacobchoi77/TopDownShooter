using System;
using UnityEngine;

public class Weapon : MonoBehaviour{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;
    private Animator cameraAnim;

    private void Start(){
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    private void Update(){
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0)){
            if (Time.time >= shotTime){
                Instantiate(projectile, shotPoint.position, transform.rotation);
                cameraAnim.SetTrigger("shake");
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}