using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    // Représente la duréede vie du projectile
    public float    lifespan = 10f;

    // Représente la vitesse du projectile
    public Vector2  speed = new Vector2(10f, 0f);

    // Cette fonction se lance à chaque frame
    void            FixedUpdate()
    {
        Vector3     relativeSpeed;                                                  // Représente la vitesse du projectile relativement au temps écoulé depuis la dernière frame

        relativeSpeed = speed * Time.deltaTime;                                     // Assigne la valeur de relativeSpeeds
        transform.position = new Vector3(transform.position.x + relativeSpeed.x,    // Modifie la position du projectile
                                         transform.position.y + relativeSpeed.y);
        if (lifespan <= 0)                                                          // Vérifie si l'objet doit diparaitre
            Destroy(gameObject);                                                    // Détruit l'objet
        else
            lifespan -= Time.deltaTime;                                             // Met à jour la durée de vie du projectile
    }
}
