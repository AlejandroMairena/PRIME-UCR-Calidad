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
        [Parameter]
        public MultimediaModal MultimediaModal { get; set; }


        // Element References
        ElementReference recordButton;
        ElementReference stopButton;
        ElementReference audio;
        ElementReference timer;
        ElementReference downloadLink;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // call JS code to initialize Element References
            await JS.InvokeAsync<bool>("initAudio", recordButton, stopButton, audio, timer, downloadLink);
        }

        void OnClose()
        {
            MultimediaModal?.CloseImageView();
        }

        async void OnTitleChanged(Tuple<bool, string> tuple)
        {
            if (!tuple.Item1) // if valid
                await JS.InvokeAsync<bool>("updateAudioName", tuple.Item2);

        }

    }
}
