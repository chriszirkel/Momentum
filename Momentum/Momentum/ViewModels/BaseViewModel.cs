using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.ViewModels
{
    [ImplementPropertyChanged]
    public class BaseViewModel
    {
        public bool IsBusy
        {
            get;
            set;
        }

        public BaseViewModel() { }
    }
}
