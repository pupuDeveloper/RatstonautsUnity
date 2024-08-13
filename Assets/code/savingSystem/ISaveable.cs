using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    void Save(BinarySaver writer);
    void Load(BinarySaver reader);
}
