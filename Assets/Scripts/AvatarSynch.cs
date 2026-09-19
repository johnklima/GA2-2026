using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Core.MethodArguments;
using Alteruna.Multiplayer.Core.PacketProcessing;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
public class AvatarSynch : Synchronizable
{
    public UniqueAvatarChild avatarChild;
    public bool doIt;

    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        //enabled = isMe;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the avatar child so we can change it
        avatarChild = GetComponent<UniqueAvatarChild>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (true) Debug.Log("true");
        
    }
    
    private int _avatarPrefab;
    public override void DisassembleData(Reader reader, UnserializeInfo info)
    {
        _avatarPrefab = reader.ReadInt();
        avatarChild.OverwritePrefab(avatarChild.Prefabs[_avatarPrefab]);
    }

    public override void AssembleData(Writer writer, SerializeInfo info)
    {
        writer.Write(_avatarPrefab);
    }

    public void SetAvatarPrefab(int index)
    {
        _avatarPrefab = index;
        Commit();
        SyncUpdate();
        avatarChild.OverwritePrefab(avatarChild.Prefabs[_avatarPrefab]);
    }
    
}
