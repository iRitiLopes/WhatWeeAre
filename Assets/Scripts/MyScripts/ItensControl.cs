using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensControl : MonoBehaviour {

    

    GameControl game;
	[SerializeField]
    private bool IsDismantled = false;
	[SerializeField]
    private int ItemIndex;

	// Use this for initialization
	void Start () {
		game = GameObject.Find("Main Camera").GetComponent<GameControl>();
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && !IsDismantled)
        {
            DestroyItem();
            game.ItensLevel1[ItemIndex] = true;
        }
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}


