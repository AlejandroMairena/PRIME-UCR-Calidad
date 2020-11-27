using PRIME_UCR.Domain.Models.UserAdministration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.DTOs.UserAdministration
{
    public class AskForPermissionModel
    {
        public AskForPermissionModel()
        {
            PermissionsList = new List<string>();
        }

        public Usuario User { get; set; }
        public string UserMessage { get; set; }

        public string StatusMessage { get; set; }

        public string StatusMessageType { get; set; }

        public List<string> PermissionsList { get; set; }
    }
}
