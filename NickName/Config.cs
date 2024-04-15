using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Nickname
{
    public class Config : IConfig
    {
        [Description("Indicates whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates whether debug mode is enabled.")]
        public bool Debug { get; set; } = false;
        
        public bool NameCommand { get; set; } = true; 
        public bool NickNameCommand { get; set; } = false; 
        public bool ClearNicknameCommand { get; set; } = false;
        public string NameHintMessage { get; set; } = "Nazywasz się {0}";
        public int NameHintDuration { get; set; } = 5;
    }
}