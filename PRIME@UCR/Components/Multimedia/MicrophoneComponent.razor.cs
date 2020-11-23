using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Components.Multimedia
{
    public partial class MicrophoneComponent
    {

        // Element References
        ElementReference recordButton;
        ElementReference stopButton;
        ElementReference audio;
        ElementReference timer;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // call JS code to initialize Element References
            await JS.InvokeAsync<bool>("initAudio", recordButton, stopButton, audio, timer);
        }

    }
}
