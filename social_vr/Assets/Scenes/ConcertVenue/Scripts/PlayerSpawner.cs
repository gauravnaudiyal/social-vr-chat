using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [Header("XR References")]
    public Transform headTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;

    private GameObject spawnedAvatar;
    private Transform avatarHead;
    private Transform avatarLeftHand;
    private Transform avatarRightHand;

    public override void OnJoinedRoom()
    {
        SpawnAvatar();
    }

    void SpawnAvatar()
    {
        spawnedAvatar = PhotonNetwork.Instantiate("PlayerAvatar", Vector3.zero, Quaternion.identity);

        avatarHead = spawnedAvatar.transform.Find("Head");
        avatarLeftHand = spawnedAvatar.transform.Find("LeftHand");
        avatarRightHand = spawnedAvatar.transform.Find("RightHand");

        // Give each player a random colour
        Color playerColor = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
        foreach (Renderer r in spawnedAvatar.GetComponentsInChildren<Renderer>())
        {
            r.material.color = playerColor;
        }
    }

void Update()
{
    if (spawnedAvatar == null) return;
    
    // Only move YOUR OWN avatar
    if (!spawnedAvatar.GetComponent<PhotonView>().IsMine) return;

    if (headTransform != null && avatarHead != null)
    {
        avatarHead.position = headTransform.position;
        avatarHead.rotation = headTransform.rotation;
    }

    if (leftHandTransform != null && avatarLeftHand != null)
    {
        avatarLeftHand.position = leftHandTransform.position;
        avatarLeftHand.rotation = leftHandTransform.rotation;
    }

    if (rightHandTransform != null && avatarRightHand != null)
    {
        avatarRightHand.position = rightHandTransform.position;
        avatarRightHand.rotation = rightHandTransform.rotation;
    }
}
}