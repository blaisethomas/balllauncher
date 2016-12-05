using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class ScoreManagerPractice : MonoBehaviour {


	
	public void MakeCall (string variable) {

        Debug.Log("in score manager");

        string url = "http://localhost:3000/posts";

        WWWForm form = new WWWForm();
        form.AddField("var1", variable);
        //form.AddField("var2", "value2");
        WWW www = new WWW(url, form);

        //StartCoroutine(WaitForResponse(www));

        WaitForResponse(www);
    }

    
    IEnumerator WaitForResponse(WWW www)
    {
        yield return www;

        //error handling

        if (www.error == null )
        {
            Debug.Log(" WWW OK! " + www.text);
        } else
        {
            Debug.Log(" WWW ERR !!! " + www.text);
        }
    }
}
