﻿using Microsoft.AspNetCore.Components;

using System.Collections.Generic;

namespace BlazorBlog.Components
{
    public partial class ValidationComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Error { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        protected async void HandleInputChanged(ChangeEventArgs eventArgs)
        {
            await ValueChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
