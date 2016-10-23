using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    // Représente le delay entre chaque spawn
    public float        delay = 5f;

    // Représente la position minimale des objets spawnés
    public Vector2      minPos = new Vector2(-5, -5);

    // Représente la position minimale des objets spawnés
    public Vector2      maxPos = new Vector2(5, 5);

    // Représente l'objet à spawner
    public Transform    prefab;

    // Représente le nombre d'objets à spawner
    public int          amount = 1;

    // Représente le delay restant avant le prochain spawn
    public float cooldown { get; set; }

    // Cette fonction se lance à la création de l'objet
    void    Start()
    {
        if (!prefab)        // Vérifie si l'objet à spawn n'a pas été défini
            Destroy(this);  // Si c'est le cas, détruit le spawner
    }

    // Cette fonction se lance à chaque frame
    void FixedUpdate()
    {
        float       randx;                                                      // Représente la position aléatoire en x choisie pour l'objet
        float       randy;                                                      // Représente la position aléatoire en y choisie pour l'objet
        Vector2     pos;                                                        // Représente la position du spawner
        int         counter;                                                    // Représente un compteur allant de 0 jusyu'au nombre d'objets à compter

        if (cooldown <= 0)                                                      // Vérifie si le cooldown est terminé
        {
            pos = transform.position;                                           // Assigne la position du spawner
            counter = 0;                                                        // Initialise le compteur
            while (counter < amount)                                            // Répète tant que le compteur n'a pas atteint le nombre d'objets à spawn
            {
                Instantiate(prefab);                                            // Spawn l'objet
                randx = Random.Range(minPos.x + pos.x, maxPos.x + pos.x);       // Calcul de la position aléatoire en x
                randy = Random.Range(minPos.y + pos.y, maxPos.y + pos.y);       // Calcul de la position aléatoire en y
                prefab.position = new Vector3(randx, randy, prefab.position.z); // Modifie la position de l'objet spawné
            }
            cooldown = delay;                                                   // Réinitialise le cooldown
        }
        else                                                                    // Si le cooldown n'est pas terminé
        {
            cooldown -= Time.fixedDeltaTime;                                    // Décrémente le cooldown
        }
    }

    void    OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + (maxPos.x + minPos.x) / 2, transform.position.y + (maxPos.y + minPos.y) / 2),
                            new Vector3(maxPos.x - minPos.x, maxPos.y - minPos.y));
    }
}
