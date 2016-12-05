using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class ScoreManagerMain : MonoBehaviour {

    private string api = "https://trainrobber-ztebasketball.herokuapp.com/players/";
    public PlayerManager player;


    public void CreateHighScore (string score, string totalShots) {

        string endpoint = "createhighscore";
        string url = api + endpoint;
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("totalShots", totalShots);
        form.AddField("name", player.name);
        form.AddField("email", player.email);
        WWW www = new WWW(url, form);
        //StartCoroutine(WaitForResponse(www));
        WaitForResponse(www);
    }


    public void LastAdded ()
    {
        string endpoint = "lastadded";
        string url = api + endpoint;
        Debug.Log(url);
        WWW www = new WWW(url);

        StartCoroutine(
        WaitForResponse(www)
        );
        
        //print(www.text);
        //return www.text;
    }

    
    IEnumerator WaitForResponse(WWW www)
    {

        yield return www;
        
        if (www.error == null )
        {
            Debug.Log(" WWW OK " + www.text);
            
            JSONObject json = new JSONObject(www.text);

            Debug.Log(json[0].GetField("name"));

            string name = json[0].GetField("name").ToString().Replace("\"", "");
            string email = json[0].GetField("email").ToString().Replace("\"", "");

            player = new PlayerManager(name, email);

            Debug.Log(player.name + " : " + player.email );

            

        }
        else
        {
            Debug.Log(" WWW ERR !!! " + www.text);
        }

        
    }


}
