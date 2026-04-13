using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MusicSync : MonoBehaviourPun, IInRoomCallbacks, IMatchmakingCallbacks
{
    public AudioSource audioSource;

    void OnEnable() { PhotonNetwork.AddCallbackTarget(this); }
    void OnDisable() { PhotonNetwork.RemoveCallbackTarget(this); }

    public void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            double startTime = PhotonNetwork.Time;
            Hashtable props = new Hashtable();
            props["musicStartTime"] = startTime;
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
            audioSource.Play();
        }
        else
        {
            StartCoroutine(SyncAfterDelay());
        }
    }

    IEnumerator SyncAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("musicStartTime"))
        {
            double startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["musicStartTime"];
            double elapsed = PhotonNetwork.Time - startTime;
            float syncPosition = (float)(elapsed % audioSource.clip.length);
            audioSource.time = syncPosition;
            audioSource.Play();
            Debug.Log($"Synced at {syncPosition:F3}s");
        }
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("musicStartTime"))
        {
            double startTime = (double)propertiesThatChanged["musicStartTime"];
            double elapsed = PhotonNetwork.Time - startTime;
            float syncPosition = (float)(elapsed % audioSource.clip.length);
            audioSource.time = syncPosition;
            audioSource.Play();
        }
    }

    // IMatchmakingCallbacks stubs
    public void OnFriendListUpdate(System.Collections.Generic.List<FriendInfo> friendList) { }
    public void OnCreatedRoom() { }
    public void OnCreateRoomFailed(short returnCode, string message) { }
    public void OnJoinRoomFailed(short returnCode, string message) { }
    public void OnJoinRandomFailed(short returnCode, string message) { }
    public void OnLeftRoom() { }

    // IInRoomCallbacks stubs
    public void OnPlayerEnteredRoom(Player newPlayer) { }
    public void OnPlayerLeftRoom(Player otherPlayer) { }
    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) { }
    public void OnMasterClientSwitched(Player newMasterClient) { }
}