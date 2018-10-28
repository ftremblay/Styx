using UnityEngine;

namespace Styx.Commands
{
    public abstract class Command : MonoBehaviour
    {
        abstract public void Execute();
    }
}
