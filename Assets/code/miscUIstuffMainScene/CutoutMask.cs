using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutoutMask : Image
{
    //this basically overrides the default material inside unitys "mask" component
    //the default mask does a comparing thingy with the mask and pixels of stuff it wants to cover, and we are just reversing that
    public override Material materialForRendering 
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
