using System;
using UnityEngine;

namespace Styx.Entities
{
    public class PlayerId : MonoBehaviour
    {
        [SerializeField]
        private Guid _id = Guid.NewGuid();

        public Guid Value
        {
            get { return _id; }
        }
    }
}
