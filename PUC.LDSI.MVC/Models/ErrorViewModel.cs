using System;

namespace PUC.LDSI.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorTitle { get; set; }
		public string[] Errors { get; set; }
        public string Trace { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}