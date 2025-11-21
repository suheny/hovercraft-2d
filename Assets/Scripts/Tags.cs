using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] private List<string> list;

    public bool HasTag(string tag) => list.Contains(tag);
}
