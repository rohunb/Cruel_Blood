using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	
	void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("as");
            Application.LoadLevel(0);
        }
    }
}
