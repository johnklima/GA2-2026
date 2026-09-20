using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Core.MethodArguments;
using Alteruna.Multiplayer.Core.PacketProcessing;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;
public class AvatarSynch : Synchronizable
{
    public UniqueAvatarChild avatarChild;
    public bool doIt;
    public Text debug;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        debug.text += "hello awake \n";
        //get the avatar child so we can change it
        avatarChild = GetComponent<UniqueAvatarChild>();  //does not get set on avatar instance in game
        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        //if (true) Debug.Log("true"); //fires
        //if (doIt) Debug.Log("doIt"); //does not fire when ticked in the inspector

        //thus this does nothing
        if (doIt)
        {
            doIt = false;
            SetAvatarPrefab(5); //just a test, I have 7
        }


    }
    */

   
    private int _avatarPrefab;
    public override void DisassembleData(Reader reader, UnserializeInfo info)
    {
        debug.text += "DisassembleData \n";
        _avatarPrefab = reader.ReadInt();
        avatarChild.OverwritePrefab(avatarChild.Prefabs[_avatarPrefab]);
    }

    public override void AssembleData(Writer writer, SerializeInfo info)
    {
        debug.text += "AssembleData \n";
        writer.Write(_avatarPrefab);
    }

    public void SetAvatarPrefab(int index)
    {
        _avatarPrefab = index;
        //for LOD?
        //Commit();
        //SyncUpdate();
        avatarChild.OverwritePrefab(avatarChild.Prefabs[_avatarPrefab]);
        Multiplayer.Sync(this);
    }
    
}
