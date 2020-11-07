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
    const constraints = {
        video: { width: { exact: 512 }, height: { exact: 384 } }
    };

    if (videoRef != null) {
        navigator.mediaDevices.getUserMedia(constraints).
            then((stream) => { videoRef.srcObject = stream }).catch(handleError);
    }
    else {
        console.error('Null video reference.');
    }

    return true;
}

function takePhotograph(canvasRef, videoRef, imageRef) {
    canvasRef.width = videoRef.videoWidth;
    canvasRef.height = videoRef.videoHeight;
    canvasRef.getContext('2d').drawImage(videoRef, 0, 0);
    imageRef.src = canvasRef.toDataURL('image/webp');
    return imageRef.src;
}

function closeCamera(videoRef) {
    videoRef.srcObject = null;
    return true;
}

function handleSuccess(stream) {
    video_e.srcObject = stream;
    console.log(video_e.srcObject);
}

function handleError(error) {
    console.error('Error: ', error);
}