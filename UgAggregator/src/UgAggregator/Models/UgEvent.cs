using System;

namespace UgAggregator.Models {
    public class UgEvent {
        public string GroupName { get; set; }
        public string GroupUrl { get; set; }
        public string Name { get; set; }
        public string EventUrl { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Description { get; set; }
        public string EventSource { get; set; }
    }
}