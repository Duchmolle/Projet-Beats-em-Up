using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] EnnemyCombat _ennemy;

    public void PunchTrigger()
    {
        _ennemy.PunchPlayer();

           //Instantiate VFX
            //Son
    }

    public void OnDeathTrigger()
    {
        //Instantiate discs & tapes
    }
}
