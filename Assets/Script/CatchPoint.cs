using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPoint : MonoBehaviour
{
	[SerializeField]
	Transform jumpLine;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	//private void OnCollisionEnter(Collision collision)
	//{
 //       if (collision.gameObject.tag == "Player")
 //       {
	//		Debug.Log((collision.gameObject.transform.position - jumpLine.position).magnitude);
 //           Time.timeScale = 0;
	//	}
	//}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Debug.Log((other.gameObject.transform.position - jumpLine.position).magnitude);
			Time.timeScale = 0;
			Power.Instance.ViewScore((other.gameObject.transform.position - jumpLine.position).magnitude);
		}
	}
}
