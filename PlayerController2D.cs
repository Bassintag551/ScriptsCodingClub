using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {
    
    // Représente le projectile crée lorsque le joueur tire
    public Transform    projectile = null;

    // Représente la vitesse du joueur
    public Vector2      speed = new Vector2(10f, 8f);

    // Représente la position minimale/maximale du joueur (relativement à sa position de départ)
    public Vector2      minPos = new Vector2(0, 0);
    public Vector2      maxPos = new Vector2(10, 10);

    // Représente la position de départ des projectiles (relativement à la position de départ du joueur)
    public Vector2      projectileOffset = new Vector2(0.5f, 0);

    // Représente le délay entre chaques tirs
    public float        cooldown = 1f;

    // Représente la valeur minimale du joystick pour que le joueur bouge
    public float inputThreshold = 0.5f;

    // Compte le temps écoulé depuis le dernier tir
    public float        shooterCooldown { private set; get; }
	
    // Cette fonction se lance à la création de l'objet
    void        Start()
    {
        Vector2 pos;                                                // Représente la position du joueur

        pos = transform.position;                                   // Enregister la position du joueur dans la variable
        maxPos = new Vector2(pos.x + maxPos.x, pos.y + maxPos.y);   // Relativise la position maximale
        minPos = new Vector2(pos.x + minPos.x, pos.y + minPos.y);   // Relativise la position minimale
    }

    // Cette fonction se lance à chaque frame
    void    FixedUpdate()
    {
        UpdatePosition();   // Met à jour la position du joueur
        UpdateShooter();    // Met à jour le lanceur de projectiles
    }

    // Gère la génération des projectiles
    void            UpdateShooter()
    {
        Transform   instance;                                                                                                   // Représente une nouvelle instance du projectile

        if (shooterCooldown > 0)                                                                                                // Arrète la fonction si le cooldown n'est pas terminé
        {
            shooterCooldown -= Time.deltaTime;                                                                                  // Décrémente le cooldown par le temps écoulé depuis la dernière frame
            return;
        }
        if (projectile == null || Input.GetAxis("Fire1") < 0.1f)                                                                // Vérifie si le bouton de tir est appuyé
            return;
        instance = Instantiate(projectile);                                                                                     // Instancie le projectile
        instance.position = new Vector3(transform.position.x + projectileOffset.x, transform.position.y + projectileOffset.y);  // Modifie la position du projectile
        shooterCooldown = cooldown;                                                                                             // Réinitialise le cooldown
    }

    // Gère la position
	void        UpdatePosition()
    {
        float   vertical;                                                                   // Représente la valeur du contrôle de l'axe horizontal
        float   horizontal;                                                                 // Représente la valeur du contrôle de l'axe vertical
        float   deltaTime;                                                                  // Représente le temps écoulé depuis la dernière frame par rapport au temps cible
        float   velX;                                                                       // Représente la vélocité en X du joueur
        float   velY;                                                                       // Représente la vélocité en y du joueur
        Vector3 position;                                                                   // Représente la position du joueur

        velX = 0;                                                                           // Début de l'initialisation
        velY = 0;
        deltaTime = Time.deltaTime;
        position = transform.position;
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");                                           // Fin de l'initialisation
        if (horizontal >= inputThreshold || horizontal <= -inputThreshold)                  // Vérifie si le joueur doit se déplacer en X
        {
            velX = speed.x * deltaTime;                                                     // Calcul l'amplitude du déplacement
            if (horizontal < 0)
                velX *= -1;                                                                 // Inverse la direction si nécessaire
        }
        if (vertical >= inputThreshold || vertical <= -inputThreshold)                      // Même principe que précédement mais en Y
        {
            velY = speed.y * deltaTime;
            if (vertical < 0)
                velY *= -1;
        }
        if (velX + position.x > maxPos.x || velX + position.x < minPos.x)                   // Vérifie si la position est valide en X
            velX = 0;
        if (velY + position.y > maxPos.y || velY + position.y < minPos.y)                   // Vérifie si la position est valide en Y
            velY = 0;
        transform.position = new Vector3(position.x + velX, position.y + velY, position.z); // Modifie la position  du joueur
    }
}
