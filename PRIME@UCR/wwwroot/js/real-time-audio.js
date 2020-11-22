let timerRef;
let timerInterval;
let counter = 0;


function initAudio(record, stop, audio, _timerRef) {
    timerRef = _timerRef;
    if (navigator.mediaDevices.getUserMedia) {
        console.log('getUserMedia supported.');

        const constraints = { audio: true };
        let chunks = [];

        let onSuccess = function (stream) {
            const mediaRecorder = new MediaRecorder(stream);

            record.onclick = function () {
                timerInterval = setInterval(increaseCounter, 1000);
                timerRef.innerText = '0 segundos';

                mediaRecorder.start();
                console.log(mediaRecorder.state);
                console.log("recorder started");

                stop.disabled = false;
                record.disabled = true;
            }

            stop.onclick = function () {
                clearInterval(timerInterval);
                timerRef.innerText = '';

                mediaRecorder.stop();
                console.log(mediaRecorder.state);
                console.log("recorder stopped");

                stop.disabled = true;
                record.disabled = false;
            }

            mediaRecorder.onstop = function (e) {
                console.log("data available after MediaRecorder.stop() called.");

                //const clipName = 'audio';

                audio.setAttribute('controls', '');

                audio.controls = true;
                const blob = new Blob(chunks, { 'type': 'audio/ogg; codecs=opus' });
                chunks = [];
                const audioURL = window.URL.createObjectURL(blob);
                audio.src = audioURL;
                console.log("recorder stopped");

            }

            mediaRecorder.ondataavailable = function (e) {
                chunks.push(e.data);
            }
        }

        let onError = function (err) {
            console.log('The following error occured: ' + err);
        }

        navigator.mediaDevices.getUserMedia(constraints).then(onSuccess, onError);
        return true;

    } else {
        console.log('getUserMedia not supported on your browser!');
        return false;
    }
}

function increaseCounter() {
    counter++;
    if (counter == 1) {
        timerRef.innerText = counter + ' segundo';
    }
    else {
        timerRef.innerText = counter + ' segundos';
    }
}