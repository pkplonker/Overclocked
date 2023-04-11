using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart
{
    public interface IInteractable
    {
        public bool Interact(Interactor interactor);
    }
}
