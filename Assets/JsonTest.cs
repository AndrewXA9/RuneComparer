using UnityEngine;
using System.Collections;



//example https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/AndrewXA9?api_key=6b394d7f-7d69-4140-a822-0c5496430201
//my summ id 19492222

public class JsonTest : MonoBehaviour {

	private WWW downloader;
	
	private JSONObject json;
	
	void Start () {
		//downloader = new WWW("https://na.api.pvp.net/api/lol/na/v2.2/matchhistory/by-summoner/19492222?api_key=6b394d7f-7d69-4140-a822-0c5496430201"); //match history
		downloader = new WWW("https://na.api.pvp.net/api/lol/na/v2.2/matchlist/by-summoner/19492222?api_key=6b394d7f-7d69-4140-a822-0c5496430201"); //match list
		//downloader = new WWW("https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/AndrewXA9?api_key=6b394d7f-7d69-4140-a822-0c5496430201"); //summoner name
		
	}
	
	void Update(){
		if(downloader != null){
			if(downloader.isDone){
				json = new JSONObject(downloader.text);
				downloader.Dispose();
				downloader = null;
				accessData(json);
			}
		}
	}
	
	void OnGUI () {
		
		if(downloader != null){
			if(downloader.isDone){
				//GUI.TextArea(new Rect(10,10,Screen.width-20,Screen.height-20),downloader.text);
			}
			else{
				GUI.Label(new Rect(10,10,200,20),(downloader.progress*100f).ToString());
			}
		}
		
	}
	
	void accessData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				Debug.Log(key);
				accessData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			foreach(JSONObject j in obj.list){
				accessData(j);
			}
			break;
		case JSONObject.Type.STRING:
			Debug.Log("string: "+obj.str);
			break;
		case JSONObject.Type.NUMBER:
			Debug.Log("number: "+obj.n);
			break;
		case JSONObject.Type.BOOL:
			Debug.Log("bool: "+obj.b);
			break;
		case JSONObject.Type.NULL:
			Debug.Log("NULL");
			break;
			
		}
	}
}

