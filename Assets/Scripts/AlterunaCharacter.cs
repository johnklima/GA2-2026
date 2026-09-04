using Alteruna.Multiplayer.Core.MethodArguments;
using Alteruna.Multiplayer.Core.PacketProcessing;
using Alteruna.Multiplayer.Unity;
using UnityEngine;

public class AlterunaCharacter : Synchronizable
{
    public override void AssembleData(Writer writer, SerializeInfo info)
    {
        throw new System.NotImplementedException();
    }

    public override void DisassembleData(Reader reader, UnserializeInfo info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(int which)
    {
        BroadcastRemoteMethod("ChangeAvatar",which);
    }

    [SynchronizableMethod]
    void ChangeAvatar(int which)
    {
        Debug.Log("ChangeAvatar");
    }
}
