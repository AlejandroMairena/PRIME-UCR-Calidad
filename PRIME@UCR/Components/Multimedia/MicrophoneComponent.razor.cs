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
        /* Appointment code for auto naming real time multimedia content.
         */
        [Parameter]
        public string ApCode { get; set; }

        // Element References
        ElementReference recordButton;
        ElementReference stopButton;
        ElementReference audio;
        ElementReference timer;
        ElementReference downloadLink;

        string fileName = "";
        MAlertMessage AlertMessage;
        MAlertMessage RecordAlertMessage;
        MAlertMessage StopAlertMessage;
        MAlertMessage SaveAudioMessageAlertMessage;

        protected override void OnInitialized()
        {
            fileName = GetFileName();
            UpdateFileName();

            RecordAlertMessage = new MAlertMessage
            {
                AlertType = AlertType.Primary,
                Message = "Presione el botón de Grabar para empezar a grabar el audio."
            };

            StopAlertMessage = new MAlertMessage
            {
                AlertType = AlertType.Primary,
                Message = "Presione el botón de Detener para detener la grabación del audio."
            };

            SaveAudioMessageAlertMessage = new MAlertMessage
            {
                AlertType = AlertType.Primary,
                Message = "Presione el botón de Guardar para adjuntar el audio o Grabar para grabar" +
                " otro audio."
            };

            AlertMessage = RecordAlertMessage;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // call JS code to initialize Element References
            await JS.InvokeAsync<bool>("initAudio", recordButton, stopButton, audio, timer, downloadLink);
        }

        void OnClose()
        {
            MultimediaModal?.CloseImageView();
        }

        void OnRecord()
        {
            AlertMessage = StopAlertMessage;
        }

        void OnStop()
        {
            AlertMessage = SaveAudioMessageAlertMessage;
        }


        string GetFileName()
        {
            return "AUD-" + ApCode + "-" + MultimediaContentComponent.FormatDate(DateTime.Now);
        }

        async Task UpdateFileName()
        {
            await JS.InvokeAsync<bool>("updateAudioName", fileName);
        }


    }
}
