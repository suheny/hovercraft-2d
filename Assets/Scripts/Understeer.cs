using UnityEngine;

namespace Assets.Scripts
{
    public class Understeer : MonoBehaviour
    {   
        [field: Header("Parameter")]
        [field: Tooltip("Amount value of understeer")]
        [field: SerializeField] public float Amount { get; private set; }
    }
}