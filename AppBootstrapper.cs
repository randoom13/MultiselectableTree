using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiselectableTree.ViewModels;

namespace MultiselectableTree
{
    public class AppBootstrapper : Caliburn.Micro.BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<TreeOwnerViewModel>();
        }
    }
}
