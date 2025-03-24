using System.Collections.Generic;
using UnityEngine;

namespace NaughtyAttributes.Test
{
    public class _NaughtyComponent : MonoBehaviour
    {
        [Scene]
        public string bootScene; // scene name

        [Scene]
        public int tutorialScene; // scene index
    }

    [System.Serializable]
    public class MyClass
    {
    }

    [System.Serializable]
    public struct MyStruct
    {
    }
}
