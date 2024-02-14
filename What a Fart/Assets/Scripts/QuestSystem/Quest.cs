using UnityEngine;

namespace QuestSystem
{
    [System.Serializable]
    public class Quest
    {
        public bool isActive;
        
        public string title;
        public string description,trackText;
        public int trackPoints;
        public int fartReward;

        public QuestGoal goal;

        public void Complete()
        {
            isActive = false;
            goal.currentAmount = 0;
            Debug.Log("cumplio");
        }
    }
}
