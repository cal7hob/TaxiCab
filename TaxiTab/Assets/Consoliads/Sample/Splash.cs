using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(loadScene());
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void gotoscene1()
    {
        
    }

    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(12);
        Application.LoadLevel("Sample");
    }
}
