using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualGame.Pool
{
    public interface IObjectPooling
    {
        void PopulatePulpit(IGameManager gameManager);

        GameObject GetPooledObject();

    }

}
