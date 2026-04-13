using Photon.Pun;
using Photon.Realtime;

public class RoomConnector : MonoBehaviourPunCallbacks
{
    void Start() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster() => 
        PhotonNetwork.JoinOrCreateRoom("LectureHall", new RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
}