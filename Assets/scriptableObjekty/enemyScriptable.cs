using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemaci", menuName = "DataProEnemy", order = 0)]
public class enemyScriptable : ScriptableObject
{ 
   public List<enemyHolder> enemies; 
}
