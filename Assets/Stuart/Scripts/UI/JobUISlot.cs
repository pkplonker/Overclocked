using Stuart;
using TMPro;
using UnityEngine;

namespace Stuart
{
    public class JobUISlot : MonoBehaviour
    {
        public JobWithTiming job { get; private set; }
        [SerializeField] private TextMeshProUGUI gpu;
        [SerializeField] private TextMeshProUGUI hdd;
        [SerializeField] private TextMeshProUGUI ram;

        public void Init(JobWithTiming job)
        {
            this.job = job;
            foreach (var item in job.job.requiredItems)
            {
                switch (item.type)
                {
                    case ItemType.HDD:
                        gpu.text = $"{item.value}TB {item.type.ToString()}";
                        break;
                    case ItemType.GPU:
                        hdd.text = $"{item.value} {item.type.ToString()}";
                        break;
                    case ItemType.RAM:
                        ram.text = $"{item.value}GB {item.type.ToString()}";
                        break;
                }
            }
        }
    }
}