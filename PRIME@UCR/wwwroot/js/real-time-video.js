let recordingTimeMS = 30000;
let startButton;
let stopButton;
let videoFeed;
let videoPreview;
let closeButton;
let videoName = "video.webm";

function wait(delayInMS) {
    return new Promise(resolve => setTimeout(resolve, delayInMS));
}

function startRecording(stream, lengthInMS) {
    startButton.className = "hidden";
    stopButton.className = "btn btn-danger rt-button";
    videoFeed.className = "rt-box";
    videoPreview.className = "hidden";
    closeButton.disabled = true;

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

function stop(video, reset) {
    startButton.className = "btn btn-primary rt-button";
    stopButton.className = "hidden";
    videoFeed.className = "hidden";
    videoPreview.className = "rt-box";
    closeButton.disabled = false;

    stream = video.srcObject;
    if (stream != null) {
        stream.getTracks().forEach(track => track.stop());
    }
    if (reset) {
        videoFeed.className = "rt-box";
        videoPreview.className = "hidden";
    }
    return true;
}

function videoInit(_startButton, preview, recording, downloadButton, _stopButton, _closeButton) {
    startButton = _startButton;
    stopButton = _stopButton;
    videoFeed = preview;
    videoPreview = recording;
    closeButton = _closeButton;

    stopButton.className = "hidden";
    videoPreview.className = "hidden";

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
                downloadButton.download = videoName;
            })
            .catch((e) => console.log(e));
    }, false);

    stopButton.addEventListener("click", function () {
        stop(preview, false);
    }, false);


    return true;
}

function setDownloadName(fileName) {
    videoName = fileName + ".webm";
    console.log(videoName);
    return true;
}