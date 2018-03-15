﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebPlatform.Models.OPCUA
{
    public class MonitorableNode
    {
        public string NodeId { get; set; }
        public int SamplingInterval { get; set; }
        public string DeadBand { get; set; }
        public double DeadBandValue { get; set; }

        //TODO: i parametri numerici possono essere = 0?
        public bool isValid()
        {
            return Regex.IsMatch(NodeId, @"^(\d+)-(?:(\d+)|(\S+))$") &&
                   SamplingInterval > 0 &&
                   DeadBand.Length > 0 && 
                   DeadBandValue > 0;
        }
    }

    public class MonitorParams
    {
        public string Topic { get; set; }
        public string BrokerUrl { get; set; }
        public MonitorableNode[] MonitorableNodes { get; set; }

        public bool isValid()
        {
            return Regex.IsMatch(BrokerUrl, @"^(?:mqtt|signalr):(.*)$") && 
                   Topic.Length != 0 &&
                   MonitorableNodes.All(m => m.isValid());
        }
    }
}