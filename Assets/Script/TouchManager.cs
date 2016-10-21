using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    public GameObject Player;
    public GameObject target;
    public float Jump = 3f;
   


	void Update () {

        if(Input.GetMouseButtonDown(0))
        {
            print(Input.mousePosition);
            if(Input.mousePosition.x > Screen.width / 2)
            {
                target.GetComponent<Transform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
                print("반 이하입니다.");
            }
           

        }

        
	
	}
}
