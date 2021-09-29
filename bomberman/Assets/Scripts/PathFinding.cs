using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    public class PathFinding : MonoBehaviour
    {
        private void GetPath(Transform startPosition)
        {
            ShortestPath(startPosition);
        }

        private void CompareNodes()
        {
            
        }
        
        private void ShortestPath(Transform startPosition)
        {
            List<Transform> closedList = new List<Transform>();
            
            closedList.Add(startPosition);
            
            
        }
    }
    
    
}
