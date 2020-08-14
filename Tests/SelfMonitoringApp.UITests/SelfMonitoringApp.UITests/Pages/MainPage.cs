using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfMonitoringApp.UITests.Pages
{
    public class MainPage : BasePage
    {



        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("menu_add_task"),
            iOS = x => x.Marked("Tasky")
        };

    }
}
