using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager gameManagerInstance;

    public MatchSettings matchSettings;

    [SerializeField]
    private GameObject sceneCamera;

    private void Awake()
    {
        if (gameManagerInstance != null)
            Debug.LogError("More than one GameManager !");
        else
            gameManagerInstance = this;
    }

    public void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera == null)
            return;

        sceneCamera.SetActive(isActive);
    }

    #region PlayerRegistering

    private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

    private const string PLAYER_ID_PREFIX = "Player_";

    public static void RegisterPlayer(string netID, PlayerManager player)
    {
        string playerID = PLAYER_ID_PREFIX + netID;

        players.Add(playerID, player);
        player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }

    public static PlayerManager GetPlayer(string playerID)
    {
        return players[playerID];
    }

    /*    private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(200,200,200,500));
            GUILayout.BeginVertical();

            foreach (string playerID in players.Keys)
            {
                GUILayout.Label(playerID + "  -  " + players[playerID].transform.name);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    */
    #endregion


}
