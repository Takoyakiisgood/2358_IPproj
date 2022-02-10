using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flatten : MonoBehaviour
{
    public GameObject flatVersion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "rollingPin")
        {
            //play the animation 
            //show the dough into 4 pieces
        }
    }

    private IEnumerator Waitfor(int sec)
    {
        yield return new WaitForSeconds(sec);
        //set the flat dough to active
        if (flatVersion != null)
        {
            flatVersion.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (flatVersion != null)
        {
            flatVersion.SetActive(false);
        }
    }
}
