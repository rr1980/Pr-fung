using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prüfung.ViewModels
{
    public class AdminViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<string> Errors { get; set; }
    }
}
