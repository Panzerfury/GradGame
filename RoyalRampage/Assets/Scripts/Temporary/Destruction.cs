﻿using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour
{
    private GameObject rubblePrefab;
    private int life;
    private int rubbleAmount;
    private int state;

    private GameObject player;
    private ParticleSystem particleSys;

    [HideInInspector]
    public int score;


    //object name to be used for Quest system??
    public enum DestructableObject {
		BARREL, CHAIR, TABLE, WARDROBE, BED, BOX
	}

	public DestructableObject objType;

    // Use this for initialization
    void Start()
    {
        switch(objType)
        {
            case DestructableObject.BARREL:
            score = ObjectManager.instance.barrelScore;
            life = ObjectManager.instance.barrelLife;
            rubbleAmount = ObjectManager.instance.barrelRubbleAmount;
            rubblePrefab = ObjectManager.instance.barrelRubblePrefab;
            break;
            case DestructableObject.BED:
            score = ObjectManager.instance.bedScore;
            life = ObjectManager.instance.bedLife;
            rubbleAmount = ObjectManager.instance.bedRubbleAmount;
            rubblePrefab = ObjectManager.instance.bedRubblePrefab;
            break;
            case DestructableObject.BOX:
            score = ObjectManager.instance.boxScore;
            life = ObjectManager.instance.boxLife;
            rubbleAmount = ObjectManager.instance.boxRubbleAmount;
            rubblePrefab = ObjectManager.instance.boxRubblePrefab;
            break;
            case DestructableObject.CHAIR:
            score = ObjectManager.instance.chairScore;
            life = ObjectManager.instance.chairLife;
            rubbleAmount = ObjectManager.instance.chairRubbleAmount;
            rubblePrefab = ObjectManager.instance.chairRubblePrefab;
            break;
            case DestructableObject.TABLE:
            score = ObjectManager.instance.tableScore;
            life = ObjectManager.instance.tableLife;
            rubbleAmount = ObjectManager.instance.tableRubbleAmount;
            rubblePrefab = ObjectManager.instance.tableRubblePrefab;
            break;
            case DestructableObject.WARDROBE:
            score = ObjectManager.instance.wardrobeScore;
            life = ObjectManager.instance.wardrobeLife;
            rubbleAmount = ObjectManager.instance.wardrobeRubbleAmount;
            rubblePrefab = ObjectManager.instance.wardrobeRubblePrefab;
            break;
                default:
            break;

        }
        state = 1;
        particleSys = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision col)
    {

        if(col.collider.gameObject == player)
        {
            col.collider.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            if(state == (life - life) + state)
            {
                if (state >= life)
                {           
                    for (int i = 0; i < rubbleAmount; i++)
                    {
                        Instantiate(rubblePrefab,transform.position, Quaternion.identity);
                    }

					Destroy (gameObject);
                }
				if (particleSys != null) 
				{
					particleSys.startSize *= state; 
					particleSys.Play ();
				}
				state += 1;
            }
        }
    }
}
