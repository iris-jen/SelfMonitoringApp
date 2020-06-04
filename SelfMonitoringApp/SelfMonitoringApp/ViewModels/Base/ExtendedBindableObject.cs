using System;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels.Base
{
    public abstract class ExtendedBindableObject: BindableObject
    {
        public void RaisePropertyChanged([CallerMemberName]string caller = "")
        {
            OnPropertyChanged(caller);
        }
    }
}