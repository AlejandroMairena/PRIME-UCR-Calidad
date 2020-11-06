let video_e = null;
let v_canvas = null;

function hasGetUserMedia() {
    let has_um = !!(navigator.mediaDevices &&
        navigator.mediaDevices.getUserMedia);

    if (has_um) {
        // Good to go!
        console.log('Good to go!');
    } else {
        //alert('getUserMedia() is not supported by your browser');
        console.log('Not good to go!');
    }
    return has_um;
}

function openCamera(videoRef) {
    console.log("OpenCamera");
    video_e = videoRef;
    console.log(video_e);

    const constraints = {
        video: { width: { exact: 512 }, height: { exact: 384 } }
    };

    if (video_e != null) {
        navigator.mediaDevices.getUserMedia(constraints).
            then(handleSuccess).catch(handleError);
    }
    else {
        console.error('Null video reference.');
    }

    return true;
}

function takePhotograph(canvasRef) {
    console.log("Take Photograph");
    v_canvas = canvasRef;
    console.log(v_canvas);

    v_canvas.width = video_e.videoWidth;
    v_canvas.height = video_e.videoHeight;
    v_canvas.getContext('2d').drawImage(video_e, 0, 0);
    return true;
}

function handleSuccess(stream) {
    console.log("Success");
    console.log(video_e);
    console.log(stream);
    video_e.srcObject = stream;
    console.log(video_e.srcObject);
}

function handleError(error) {
    console.error('Error: ', error);
}