let recordingTimeMS = 30000;
let startButton;
let stopButton;


function wait(delayInMS) {
    return new Promise(resolve => setTimeout(resolve, delayInMS));
}

function startRecording(stream, lengthInMS) {
    startButton.disabled = true;
    stopButton.disabled = false;

    let recorder = new MediaRecorder(stream);
    let data = [];

    recorder.ondataavailable = event => data.push(event.data);
    recorder.start();

    let stopped = new Promise((resolve, reject) => {
        recorder.onstop = resolve;
        recorder.onerror = event => reject(event.name);
    });

    let recorded = wait(lengthInMS).then(
        () => recorder.state == "recording" && recorder.stop()
    );

    return Promise.all([
        stopped,
        recorded
    ]).then(() => data);
}

function stop(stream) {
    startButton.disabled = false;
    stopButton.disabled = true;

    stream.getTracks().forEach(track => track.stop());
}

function videoInit(_startButton, preview, recording, downloadButton, _stopButton) {
    startButton = _startButton;
    stopButton = _stopButton;

    stopButton.disable = true;
    
    startButton.addEventListener("click", function () {
        navigator.mediaDevices.getUserMedia({
            video: true,
            audio: true
        }).then(stream => {
            preview.srcObject = stream;
            downloadButton.href = stream;
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;
            return new Promise(resolve => preview.onplaying = resolve);
        }).then(() => startRecording(preview.captureStream(), recordingTimeMS))
            .then(recordedChunks => {
                let recordedBlob = new Blob(recordedChunks, { type: "video/webm" });
                recording.src = URL.createObjectURL(recordedBlob);
                downloadButton.href = recording.src;
                downloadButton.download = "RecordedVideo.webm";
            })
            .catch((e) => console.log(e));
    }, false);

    stopButton.addEventListener("click", function () {
        stop(preview.srcObject);
    }, false);


    return true;
}