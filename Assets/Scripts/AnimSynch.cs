using Alteruna.Multiplayer.Core.MethodArguments;
using Alteruna.Multiplayer.Core.PacketProcessing;
using Alteruna.Multiplayer.Unity;
using UnityEngine;

public class AnimSynch : Synchronizable
{
    public Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    float _avatarSpeed;
    public override void DisassembleData(Reader reader, UnserializeInfo info)
    {
        animator = GetComponent<Animator>();
        _avatarSpeed = reader.ReadFloat();
        animator.SetFloat("Velocity", _avatarSpeed);

    }

    public override void AssembleData(Writer writer, SerializeInfo info)
    {

        writer.Write(_avatarSpeed);
    }

    public void SetAvatarSpeed(float magnitude)
    {
        _avatarSpeed = magnitude;

        //Commit();
        //SyncUpdate();
        Multiplayer.Sync(this);

    }

}
