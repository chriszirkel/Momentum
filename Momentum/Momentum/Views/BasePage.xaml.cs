using Momentum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Momentum.Views
{
    public partial class BasePage : ContentPage
    {
        //public ObservableCollection<ToolbarItem> LeftToolbarItems { get; set; }

        public BasePage()
        {
            InitializeComponent();

            //LeftToolbarItems = new ObservableCollection<ToolbarItem>();

            //SetBinding(Page.TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
            //SetBinding(Page.IconProperty, new Binding(BaseViewModel.IconPropertyName));
            SetBinding(Page.IsBusyProperty, new Binding(nameof(BaseViewModel.IsBusy)));
        }
    }
}
