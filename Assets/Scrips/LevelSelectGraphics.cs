using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class LevelSelectGraphics : MonoBehaviour
{
    void Start()
    {
        ServiceLocator.GetService<AudioManager>().PlayLevelSelectMusic();
    }
}
