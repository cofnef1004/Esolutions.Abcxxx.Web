﻿using System;
using System.Threading.Tasks;
using ES.QLBongDa.Views;
using Xamarin.Forms;

namespace ES.QLBongDa.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task SetMainPage<TView>(object navigationParameter = null, bool clearNavigationHistory = false)
            where TView : IXamarinView;

        Task SetDetailPageAsync(Type viewType, object navigationParameter = null, bool pushToStack = false);

        Task<Page> GoBackAsync();
    }
}