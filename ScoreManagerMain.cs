using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManagerMain : MonoBehaviour {


	
	public void MakeCall (string score, string totalShotsTaken) {

        Debug.Log("in score manager");

        string url = "http://localhost:3000/posts";

        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("totalShotsTaken", totalShotsTaken);
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
