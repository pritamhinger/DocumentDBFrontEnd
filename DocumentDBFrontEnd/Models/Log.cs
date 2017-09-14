using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DocumentDBFrontEnd.Models
{
    public class Log
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty(PropertyName = "CRMCustomerId")]
        public string CRMCustomerId { get; set; }

        [JsonProperty(PropertyName = "CustomerId")]
        public long CustomerId { get; set; }

        [JsonProperty(PropertyName = "UerEmail")]
        public string UserEmail { get; set; }

        [JsonProperty(PropertyName = "SapUsername")]
        public string SapUsername { get; set; }

        [JsonProperty(PropertyName = "SapSystem")]
        public string SapSystem { get; set; }

        [JsonProperty(PropertyName = "SapClient")]
        public string SapClient { get; set; }

        [JsonProperty(PropertyName = "ActivityDateTimeTicks")]
        public long ActivityDateTimeTicks { get; set; }

        [JsonProperty(PropertyName = "ManualUploadTime")]
        public string ManualUploadTime { get; set; }

        [JsonProperty(PropertyName = "ShuttleUploadTime")]
        public string ShuttleUploadTime { get; set; }

        [JsonProperty(PropertyName = "TimeSaved")]
        public string TimeSaved { get; set; }

        [JsonProperty(PropertyName = "TotalTimeSaved")]
        public string TotalTimeSaved { get; set; }

        [JsonProperty(PropertyName = "SapTcode")]
        public string SapTcode { get; set; }

        [JsonProperty(PropertyName = "CRMLicenseId")]
        public string CRMLicenseId { get; set; }

        [JsonProperty(PropertyName = "Module")]
        public string Module { get; set; }

        [JsonProperty(PropertyName = "ScriptFilePath")]
        public string ScriptFilePath { get; set; }

        [JsonProperty(PropertyName = "DataFilePath")]
        public string DataFilePath { get; set; }

        [JsonProperty(PropertyName = "RecordsUploaded")]
        public int RecordsUploaded { get; set; }

        [JsonProperty(PropertyName = "ErroredRecords")]
        public int ErroredRecords { get; set; }

        [JsonProperty(PropertyName = "RunReason")]
        public string RunReason { get; set; }

        [JsonProperty(PropertyName = "RecordingMode")]
        public string RecordingMode { get; set; }
    }
}