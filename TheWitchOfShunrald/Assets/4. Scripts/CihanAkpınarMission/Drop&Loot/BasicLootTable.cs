using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicLootTable : ScriptableObject
{
  [Serializable]
  public class BasicDrop
  {
    public BasicItem basicDrop;
    public int basicWeight;
  }

  public List<BasicDrop> table;
  
  [NonSerialized]
  private int basicTotalWeight = -1;
  public int BasicTotalWeight
  {
    get
    {
      if (basicTotalWeight==-1)
      {
        BasicCalculateTotatWeight();
      }

      return basicTotalWeight;
    }
    
  }

  void BasicCalculateTotatWeight()
  {
    basicTotalWeight = 0;
    for (int i = 0; i < table.Count; i++)
    {
      basicTotalWeight += table[i].basicWeight;
    }
  }

  public BasicItem BasicGetDrop()
  {
    int roll = UnityEngine.Random.Range(0, BasicTotalWeight);
    for (int i = 0; i < table.Count; i++)
    {
      roll -= table[i].basicWeight;
      if (roll<0)
      {
        return table[i].basicDrop;
      }
    }

    return table[0].basicDrop;
  }
  
}
